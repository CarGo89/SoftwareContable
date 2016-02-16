using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareContable.Models
{
    [DisplayName("reports")]
    public class Report : IModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "RFC Client es requerido.")]
        public int ClientId { get; set; }

        public Client Client { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fecha Factura es requerido.")]
        public DateTime? InvoiceDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Importe Factura es requerido.")]
        public decimal InvoiceTotal { get; set; }

        public DateTime? CreationDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sistema Vendido es requerido.")]
        public int SoldSystemId { get; set; }

        public SoldSystem SoldSystem { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Comentarios es requerido.")]
        public string Comments { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fecha Promesa es requerido.")]
        public DateTime? PromiseDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lote es requerido.")]
        public string Lote { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Folio Factura es requerido.")]
        public string InvoiceSerial { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Numero de Usuarios es requerido.")]
        public int NumberOfUsers { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fecha Activación es requerido.")]
        public DateTime? ActivatedDate { get; set; }
    }
}