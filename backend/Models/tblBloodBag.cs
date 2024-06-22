using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblBloodBag
    {
        public int BagID { get; set; }
        public int BloodTypeID { get; set; }
        public int BloodBankID { get; set; }
        public int DonationID { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public bool IsReserved { get; set; }

        public required tblBloodType BloodType { get; set; }
        public required tblBloodBank BloodBank { get; set; }
        public required tblDonation Donation { get; set; }
    }
}