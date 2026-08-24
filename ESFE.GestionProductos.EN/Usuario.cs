using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Usuario
    {
        [Key]
        public int IdUsuarioPK { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(256)]
        public string? Password { get; set; }

        public short? Id_RolFK { get; set; }

        public bool? Estado { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string NombreRol { get; set; }

        // Computed: lo que se muestra en el combo
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string NombreConRol =>
            string.IsNullOrEmpty(NombreRol)
                ? Nombre
                : $"{Nombre} ({NombreRol})";
    }
}
