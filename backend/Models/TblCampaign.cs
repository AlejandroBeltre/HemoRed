using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblCampaign
{
    public int CampaignId { get; set; }

    public int? AddressId { get; set; }

    public int? OrganizerId { get; set; }

    public string CampaignName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartTimestamp { get; set; }

    public DateTime EndTimestamp { get; set; }

    public string? Image { get; set; }

    public virtual TblAddress? Address { get; set; }

    public virtual TblOrganizer? Organizer { get; set; }

    public virtual ICollection<TblCampaignParticipation?> TblCampaignParticipations { get; set; } = new List<TblCampaignParticipation?>();

    public virtual ICollection<TblUser> UserDocuments { get; set; } = new List<TblUser>();
}
