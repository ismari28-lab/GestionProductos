// Este enum sirve para indicar los posibles resultados al subir una imagen de producto.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoSubirImagen
    {
        Ok,
        LimiteAlcanzado,
        ArchivoInvalido,
        ProductoNoEncontrado,
        ErrorInterno
    }
}
