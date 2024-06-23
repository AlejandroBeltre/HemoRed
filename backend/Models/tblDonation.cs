using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    public class tblDonation
    {
        public int donationID { get; set; }
        public string? userDocument { get; set; }
        public int bloodTypeID { get; set; }
        public int bloodBankID { get; set; }
        public required DateTime donationTimestamp { get; set; }
        public required Status status { get; set; }

        public tblUser? tblUser { get; set; }
        public required tblBloodType tblBloodType { get; set; }
        public required tblBloodBank tblBloodBank { get; set; }
    }
}