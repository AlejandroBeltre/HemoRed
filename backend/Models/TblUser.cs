using System;
using System.Collections.Generic;
using backend.Enums;

namespace backend.Models;

public partial class TblUser
{
    public string DocumentNumber { get; set; } = null!;

    public DocumentType? DocumentType { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public int? AddressId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public Gender? Gender { get; set; } = null!;

    public string? Phone { get; set; }

    public UserRole? UserRole { get; set; } = null!;

    public DateTime? LastDonationDate { get; set; }

    public string? Image { get; set; }

    public virtual TblAddress? Address { get; set; }

    public virtual TblBloodType BloodType { get; set; } = null!;

    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblUserEula> TblUserEulas { get; set; } = new List<TblUserEula>();

    public virtual ICollection<TblCampaign> Campaigns { get; set; } = new List<TblCampaign>();
}
