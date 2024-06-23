using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblCampaign
    {
        public int campaignID { get; set; }
        public int? addressID { get; set; }
        public int organizerID { get; set; }
        public required string campaignName { get; set; }
         public required string description { get; set; }
        public required DateTime startTimestamp { get; set; }
        public required DateTime endTimestamp { get; set; }
        public string? image { get; set; }

        public tblAddress? tblAddress { get; set; }
        public required tblOrganizer tblOrganizer { get; set; }
    }
}