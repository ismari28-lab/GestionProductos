// Este DTO sirve para devolver el resultado de subir una imagen de producto junto con la imagen registrada o el mensaje de error.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoSubirImagenDTO
    {
        public ResultadoSubirImagen Resultado { get; set; }
        public string? Mensaje { get; set; }
        public ImagenProductoDTO? Imagen { get; set; }
    }
}
