using System.ComponentModel.DataAnnotations;

namespace SoftwareContable.Models.Account
{
    public class UserLogin
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsAuthorized { get; set; }
    }
}