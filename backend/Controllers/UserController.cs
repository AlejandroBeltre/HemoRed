using backend.Context;
using backend.DTO;
using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(HemoRedContext _hemoredContext) : ControllerBase
{

    // GET: api/Users
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await _hemoredContext.TblUsers
            .Select(u => new UserDto
            {
                DocumentNumber = u.DocumentNumber,
                DocumentType= u.DocumentType,
                BloodTypeID= u.BloodTypeId,
                AddressID= u.AddressId,
                FullName= u.FullName,
                Email= u.Email,
                Password = u.Password,
                BirthDate = u.BirthDate,
                Gender = u.Gender,
                Phone = u.Phone,
                UserRole = u.UserRole,
                LastDonationDate = u.LastDonationDate,
                Image = u.Image
            })
            .ToListAsync();
    }
    
    
    // GET: api/Users/get/id
    [HttpGet("get/id")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _hemoredContext.TblUsers.Where(u => u.DocumentNumber == id).Select(u => new UserDto
            {
                DocumentNumber = u.DocumentNumber,
                DocumentType= u.DocumentType,
                BloodTypeID= u.BloodTypeId,
                AddressID= u.AddressId,
                FullName= u.FullName,
                Email= u.Email,
                Password = u.Password,
                BirthDate = u.BirthDate,
                Gender = u.Gender,
                Phone = u.Phone,
                UserRole = u.UserRole,
                LastDonationDate = u.LastDonationDate,
                Image = u.Image
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
        var address = await _hemoredContext.TblAddresses.FindAsync(newUserDto.AddressID);
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(newUserDto.BloodTypeID);
        
        if (bloodType == null || address == null)
        {
            return BadRequest("Invalid BloodTypeID, address, or DonationID");
        }
        var user = new TblUser
        {
            DocumentNumber= newUserDto.DocumentNumber,
            BloodTypeId = newUserDto.BloodTypeID,
            AddressId= newUserDto.AddressID,
            FullName= newUserDto.FullName,
            Email= newUserDto.Email,
            Password = newUserDto.Password,
            BirthDate= newUserDto.BirthDate,
            Gender= newUserDto.Gender,
            Phone= newUserDto.Phone,
            UserRole = newUserDto.UserRole,
            LastDonationDate = newUserDto.LastDonationDate,
            Image= newUserDto.Image,
            DocumentType = DocumentType.C,
            BloodType= bloodType,
            Address = address
        };

        _hemoredContext.TblUsers.Add(user);
        await _hemoredContext.SaveChangesAsync();

        var createdUserDto= new UserDto()
        {
            BloodTypeID= user.BloodTypeId,
            AddressID= user.AddressId,
            FullName= user.FullName,
            Email= user.Email,
            Password = user.Password,
            BirthDate = user.BirthDate,
            Gender = user.Gender,
            Phone = user.Phone,
            UserRole = user.UserRole,
            LastDonationDate = user.LastDonationDate,
            Image = user.Image
        };

        return CreatedAtAction("GetUsers", new { id = user.DocumentNumber }, createdUserDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, NewUserDTO newUserDto)
    {
        var user = await _hemoredContext.TblUsers.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        user.BloodTypeId = newUserDto.BloodTypeID;
        user.AddressId= newUserDto.AddressID;
        user.FullName= newUserDto.FullName;
        user.Email= newUserDto.Email;
        user.Password = newUserDto.Password;
        user.BirthDate = newUserDto.BirthDate;
        user.Gender= newUserDto.Gender;
        user.Phone= newUserDto.Phone;
        user.UserRole= newUserDto.UserRole;
        user.LastDonationDate = newUserDto.LastDonationDate;
        user.Image = newUserDto.Image;

        try
        {
            await _hemoredContext.SaveChangesAsync();
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
        var user = await _hemoredContext.TblUsers.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _hemoredContext.TblUsers.Remove(user);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UserExists(string id)
    {
        return _hemoredContext.TblUsers.Any(e => e.DocumentNumber == id);
    }
}