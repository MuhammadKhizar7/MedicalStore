using System;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
    public class Sale : EntityBase
    {
        public Int64 Id { get; set; }
        public string ProductName { get; set; }
        public int Quentiy { get; set; }
        public decimal Tex { get; set; }
        public decimal Discount { get; set; }
        public decimal SalePrice { get; set; }

        public Int64 MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
       
       
    }
}