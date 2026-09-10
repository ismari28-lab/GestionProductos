using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN
{
    public class MovimientoLN
    {
        private const string TipoEntrada = "Entrada";
        private const string TipoSalida = "Salida";

        private readonly MovimientoDAL movimientoDAL = new MovimientoDAL();

        public List<ProductoBusquedaDTO> BuscarProductos(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new List<ProductoBusquedaDTO>();

            return movimientoDAL.BuscarProductos(termino.Trim())
                .Select(p => new ProductoBusquedaDTO
                {
                    IdProductoPK = p.IdProductoPK,
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    StockActual = p.StockActual
                })
                .ToList();
        }

        public ResultadoGuardarMovimientoDTO Guardar(MovimientoInputDTO input, short idUsuarioFK)
        {
            var errorValidacion = ValidarEntrada(input);
            if (errorValidacion != null)
                return errorValidacion;

            // Consolidar detalles duplicados: si el cliente envía el mismo producto dos veces, sumar cantidades
            var detalles = input.Detalles
                .GroupBy(d => d.IdProductoPK)
                .Select(g => new DetalleMovimientoInputDTO { IdProductoPK = g.Key, Cantidad = g.Sum(x => x.Cantidad) })
                .ToList();

            if (detalles.Any(d => d.Cantidad > short.MaxValue))
                return Invalido("La cantidad total de un producto excede el máximo permitido.");

            bool esSalida = string.Equals(input.TipoMovimiento, TipoSalida, StringComparison.OrdinalIgnoreCase);

            try
            {
                using var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled);

                // Revalidar stock server-side dentro de la transacción (previene condiciones de carrera)
                var problemas = new List<string>();
                var resultadoValidacion = ResultadoGuardarMovimiento.Ok;
                var stocksActuales = new Dictionary<int, (short? IdStockProductoPK, int Stock)>();

                foreach (var detalle in detalles)
                {
                    // TODO: alinear a int cuando se resuelva deuda técnica
                    var stock = movimientoDAL.ObtenerStock((short)detalle.IdProductoPK);

                    if (esSalida)
                    {
                        if (stock == null)
                        {
                            problemas.Add($"Producto #{detalle.IdProductoPK}: sin stock registrado");
                            resultadoValidacion = ResultadoGuardarMovimiento.ProductoSinStockEnSalida;
                            continue;
                        }

                        if (stock.Value.Stock < detalle.Cantidad)
                        {
                            problemas.Add($"Producto #{detalle.IdProductoPK}: disponible {stock.Value.Stock}, solicitado {detalle.Cantidad}");
                            if (resultadoValidacion == ResultadoGuardarMovimiento.Ok)
                                resultadoValidacion = ResultadoGuardarMovimiento.StockInsuficiente;
                            continue;
                        }
                    }

                    stocksActuales[detalle.IdProductoPK] = (stock?.IdStockProductoPK, stock?.Stock ?? 0);
                }

                if (problemas.Count > 0)
                {
                    return new ResultadoGuardarMovimientoDTO
                    {
                        Resultado = resultadoValidacion,
                        Mensaje = resultadoValidacion == ResultadoGuardarMovimiento.ProductoSinStockEnSalida
                            ? "Uno o más productos no tienen stock registrado."
                            : "Stock insuficiente para uno o más productos.",
                        ProductosProblema = problemas
                    };
                }

                short idMovimiento = movimientoDAL.InsertarCabecera(input.TipoMovimiento, input.Referencia, input.Fecha, idUsuarioFK);

                foreach (var detalle in detalles)
                {
                    // TODO: alinear a int cuando se resuelva deuda técnica
                    short idProductoShort = (short)detalle.IdProductoPK;
                    short cantidad = (short)detalle.Cantidad;

                    movimientoDAL.InsertarDetalle(idMovimiento, idProductoShort, cantidad);

                    if (esSalida)
                    {
                        movimientoDAL.ActualizarStock(idProductoShort, (short)-cantidad);
                    }
                    else
                    {
                        var stockInfo = stocksActuales[detalle.IdProductoPK];
                        if (stockInfo.IdStockProductoPK == null)
                            movimientoDAL.CrearStock(idProductoShort, cantidad);
                        else
                            movimientoDAL.ActualizarStock(idProductoShort, cantidad);
                    }
                }

                scope.Complete();

                return new ResultadoGuardarMovimientoDTO
                {
                    Resultado = ResultadoGuardarMovimiento.Ok,
                    Mensaje = "Movimiento registrado correctamente",
                    IdMovimientoGenerado = idMovimiento
                };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarMovimientoDTO
                {
                    Resultado = ResultadoGuardarMovimiento.ErrorInterno,
                    Mensaje = ex.Message
                };
            }
        }

        private static ResultadoGuardarMovimientoDTO? ValidarEntrada(MovimientoInputDTO input)
        {
            if (input == null)
                return Invalido("Datos del movimiento faltantes.");

            if (input.TipoMovimiento != TipoEntrada && input.TipoMovimiento != TipoSalida)
                return Invalido("Tipo de movimiento inválido.");

            if (input.Fecha.Date > DateTime.Today)
                return Invalido("La fecha no puede ser futura.");

            if (input.Detalles == null || input.Detalles.Count == 0)
                return Invalido("Debe agregar al menos un producto al detalle.");

            foreach (var d in input.Detalles)
            {
                if (d.IdProductoPK <= 0)
                    return Invalido("Producto inválido en el detalle.");

                if (d.Cantidad <= 0 || d.Cantidad > short.MaxValue)
                    return Invalido("La cantidad de cada producto debe ser mayor que cero.");
            }

            return null;
        }

        private static ResultadoGuardarMovimientoDTO Invalido(string mensaje)
        {
            return new ResultadoGuardarMovimientoDTO
            {
                Resultado = ResultadoGuardarMovimiento.DatosInvalidos,
                Mensaje = mensaje
            };
        }

        // --- Historial de movimientos (Sprint B) ---

        private static readonly int[] TamaniosPaginaPermitidos = { 25, 50, 75 };

        public ResultadoPaginadoDTO<HistorialMovimientoDTO> ListarHistorial(FiltrosHistorialDTO filtros)
        {
            DateTime desde = filtros.Desde.Date;
            DateTime hasta = filtros.Hasta.Date;
            if (desde > hasta)
                (desde, hasta) = (hasta, desde);

            string? tipo = (filtros.Tipo == TipoEntrada || filtros.Tipo == TipoSalida) ? filtros.Tipo : null;

            string? termino = filtros.Termino?.Trim();
            if (string.IsNullOrEmpty(termino) || termino.Length < 2)
                termino = null;

            int pagina = filtros.Pagina < 1 ? 1 : filtros.Pagina;
            int tamanioPagina = TamaniosPaginaPermitidos.Contains(filtros.TamanioPagina) ? filtros.TamanioPagina : 25;

            var (total, items) = movimientoDAL.ListarHistorial(desde, hasta, tipo, termino, pagina, tamanioPagina);

            return new ResultadoPaginadoDTO<HistorialMovimientoDTO>
            {
                Items = items
                    .Select(i => new HistorialMovimientoDTO
                    {
                        IdMovimientoPK = i.IdMovimientoPK,
                        Referencia = i.Referencia,
                        Fecha = i.Fecha,
                        TipoMovimiento = i.TipoMovimiento,
                        NombreUsuario = i.NombreUsuario,
                        TotalItems = i.TotalItems
                    })
                    .ToList(),
                TotalRegistros = total,
                Pagina = pagina,
                TamanioPagina = tamanioPagina
            };
        }

        public DetalleMovimientoModalDTO? ObtenerDetalle(short idMovimientoPK)
        {
            if (idMovimientoPK <= 0)
                return null;

            var resultado = movimientoDAL.ObtenerDetalle(idMovimientoPK);
            if (resultado == null)
                return null;

            return new DetalleMovimientoModalDTO
            {
                IdMovimientoPK = resultado.Value.IdMovimientoPK,
                Referencia = resultado.Value.Referencia,
                TipoMovimiento = resultado.Value.TipoMovimiento,
                Fecha = resultado.Value.Fecha,
                NombreUsuario = resultado.Value.NombreUsuario,
                Lineas = resultado.Value.Lineas
                    .Select(l => new LineaDetalleDTO
                    {
                        Codigo = l.Codigo,
                        ProductoNombre = l.ProductoNombre,
                        Cantidad = l.Cantidad
                    })
                    .ToList()
            };
        }
    }
}
