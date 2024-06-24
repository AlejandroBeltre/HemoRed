namespace backend.DTO;

public class BloodBagDto
{
    public int BagID { get; set; }
    public int BloodTypeID { get; set; }
    public int BloodBankID { get; set; }
    public int DonationID { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public bool IsReserved { get; set; }
}

public class NewBloodBagDTO
{
    public int BloodTypeID { get; set; }
    public int BloodBankID { get; set; }
    public int DonationID { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public bool IsReserved { get; set; }
}