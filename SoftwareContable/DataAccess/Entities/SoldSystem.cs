using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareContable.DataAccess.Entities
{
    [Table("SoldSystems", Schema = "sc")]
    public class SoldSystem : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}