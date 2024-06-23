using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblCampaignParticipation
    {
        public int campaignID { get; set; }
        public int organizerID { get; set; }
        public int donationID { get; set; }
        public required tblCampaign tblCampaign { get; set; }
        public required tblOrganizer tblOrganizer { get; set; }
        public required tblDonation tblDonation { get; set; }
    }
}