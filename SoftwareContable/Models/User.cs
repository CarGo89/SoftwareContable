
namespace SoftwareContable.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}