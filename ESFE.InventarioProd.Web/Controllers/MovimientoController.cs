using System.Security.Claims;
using ESFE.GestionProductos.LN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,Inventariado")]
    public class MovimientoController : Controller
    {
        private readonly MovimientoLN movimientoLN = new MovimientoLN();

        // GET /Movimiento
        public IActionResult Index()
        {
            var vm = new MovimientoIndexViewModel
            {
                NombreUsuario = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                FechaHoyIso = DateTime.Today.ToString("yyyy-MM-dd")
            };
            return View(vm);
        }

        // GET /Movimiento/Historial → vista
        public IActionResult Historial()
        {
            var vm = new HistorialIndexViewModel
            {
                DesdeIso = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd"),
                HastaIso = DateTime.Today.ToString("yyyy-MM-dd")
            };
            return View(vm);
        }

        // GET /Movimiento/ListarHistorial?desde=...&hasta=...&tipo=...&termino=...&pagina=1&tamanioPagina=25 (AJAX)
        [HttpGet]
        public IActionResult ListarHistorial(
            DateTime? desde, DateTime? hasta, string? tipo, string? termino,
            int pagina = 1, int tamanioPagina = 25)
        {
            var filtros = new FiltrosHistorialDTO
            {
                Desde = desde ?? DateTime.Today.AddDays(-30),
                Hasta = hasta ?? DateTime.Today,
                Tipo = tipo,
                Termino = termino,
                Pagina = pagina,
                TamanioPagina = tamanioPagina
            };

            return Json(movimientoLN.ListarHistorial(filtros));
        }

        // GET /Movimiento/Detalle/{id} (AJAX)
        [HttpGet]
        public IActionResult Detalle(short id)
        {
            var detalle = movimientoLN.ObtenerDetalle(id);
            if (detalle == null)
                return NotFound(new { ok = false, mensaje = "Movimiento no encontrado" });

            return Json(detalle);
        }

        // GET /Movimiento/BuscarProductos?termino=xxx (AJAX)
        [HttpGet]
        public IActionResult BuscarProductos(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new List<ProductoBusquedaDTO>());

            return Json(movimientoLN.BuscarProductos(termino.Trim()));
        }

        // POST /Movimiento/Guardar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guardar([FromBody] MovimientoInputDTO input)
        {
            var idUsuarioStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!short.TryParse(idUsuarioStr, out var idUsuario))
                return Unauthorized();

            var resultado = movimientoLN.Guardar(input, idUsuario);

            return resultado.Resultado switch
            {
                ResultadoGuardarMovimiento.Ok =>
                    Ok(new { ok = true, id = resultado.IdMovimientoGenerado, mensaje = resultado.Mensaje ?? "Movimiento registrado correctamente" }),
                ResultadoGuardarMovimiento.StockInsuficiente =>
                    Conflict(new { ok = false, mensaje = resultado.Mensaje ?? "Stock insuficiente", productos = resultado.ProductosProblema }),
                ResultadoGuardarMovimiento.ProductoSinStockEnSalida =>
                    Conflict(new { ok = false, mensaje = resultado.Mensaje ?? "Producto sin stock registrado", productos = resultado.ProductosProblema }),
                ResultadoGuardarMovimiento.DatosInvalidos =>
                    BadRequest(new { ok = false, mensaje = resultado.Mensaje ?? "Datos inválidos" }),
                _ => StatusCode(500, new { ok = false, mensaje = resultado.Mensaje ?? "Error interno" })
            };
        }
    }
}
