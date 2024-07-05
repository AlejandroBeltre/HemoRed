using backend.Context;
using backend.DTO;
using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class DonationController(ApplicationContext applicationContext) : Controller
{
// GET: api/BloodBag
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<DonationDto>>> GetDonations()
    {
        return await applicationContext.tblDonation
            .Select(d => new DonationDto
            {
                DonationID = d.donationID,
                UserDocument = d.userDocument,
                BloodTypeID = d.bloodTypeID,
                BloodBankID = d.bloodBankID,
                DonationTimestamp = d.donationTimestamp,
                Status = d.status,
            }).ToListAsync();
    }

    // GET: api/Users/get/id
    [HttpGet("get/id")]
    public async Task<ActionResult<DonationDto>> GetDonation(int id)
    {
        var donation = await applicationContext.tblDonation.Where(d => d.donationID == id).Select(d => new DonationDto
            {
                DonationID = d.donationID,
                UserDocument = d.userDocument,
                BloodTypeID = d.bloodTypeID,
                BloodBankID = d.bloodBankID,
                DonationTimestamp = d.donationTimestamp,
                Status = d.status,
            })
            .FirstOrDefaultAsync();
        if (donation == null)
        {
            return NotFound();
        }

        return donation;
    }

    // POST: api/BloodBank
    [HttpPost("post")]
    public async Task<ActionResult<BloodBankDto>> PostDonation([FromForm] NewDonationDTO newDonationDto)
    {
        // Fetch related entities
        var bloodType = await applicationContext.tblBloodType.FindAsync(newDonationDto.BloodTypeID);
        var userDocument = await applicationContext.tblUser.FindAsync(newDonationDto.UserDocument);
        var bloodBank = await applicationContext.tblBloodBank.FindAsync(newDonationDto.BloodBankID);

        if (bloodType == null || bloodBank == null || userDocument == null)
        {
            return BadRequest("Invalid BloodTypeID, BloodBankID, or UserDocument");
        }

        var donation = new tblDonation
        {
            donationTimestamp = newDonationDto.DonationTimestamp,
            status = newDonationDto.Status,
            bloodTypeID = newDonationDto.BloodTypeID,
            userDocument = newDonationDto.UserDocument,
            bloodBankID = newDonationDto.BloodBankID,
            tblBloodType = bloodType,
            tblBloodBank = bloodBank,
            tblUser = userDocument
        };

        applicationContext.tblDonation.Add(donation);
        await applicationContext.SaveChangesAsync();

        var donationDto = new DonationDto()
        {
            DonationID = donation.donationID,
            BloodBankID = newDonationDto.BloodBankID,
            BloodTypeID = newDonationDto.BloodTypeID,
            DonationTimestamp = newDonationDto.DonationTimestamp,
            Status = newDonationDto.Status,
            UserDocument = newDonationDto.UserDocument
        };

        return CreatedAtAction("GetDonation", new { id = donation.donationID }, donationDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDonation(int id, DonationDto newDonationDto)
    {
        var donation = await applicationContext.tblDonation.FindAsync(id);
        if (donation == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        donation.userDocument = newDonationDto.UserDocument;
        donation.donationTimestamp = newDonationDto.DonationTimestamp;
        donation.bloodTypeID = newDonationDto.BloodTypeID;
        donation.status = newDonationDto.Status;
        donation.bloodBankID = newDonationDto.BloodBankID;

        try
        {
            await applicationContext.SaveChangesAsync();
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
        var donation = await applicationContext.tblDonation.FindAsync(id);
        if (donation == null)
        {
            return NotFound();
        }

        applicationContext.tblDonation.Remove(donation);
        await applicationContext.SaveChangesAsync();

        return NoContent();
    }


    private bool DonationExist(int id)
    {
        return applicationContext.tblDonation.Any(e => e.donationID == id);
    }
}