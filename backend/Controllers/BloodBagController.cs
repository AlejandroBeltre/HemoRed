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
public class BloodBagController(HemoRedContext _hemoredContext) : ControllerBase
{
    // GET: api/BloodBag
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BloodBagDto>>> GetBloodBags()
    {
        return await _hemoredContext.TblBloodBags
            .Select(b => new BloodBagDto
            {
                BagID = b.BagId,
                BloodTypeID = b.BloodTypeId,
                BloodBankID = b.BloodBankId,
                DonationID = b.DonationId,
                ExpirationDate = b.ExpirationDate,
                IsReserved = b.IsReserved
            })
            .ToListAsync();
    }

    // GET: api/BloodBag/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BloodBagDto>> GetBloodBag(int id)
    {
        var bloodBag = await _hemoredContext.TblBloodBags
            .Where(b => b.BagId== id)
            .Select(b => new BloodBagDto
            {
                BagID = b.BagId,
                BloodTypeID = b.BloodTypeId,
                BloodBankID = b.BloodBankId,
                DonationID = b.DonationId,
                ExpirationDate = b.ExpirationDate,
                IsReserved = b.IsReserved
            })
            .FirstOrDefaultAsync();

        if (bloodBag == null)
        {
            return NotFound();
        }

        return bloodBag;
    }

    // PUT: api/BloodBag/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBloodBag(int id, NewBloodBagDTO newBloodBagDto)
    {
        var bloodBag = await _hemoredContext.TblBloodBags
            .Where(b => b.BagId == id)
            .FirstOrDefaultAsync();
        if (bloodBag == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        bloodBag.BloodTypeId = newBloodBagDto.BloodTypeID;
        bloodBag.BloodBankId = newBloodBagDto.BloodBankID;
        bloodBag.DonationId = newBloodBagDto.DonationID;
        bloodBag.ExpirationDate = newBloodBagDto.ExpirationDate;
        bloodBag.IsReserved = newBloodBagDto.IsReserved;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BloodBagExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // POST: api/BloodBag
    [HttpPost]
    public async Task<ActionResult<BloodBagDto>> PostBloodBag(NewBloodBagDTO newBloodBagDto)
    {
        // Fetch related entities
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(newBloodBagDto.BloodTypeID);
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(newBloodBagDto.BloodBankID);
        var donation = await _hemoredContext.TblDonations.FindAsync(newBloodBagDto.DonationID);

        if (bloodType == null || bloodBank == null || donation == null)
        {
            return BadRequest("Invalid BloodTypeID, BloodBankID, or DonationID");
        }

        var bloodBag = new TblBloodBag
        {
            BloodTypeId = newBloodBagDto.BloodTypeID,
            BloodBankId= newBloodBagDto.BloodBankID,
            DonationId= newBloodBagDto.DonationID,
            ExpirationDate = newBloodBagDto.ExpirationDate,
            IsReserved = newBloodBagDto.IsReserved,
            BloodType = bloodType,
            BloodBank = bloodBank,
            Donation= donation
        };

        _hemoredContext.TblBloodBags.Add(bloodBag);
        await _hemoredContext.SaveChangesAsync();

        var createdBagDto = new BloodBagDto
        {
            BagID = bloodBag.BagId,
            BloodTypeID = bloodBag.BloodTypeId,
            BloodBankID = bloodBag.BloodBankId,
            DonationID = bloodBag.DonationId,
            ExpirationDate = bloodBag.ExpirationDate,
            IsReserved = bloodBag.IsReserved
        };

        return CreatedAtAction("GetBloodBag", new { id = bloodBag.BagId }, createdBagDto);
    }

    // DELETE: api/BloodBag/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBloodBag(int id)
    {
        var bloodBag = await _hemoredContext.TblBloodBags
            .Where(b => b.BagId == id)
            .FirstOrDefaultAsync();
        if (bloodBag == null)
        {
            return NotFound();
        }

        _hemoredContext.TblBloodBags.Remove(bloodBag);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }
    private bool BloodBagExists(int id)
    {
        return _hemoredContext.TblBloodBags.Any(e => e.BagId == id);
    }
}