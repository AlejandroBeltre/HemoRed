namespace backend.DTO;

public class OrganizerDto
{
    public int OrganizerID { get; set; }
    public int? AddressID { get; set; }
    public string OrganizerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class NewOrganizerDTO
{
    public int? AddressID { get; set; }
    public string OrganizerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
