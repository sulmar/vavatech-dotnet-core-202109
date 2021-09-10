using System;
using System.ComponentModel.DataAnnotations;

namespace Vavatech.Shop.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        
        [StringLength(11, MinimumLength = 11)]
        public string Pesel { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address ShipAddress { get; set; }
        public bool IsRemoved { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
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
