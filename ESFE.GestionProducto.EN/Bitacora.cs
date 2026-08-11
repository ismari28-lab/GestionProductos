using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProducto.EN
{
    public class Bitacora
    {
        public short IdBitacoraPK { get; set; }
        public short? IdUsuarioFK { get; set; }
        public string Accion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Modulo { get; set; }
        public short? IdRegistroAfectado { get; set; }
    }
}
