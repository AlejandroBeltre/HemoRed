using backend.Context;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizerController(HemoRedContext _hemoredContext) : Controller
{
    // GET: api/Users
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<OrganizerDto>>> GetOrganizers()
    {
        return await _hemoredContext.TblOrganizers
            .Select(o => new OrganizerDto
            {
                OrganizerID = o.OrganizerId,
                AddressID = o.AddressId,
                OrganizerName = o.OrganizerName,
                Email = o.Email,
                Phone = o.Phone
            }).ToListAsync();
    }

    [HttpGet("get/id")]
    public async Task<ActionResult<OrganizerDto>> GetOrganizer(int id)
    {
        var organizer = await _hemoredContext.TblOrganizers.Where(o => o.OrganizerId == id).Select(o => new OrganizerDto
        {
            OrganizerID = o.OrganizerId,
            AddressID = o.AddressId,
            OrganizerName = o.OrganizerName,
            Email = o.Email,
            Phone = o.Phone
        }).FirstOrDefaultAsync();
        if (organizer == null)
        {
            return NotFound();
        }

        return organizer;
    }

    // POST: api/BloodBank
    [HttpPost("post")]
    public async Task<ActionResult<OrganizerDto>> PostUser([FromForm] NewOrganizerDTO newOrganizerDto)
    {
        var organizer = new TblOrganizer
        {
            OrganizerName = newOrganizerDto.OrganizerName,
            Email = newOrganizerDto.Email,
            Phone = newOrganizerDto.Phone,
            AddressId = newOrganizerDto.AddressID
        };

        _hemoredContext.TblOrganizers.Add(organizer);
        await _hemoredContext.SaveChangesAsync();

        var createOrganizerDto = new OrganizerDto
        {
            OrganizerID = organizer.OrganizerId,
            AddressID = organizer.AddressId,
            Email = organizer.Email,
            OrganizerName = organizer.OrganizerName,
            Phone = organizer.Phone
        };

        return CreatedAtAction("GetOrganizer", new { id = organizer.AddressId }, createOrganizerDto);
    }

    // PUT: api/organizer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrganizer(int id, [FromForm] NewOrganizerDTO newOrganizerDto)
    {
        var organizer = await _hemoredContext.TblOrganizers.FindAsync(id);
        if (organizer == null)
        {
            return NotFound();
        }

        organizer.AddressId = newOrganizerDto.AddressID;
        organizer.OrganizerName = newOrganizerDto.OrganizerName;
        organizer.Phone = newOrganizerDto.Phone;
        organizer.Email = newOrganizerDto.Email;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrganizerExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/Organizer/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganizer(int id)
    {
        var organizer = await _hemoredContext.TblOrganizers.FindAsync(id);
        if (organizer == null)
        {
            return NotFound();
        }

        _hemoredContext.TblOrganizers.Remove(organizer);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }

    private bool OrganizerExists(int id)
    {
        return _hemoredContext.TblOrganizers.Any(e => e.OrganizerId == id);
    }
}