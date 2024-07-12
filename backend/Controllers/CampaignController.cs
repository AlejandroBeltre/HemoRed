using backend.Context;
using backend.Models;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignController(HemoRedContext _hemoredContext) : ControllerBase
{
    // GET: api/Campaign
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns()
    {
        return await _hemoredContext.TblCampaigns
            .Select(b => new CampaignDto
            {
                AddressID = b.AddressId,
                CampaignID = b.CampaignId,
                CampaignName = b.CampaignName,
                Description = b.Description,
                StartTimestamp = b.StartTimestamp,
                EndTimestamp = b.EndTimestamp,
                Image = b.Image,
                OrganizerID = b.OrganizerId
                
            })
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
    {
        var campaign = await _hemoredContext.TblCampaigns
            .Where(c => c.CampaignId== id)
            .Select(c => new CampaignDto
            {
                AddressID = c.AddressId,
                CampaignID = c.CampaignId,
                CampaignName = c.CampaignName,
                Description = c.Description,
                EndTimestamp = c.EndTimestamp,
                Image = c.Image,
                OrganizerID = c.OrganizerId,
                StartTimestamp = c.StartTimestamp
            })
            .FirstOrDefaultAsync();

        if (campaign == null)
        {
            return NotFound();
        }

        return campaign;
    }
    [HttpPost]
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
        
        var address = await _hemoredContext.TblAddresses.FindAsync(newCampaignDto.AddressID);
        var organizer = await _hemoredContext.TblOrganizers.FindAsync(newCampaignDto.OrganizerID);
        var campaign = new TblCampaign
        {
             AddressId= newCampaignDto.AddressID,
            CampaignName = newCampaignDto.CampaignName,
            Description= newCampaignDto.Description,
            EndTimestamp= newCampaignDto.EndTimestamp,
            Image= imagePath,
            Address = address,
            Organizer = organizer,
            OrganizerId= newCampaignDto.OrganizerID,
            StartTimestamp = newCampaignDto.StartTimestamp,
        };

        _hemoredContext.TblCampaigns.Add(campaign);
        await _hemoredContext.SaveChangesAsync();

        var createdCampaignDto= new CampaignDto()
        {
            AddressID = campaign.AddressId,
            CampaignName = campaign.CampaignName,
            Description= campaign.Description,
            Image = campaign.Image,
            StartTimestamp = campaign.StartTimestamp,
            EndTimestamp = campaign.EndTimestamp,
            OrganizerID = campaign.OrganizerId
        };

        return CreatedAtAction("GetCampaign", new { id = campaign.CampaignId}, createdCampaignDto);
    }
// PUT: api/BloodBank/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCampaign(int id, [FromForm]CampaignDto newCampaignDto, [FromForm] IFormFile? image)
    {
        var campaign = await _hemoredContext.TblCampaigns.FindAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }

        string? imagePath = campaign.Image; // Keep the existing image path if no new image is uploaded
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

        campaign.AddressId = newCampaignDto.AddressID;
        campaign.CampaignName = newCampaignDto.CampaignName;
        campaign.StartTimestamp =  newCampaignDto.StartTimestamp;
        campaign.EndTimestamp=  newCampaignDto.EndTimestamp;
        campaign.Description= newCampaignDto.Description;
        campaign.OrganizerId =  newCampaignDto.OrganizerID;
        campaign.Image= imagePath;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CampaignExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }
    // DELETE: api/BloodBank/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaign = await _hemoredContext.TblCampaigns.FindAsync(id);
        if (campaign == null)
        {
            return NotFound();
        }

        _hemoredContext.TblCampaigns.Remove(campaign);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }
    

    [HttpPost("userPost")]
    public async Task<IActionResult> AddUserToCampaign(UserCampaignDto userCampaignDto)
    {
        var user = await _hemoredContext.TblUsers.FindAsync(userCampaignDto.UserDocument);
        var campaign = await _hemoredContext.TblCampaigns.FindAsync(userCampaignDto.CampaignID);

        if (user == null || campaign == null)
        {
            return BadRequest("Invalid UserDocument or CampaignId");
        }

        var userCampaign = new Dictionary<string, object>
        {
            { "UserDocument", userCampaignDto.UserDocument },
            { "CampaignId", userCampaignDto.CampaignID }
        };

        _hemoredContext.Set<Dictionary<string, object>>("TblUserCampaign").Add(userCampaign);
        await _hemoredContext.SaveChangesAsync();

        return Ok(new { userCampaignDto.UserDocument, userCampaignDto.CampaignID});
    }
    
    [HttpGet("userCampaigns")]
    public async Task<ActionResult<IEnumerable<UserCampaignDto>>> GetUserCampaigns()
    {
        var userCampaigns = await _hemoredContext.TblUsers
            .SelectMany(user => user.Campaigns, (user, campaign) => new UserCampaignDto
            {
                UserDocument = user.DocumentNumber,
                CampaignID = campaign.CampaignId
            })
            .ToListAsync();

        return Ok(userCampaigns);
    }
    
    [HttpGet("userCampaign/{campaignId}")]
    public async Task<ActionResult<IEnumerable<UserCampaignDto>>> GetUserCampaignsByCampaign(int campaignId)
    {
        var campaign = await _hemoredContext.TblCampaigns
            .Include(c => c.UserDocuments)
            .FirstOrDefaultAsync(c => c.CampaignId == campaignId);

        if (campaign == null)
        {
            return NotFound("Campaign not found.");
        }

        var userCampaigns = campaign.UserDocuments
            .Select(user => new UserCampaignDto
            {
                UserDocument = user.DocumentNumber,
                CampaignID = campaign.CampaignId
            })
            .ToList();

        return Ok(userCampaigns);
    }
    
    private bool CampaignExists(int id)
    {
        return _hemoredContext.TblCampaigns.Any(e => e.CampaignId == id);
    }

}