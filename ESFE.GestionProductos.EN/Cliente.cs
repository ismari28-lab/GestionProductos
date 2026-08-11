using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Cliente
    {
            [Key]
            public int IdClientePK { get; set; }

            [StringLength(100)]
            public string? Nombre { get; set; }

            [StringLength(100)]
            public string? Apellido { get; set; }

            [StringLength(150)]
            public string? correo_electronico { get; set; }

            [StringLength(20)]
            public string? N_tel { get; set; }

            [StringLength(20)]
            public string? DUI { get; set; }

            [StringLength(255)]
            public string? Direccion_linea { get; set; }

            [StringLength(100)]
            public string? Municipio { get; set; }

            [StringLength(100)]
            public string? Distrito { get; set; }

            public int? IdUsuarioFK { get; set; }

            public bool? Estado { get; set; }

            // Opcional: Propiedad de navegación si vas a usar la relación en EF Core
            [ForeignKey("IdUsuarioFK")]
            public virtual Usuario? Usuario { get; set; }
       
    }
}
