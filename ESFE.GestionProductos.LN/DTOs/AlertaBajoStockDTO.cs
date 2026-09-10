namespace ESFE.GestionProductos.LN.DTOs
{
    public class AlertaBajoStockDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public int Existencias { get; set; }

        public int Minimo { get; set; }
    }
}
