using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Empleado
    {
        public short IdEmpleadoPK { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public short? Cargo { get; set; }
        public short? IdUsuarioFK { get; set; }
        public bool? Estado { get; set; }

    }
}
