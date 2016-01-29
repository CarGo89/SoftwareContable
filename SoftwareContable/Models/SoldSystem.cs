using System.ComponentModel;

namespace SoftwareContable.Models
{
    [DisplayName("soldSystems")]
    public class SoldSystem : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}