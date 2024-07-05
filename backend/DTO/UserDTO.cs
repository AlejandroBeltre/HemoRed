using backend.Enums;

namespace backend.DTO;

public class UserDto
{
    public string DocumentNumber { get; set; }
    public DocumentType DocumentType { get; set; }
    public int BloodTypeID { get; set; }
    public int? AddressID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public UserRole UserRole { get; set; }
    public DateOnly? LastDonationDate { get; set; }
    public string? Image { get; set; }
}
public class NewUserDTO
{
    public string DocumentNumber { get; set; }
    public int BloodTypeID { get; set; }
    public int? AddressID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public UserRole UserRole { get; set; }
    public DateOnly? LastDonationDate { get; set; }
    public string? Image { get; set; }
}