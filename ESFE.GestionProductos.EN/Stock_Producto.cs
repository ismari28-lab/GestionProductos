using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Stock_Producto
    {
        public short IdStock_ProductoPK { get; set; }
        public short? IdProductoFK { get; set; }
        public short? Stock { get; set; }
        public short? Stock_Minimo { get; set; }
        public bool? Estado { get; set; }

    }
}
