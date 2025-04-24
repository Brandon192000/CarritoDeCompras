using System.ComponentModel.DataAnnotations;

namespace CarritoCompras.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido, ingrese uno valido de nuevo.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string? Contrasenia { get; set; }

    }
}
