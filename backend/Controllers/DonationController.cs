using backend.Context;
using backend.DTO;
using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class DonationController(HemoRedContext _hemoredContext) : Controller
{
// GET: api/BloodBag
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DonationDto>>> GetDonations()
    {
        return await _hemoredContext.TblDonations
            .Select(d => new DonationDto
            {
                DonationID = d.DonationId,
                UserDocument = d.UserDocument,
                BloodTypeID = d.BloodTypeId,
                BloodBankID = d.BloodBankId,
                DonationTimestamp = d.DonationTimestamp,
                Status = d.Status,
            }).ToListAsync();
    }

    // GET: api/Users/get/id
    [HttpGet("{id}")]
    public async Task<ActionResult<DonationDto>> GetDonation(int id)
    {
        var donation = await _hemoredContext.TblDonations.Where(d => d.DonationId == id).Select(d => new DonationDto
            {
                DonationID = d.DonationId,
                UserDocument = d.UserDocument,
                BloodTypeID = d.BloodTypeId,
                BloodBankID = d.BloodBankId,
                DonationTimestamp = d.DonationTimestamp,
                Status = d.Status,
            })
            .FirstOrDefaultAsync();
        if (donation == null)
        {
            return NotFound();
        }

        return donation;
    }

    // POST: api/BloodBank
    [HttpPost]
    public async Task<ActionResult<BloodBankDto>> PostDonation(NewDonationDTO newDonationDto)
    {
        // Fetch related entities
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(newDonationDto.BloodTypeID);
        var userDocument = await _hemoredContext.TblUsers.FindAsync(newDonationDto.UserDocument);
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(newDonationDto.BloodBankID);

        if (bloodType == null || bloodBank == null || userDocument == null)
        {
            return BadRequest("Invalid BloodTypeID, BloodBankID, or UserDocument");
        }

        var donation = new TblDonation
        {
            DonationTimestamp= newDonationDto.DonationTimestamp,
            Status= newDonationDto.Status,
            BloodTypeId = newDonationDto.BloodTypeID,
            UserDocument = newDonationDto.UserDocument,
            BloodBankId = newDonationDto.BloodBankID,
            BloodType = bloodType,
            BloodBank = bloodBank,
            UserDocumentNavigation = userDocument
        };

        _hemoredContext.TblDonations.Add(donation);
        await _hemoredContext.SaveChangesAsync();

        var donationDto = new DonationDto()
        {
            DonationID = donation.DonationId,
            BloodBankID = newDonationDto.BloodBankID,
            BloodTypeID = newDonationDto.BloodTypeID,
            DonationTimestamp = newDonationDto.DonationTimestamp,
            Status = newDonationDto.Status,
            UserDocument = newDonationDto.UserDocument
        };

        return CreatedAtAction("GetDonation", new { id = donation.DonationId }, donationDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDonation(int id, NewDonationDTO newDonationDto)
    {
        var donation = await _hemoredContext.TblDonations.FindAsync(id);
        if (donation == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        donation.UserDocument = newDonationDto.UserDocument;
        donation.DonationTimestamp = newDonationDto.DonationTimestamp;
        donation.BloodTypeId= newDonationDto.BloodTypeID;
        donation.Status= newDonationDto.Status;
        donation.BloodBankId = newDonationDto.BloodBankID;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DonationExist(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonation(int id)
    {
        var donation = await _hemoredContext.TblDonations.FindAsync(id);
        if (donation == null)
        {
            return NotFound();
        }

        _hemoredContext.TblDonations.Remove(donation);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }


    private bool DonationExist(int id)
    {
        return _hemoredContext.TblDonations.Any(e => e.DonationId == id);
    }
}