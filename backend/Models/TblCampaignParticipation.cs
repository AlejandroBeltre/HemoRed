using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblCampaignParticipation
{
    public int CampaignId { get; set; }

    public int OrganizerId { get; set; }

    public int DonationId { get; set; }

    public virtual TblCampaign Campaign { get; set; } = null!;

    public virtual TblDonation? Donation { get; set; }

    public virtual TblOrganizer Organizer { get; set; } = null!;
}
