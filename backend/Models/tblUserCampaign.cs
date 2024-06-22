using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblUserCampaign
    {
        public int CampaignID { get; set; }
        public required string UserDocument { get; set; }
        public required tblCampaign Campaign { get; set; }
        public required tblUser User { get; set; }
    }
}