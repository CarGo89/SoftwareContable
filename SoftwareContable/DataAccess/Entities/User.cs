using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareContable.DataAccess.Entities
{
    [Table("Users", Schema = "sc")]
    public class User : IEntity
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Index(IsUnique = true)]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public bool IsAuthorized { get; set; }

        [Required]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int? AuthorizedByUserId { get; set; }

        public virtual User AuthorizedByUser { get; set; }

        public DateTime? AuthorizationDate { get; set; }
    }
}