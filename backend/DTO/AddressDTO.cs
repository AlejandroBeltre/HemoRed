namespace backend.DTO;

public class AddressDto
{
    public int AddressID { get; set; }
    public int MunicipalityID { get; set; }
    public int ProvinceID { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
}

public class newAddressDTO
{
    public int MunicipalityID { get; set; }
    public int ProvinceID { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
}