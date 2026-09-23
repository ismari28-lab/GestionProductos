// Este enum sirve para indicar los posibles resultados al guardar un movimiento de inventario.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoGuardarMovimiento
    {
        Ok,
        StockInsuficiente,
        ProductoSinStockEnSalida,
        DatosInvalidos,
        ErrorInterno
    }
}
