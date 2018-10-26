using System;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
   public class Patient : EntityBase
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Disease { get; set; }
        public string Tel { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

      
    }
}
