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
    private const string IMAGE_SERVER_URL = "http://localhost:8080/";
    // GET: api/BloodBank
    [HttpGet]
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
    [HttpPost]
    public async Task<ActionResult<BloodBankDto>> PostBloodBank([FromForm] NewBloodBankDTO newBloodBankDto)
    {
        string imageUrl = null;
        if (newBloodBankDto.Image != null && newBloodBankDto.Image.Length > 0)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newBloodBankDto.Image.FileName);
            var fullPath = Path.Combine(uploadsFolderPath, fileName);

            imageUrl = IMAGE_SERVER_URL + fileName;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await newBloodBankDto.Image.CopyToAsync(stream);
            }
        }

        var bloodBank = new TblBloodBank
        {
            AddressId = newBloodBankDto.AddressID,
            BloodBankName = newBloodBankDto.BloodBankName,
            AvailableHours = newBloodBankDto.AvailableHours,
            Phone = newBloodBankDto.Phone,
            Image = imageUrl  // Store the full URL
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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBloodBank(int id, [FromForm] NewBloodBankDTO newBloodBankDto)
    {
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(id);
        if (bloodBank == null)
        {
            return NotFound();
        }

        if (newBloodBankDto.Image != null && newBloodBankDto.Image.Length > 0)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newBloodBankDto.Image.FileName);
            var fullPath = Path.Combine(uploadsFolderPath, fileName);
            var newImageUrl = IMAGE_SERVER_URL + fileName;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await newBloodBankDto.Image.CopyToAsync(stream);
            }

            // Delete the old image if it exists
            if (!string.IsNullOrEmpty(bloodBank.Image))
            {
                try
                {
                    var oldFileName = Path.GetFileName(new Uri(bloodBank.Image).LocalPath);
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

            bloodBank.Image = newImageUrl;
        }

        // Update other properties
        bloodBank.AddressId = newBloodBankDto.AddressID;
        bloodBank.BloodBankName = newBloodBankDto.BloodBankName;
        bloodBank.AvailableHours = newBloodBankDto.AvailableHours;
        bloodBank.Phone = newBloodBankDto.Phone;

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
            else
            {
                throw;
            }
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