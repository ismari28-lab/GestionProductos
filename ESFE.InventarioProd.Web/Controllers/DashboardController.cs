using ESFE.GestionProductos.LN;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardLN dashboardLN = new DashboardLN();

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                ValorTotalInventario = dashboardLN.ObtenerValorTotalInventario(),
                ProductosBajoStockCount = dashboardLN.ContarProductosBajoStock(),
                ProductoMasVendido = dashboardLN.ObtenerProductoMasVendidoDelMes(),
                AlertasBajoStock = dashboardLN.ObtenerAlertasBajoStock(10)
            };

            return View(model);
        }
    }
}
