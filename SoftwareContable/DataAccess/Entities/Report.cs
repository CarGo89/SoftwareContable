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

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }

        [Required]
        public decimal? InvoiceTotal { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string InvoiceSerial { get; set; }

        [Required]
        public int SoldSystemId { get; set; }

        public virtual SoldSystem SoldSystem { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(8000)]
        public string Comments { get; set; }

        [Required]
        public DateTime? PromiseDate { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR"), MaxLength(255)]
        public string Lote { get; set; }

        [Required]
        public int? NumberOfUsers { get; set; }

        [Required]
        public DateTime? ActivatedDate { get; set; }
    }
}