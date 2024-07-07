using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblOrganizer
{
    public int OrganizerId { get; set; }

    public int? AddressId { get; set; }

    public string OrganizerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual TblAddress? Address { get; set; }

    public virtual ICollection<TblCampaignParticipation> TblCampaignParticipations { get; set; } = new List<TblCampaignParticipation>();

    public virtual ICollection<TblCampaign> TblCampaigns { get; set; } = new List<TblCampaign>();
}
