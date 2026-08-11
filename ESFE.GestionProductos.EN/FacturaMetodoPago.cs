using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class FacturaMetodoPago
    {
        public short IdPK { get; set; }

        public short? IdFacturaFK { get; set; }

        public short? IdMetodoPagoFK { get; set; } 

        public double? Monto_Cancelado { get; set; }    

        public bool? Estado {  get; set; }

    }
}
