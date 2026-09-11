using ESFE.GestionProductos.LN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        private readonly ProveedorLN proveedorLN = new ProveedorLN();

        // GET /Proveedor
        public IActionResult Index()
        {
            return View();
        }

        // GET /Proveedor/Listar (AJAX)
        [HttpGet]
        public IActionResult Listar()
        {
            return Json(proveedorLN.ListarParaWeb());
        }

        // GET /Proveedor/ObtenerParaEdicion/{id} (AJAX)
        [HttpGet]
        public IActionResult ObtenerParaEdicion(int id)
        {
            var proveedor = proveedorLN.ObtenerParaEdicion(id);
            if (proveedor == null)
                return NotFound(new { ok = false, mensaje = "Proveedor no encontrado" });

            return Json(proveedor);
        }

        // POST /Proveedor/Crear (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([FromBody] ProveedorFormDTO form)
        {
            var r = proveedorLN.Crear(form);
            return r.Resultado switch
            {
                ResultadoGuardarProveedor.Ok => Ok(new { ok = true, id = r.IdGenerado, mensaje = "Proveedor creado correctamente" }),
                ResultadoGuardarProveedor.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Proveedor/Editar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([FromBody] ProveedorFormDTO form)
        {
            var r = proveedorLN.Actualizar(form);
            return r.Resultado switch
            {
                ResultadoGuardarProveedor.Ok => Ok(new { ok = true, mensaje = "Proveedor actualizado correctamente" }),
                ResultadoGuardarProveedor.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarProveedor.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Proveedor/Eliminar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar([FromBody] EliminarProveedorRequest req)
        {
            var r = proveedorLN.Eliminar(req.Id);
            return r.Resultado switch
            {
                ResultadoEliminarProveedor.Ok => Ok(new { ok = true, mensaje = "Proveedor eliminado correctamente" }),
                ResultadoEliminarProveedor.TieneProductos => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoEliminarProveedor.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }
    }

    public class EliminarProveedorRequest
    {
        public int Id { get; set; }
    }
}
