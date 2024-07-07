using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblBloodType
{
    public int BloodTypeId { get; set; }

    public string BloodType { get; set; } = null!;

    public virtual ICollection<TblBloodBag> TblBloodBags { get; set; } = new List<TblBloodBag>();

    public virtual ICollection<TblDonation> TblDonations { get; set; } = new List<TblDonation>();

    public virtual ICollection<TblRequest> TblRequests { get; set; } = new List<TblRequest>();

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
