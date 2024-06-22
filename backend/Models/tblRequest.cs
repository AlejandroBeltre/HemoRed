using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblRequest
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public int BloodBankID { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string Status { get; set; }

        public required tblUser User { get; set; }
        public required tblBloodBank BloodBank { get; set; }
    }
}