using System;
using System.ComponentModel;

namespace SoftwareContable.Models
{
    [DisplayName("reports")]
    public class Report : IModel
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal InvoiceTotal { get; set; }

        public SoldSystem SoldSystem { get; set; }

        public string Comments { get; set; }

        public DateTime PromiseDate { get; set; }

        public string Lote { get; set; }

        public string Serial { get; set; }

        public int NumberOfUsers { get; set; }

        public DateTime ActivatedDate { get; set; }
    }
}