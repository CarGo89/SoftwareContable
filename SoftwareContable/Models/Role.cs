using System.ComponentModel;

namespace SoftwareContable.Models
{
    [DisplayName("roles")]
    public class Role : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}