using System.ComponentModel.DataAnnotations;

namespace SoftwareContable.Models.Account
{
    public class UserRegister : UserLogin
    {
        [Required]
        public string ConfirmPassword { get; set; }
    }
}