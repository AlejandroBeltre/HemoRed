namespace backend.DTO;

public class DonationDto
{
    public int DonationID { get; set; }
    public string? UserDocument { get; set; }
    public int BloodTypeID { get; set; }
    public int BloodBankID { get; set; }
    public DateTime DonationTimestamp { get; set; }
    public Status Status { get; set; }
}

public class NewDonationDTO
{
    public string? UserDocument { get; set; }
    public int BloodTypeID { get; set; }
    public int BloodBankID { get; set; }
    public DateTime DonationTimestamp { get; set; }
    public Status Status { get; set; }
}
