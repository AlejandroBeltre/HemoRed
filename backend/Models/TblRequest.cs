using System;
using System.Collections.Generic;
using backend.Enums;

namespace backend.Models;

public partial class TblRequest
{
    public int RequestId { get; set; }

    public string UserDocument { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public DateTime RequestTimestamp { get; set; }

    public string RequestReason { get; set; } = null!;

    public int RequestedAmount { get; set; }

    public Status? Status { get; set; } = null!;

    public int BloodBankId { get; set; }

    public virtual TblBloodBank? BloodBank { get; set; }

    public virtual TblBloodType BloodType { get; set; } = null!;

    public virtual TblUser UserDocumentNavigation { get; set; } = null!;
}
