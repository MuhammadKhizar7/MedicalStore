using System;

namespace MedicalStore.Model.Entities
{

    public class InvoiceItem
    {

        public Int64 Id { get; set; }

    
        public decimal UnitPrice { get; set; }

        public int Qnt { get; set; }

        public decimal TTC { get; set; }

        public decimal THT { get; set; }

        public decimal Tax { get; set; }

        public Int64? MedicineId { get; set; }

        public Int64 InvoiceId { get; set; }
        public Medicine Medicine { get; set; }

        public Invoice Invoice { get; set; }

    }
}
