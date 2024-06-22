using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblBloodBank
    {
        public int BloodBankID { get; set; }
        public int AddressID { get; set; }
        public required string BloodBankName { get; set; }
        public required string AvailableHours { get; set; }
        public required string Phone { get; set; }
        public string? Image { get; set; }

        public required tblAddress Address { get; set; }
    }
}