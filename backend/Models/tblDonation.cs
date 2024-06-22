using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblDonation
    {
        public int DonationID { get; set; }
        public string? UserDocument { get; set; }
        public int BloodTypeID { get; set; }
        public int BloodBankID { get; set; }
        public required DateTime DonationTimestamp { get; set; }
        public required string Status { get; set; }

        public tblUser? User { get; set; }
        public required tblBloodType BloodType { get; set; }
        public required tblBloodBank BloodBank { get; set; }
    }
}