using backend.Context;
using backend.DTO;
using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ApplicationContext applicationContext) : ControllerBase
{

    // GET: api/Users
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await applicationContext.tblUser
            .Select(u => new UserDto
            {
                DocumentNumber = u.documentNumber,
                DocumentType= u.documentType,
                BloodTypeID= u.bloodTypeID,
                AddressID= u.addressID,
                FullName= u.fullName,
                Email= u.email,
                Password = u.password,
                BirthDate = u.birthDate,
                Gender = u.gender,
                Phone = u.phone,
                UserRole = u.userRole,
                LastDonationDate = u.lastDonationDate,
                Image = u.image
            })
            .ToListAsync();
    }
    
    
    // GET: api/Users/get/id
    [HttpGet("get/id")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await applicationContext.tblUser.Where(u => u.documentNumber == id).Select(u => new UserDto
            {
                DocumentNumber = u.documentNumber,
                DocumentType= u.documentType,
                BloodTypeID= u.bloodTypeID,
                AddressID= u.addressID,
                FullName= u.fullName,
                Email= u.email,
                Password = u.password,
                BirthDate = u.birthDate,
                Gender = u.gender,
                Phone = u.phone,
                UserRole = u.userRole,
                LastDonationDate = u.lastDonationDate,
                Image = u.image
            })
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
    // POST: api/BloodBank
    [HttpPost("post")]
    public async Task<ActionResult<BloodBankDto>> PostUser([FromForm]NewUserDTO newUserDto, [FromForm]IFormFile? image)
    {
        if (image != null && image.Length > 0)
        {
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if(!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(uploadFolderPath, fileName);
            await using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);
        }
        var address = await applicationContext.tblAddress.FindAsync(newUserDto.AddressID);
        var bloodType = await applicationContext.tblBloodType.FindAsync(newUserDto.BloodTypeID);
        
        if (bloodType == null || address == null)
        {
            return BadRequest("Invalid BloodTypeID, address, or DonationID");
        }
        var user = new tblUser
        {
            documentNumber = newUserDto.DocumentNumber,
            bloodTypeID = newUserDto.BloodTypeID,
            addressID = newUserDto.AddressID,
            fullName = newUserDto.FullName,
            email = newUserDto.Email,
            password = newUserDto.Password,
            birthDate = newUserDto.BirthDate,
            gender = newUserDto.Gender,
            phone = newUserDto.Phone,
            userRole = newUserDto.UserRole,
            lastDonationDate = newUserDto.LastDonationDate,
            image = newUserDto.Image,
            documentType = DocumentType.P,
            tblBloodType = bloodType,
            tblAddress = address
        };

        applicationContext.tblUser.Add(user);
        await applicationContext.SaveChangesAsync();

        var createdUserDto= new UserDto()
        {
            BloodTypeID= user.bloodTypeID,
            AddressID= user.addressID,
            FullName= user.fullName,
            Email= user.email,
            Password = user.password,
            BirthDate = user.birthDate,
            Gender = user.gender,
            Phone = user.phone,
            UserRole = user.userRole,
            LastDonationDate = user.lastDonationDate,
            Image = user.image
        };

        return CreatedAtAction("GetUsers", new { id = user.documentNumber }, createdUserDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, NewUserDTO newUserDto)
    {
        var user = await applicationContext.tblUser.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        user.bloodTypeID = newUserDto.BloodTypeID;
        user.addressID = newUserDto.AddressID;
        user.fullName = newUserDto.FullName;
        user.email = newUserDto.Email;
        user.password = newUserDto.Password;
        user.birthDate = newUserDto.BirthDate;
        user.gender = newUserDto.Gender;
        user.phone = newUserDto.Phone;
        user.userRole= newUserDto.UserRole;
        user.lastDonationDate = newUserDto.LastDonationDate;
        user.image = newUserDto.Image;

        try
        {
            await applicationContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await applicationContext.tblUser.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        applicationContext.tblUser.Remove(user);
        await applicationContext.SaveChangesAsync();

        return NoContent();
    }


    private bool UserExists(string id)
    {
        return applicationContext.tblUser.Any(e => e.documentNumber == id);
    }
}