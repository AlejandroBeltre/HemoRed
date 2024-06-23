using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblBloodBank
    {
        public int bloodBankID { get; set; }
        public int addressID { get; set; }
        public required string bloodBankName { get; set; }
        public required string availableHours { get; set; }
        public required string phone { get; set; }
        public string? image { get; set; }

        public required tblAddress tblAddress { get; set; }
    }
}