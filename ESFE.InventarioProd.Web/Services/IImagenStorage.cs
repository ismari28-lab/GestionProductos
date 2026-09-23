// Esta interfaz de servicio define cómo guardar y eliminar físicamente las imágenes de productos, y el resultado de guardar una imagen.
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ESFE.InventarioProd.Web.Services
{
    public interface IImagenStorage
    {
        Task<ResultadoGuardarImagen> GuardarAsync(IFormFile archivo, CancellationToken ct = default);
        void Eliminar(string nombreArchivo);
    }

    public class ResultadoGuardarImagen
    {
        public bool Ok { get; set; }
        public string? NombreArchivo { get; set; }
        public string? MensajeError { get; set; }
    }
}
