using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblUserCampaign
    {
        public int campaignID { get; set; }
        public required string userDocument { get; set; }
        public required tblCampaign tblCampaign { get; set; }
        public required tblUser tblUser { get; set; }
    }
}