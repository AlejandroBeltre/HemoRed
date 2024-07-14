using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblBloodBank
{
    public int BloodBankId { get; set; }

    public int AddressId { get; set; }

    public string BloodBankName { get; set; } = null!;

    public string AvailableHours { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Image { get; set; }

    public virtual TblAddress Address { get; set; } = null!;

    public virtual ICollection<TblBloodBag> TblBloodBags { get; set; } = new List<TblBloodBag>();

    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();
}
