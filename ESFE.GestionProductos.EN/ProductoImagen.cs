// Esta entidad representa una imagen asociada a un producto, indicando el archivo, su orden y si es la imagen principal.
using System;

namespace ESFE.GestionProductos.EN
{
    public class ProductoImagen
    {
        public int IdProductoImagenPK { get; set; }
        public int IdProductoFK { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public short Orden { get; set; }
        public bool EsPrincipal { get; set; }
        public DateTime FechaSubida { get; set; }
        public bool Estado { get; set; }
    }
}
