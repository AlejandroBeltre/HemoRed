using System;
using System.Collections.Generic;
using backend.Enums;

namespace backend.Models;

public partial class TblUser
{
    public string DocumentNumber { get; set; }

    public DocumentType? DocumentType { get; set; }

    public int BloodTypeId { get; set; }

    public int? AddressId { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }

    public Gender? Gender { get; set; }

    public string? Phone { get; set; }

    public UserRole? UserRole { get; set; }

    public DateTime? LastDonationDate { get; set; }

    public string? Image { get; set; }

    public virtual TblAddress? Address { get; set; }

    public virtual TblBloodType BloodType { get; set; }

    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblUserEula> TblUserEulas { get; set; } = new List<TblUserEula>();

    public virtual ICollection<TblCampaign> Campaigns { get; set; } = new List<TblCampaign>();
}
