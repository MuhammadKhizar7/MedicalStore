using System;
using MedicalStore.Model.Entities.@base;

namespace MedicalStore.Model.Entities
{
  public  class Employee : EntityBase
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public EmployeeType EmployeeType  { get; set; }
        public string Tel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
       
    }
}
