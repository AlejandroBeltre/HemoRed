using backend.Context;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class OrganizerController(ApplicationContext applicationContext) : Controller
{
    
    // GET: api/Users
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<OrganizerDto>>> GetOrganizers()
    {
        return await applicationContext.tblOrganizer
            .Select(o => new OrganizerDto
            {
                OrganizerID = o.organizerID,
                AddressID = o.addressID,
                OrganizerName = o.organizerName,
                Email = o.email,
                Phone = o.phone
            }).ToListAsync();
    }
    
    [HttpGet("get/id")]
    public async Task<ActionResult<OrganizerDto>> GetOrganizer(int id)
    {
        var organizer = await applicationContext.tblOrganizer.Where(o => o.organizerID == id).Select(o => new OrganizerDto
        {
                OrganizerID = o.organizerID,
                AddressID = o.addressID,
                OrganizerName = o.organizerName,
                Email = o.email,
                Phone = o.phone
            }).FirstOrDefaultAsync();
        if (organizer == null)
        {
            return NotFound();
        }

        return organizer;
    }
    
    // POST: api/BloodBank
    [HttpPost("post")]
    public async Task<ActionResult<OrganizerDto>> PostUser([FromForm]NewOrganizerDTO newOrganizerDto)
    {
        var organizer = new tblOrganizer
        {
            organizerName = newOrganizerDto.OrganizerName,
            email = newOrganizerDto.Email,
            phone = newOrganizerDto.Phone,
            addressID = newOrganizerDto.AddressID
        };

        applicationContext.tblOrganizer.Add(organizer);
        await applicationContext.SaveChangesAsync();

        var createOrganizerDto = new OrganizerDto
        {
            OrganizerID= organizer.organizerID,
            AddressID= organizer.addressID,
            Email = organizer.email,
            OrganizerName = organizer.organizerName,
            Phone = organizer.phone
        };

        return CreatedAtAction("GetOrganizer", new { id = organizer.organizerID}, createOrganizerDto);
    }
    
    // PUT: api/BloodBank/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrganizer(int id, [FromForm]NewOrganizerDTO newOrganizerDto)
    {
        var organizer = await applicationContext.tblOrganizer.FindAsync(id);
        if (organizer == null)
        {
            return NotFound();
        }
        
        organizer.addressID = newOrganizerDto.AddressID;
        organizer.organizerName= newOrganizerDto.OrganizerName;
        organizer.phone = newOrganizerDto.Phone;
        organizer.email = newOrganizerDto.Email;

        try
        {
            await applicationContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrganizerExists(id))
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
    // DELETE: api/Organizer/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganizer(int id)
    {
        var organizer = await applicationContext.tblOrganizer.FindAsync(id);
        if (organizer == null)
        {
            return NotFound();
        }

        applicationContext.tblOrganizer.Remove(organizer);
        await applicationContext.SaveChangesAsync();

        return NoContent();
    }
    
    private bool OrganizerExists(int id)
    {
        return applicationContext.tblOrganizer.Any(e => e.organizerID == id);
    }
 
}