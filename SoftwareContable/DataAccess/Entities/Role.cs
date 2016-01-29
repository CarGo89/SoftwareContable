using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareContable.DataAccess.Entities
{
    public class Role : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}