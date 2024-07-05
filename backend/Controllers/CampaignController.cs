using backend.Context;
using backend.Models;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignController(ApplicationContext applicationContext) : Controller
{
    // GET: api/Campaign
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns()
    {
        return await applicationContext.tblCampaign
            .Select(b => new CampaignDto
            {
                AddressID = b.addressID,
                CampaignID = b.campaignID,
                CampaignName = b.campaignName,
                Description = b.description,
                EndTimestamp = b.endTimestamp,
                Image = b.image,
                OrganizerID = b.organizerID,
                StartTimestamp = b.startTimestamp
            })
            .ToListAsync();
    }
    
    [HttpGet("get/{id}")]
    public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
    {
        var campaign = await applicationContext.tblCampaign
            .Where(c => c.campaignID== id)
            .Select(c => new CampaignDto
            {
                AddressID = c.addressID,
                CampaignID = c.campaignID,
                CampaignName = c.campaignName,
                Description = c.description,
                EndTimestamp = c.endTimestamp,
                Image = c.image,
                OrganizerID = c.organizerID,
                StartTimestamp = c.startTimestamp
            })
            .FirstOrDefaultAsync();

        if (campaign == null)
        {
            return NotFound();
        }

        return campaign;
    }
    [HttpPost("post")]
    public async Task<ActionResult<CampaignDto>> PostCampaign([FromForm]NewCampaignDto newCampaignDto, [FromForm]IFormFile? image)
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
        
        var address = await applicationContext.tblAddress.FindAsync(newCampaignDto.AddressID);
        var organizer = await applicationContext.tblOrganizer.FindAsync(newCampaignDto.OrganizerID);
        var campaign = new tblCampaign()
        {
            addressID = newCampaignDto.AddressID,
            campaignName = newCampaignDto.CampaignName,
            description = newCampaignDto.Description,
            endTimestamp = newCampaignDto.EndTimestamp,
            image = imagePath,
            tblAddress = address,
            tblOrganizer = organizer,
            organizerID = newCampaignDto.OrganizerID,
            startTimestamp = newCampaignDto.StartTimestamp
        };

        applicationContext.tblCampaign.Add(campaign);
        await applicationContext.SaveChangesAsync();

        var createdCampaignDto= new CampaignDto()
        {
            AddressID = campaign.addressID,
            CampaignName = campaign.campaignName,
            Description= campaign.description,
            Image = campaign.image,
            StartTimestamp = campaign.startTimestamp,
            EndTimestamp = campaign.endTimestamp,
            OrganizerID = campaign.organizerID
        };

        return CreatedAtAction("GetCampaign", new { id = campaign.campaignID }, createdCampaignDto);
    }
// PUT: api/BloodBank/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCampaign(int id, [FromForm]CampaignDto newCampaignDto, [FromForm] IFormFile? image)
    {
        var campaign = await applicationContext.tblCampaign.FindAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }

        string? imagePath = campaign.image; // Keep the existing image path if no new image is uploaded
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

        campaign.addressID = newCampaignDto.AddressID;
        campaign.campaignName = newCampaignDto.CampaignName;
        campaign.startTimestamp =  newCampaignDto.StartTimestamp;
        campaign.endTimestamp =  newCampaignDto.EndTimestamp;
        campaign.description = newCampaignDto.Description;
        campaign.organizerID =  newCampaignDto.OrganizerID;
        campaign.image = imagePath;

        try
        {
            await applicationContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CampaignExists(id))
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
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaign = await applicationContext.tblCampaign.FindAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }

        applicationContext.tblCampaign.Remove(campaign);
        await applicationContext.SaveChangesAsync();

        return NoContent();
    }
    
    private bool CampaignExists(int id)
    {
        return applicationContext.tblCampaign.Any(e => e.campaignID == id);
    }

}