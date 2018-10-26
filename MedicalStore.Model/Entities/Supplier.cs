using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
    
    public class Supplier:EntityBase
    {
      

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }
     

    }
}
