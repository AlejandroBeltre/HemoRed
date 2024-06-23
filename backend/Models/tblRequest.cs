using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    public class tblRequest
    {
        public int requestID { get; set; }
        public string userDocument { get; set; }
        public int bloodTypeID { get; set; }
        public required DateTime requestTimeStamp { get; set; }
        public required string requestReason { get; set; }
        public required int requestedAmount { get; set; }
        public required Status status { get; set; }

        public required tblUser tblUser { get; set; }
        public required tblBloodBank tblBloodBank { get; set; }
    }
}