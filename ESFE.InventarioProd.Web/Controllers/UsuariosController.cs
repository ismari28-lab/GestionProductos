using System.Security.Claims;
using ESFE.GestionProductos.LN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly UserLN userLN = new UserLN();
        private readonly RolLN rolLN = new RolLN();

        // GET /Usuarios
        public IActionResult Index()
        {
            var vm = new UsuariosIndexViewModel
            {
                Roles = ListarRolesDTO()
            };
            return View(vm);
        }

        // GET /Usuarios/Listar (AJAX)
        [HttpGet]
        public IActionResult Listar()
        {
            return Json(userLN.ListarParaWeb());
        }

        // GET /Usuarios/ObtenerParaEdicion/{id} (AJAX)
        [HttpGet]
        public IActionResult ObtenerParaEdicion(int id)
        {
            var usuario = userLN.ObtenerParaEdicion(id);
            if (usuario == null)
                return NotFound(new { ok = false, mensaje = "Usuario no encontrado" });

            return Json(new
            {
                usuario,
                roles = ListarRolesDTO()
            });
        }

        // POST /Usuarios/Crear (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([FromBody] UsuarioFormDTO form)
        {
            var r = userLN.Crear(form);
            return r.Resultado switch
            {
                ResultadoGuardarUsuario.Ok => Ok(new { ok = true, mensaje = "Usuario creado correctamente" }),
                ResultadoGuardarUsuario.NombreDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarUsuario.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Usuarios/Editar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([FromBody] UsuarioFormDTO form)
        {
            var r = userLN.Actualizar(form);
            return r.Resultado switch
            {
                ResultadoGuardarUsuario.Ok => Ok(new { ok = true, mensaje = "Usuario actualizado correctamente" }),
                ResultadoGuardarUsuario.NombreDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarUsuario.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
                ResultadoGuardarUsuario.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        // POST /Usuarios/Eliminar (AJAX, JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar([FromBody] EliminarUsuarioRequest req)
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var idUsuarioActual);

            var r = userLN.EliminarConValidacion(req.Id, idUsuarioActual);
            return r.Resultado switch
            {
                ResultadoEliminarUsuario.Ok => Ok(new { ok = true, mensaje = "Usuario eliminado correctamente" }),
                ResultadoEliminarUsuario.NoPermitido => Conflict(new { ok = false, mensaje = r.Mensaje }),
                ResultadoEliminarUsuario.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
                _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
            };
        }

        private List<RolFiltroDTO> ListarRolesDTO()
        {
            return rolLN.Listar()
                .Select(r => new RolFiltroDTO { IdRolPK = r.IdRolPK, NombreRol = r.NombreRol ?? string.Empty })
                .ToList();
        }
    }

    public class EliminarUsuarioRequest
    {
        public int Id { get; set; }
    }
}
