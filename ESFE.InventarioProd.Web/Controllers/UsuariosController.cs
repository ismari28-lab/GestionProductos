using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}