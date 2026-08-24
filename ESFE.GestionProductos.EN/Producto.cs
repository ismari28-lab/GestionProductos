using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ESFE.GestionProductos.EN
{
    public class Producto
    {
        [Key]
        public int IdProductoPK { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PrecioCompra { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PrecioVenta { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PorcentajeIVA { get; set; }

        public bool? AplicaIVA { get; set; }

        public int? IdProveedorFK { get; set; }

        public short? IdCategoriaFK { get; set; }

        public bool? Estado { get; set; }

        // Opcional: Propiedades de navegación para relaciones con Proveedor y Categoria
        [ForeignKey("IdProveedorFK")]
        public virtual Proveedor? Proveedor { get; set; }

        [ForeignKey("IdCategoriaFK")]
        public virtual Categoria? Categoria { get; set; }

        public string Codigo { get; set; }
    }
}
