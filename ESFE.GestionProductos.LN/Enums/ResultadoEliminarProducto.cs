// Este enum sirve para indicar los posibles resultados al eliminar un producto.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoEliminarProducto
    {
        Ok,
        TieneMovimientos,
        NoEncontrado,
        ErrorInterno
    }
}
