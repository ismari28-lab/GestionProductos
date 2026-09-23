// Esta entidad representa un rol de usuario (por ejemplo, administrador) que define sus permisos en el sistema.
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Rol
    {
        public short IdRolPK { get; set; }

        public string? NombreRol { get; set; }

    }
}
