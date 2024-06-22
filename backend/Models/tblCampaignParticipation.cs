using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblCampaignParticipation
    {
        public int CampaignID { get; set; }
        public int OrganizerID { get; set; }
        public int DonationID { get; set; }
        public required tblCampaign Campaign { get; set; }
        public required tblOrganizer Organizer { get; set; }
        public required tblDonation Donation { get; set; }
    }
}