using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
   public class Category:EntityBase
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Medicine> Medicines { get; set; }
       
       
    }
}
