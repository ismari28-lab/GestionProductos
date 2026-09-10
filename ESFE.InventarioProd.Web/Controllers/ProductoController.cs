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
    }

    public class EliminarProductoRequest
    {
        public int Id { get; set; }
    }
}
