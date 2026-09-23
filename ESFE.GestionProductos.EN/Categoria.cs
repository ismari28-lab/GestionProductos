// Esta entidad representa una categoría de productos tal como se almacena en la base de datos.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Categoria
    {
        [Key]
        public short IdCategoriaPK { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        public bool? Estado { get; set; }
    }
}
