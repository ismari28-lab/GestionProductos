using ClosedXML.Excel;
using ESFE.GestionProductos.LN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,Inventariado,Vendedor,Cajero")]
    public class ProductoController : Controller
    {
        private readonly ProductoLN productoLN = new ProductoLN();

        // GET /Producto
        public IActionResult Index()
        {
            var vm = new ProductoIndexViewModel
            {
                Categorias = productoLN.ListarCategoriasActivas()
            };
            return View(vm);
        }

        // GET /Producto/Listar?termino=&idCategoria=&incluirInactivos=&ordenarPor=&direccion=&pagina=&tamanioPagina= (AJAX)
        [HttpGet]
        public IActionResult Listar(
            string? termino, short? idCategoria, bool incluirInactivos = false,
            string ordenarPor = "nombre", string direccion = "ASC",
            int pagina = 1, int tamanioPagina = 25)
        {
            var filtros = new FiltrosProductoDTO
            {
                Termino = termino,
                IdCategoria = idCategoria,
                IncluirInactivos = incluirInactivos,
                OrdenarPor = ordenarPor,
                Direccion = direccion,
                Pagina = pagina,
                TamanioPagina = tamanioPagina
            };

            return Json(productoLN.ListarProductos(filtros));
        }

        // --- Módulo Productos (STOCKEO): modal Crear/Editar/Eliminar (Sprint B) ---

        // GET /Producto/DatosNuevo (AJAX) — hidrata el modal Crear
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult DatosNuevo()
        {
            return Json(productoLN.ObtenerDatosNuevoProducto());
        }

        // GET /Producto/ObtenerParaEdicion/{id} (AJAX) — hidrata el modal Editar
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult ObtenerParaEdicion(int id)
        {
            var datos = productoLN.ObtenerParaEdicion(id);
            if (datos == null)
                return NotFound(new { ok = false, mensaje = "Producto no encontrado" });

            var respuesta = new
            {
                producto = datos,
                categorias = productoLN.ListarCategoriasActivas(),
                proveedores = productoLN.ListarProveedoresActivos()
            };
            return Json(respuesta);
        }

        // POST /Producto/Crear (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult Crear([FromBody] ProductoFormDTO form)
        {
            var r = productoLN.Crear(form);
            return r.Resultado switch
            {
                ResultadoGuardarProducto.Ok => Ok(new { ok = true, id = r.IdGenerado, mensaje = "Producto creado correctamente" }),
                ResultadoGuardarProducto.CodigoDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarProducto.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Producto/Editar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult Editar([FromBody] ProductoFormDTO form)
        {
            var r = productoLN.Actualizar(form);
            return r.Resultado switch
            {
                ResultadoGuardarProducto.Ok => Ok(new { ok = true, mensaje = "Producto actualizado correctamente" }),
                ResultadoGuardarProducto.CodigoDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarProducto.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarProducto.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Producto/Eliminar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult Eliminar([FromBody] EliminarProductoRequest req)
        {
            var r = productoLN.Eliminar(req.Id);
            return r.Resultado switch
            {
                ResultadoEliminarProducto.Ok => Ok(new { ok = true, mensaje = "Producto eliminado correctamente" }),
                ResultadoEliminarProducto.TieneMovimientos => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoEliminarProducto.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // --- Módulo Productos (STOCKEO): exportar a Excel (Sprint C) ---

        // GET /Producto/ContarParaExportar?termino=...&idCategoria=...&incluirInactivos=... (AJAX)
        // Reutiliza ListarProductos (que ya calcula TotalRegistros vía COUNT(*) interno en el SP)
        // en vez de un SP dedicado: evita duplicar la misma lógica de filtros solo para contar.
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult ContarParaExportar(string? termino, short? idCategoria, bool incluirInactivos = false)
        {
            var filtros = new FiltrosProductoDTO
            {
                Termino = termino,
                IdCategoria = idCategoria,
                IncluirInactivos = incluirInactivos
            };

            var resultado = productoLN.ListarProductos(filtros);
            return Json(new { total = resultado.TotalRegistros });
        }

        // GET /Producto/Exportar?termino=...&idCategoria=...&incluirInactivos=...&ordenarPor=...&direccion=...
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor,Inventariado")]
        public IActionResult Exportar(
            string? termino, short? idCategoria, bool incluirInactivos = false,
            string ordenarPor = "nombre", string direccion = "ASC")
        {
            var filtros = new FiltrosProductoDTO
            {
                Termino = termino,
                IdCategoria = idCategoria,
                IncluirInactivos = incluirInactivos,
                OrdenarPor = ordenarPor,
                Direccion = direccion
            };

            var datos = productoLN.ListarParaExportar(filtros);

            var descripcionFiltros = new List<string>();
            if (!string.IsNullOrWhiteSpace(termino))
                descripcionFiltros.Add($"Búsqueda: «{termino}»");
            if (idCategoria.HasValue)
            {
                var categoria = productoLN.ListarCategoriasActivas().FirstOrDefault(c => c.IdCategoriaPK == idCategoria.Value);
                if (categoria != null)
                    descripcionFiltros.Add($"Categoría: {categoria.Nombre}");
            }
            if (incluirInactivos)
                descripcionFiltros.Add("Incluye inactivos");
            string filtrosTexto = descripcionFiltros.Count == 0 ? "sin filtros" : string.Join(" · ", descripcionFiltros);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Productos");

            // Fila 1: título
            ws.Cell(1, 1).Value = "STOCKEO — Catálogo de productos";
            ws.Range(1, 1, 1, 11).Merge();
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Fila 2: subtítulo
            ws.Cell(2, 1).Value = $"Exportado: {DateTime.Now:dd/MM/yyyy HH:mm} — Filtros: {filtrosTexto}";
            ws.Range(2, 1, 2, 11).Merge();
            ws.Cell(2, 1).Style.Font.Italic = true;
            ws.Cell(2, 1).Style.Font.FontColor = XLColor.Gray;
            ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Fila 4: header
            string[] headers = { "Código", "Nombre", "Categoría", "Proveedor",
                                 "Existencias", "Stock Mínimo", "Precio Compra", "Precio Venta",
                                 "Aplica IVA", "% IVA", "Estado" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(4, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#E5E7EB");
                cell.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            }

            // Datos desde fila 5
            int row = 5;
            foreach (var p in datos)
            {
                ws.Cell(row, 1).Value = p.Codigo;
                ws.Cell(row, 2).Value = p.Nombre;
                ws.Cell(row, 3).Value = p.CategoriaNombre;
                ws.Cell(row, 4).Value = p.ProveedorNombre;
                ws.Cell(row, 5).Value = p.Existencias;
                ws.Cell(row, 6).Value = p.StockMinimo;
                ws.Cell(row, 7).Value = p.PrecioCompra;
                ws.Cell(row, 8).Value = p.PrecioVenta;
                ws.Cell(row, 9).Value = p.AplicaIVA ? "Sí" : "No";
                ws.Cell(row, 10).Value = p.PorcentajeIVA;
                ws.Cell(row, 11).Value = p.Estado ? "Activo" : "Inactivo";
                row++;
            }

            if (datos.Count > 0)
            {
                int lastRow = 4 + datos.Count;
                ws.Range(5, 5, lastRow, 5).Style.NumberFormat.Format = "#,##0";       // Existencias
                ws.Range(5, 6, lastRow, 6).Style.NumberFormat.Format = "#,##0";       // Stock Mínimo
                ws.Range(5, 7, lastRow, 7).Style.NumberFormat.Format = "$#,##0.00";   // P. Compra
                ws.Range(5, 8, lastRow, 8).Style.NumberFormat.Format = "$#,##0.00";   // P. Venta
                ws.Range(5, 10, lastRow, 10).Style.NumberFormat.Format = "0.00\"%\""; // % IVA

                ws.Range(4, 1, lastRow, 11).SetAutoFilter();
            }

            ws.SheetView.FreezeRows(4);
            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            byte[] bytes = stream.ToArray();

            string nombreArchivo = $"productos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        }
    }

    public class EliminarProductoRequest
    {
        public int Id { get; set; }
    }
}
