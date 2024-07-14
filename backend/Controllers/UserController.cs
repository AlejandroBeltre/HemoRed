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
public class UserController : ControllerBase
{
    private readonly HemoRedContext _hemoredContext;
    private readonly JwtOptions _jwtOptions;

    public UserController(HemoRedContext hemoredContext, IOptions<JwtOptions> jwtOptions)
    {
        _hemoredContext = hemoredContext;
        _jwtOptions = jwtOptions.Value;
    }
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
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterUserDTO registerUserDTO, [FromForm] IFormFile? image)
    {
        if (await _hemoredContext.TblUsers.AnyAsync(u => u.DocumentNumber == registerUserDTO.DocumentNumber))
        {
            return BadRequest("User already exists");
        }

        string? imagePath = null;
        if (image != null && image.Length > 0)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            } 
            
            imagePath = fileName;
        }

        var userToCreate = new TblUser
        {
            DocumentNumber = registerUserDTO.DocumentNumber,
            DocumentType = registerUserDTO.DocumentType,
            BloodTypeId = registerUserDTO.BloodTypeID,
            AddressId = registerUserDTO.AddressID,
            FullName = registerUserDTO.FullName,
            Email = registerUserDTO.Email,
            Password = registerUserDTO.Password,
            BirthDate = registerUserDTO.BirthDate,
            Gender = registerUserDTO.Gender,
            Phone = registerUserDTO.Phone,
            UserRole = registerUserDTO.UserRole,
            LastDonationDate = registerUserDTO.LastDonationDate,
            Image = imagePath
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
        var userFromDB = await _hemoredContext.TblUsers.FirstOrDefaultAsync(u => u.Email == loginUserDTO.Email && u.Password == loginUserDTO.Password);
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

        return Ok(new {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            role = userFromDB.UserRole
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserDTO updateUserDTO, [FromForm] IFormFile? image)
    {
        var user = await _hemoredContext.TblUsers.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (updateUserDTO.Email != null)
        {
            user.Email = updateUserDTO.Email;
        }
        if (updateUserDTO.Phone != null)
        {
            user.Phone = updateUserDTO.Phone;
        }

        if (image != null && image.Length > 0)
        {
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
        }

        await _hemoredContext.SaveChangesAsync();
        return NoContent();
    }
}