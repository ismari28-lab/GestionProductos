// Este enum sirve para indicar los posibles resultados al eliminar un proveedor.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoEliminarProveedor
    {
        Ok,
        TieneProductos,
        NoEncontrado,
        ErrorInterno
    }
}
