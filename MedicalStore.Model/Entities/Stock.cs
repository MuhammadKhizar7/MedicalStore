using System;
using System.Collections.Generic;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
   public class Stock : EntityBase
    {
        public Int64 Id { get; set; }
        public string ProductName { get; set; }     
        public int Quentity { get; set; }
        public int BatchNo { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal TotalCost { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Int64? MedicineId { get; set; }
        public Medicine Medicine { get; set; }

    }
}
