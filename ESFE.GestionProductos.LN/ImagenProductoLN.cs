// Esta clase de lógica de negocio sirve para gestionar la galería de imágenes de un producto: aplica las reglas (límite de imágenes, imagen principal) y coordina con ProductoImagenDAL.
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN
{
    public class ImagenProductoLN
    {
        public const int MAXIMO_IMAGENES_POR_PRODUCTO = 5;

        private readonly ProductoImagenDAL imagenDAL = new ProductoImagenDAL();

        public List<ImagenProductoDTO> Listar(int idProducto)
        {
            return imagenDAL.Listar(idProducto)
                .Select(img => MapearDTO(img.IdProductoImagenPK, img.NombreArchivo, img.EsPrincipal, img.Orden))
                .ToList();
        }

        public int ContarActivas(int idProducto)
        {
            return imagenDAL.ContarActivas(idProducto);
        }

        // La LN no toca el disco: el nombre de archivo ya fue generado y guardado
        // físicamente por IImagenStorage antes de llegar aquí (responsabilidad del controller).
        public ResultadoSubirImagenDTO RegistrarImagen(int idProducto, string nombreArchivoGuardado, bool esPrincipalSolicitado)
        {
            int activas = imagenDAL.ContarActivas(idProducto);

            if (activas >= MAXIMO_IMAGENES_POR_PRODUCTO)
                return new ResultadoSubirImagenDTO
                {
                    Resultado = ResultadoSubirImagen.LimiteAlcanzado,
                    Mensaje = "Máximo 5 imágenes por producto"
                };

            // Primera imagen del producto: siempre queda como principal, sin importar el flag.
            bool esPrincipal = activas == 0 || esPrincipalSolicitado;

            int nuevoId = imagenDAL.Crear(idProducto, nombreArchivoGuardado, esPrincipal);

            return new ResultadoSubirImagenDTO
            {
                Resultado = ResultadoSubirImagen.Ok,
                Imagen = MapearDTO(nuevoId, nombreArchivoGuardado, esPrincipal, (short)activas)
            };
        }

        public ResultadoEliminarImagenDTO Eliminar(int idImagen)
        {
            var r = imagenDAL.Eliminar(idImagen);

            if (string.IsNullOrEmpty(r.NombreArchivo))
                return new ResultadoEliminarImagenDTO { Ok = false, Mensaje = "Imagen no encontrada" };

            ImagenProductoDTO? nuevaPrincipal = null;
            if (r.IdImagenPromovida.HasValue && r.NombreArchivoPromovido != null)
                nuevaPrincipal = MapearDTO(r.IdImagenPromovida.Value, r.NombreArchivoPromovido, esPrincipal: true, orden: 0);

            return new ResultadoEliminarImagenDTO
            {
                Ok = true,
                NombreArchivo = r.NombreArchivo,
                NuevaPrincipal = nuevaPrincipal
            };
        }

        public bool MarcarPrincipal(int idImagen)
        {
            return imagenDAL.MarcarPrincipal(idImagen);
        }

        private static ImagenProductoDTO MapearDTO(int id, string nombreArchivo, bool esPrincipal, short orden)
        {
            string nombreSinExtension = Path.GetFileNameWithoutExtension(nombreArchivo);

            return new ImagenProductoDTO
            {
                Id = id,
                Url = $"/uploads/productos/full/{nombreArchivo}",
                UrlThumb = $"/uploads/productos/thumb/{nombreSinExtension}_thumb.webp",
                EsPrincipal = esPrincipal,
                Orden = orden
            };
        }
    }
}
