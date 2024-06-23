using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    public class tblUser
    {
        public required string documentNumber { get; set; }
        public required DocumentType documentType { get; set; }
        public int bloodTypeID { get; set; }
        public int? addressID { get; set; }
        public required string fullName { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public required DateOnly birthDate { get; set; }
        public required Gender gender { get; set; }
        public string? phone { get; set; }
        public required UserRole userRole { get; set; }
        public DateOnly? lastDonationDate { get; set; }
        public string? image { get; set; }

        public required tblBloodType tblBloodType { get; set; }
        public tblAddress? tblAddress { get; set; }
    }
}