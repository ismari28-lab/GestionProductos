using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class DetalleFactura
    {
        public short IdDetalleFacturaPK { get; set; }

        public short? IdFacturaPK { get; set; }

        public short? IdProductoPK { get; set; }

        public short? Cantidad { get; set; }

        public double? PrecioUnitario { get; set; }
        
        public bool? Estado { get; set; }
    }
}
