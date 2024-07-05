namespace backend.DTO;

public class CampaignDto
{
    public int CampaignID { get; set; }
    public int? AddressID { get; set; }
    public int OrganizerID { get; set; }
    public string CampaignName { get; set; }
    public string Description { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime EndTimestamp { get; set; }
    public string? Image { get; set; }
}

public class NewCampaignDto
{
    public int? AddressID { get; set; }
    public int OrganizerID { get; set; }
    public string CampaignName { get; set; }
    public string Description { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime EndTimestamp { get; set; }
    public string? Image { get; set; }
}