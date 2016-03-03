using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareContable.DataAccess.Entities
{
    [Table("Clients", Schema = "sc")]
    public class Client : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Rfc { get; set; }

        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string CorporateName { get; set; }

        [Column(TypeName = "VARCHAR"), MaxLength(255), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}