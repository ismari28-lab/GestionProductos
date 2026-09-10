using System.ComponentModel.DataAnnotations;

namespace ESFE.InventarioProd.Web.Models
{
    public class PerfilViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string PasswordActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StringLength(256, ErrorMessage = "La contraseña no puede superar los 256 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string PasswordNueva { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes confirmar la nueva contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare(nameof(PasswordNueva), ErrorMessage = "Las contraseñas no coinciden")]
        public string PasswordConfirmar { get; set; } = string.Empty;
    }
}
