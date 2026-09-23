// Este enum sirve para indicar los posibles resultados al crear o editar un producto.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoGuardarProducto
    {
        Ok,
        CodigoDuplicado,
        DatosInvalidos,
        NoEncontrado,
        ErrorInterno
    }
}
