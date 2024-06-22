using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblCampaign
    {
        public int CampaignID { get; set; }
        public int? AddressID { get; set; }
        public int OrganizerID { get; set; }
        public required string CampaignName { get; set; }
         public required string Description { get; set; }
        public required DateTime StartTimestamp { get; set; }
        public required DateTime EndTimestamp { get; set; }
        public string? Image { get; set; }

        public tblAddress? Address { get; set; }
        public required tblOrganizer Organizer { get; set; }
    }
}