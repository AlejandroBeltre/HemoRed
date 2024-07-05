using backend.Context;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController(ApplicationContext _context) : Controller
{
// GET: api/Request
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
    {
        return await _context.tblRequest
            .Select(r => new RequestDto
            {
                RequestID = r.requestID,
                UserDocument = r.userDocument,
                BloodTypeId = r.bloodTypeID,
                RequestTimeStamp = r.requestTimeStamp,
                RequestReason = r.requestReason,
                RequestedAmount = r.requestedAmount,
                Status = r.status
            })
            .ToListAsync();
    }

    // GET: api/Request/5
    [HttpGet("get/{id}")]
    public async Task<ActionResult<RequestDto>> GetRequest(int id)
    {
        var request = await _context.tblRequest
            .Where(r => r.requestID == id)
            .Select(r => new RequestDto()
            {
                RequestID = r.requestID,
                UserDocument = r.userDocument,
                BloodTypeId = r.bloodTypeID,
                RequestTimeStamp = r.requestTimeStamp,
                RequestReason = r.requestReason,
                RequestedAmount = r.requestedAmount,
                Status = r.status
            }).FirstOrDefaultAsync();

        if (request == null)
        {
            return NotFound();
        }

        return request;
    }

    [HttpPost("post")]
    public async Task<ActionResult<RequestDto>> PostRequest([FromForm] NewRequestDto newRequestDto)
    {
        var user = await _context.tblUser.FindAsync(newRequestDto.UserDocument);
        var bloodbank = await _context.tblBloodBank.FindAsync(newRequestDto.BloodBank);
        if (user == null || bloodbank == null)
        {
            return BadRequest("Invalid user or blood bank");
        }
        var request = new tblRequest
        {
            userDocument = newRequestDto.UserDocument,
            bloodTypeID = newRequestDto.BloodTypeId,
            requestTimeStamp = newRequestDto.RequestTimeStamp,
            requestReason = newRequestDto.RequestReason,
            requestedAmount = newRequestDto.RequestedAmount,
            status = newRequestDto.Status,
            tblUser = user,
            tblBloodBank = bloodbank
        };

        _context.tblRequest.Add(request);
        await _context.SaveChangesAsync();

        var createRequestDto = new RequestDto()
        {
            UserDocument = request.userDocument,
            BloodTypeId = request.bloodTypeID,
            RequestTimeStamp = request.requestTimeStamp,
            RequestReason = request.requestReason,
            RequestedAmount = request.requestedAmount,
            Status = request.status
        };
        return CreatedAtAction("GetRequest", new { id = request.requestID }, createRequestDto);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequest(int id, NewRequestDto newRequestDto)
    {
        var request = await _context.tblRequest.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        request.bloodTypeID = newRequestDto.BloodTypeId;
        request.userDocument = newRequestDto.UserDocument;
        request.requestTimeStamp = newRequestDto.RequestTimeStamp;
        request.requestedAmount = newRequestDto.RequestedAmount;
        request.requestReason = newRequestDto.RequestReason;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RequestExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRquest(string id)
    {
        var request = await _context.tblRequest.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        _context.tblRequest.Remove(request);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RequestExists(int id)
    {
        return _context.tblRequest.Any(e => e.requestID == id);
    }
}