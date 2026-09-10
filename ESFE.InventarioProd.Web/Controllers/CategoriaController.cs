using ESFE.GestionProductos.LN;
using ESFE.GestionProductos.LN.Enums;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly CategoriaLN categoriaLN = new CategoriaLN();

        // GET /Categoria
        public IActionResult Index()
        {
            var categorias = categoriaLN.ListarConConteo();
            return View(categorias);
        }

        // GET /Categoria/Listar (refresco AJAX del grid)
        [HttpGet]
        public IActionResult Listar()
        {
            return Json(categoriaLN.ListarConConteo());
        }

        // POST /Categoria/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([FromBody] CategoriaFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { ok = false, errores = ObtenerErroresModelo() });

            try
            {
                short id = categoriaLN.Crear(model.Nombre, model.Descripcion);
                return Json(new { ok = true, id, mensaje = "Categoría creada correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ok = false, errores = new[] { ex.Message } });
            }
        }

        // POST /Categoria/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([FromBody] CategoriaFormViewModel model)
        {
            if (model.IdCategoriaPK == null)
                return BadRequest(new { ok = false, errores = new[] { "Falta el identificador de la categoría." } });

            if (!ModelState.IsValid)
                return BadRequest(new { ok = false, errores = ObtenerErroresModelo() });

            try
            {
                bool actualizado = categoriaLN.Actualizar(model.IdCategoriaPK.Value, model.Nombre, model.Descripcion);
                if (!actualizado)
                    return NotFound(new { ok = false, mensaje = "Categoría no encontrada." });

                return Json(new { ok = true, mensaje = "Categoría actualizada correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ok = false, errores = new[] { ex.Message } });
            }
        }

        // POST /Categoria/Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar([FromBody] EliminarRequest req)
        {
            var resultado = categoriaLN.Eliminar(req.Id);

            return resultado switch
            {
                ResultadoEliminacionCategoria.Ok =>
                    Json(new { ok = true, mensaje = "Categoría eliminada correctamente." }),
                ResultadoEliminacionCategoria.TieneProductos =>
                    StatusCode(409, new { ok = false, mensaje = "No se puede eliminar: tiene productos activos." }),
                ResultadoEliminacionCategoria.NoEncontrada =>
                    NotFound(new { ok = false, mensaje = "Categoría no encontrada." }),
                _ => StatusCode(500, new { ok = false, mensaje = "Error inesperado." })
            };
        }

        private IEnumerable<string> ObtenerErroresModelo()
        {
            return ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }

    public class EliminarRequest
    {
        public short Id { get; set; }
    }
}
