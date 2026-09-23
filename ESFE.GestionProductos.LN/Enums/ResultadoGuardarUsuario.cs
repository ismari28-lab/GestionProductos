// Este enum sirve para indicar los posibles resultados al crear o editar un usuario.
namespace ESFE.GestionProductos.LN.Enums
{
    public enum ResultadoGuardarUsuario
    {
        Ok,
        NombreDuplicado,
        DatosInvalidos,
        NoEncontrado,
        ErrorInterno
    }
}
