using backend.Enums;

namespace backend.DTO;

public class RequestDto
{
    public int RequestID { get; set; }
    public string UserDocument { get; set; }
    public int BloodBankId { get; set; }
    public int BloodTypeId { get; set; }
    public DateTime RequestTimeStamp { get; set; }
    public string RequestReason { get; set; }
    public int RequestedAmount { get; set; }
    public Status? Status { get; set; }
}

public class NewRequestDto
{
    public string UserDocument { get; set; }
    public int BloodTypeId { get; set; }
    public int BloodBankId { get; set; }
    public DateTime RequestTimeStamp { get; set; }
    public string RequestReason { get; set; }
    public int RequestedAmount { get; set; }
    public Status Status { get; set; }
}
