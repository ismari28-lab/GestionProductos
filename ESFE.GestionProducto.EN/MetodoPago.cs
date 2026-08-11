using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProducto.EN
{
    public class MetodoPago
    {
        public short IdMetodoPagoPK { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }
    }
}
