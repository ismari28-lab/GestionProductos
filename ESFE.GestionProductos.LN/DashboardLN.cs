using System;
using System.Collections.Generic;
using System.Linq;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.GestionProductos.LN
{
    public class DashboardLN
    {
        private readonly DashboardDAL dashboardDAL = new DashboardDAL();

        // Valor total del inventario
        public decimal ObtenerValorTotalInventario()
        {
            return dashboardDAL.ObtenerValorTotalInventario();
        }

        // Cantidad de productos bajo stock minimo
        public int ContarProductosBajoStock()
        {
            return dashboardDAL.ContarProductosBajoStock();
        }

        // Producto mas vendido del mes (null si no hay ventas)
        public ProductoMasVendidoDTO? ObtenerProductoMasVendidoDelMes()
        {
            var resultado = dashboardDAL.ObtenerProductoMasVendidoDelMes();

            if (resultado == null)
                return null;

            return new ProductoMasVendidoDTO
            {
                Nombre = resultado.Value.Nombre,
                TotalUnidades = resultado.Value.TotalUnidades
            };
        }

        // Top N alertas de bajo stock, ordenadas por criticidad
        public List<AlertaBajoStockDTO> ObtenerAlertasBajoStock(int top)
        {
            if (top <= 0)
                throw new ArgumentException("El número de alertas a mostrar debe ser mayor que cero.", nameof(top));

            return dashboardDAL.ObtenerAlertasBajoStock(top)
                .Select(a => new AlertaBajoStockDTO
                {
                    Nombre = a.Nombre,
                    Existencias = a.Existencias,
                    Minimo = a.Minimo
                })
                .ToList();
        }
    }
}
