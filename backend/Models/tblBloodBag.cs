using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblBloodBag
    {
        public int bagID { get; set; }
        public int bloodTypeID { get; set; }
        public int bloodBankID { get; set; }
        public int donationID { get; set; }
        public required DateOnly expirationDate { get; set; }
        public bool isReserved { get; set; }

        public required tblBloodType tblBloodType { get; set; }
        public required tblBloodBank tblBloodBank { get; set; }
        public required tblDonation tblDonation { get; set; }
    }
}