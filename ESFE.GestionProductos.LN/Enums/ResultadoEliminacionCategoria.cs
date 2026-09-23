// Este enum sirve para indicar los posibles resultados al eliminar una categoría (eliminada, tiene productos asociados o no encontrada).
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoEliminacionCategoria
    {
        Ok,
        TieneProductos,
        NoEncontrada
    }
}
