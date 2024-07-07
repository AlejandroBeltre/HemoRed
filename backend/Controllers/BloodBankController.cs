using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.Models;
using backend.DTO;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodBankController(HemoRedContext _hemoredContext) : ControllerBase
{
    // GET: api/BloodBank
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<BloodBankDto>>> GetBloodBanks()
    {
        return await _hemoredContext.TblBloodBanks
            .Select(b => new BloodBankDto
            {
                BloodBankID = b.BloodBankId,
                AddressID = b.AddressId,
                BloodBankName = b.BloodBankName,
                AvailableHours = b.AvailableHours,
                Phone = b.Phone,
                Image = b.Image
            })
            .ToListAsync();
    }

    // GET: api/BloodBank/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BloodBankDto>> GetBloodBank(int id)
    {
        var bloodBank = await _hemoredContext.TblBloodBanks
            .Where(b => b.BloodBankId == id)
            .Select(b => new BloodBankDto
            {
                BloodBankID = b.BloodBankId,
                AddressID = b.AddressId,
                BloodBankName = b.BloodBankName,
                AvailableHours = b.AvailableHours,
                Phone = b.Phone,
                Image = b.Image
            })
            .FirstOrDefaultAsync();

        if (bloodBank == null)
        {
            return NotFound();
        }

        return bloodBank;
    }

    // POST: api/BloodBank
    [HttpPost("post")]
    public async Task<ActionResult<BloodBankDto>> PostBloodBank([FromForm]NewBloodBankDTO newBloodBankDto, [FromForm]IFormFile? image)
    {
        string imagePath = null!;
        if (image != null && image.Length > 0)
        {
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if(!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            imagePath = Path.Combine(uploadFolderPath, fileName);
            await using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);
        }
        var address = await _hemoredContext.TblAddresses.FindAsync(newBloodBankDto.AddressID);
        var bloodBank = new TblBloodBank
        {
            AddressId= newBloodBankDto.AddressID,
            BloodBankName = newBloodBankDto.BloodBankName,
            AvailableHours = newBloodBankDto.AvailableHours,
            Phone= newBloodBankDto.Phone,
            Image= imagePath,
            Address = address
        };

        _hemoredContext.TblBloodBanks.Add(bloodBank);
        await _hemoredContext.SaveChangesAsync();

        var createdBankDto = new BloodBankDto
        {
            BloodBankID = bloodBank.BloodBankId,
            AddressID = bloodBank.AddressId,
            BloodBankName = bloodBank.BloodBankName,
            AvailableHours = bloodBank.AvailableHours,
            Phone = bloodBank.Phone,
            Image = bloodBank.Image
        };

        return CreatedAtAction("GetBloodBank", new { id = bloodBank.BloodBankId }, createdBankDto);
    }

// PUT: api/BloodBank/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBloodBank(int id, [FromForm] NewBloodBankDTO newBloodBankDto, [FromForm] IFormFile? image)
    {
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(id);
        if (bloodBank == null)
        {
            return NotFound();
        }

        string? imagePath = bloodBank.Image; // Keep the existing image path if no new image is uploaded
        if (image != null && image.Length > 0)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            imagePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }

        bloodBank.AddressId = newBloodBankDto.AddressID;
        bloodBank.BloodBankName = newBloodBankDto.BloodBankName;
        bloodBank.AvailableHours= newBloodBankDto.AvailableHours;
        bloodBank.Phone= newBloodBankDto.Phone;
        bloodBank.Image = imagePath;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BloodBankExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/BloodBank/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBloodBank(int id)
    {
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(id);
        if (bloodBank == null)
        {
            return NotFound();
        }

        _hemoredContext.TblBloodBanks.Remove(bloodBank);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }

    private bool BloodBankExists(int id)
    {
        return _hemoredContext.TblBloodBanks.Any(e => e.BloodBankId == id);
    }
};