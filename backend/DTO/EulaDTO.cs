namespace backend.DTO;

public class EulaDto
{
    public int EulaID { get; set; }
    public string Version { get; set; }
    public DateOnly UpdateDate { get; set; }
    public string Content { get; set; }
}

public class NewEulaDTO
{
    public string Version { get; set; }
    public DateOnly UpdateDate { get; set; }
    public string Content { get; set; }
}
