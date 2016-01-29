using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareContable.DataAccess.Entities
{
    public class Report : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public decimal InvoiceTotal { get; set; }

        [Required]
        public int SoldSystemId { get; set; }

        public virtual SoldSystem SoldSystem { get; set; }

        [Column(TypeName = "VARCHAR"), MaxLength(8000)]
        public string Comments { get; set; }

        public DateTime PromiseDate { get; set; }

        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Lote { get; set; }

        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Serial { get; set; }

        public int NumberOfUsers { get; set; }

        public DateTime? ActivatedDate { get; set; }
    }
}