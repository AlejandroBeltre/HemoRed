namespace backend.DTO;

public class UserEulaDto
{
    public int EulaID { get; set; }
    public string UserDocument { get; set; }
    public bool AcceptedStatus { get; set; }
}

public class NewUserEulaDTO
{
    public int EulaID { get; set; }
    public string UserDocument { get; set; }
    public bool AcceptedStatus { get; set; }
}
