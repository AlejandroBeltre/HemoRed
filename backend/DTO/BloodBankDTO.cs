namespace backend.DTO;

public class BloodBankDto
{
    public int BloodBankID { get; set; }
    public int AddressID { get; set; }
    public string BloodBankName { get; set; }
    public string AvailableHours { get; set; }
    public string Phone { get; set; }
    public string? Image { get; set; }
}

public class NewBloodBankDTO
{
    public int AddressID { get; set; }
    public string BloodBankName { get; set; }
    public string AvailableHours { get; set; }
    public string Phone { get; set; }
    public IFormFile? Image { get; set; }
}