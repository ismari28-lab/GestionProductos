using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    internal class Factura
    {
        public short IdFacturaPK { get; set; }
        public DateTime? Fecha { get; set; }
        public short? IdClienteFK { get; set; }
        public short? IdUsuarioFK { get; set; }
        public short? Estado { get; set; }
        public double? Total { get; set; }
    }
}
