using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class MovimientoInventario
    {
        public short IdMovimientoPK { get; set; }   

        public string? TipoMovimiento { get; set; } 

        public string? Referencia { get; set; }

        public DateTime? Fecha { get; set; }

        public short? IdUsuarioFK { get; set; } 

        public short? Estado { get; set; }

    }
}
