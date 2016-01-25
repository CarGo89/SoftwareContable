﻿using System;

namespace SoftwareContable.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public DateTime CreationDate { get; set; }

        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal InvoiceTotal { get; set; }

        public System SoldSystem { get; set; }

        public string Comments { get; set; }

        public DateTime PromiseDate { get; set; }

        public string Lote { get; set; }

        public string Serial { get; set; }

        public int NumberOfUsers { get; set; }

        public DateTime ActivatedDate { get; set; }
    }
}