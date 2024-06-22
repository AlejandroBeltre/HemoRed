using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblUser
    {
        public required string DocumentNumber { get; set; }
        public required string DocumentType { get; set; }
        public int BloodTypeID { get; set; }
        public int? AddressID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Gender { get; set; }
        public string? Phone { get; set; }
        public required string UserRole { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public string? Image { get; set; }

        public required tblBloodType BloodType { get; set; }
        public tblAddress? Address { get; set; }
    }
}