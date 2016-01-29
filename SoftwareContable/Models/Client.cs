using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareContable.Models
{
    [DisplayName("clients")]
    public class Client : IModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RFC es requerido.")]
        [MaxLength(255, ErrorMessage = "RFC excede la longitud de 255 caracteres.")]
        public string Rfc { get; set; }

        [Required(ErrorMessage = "Razón Social es requerida.")]
        [MaxLength(255, ErrorMessage = "Razón Social excede la longitud de 255 caracteres.")]
        public string CorporateName { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido.")]
        [MaxLength(255, ErrorMessage = "Teléfono excede la longitud de 255 caracteres.")]
        [Phone(ErrorMessage = "Teléfono es invalido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email es requerido.")]
        [MaxLength(255, ErrorMessage = "Email excede la longitud de 255 caracteres.")]
        [EmailAddress(ErrorMessage = "Email es invalido.")]
        public string Email { get; set; }
    }
}