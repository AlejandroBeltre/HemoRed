using backend.Context;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController(HemoRedContext _hemoredContext) : Controller
{
// GET: api/Request
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
    {
        return await _hemoredContext.TblRequests
            .Select(r => new RequestDto
            {
                RequestID = r.RequestId,
                UserDocument = r.UserDocument,
                BloodTypeId = r.BloodTypeId,
                BloodBank = r.BloodBankId,
                RequestTimeStamp = r.RequestTimestamp,
                RequestReason = r.RequestReason,
                RequestedAmount = r.RequestedAmount,
                Status = r.Status
            })
            .ToListAsync();
    }

    // GET: api/Request/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDto>> GetRequest(int id)
    {
        var request = await _hemoredContext.TblRequests
            .Where(r => r.RequestId== id)
            .Select(r => new RequestDto()
            {
                RequestID = r.RequestId,
                UserDocument = r.UserDocument,
                BloodTypeId = r.BloodTypeId,
                RequestTimeStamp = r.RequestTimestamp,
                RequestReason = r.RequestReason,
                RequestedAmount = r.RequestedAmount,
                Status = r.Status
            }).FirstOrDefaultAsync();

        if (request == null)
        {
            return NotFound();
        }

        return request;
    }

    [HttpPost]
    public async Task<ActionResult<RequestDto>> PostRequest(NewRequestDto newRequestDto)
    {
        var user = await _hemoredContext.TblUsers.FindAsync(newRequestDto.UserDocument);
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(newRequestDto.BloodTypeId);
        var bloodBank = await _hemoredContext.TblBloodBanks.FindAsync(newRequestDto.BloodBankId);
        if (user == null || bloodType== null)
        {
            return BadRequest("Invalid user or blood type");
        }
        var request = new TblRequest
        {
            UserDocument = newRequestDto.UserDocument,
            BloodTypeId= newRequestDto.BloodTypeId,
            BloodBankId = newRequestDto.BloodBankId,
            RequestTimestamp = newRequestDto.RequestTimeStamp,
            RequestReason= newRequestDto.RequestReason,
            RequestedAmount = newRequestDto.RequestedAmount,
            Status= newRequestDto.Status,
            BloodBank = bloodBank,
            UserDocumentNavigation = user,
            BloodType = bloodType
        };

        _hemoredContext.TblRequests.Add(request);
        await _hemoredContext.SaveChangesAsync();

        var createRequestDto = new RequestDto()
        {
            UserDocument = request.UserDocument,
            BloodTypeId = request.BloodTypeId,
            RequestTimeStamp = request.RequestTimestamp,
            RequestReason = request.RequestReason,
            RequestedAmount = request.RequestedAmount,
            Status = request.Status
        };
        return CreatedAtAction("GetRequest", new { id = request.RequestId }, createRequestDto);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequest(int id, NewRequestDto newRequestDto)
    {
        var request = await _hemoredContext.TblRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        // Update the blood bag with the DTO data
        request.BloodTypeId = newRequestDto.BloodTypeId;
        request.UserDocument = newRequestDto.UserDocument;
        request.RequestTimestamp = newRequestDto.RequestTimeStamp;
        request.RequestedAmount= newRequestDto.RequestedAmount;
        request.RequestReason= newRequestDto.RequestReason;

        try
        {
            await _hemoredContext.SaveChangesAsync();
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
    public async Task<IActionResult> DeleteRequest(int id)
    {
        var request = await _hemoredContext.TblRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        _hemoredContext.TblRequests.Remove(request);
        await _hemoredContext.SaveChangesAsync();
        return NoContent();
    }

    private bool RequestExists(int id)
    {
        return _hemoredContext.TblRequests.Any(e => e.RequestId == id);
    }
}