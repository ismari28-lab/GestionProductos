// Este DTO sirve para recibir cada línea (producto y cantidad) de un movimiento de inventario enviado desde la vista.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class DetalleMovimientoInputDTO
    {
        public int IdProductoPK { get; set; }
        public int Cantidad { get; set; }
    }
}
