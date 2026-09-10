using System.Collections.Generic;
using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.InventarioProd.Web.Models
{
    public class DashboardViewModel
    {
        public decimal ValorTotalInventario { get; set; }

        public int ProductosBajoStockCount { get; set; }

        public ProductoMasVendidoDTO? ProductoMasVendido { get; set; }

        public List<AlertaBajoStockDTO> AlertasBajoStock { get; set; } = new List<AlertaBajoStockDTO>();
    }
}
