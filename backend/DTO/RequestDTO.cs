namespace backend.DTO;

public class RequestDto
{
    public int RequestID { get; set; }
    public string UserDocument { get; set; }
    public int BloodTypeID { get; set; }
    public DateTime RequestTimeStamp { get; set; }
    public string RequestReason { get; set; }
    public int RequestedAmount { get; set; }
    public Status Status { get; set; }
}

public class NewRequestDTO
{
    public string UserDocument { get; set; }
    public int BloodTypeID { get; set; }
    public DateTime RequestTimeStamp { get; set; }
    public string RequestReason { get; set; }
    public int RequestedAmount { get; set; }
    public Status Status { get; set; }
}
