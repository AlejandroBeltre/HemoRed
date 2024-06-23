using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblOrganizer
    {
        public int organizerID { get; set; }
        public int? addressID { get; set; }
        public required string organizerName { get; set; }
        public required string email { get; set; }
        public required string phone { get; set; }

        public tblAddress? address { get; set; }
    }
}