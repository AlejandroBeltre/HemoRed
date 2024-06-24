namespace backend.DTO;

public class MunicipalityDto
{
    public int MunicipalityID { get; set; }
    public int ProvinceID { get; set; }
    public string MunicipalityName { get; set; }
}

public class NewMunicipalityDTO
{
    public int ProvinceID { get; set; }
    public string MunicipalityName { get; set; }
}
    