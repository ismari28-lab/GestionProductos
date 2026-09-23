// Esta entidad representa una línea de detalle de un movimiento de inventario (producto y cantidad).
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class DetalleMovimiento
    {
        public short IdDetalleMovimientoPK { get; set; }

        public short? IdMovimientoFK { get; set; }

        public short? IdProductoFK { get; set; }

        public short? Cantidad { get; set; }

        public bool? Estado { get; set; }

    }
}
