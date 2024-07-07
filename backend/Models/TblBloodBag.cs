using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblBloodBag
{
    public int BagId { get; set; }

    public int BloodTypeId { get; set; }

    public int BloodBankId { get; set; }

    public int DonationId { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public bool IsReserved { get; set; }

    public virtual TblBloodBank BloodBank { get; set; } = null!;

    public virtual TblBloodType BloodType { get; set; } = null!;

    public virtual TblDonation Donation { get; set; } = null!;
}
