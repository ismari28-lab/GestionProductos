using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN
{
    public class ProductoLN
    {
        private readonly ProductoDAL productoDAL = new ProductoDAL();

        // Listar Productos
        public DataTable Listar()
        {
            return productoDAL.Listar();
        }

        // Buscar Productos
        public List<Producto> Buscar(
        string nombre = null,
        short? idProducto = null,
        string codigo = null)
            {
                return productoDAL.Buscar(nombre, idProducto, codigo);
            }

        // Guardar Producto
        public int Guardar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            if (producto.IdProductoPK > 0)
            {
                return productoDAL.Actualizar(producto);
            }

            return productoDAL.Insertar(producto);
        }

        // Actualizar Producto
        public int Actualizar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            return productoDAL.Actualizar(producto);
        }

        // Insertar Producto
        public int Insertar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            return productoDAL.Insertar(producto);
        }

        // Eliminar lógico
        public int EliminarLogico(short idProducto)
        {
            return productoDAL.EliminarLogico(idProducto);
        }

        // --- Módulo Productos (STOCKEO): listado con filtros, sort y paginación (Sprint A) ---

        private static readonly HashSet<string> ColumnasValidas = new(StringComparer.OrdinalIgnoreCase)
        {
            "codigo", "nombre", "categoria", "existencias", "preciocompra", "precioventa", "estado"
        };
        private static readonly int[] TamaniosValidos = { 25, 50, 75 };

        public List<CategoriaFiltroDTO> ListarCategoriasActivas()
        {
            return productoDAL.ListarCategoriasActivas()
                .Select(c => new CategoriaFiltroDTO { IdCategoriaPK = c.IdCategoriaPK, Nombre = c.Nombre })
                .ToList();
        }

        public ResultadoPaginadoDTO<ProductoListadoDTO> ListarProductos(FiltrosProductoDTO filtros)
        {
            string? termino = filtros.Termino?.Trim();
            if (string.IsNullOrEmpty(termino) || termino.Length < 2)
                termino = null;

            string ordenarPor = ColumnasValidas.Contains(filtros.OrdenarPor) ? filtros.OrdenarPor.ToLowerInvariant() : "nombre";
            string direccion = string.Equals(filtros.Direccion, "DESC", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";

            int pagina = filtros.Pagina < 1 ? 1 : filtros.Pagina;
            int tamanioPagina = TamaniosValidos.Contains(filtros.TamanioPagina) ? filtros.TamanioPagina : 25;

            var (total, items) = productoDAL.ListarProductos(
                termino, filtros.IdCategoria, filtros.IncluirInactivos, ordenarPor, direccion, pagina, tamanioPagina);

            return new ResultadoPaginadoDTO<ProductoListadoDTO>
            {
                Items = items
                    .Select(p => new ProductoListadoDTO
                    {
                        IdProductoPK = p.IdProductoPK,
                        Codigo = p.Codigo,
                        Nombre = p.Nombre,
                        CategoriaNombre = p.CategoriaNombre,
                        Existencias = p.Existencias,
                        StockMinimo = p.StockMinimo,
                        PrecioCompra = p.PrecioCompra,
                        PrecioVenta = p.PrecioVenta,
                        Estado = p.Estado
                    })
                    .ToList(),
                TotalRegistros = total,
                Pagina = pagina,
                TamanioPagina = tamanioPagina
            };
        }

        // --- Módulo Productos (STOCKEO): modal Crear/Editar/Eliminar (Sprint B) ---

        public List<ProveedorFiltroDTO> ListarProveedoresActivos()
        {
            return productoDAL.ListarProveedoresActivos()
                .Select(p => new ProveedorFiltroDTO { IdProveedorPK = p.IdProveedorPK, Nombre = p.Nombre })
                .ToList();
        }

        public DatosNuevoProductoDTO ObtenerDatosNuevoProducto()
        {
            return new DatosNuevoProductoDTO
            {
                CodigoSugerido = productoDAL.ObtenerCodigoSugerido(),
                Categorias = ListarCategoriasActivas(),
                Proveedores = ListarProveedoresActivos()
            };
        }

        public ProductoEdicionDTO? ObtenerParaEdicion(int id)
        {
            if (id <= 0)
                return null;

            var p = productoDAL.ObtenerParaEdicion(id);
            if (p == null)
                return null;

            return new ProductoEdicionDTO
            {
                IdProductoPK = p.Value.IdProductoPK,
                Codigo = p.Value.Codigo,
                Nombre = p.Value.Nombre,
                IdCategoriaFK = p.Value.IdCategoriaFK,
                IdProveedorFK = p.Value.IdProveedorFK,
                PrecioCompra = p.Value.PrecioCompra,
                PrecioVenta = p.Value.PrecioVenta,
                AplicaIVA = p.Value.AplicaIVA,
                PorcentajeIVA = p.Value.PorcentajeIVA,
                Estado = p.Value.Estado,
                StockActual = p.Value.StockActual,
                StockMinimo = p.Value.StockMinimo
            };
        }

        public ResultadoGuardarProductoDTO Crear(ProductoFormDTO form)
        {
            var error = ValidarFormulario(form);
            if (error != null)
                return error;

            if (!productoDAL.ValidarCodigoUnico(form.Codigo, 0))
                return new ResultadoGuardarProductoDTO
                {
                    Resultado = ResultadoGuardarProducto.CodigoDuplicado,
                    Mensaje = "El código ya existe. Vuelve a abrir el modal para obtener uno nuevo."
                };

            try
            {
                using var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled);

                decimal porcentajeIVA = form.AplicaIVA ? form.PorcentajeIVA : 0m;

                int nuevoId = productoDAL.CrearProducto(
                    form.Codigo.Trim(), form.Nombre.Trim(), form.IdCategoriaFK, form.IdProveedorFK,
                    form.PrecioCompra, form.PrecioVenta, form.AplicaIVA, porcentajeIVA);

                // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
                productoDAL.CrearStockInicial((short)nuevoId, form.StockMinimo);

                scope.Complete();

                return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.Ok, IdGenerado = nuevoId };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.ErrorInterno, Mensaje = ex.Message };
            }
        }

        public ResultadoGuardarProductoDTO Actualizar(ProductoFormDTO form)
        {
            if (!form.IdProductoPK.HasValue || form.IdProductoPK.Value <= 0)
                return new ResultadoGuardarProductoDTO
                {
                    Resultado = ResultadoGuardarProducto.DatosInvalidos,
                    Mensaje = "ID de producto inválido"
                };

            var error = ValidarFormulario(form);
            if (error != null)
                return error;

            if (!productoDAL.ValidarCodigoUnico(form.Codigo, form.IdProductoPK.Value))
                return new ResultadoGuardarProductoDTO
                {
                    Resultado = ResultadoGuardarProducto.CodigoDuplicado,
                    Mensaje = "El código ya está en uso por otro producto"
                };

            try
            {
                using var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled);

                decimal porcentajeIVA = form.AplicaIVA ? form.PorcentajeIVA : 0m;

                int filas = productoDAL.ActualizarProducto(
                    form.IdProductoPK.Value, form.Nombre.Trim(), form.IdCategoriaFK, form.IdProveedorFK,
                    form.PrecioCompra, form.PrecioVenta, form.AplicaIVA, porcentajeIVA, form.Estado);

                if (filas == 0)
                    return new ResultadoGuardarProductoDTO
                    {
                        Resultado = ResultadoGuardarProducto.NoEncontrado,
                        Mensaje = "Producto no encontrado"
                    };

                // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
                productoDAL.ActualizarStockMinimo((short)form.IdProductoPK.Value, form.StockMinimo);

                scope.Complete();

                return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.Ok, IdGenerado = form.IdProductoPK };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.ErrorInterno, Mensaje = ex.Message };
            }
        }

        public ResultadoEliminarProductoDTO Eliminar(int id)
        {
            if (id <= 0)
                return new ResultadoEliminarProductoDTO { Resultado = ResultadoEliminarProducto.NoEncontrado, Mensaje = "Producto no encontrado" };

            try
            {
                int resultado = productoDAL.EliminarProducto(id);
                return resultado switch
                {
                    -1 => new ResultadoEliminarProductoDTO
                    {
                        Resultado = ResultadoEliminarProducto.TieneMovimientos,
                        Mensaje = "No se puede eliminar: el producto tiene movimientos registrados. Puedes desactivarlo cambiando su estado a Inactivo."
                    },
                    0 => new ResultadoEliminarProductoDTO
                    {
                        Resultado = ResultadoEliminarProducto.NoEncontrado,
                        Mensaje = "Producto no encontrado"
                    },
                    _ => new ResultadoEliminarProductoDTO { Resultado = ResultadoEliminarProducto.Ok }
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEliminarProductoDTO { Resultado = ResultadoEliminarProducto.ErrorInterno, Mensaje = ex.Message };
            }
        }

        private static ResultadoGuardarProductoDTO? ValidarFormulario(ProductoFormDTO form)
        {
            if (form == null)
                return Invalido("Datos del producto faltantes.");

            if (string.IsNullOrWhiteSpace(form.Codigo))
                return Invalido("El código es obligatorio.");

            if (string.IsNullOrWhiteSpace(form.Nombre))
                return Invalido("El nombre es obligatorio.");

            if (form.Nombre.Trim().Length > 150)
                return Invalido("El nombre no puede superar los 150 caracteres.");

            if (form.IdCategoriaFK <= 0)
                return Invalido("La categoría es obligatoria.");

            if (form.PrecioCompra < 0)
                return Invalido("El precio de compra no puede ser negativo.");

            if (form.PrecioVenta < 0)
                return Invalido("El precio de venta no puede ser negativo.");

            if (form.AplicaIVA && (form.PorcentajeIVA < 0 || form.PorcentajeIVA > 100))
                return Invalido("El porcentaje de IVA debe estar entre 0 y 100.");

            if (form.StockMinimo < 0)
                return Invalido("El stock mínimo no puede ser negativo.");

            return null;
        }

        private static ResultadoGuardarProductoDTO Invalido(string mensaje)
        {
            return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.DatosInvalidos, Mensaje = mensaje };
        }

        // --- Módulo Productos (STOCKEO): exportar a Excel (Sprint C) ---

        public List<ProductoExportDTO> ListarParaExportar(FiltrosProductoDTO filtros)
        {
            string? termino = filtros.Termino?.Trim();
            if (string.IsNullOrEmpty(termino) || termino.Length < 2)
                termino = null;

            string ordenarPor = ColumnasValidas.Contains(filtros.OrdenarPor) ? filtros.OrdenarPor.ToLowerInvariant() : "nombre";
            string direccion = string.Equals(filtros.Direccion, "DESC", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";

            return productoDAL.ListarParaExportar(termino, filtros.IdCategoria, filtros.IncluirInactivos, ordenarPor, direccion)
                .Select(p => new ProductoExportDTO
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    CategoriaNombre = p.CategoriaNombre,
                    ProveedorNombre = p.ProveedorNombre,
                    Existencias = p.Existencias,
                    StockMinimo = p.StockMinimo,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    AplicaIVA = p.AplicaIVA,
                    PorcentajeIVA = p.PorcentajeIVA,
                    Estado = p.Estado
                })
                .ToList();
        }
    }
}
