using System;
using System.Collections.Generic;
using backend.Enums;

namespace backend.Models;

public partial class TblDonation
{
    public int DonationId { get; set; }

    public string? UserDocument { get; set; }

    public int BloodTypeId { get; set; }

    public int BloodBankId { get; set; }

    public DateTime DonationTimestamp { get; set; }

    public Status? Status { get; set; } = null!;

    public virtual TblBloodBank BloodBank { get; set; } = null!;

    public virtual TblBloodType BloodType { get; set; } = null!;

    public virtual ICollection<TblBloodBag> TblBloodBags { get; set; } = new List<TblBloodBag>();

    public virtual ICollection<TblCampaignParticipation> TblCampaignParticipations { get; set; } = new List<TblCampaignParticipation>();

    public virtual TblUser? UserDocumentNavigation { get; set; }
}
