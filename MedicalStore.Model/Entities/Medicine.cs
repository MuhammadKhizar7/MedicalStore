using System;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
    public class Medicine : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Formula { get; set; }
        public string Strength { get; set; }
        public int PackSize { get; set; }
        public int QuentityInPack { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public MedicineType MedicineType { get; set; }



    }

    public enum MedicineType
    {
        Tablet,
        Injection,
        Syrup,
        Cream,
        Ointment,
        Other
    }
}
