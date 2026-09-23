// Este DTO sirve para devolver el resultado de eliminar una imagen de producto (éxito, mensaje, archivo a borrar y la nueva imagen principal si cambió).
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoEliminarImagenDTO
    {
        public bool Ok { get; set; }
        public string? Mensaje { get; set; }
        public string? NombreArchivo { get; set; }  // necesario para que el controller borre los archivos físicos (full/thumb)
        public ImagenProductoDTO? NuevaPrincipal { get; set; }  // null si no había otra o si no era principal la eliminada
    }
}
