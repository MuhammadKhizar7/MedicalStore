using System;
using System.Collections.Generic;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
   
    public class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }
       
        public Int64 Id { get; set; }
        
        public string InvoiceNumber { get; set; }
        
        public decimal TTC { get; set; }

        public decimal THT { get; set; }

        public bool IsValid { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public decimal PaidAmmount { get; set; }
        
        public decimal Left { get; set; }

        public Int64? PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
