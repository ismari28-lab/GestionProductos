// Este servicio sirve para guardar y eliminar en disco (wwwroot/uploads) las imágenes de productos, validando tipo, tamaño y contenido del archivo.
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ESFE.InventarioProd.Web.Services
{
    public class ImagenStorage : IImagenStorage
    {
        private const long TAMANIO_MAXIMO_BYTES = 5 * 1024 * 1024;
        private const int MAX_DIM_FULL = 1600;
        private const int CALIDAD_FULL = 80;
        private const int MAX_DIM_THUMB = 300;
        private const int CALIDAD_THUMB = 70;
        private const string CARPETA_FULL = "uploads/productos/full";
        private const string CARPETA_THUMB = "uploads/productos/thumb";

        private static readonly HashSet<string> ExtensionesPermitidas = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".webp"
        };

        private static readonly HashSet<string> MimeTypesPermitidos = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg", "image/png", "image/webp"
        };

        private readonly IWebHostEnvironment _env;

        public ImagenStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<ResultadoGuardarImagen> GuardarAsync(IFormFile archivo, CancellationToken ct = default)
        {
            if (archivo == null || archivo.Length == 0)
                return Error("Archivo vacío o no proporcionado");

            if (archivo.Length > TAMANIO_MAXIMO_BYTES)
                return Error("El archivo supera 5 MB");

            string extension = Path.GetExtension(archivo.FileName);
            if (!ExtensionesPermitidas.Contains(extension))
                return Error("Extensión no permitida (solo JPG, PNG o WEBP)");

            if (!MimeTypesPermitidos.Contains(archivo.ContentType))
                return Error("Tipo de archivo no permitido (solo JPG, PNG o WEBP)");

            await using Stream stream = archivo.OpenReadStream();

            byte[] cabecera = new byte[12];
            int leidos = await stream.ReadAsync(cabecera.AsMemory(0, cabecera.Length), ct);
            if (leidos < cabecera.Length || !TieneMagicBytesValidos(cabecera))
                return Error("Archivo no es una imagen válida (JPG/PNG/WEBP)");

            stream.Position = 0;

            using Image imagen = await Image.LoadAsync(stream, ct);

            string guid = Guid.NewGuid().ToString("N");
            string nombreFull = $"{guid}.webp";
            string nombreThumb = $"{guid}_thumb.webp";

            string directorioFull = RutaCarpeta(CARPETA_FULL);
            string directorioThumb = RutaCarpeta(CARPETA_THUMB);
            Directory.CreateDirectory(directorioFull);
            Directory.CreateDirectory(directorioThumb);

            using (Image full = imagen.Clone(ctx =>
            {
                if (Math.Max(imagen.Width, imagen.Height) > MAX_DIM_FULL)
                    ctx.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(MAX_DIM_FULL, MAX_DIM_FULL) });
            }))
            {
                await full.SaveAsWebpAsync(Path.Combine(directorioFull, nombreFull), new WebpEncoder { Quality = CALIDAD_FULL }, ct);
            }

            using (Image thumb = imagen.Clone(ctx =>
            {
                if (Math.Max(imagen.Width, imagen.Height) > MAX_DIM_THUMB)
                    ctx.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(MAX_DIM_THUMB, MAX_DIM_THUMB) });
            }))
            {
                await thumb.SaveAsWebpAsync(Path.Combine(directorioThumb, nombreThumb), new WebpEncoder { Quality = CALIDAD_THUMB }, ct);
            }

            return new ResultadoGuardarImagen { Ok = true, NombreArchivo = nombreFull };
        }

        public void Eliminar(string nombreArchivo)
        {
            string guid = Path.GetFileNameWithoutExtension(nombreArchivo);
            BorrarSiExiste(Path.Combine(RutaCarpeta(CARPETA_FULL), $"{guid}.webp"));
            BorrarSiExiste(Path.Combine(RutaCarpeta(CARPETA_THUMB), $"{guid}_thumb.webp"));
        }

        private string RutaCarpeta(string carpetaRelativa) =>
            Path.Combine(_env.WebRootPath, carpetaRelativa.Replace('/', Path.DirectorySeparatorChar));

        private static void BorrarSiExiste(string ruta)
        {
            try
            {
                if (File.Exists(ruta))
                    File.Delete(ruta);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ImagenStorage] No se pudo borrar '{ruta}': {ex.Message}");
            }
        }

        private static ResultadoGuardarImagen Error(string mensaje) =>
            new() { Ok = false, MensajeError = mensaje };

        private static bool TieneMagicBytesValidos(byte[] cabecera)
        {
            // JPEG: FF D8 FF
            if (cabecera.Length >= 3 && cabecera[0] == 0xFF && cabecera[1] == 0xD8 && cabecera[2] == 0xFF)
                return true;

            // PNG: 89 50 4E 47 0D 0A 1A 0A
            if (cabecera.Length >= 8 &&
                cabecera[0] == 0x89 && cabecera[1] == 0x50 && cabecera[2] == 0x4E && cabecera[3] == 0x47 &&
                cabecera[4] == 0x0D && cabecera[5] == 0x0A && cabecera[6] == 0x1A && cabecera[7] == 0x0A)
                return true;

            // WEBP: "RIFF" en offset 0, "WEBP" en offset 8
            if (cabecera.Length >= 12 &&
                cabecera[0] == 0x52 && cabecera[1] == 0x49 && cabecera[2] == 0x46 && cabecera[3] == 0x46 &&
                cabecera[8] == 0x57 && cabecera[9] == 0x45 && cabecera[10] == 0x42 && cabecera[11] == 0x50)
                return true;

            return false;
        }
    }
}
