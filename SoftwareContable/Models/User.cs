using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareContable.Models
{
    [DisplayName("users")]
    public class User : IModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Usuario es requerido.")]
        [MaxLength(255, ErrorMessage = "Usuario excede la longitud de 255 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email es requerido.")]
        [MaxLength(255, ErrorMessage = "Email excede la longitud de 255 caracteres.")]
        [EmailAddress(ErrorMessage = "Email es invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida.")]
        [MaxLength(255, ErrorMessage = "Contraseña excede la longitud de 255 caracteres.")]
        [Compare("ConfirmationPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmar Contraseña es requerido.")]
        [MaxLength(255, ErrorMessage = "Confirmar Contraseña excede la longitud de 255 caracteres.")]
        public string ConfirmationPassword { get; set; }

        public bool IsAuthorized { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}