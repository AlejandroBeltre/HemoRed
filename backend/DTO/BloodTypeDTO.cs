using backend.Enums;
namespace backend.DTO;

public class BloodTypeDto
{
    public int BloodTypeID { get; set; }
    public BloodType BloodType { get; set; }
}

public class NewBloodTypeDTO
{
    public BloodType BloodType { get; set; }
}