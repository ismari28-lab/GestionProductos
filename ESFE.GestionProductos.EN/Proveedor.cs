using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Proveedor
    {

        [Key]
        public int IdProveedorPK { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(100)]
        public string? Empresa { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(150)]
        public string? Correo { get; set; }

        [StringLength(255)]
        public string? Direccion { get; set; }

        public bool? Estado { get; set; }
    }
}
