using backend.Context;
using backend.DTO;
using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.classes;
using backend.endpoints;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(HemoRedContext _hemoredContext, IOptions<JwtOptions> jwtOptions)
    : ControllerBase
{
    private const string IMAGE_SERVER_URL = "http://localhost:8080/";
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await _hemoredContext.TblUsers
            .Select(u => new UserDto
            {
                DocumentNumber = u.DocumentNumber,
                DocumentType = u.DocumentType,
                BloodTypeID = u.BloodTypeId,
                AddressID = u.AddressId,
                FullName = u.FullName,
                Email = u.Email,
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


    // GET: api/Users/id
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _hemoredContext.TblUsers.Where(u => u.DocumentNumber == id).Select(u => new UserDto
            {
                DocumentNumber = u.DocumentNumber,
                DocumentType = u.DocumentType,
                BloodTypeID = u.BloodTypeId,
                AddressID = u.AddressId,
                FullName = u.FullName,
                Email = u.Email,
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
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterUserDTO registerUserDto)
    {
        if (await _hemoredContext.TblUsers.AnyAsync(u => u.DocumentNumber == registerUserDto.DocumentNumber))
        {
            return BadRequest("User already exists");
        }

        string imageUrl = null;
        if (registerUserDto.Image != null && registerUserDto.Image.Length > 0)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(registerUserDto.Image.FileName);
            var fullPath = Path.Combine(uploadsFolderPath, fileName);

            imageUrl = IMAGE_SERVER_URL + fileName;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await registerUserDto.Image.CopyToAsync(stream);
            }
        }

        var userToCreate = new TblUser
        {
            DocumentNumber = registerUserDto.DocumentNumber,
            DocumentType = registerUserDto.DocumentType,
            BloodTypeId = registerUserDto.BloodTypeID,
            AddressId = registerUserDto.AddressID,
            FullName = registerUserDto.FullName,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password,
            BirthDate = registerUserDto.BirthDate,
            Gender = registerUserDto.Gender,
            Phone = registerUserDto.Phone,
            UserRole = registerUserDto.UserRole,
            LastDonationDate = registerUserDto.LastDonationDate,
            Image = imageUrl
        };

        _hemoredContext.TblUsers.Add(userToCreate);

        try
        {
            await _hemoredContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = userToCreate.DocumentNumber }, userToCreate);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while registering the user.");
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
    {
        var userFromDB = await _hemoredContext.TblUsers.FirstOrDefaultAsync(u =>
            u.Email == loginUserDTO.Email && u.Password == loginUserDTO.Password);
        if (userFromDB == null)
        {
            return Unauthorized("Invalid credentials");
        }

        if (string.IsNullOrEmpty(_jwtOptions.SigningKey))
        {
            return StatusCode(500, "JWT key is not configured.");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userFromDB.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userFromDB.UserRole.ToString())
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(new { userId = userFromDB.DocumentNumber, userFromDB.Email, userFromDB.UserRole });
    }

    [HttpPut("{documentNumber}")]
    public async Task<IActionResult> UpdateUser(string documentNumber, UpdateUserDTO updateUser)
    {
        var user = await _hemoredContext.TblUsers.FindAsync(documentNumber);
        if (user == null)
        {
            return NotFound();
        }

        if (updateUser.Image != null && updateUser.Image.Length > 0)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateUser.Image.FileName);
            var fullPath = Path.Combine(uploadsFolderPath, fileName);
            var newImageUrl = IMAGE_SERVER_URL + fileName;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await updateUser.Image.CopyToAsync(stream);
            }

            // Delete the old image if it exists
            if (!string.IsNullOrEmpty(user.Image))
            {
                try
                {
                    var oldFileName = Path.GetFileName(new Uri(user.Image).LocalPath);
                    var oldImagePath = Path.Combine(uploadsFolderPath, oldFileName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                catch (UriFormatException)
                {
                    // If the old image URL is invalid, just ignore it and continue
                    // You might want to log this situation
                }
            }

            user.Image = newImageUrl;
        }

        // Update other properties
        user.Email = updateUser.Email;
        user.Phone = updateUser.Phone;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(documentNumber))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    private bool UserExists(string id)
    {
        return _hemoredContext.TblUsers.Any(e => e.DocumentNumber == id);
    }
}
