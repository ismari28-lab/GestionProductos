// Este DTO sirve para transportar los datos de una imagen de producto (id, URL, miniatura, orden y si es principal) hacia la vista.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ImagenProductoDTO
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;        // /uploads/productos/full/{guid}.webp
        public string UrlThumb { get; set; } = string.Empty;   // /uploads/productos/thumb/{guid}_thumb.webp
        public bool EsPrincipal { get; set; }
        public short Orden { get; set; }
    }
}
