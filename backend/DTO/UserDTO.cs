using backend.Enums;

namespace backend.DTO;

public class UserDto
{
    public string DocumentNumber { get; set; }
    public DocumentType? DocumentType { get; set; }
    public int BloodTypeID { get; set; }
    public int? AddressID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? Phone { get; set; }  
    public UserRole? UserRole { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public string? Image { get; set; }
}
public class RegisterUserDTO
{
    public string DocumentNumber { get; set; }
    public DocumentType? DocumentType { get; set; }
    public int BloodTypeID { get; set; }
    public int? AddressID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public UserRole UserRole { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public string? Image { get; set; }
}

public class LoginUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateUserDTO {
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
}