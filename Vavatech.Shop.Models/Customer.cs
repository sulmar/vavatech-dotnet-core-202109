using System;

namespace Vavatech.Shop.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Pesel { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address ShipAddress { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}
