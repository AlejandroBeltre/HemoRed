using backend.Context;
using backend.DTO;
using backend.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodTypeController(HemoRedContext _hemoredContext) : ControllerBase
{
    // GET
    [HttpGet("get")]
    public async Task<IEnumerable<BloodTypeDto>> GetBloodTypes()
    {
        return await _hemoredContext.TblBloodTypes.Select(e => new BloodTypeDto
        {
            BloodType = e.BloodType,
            BloodTypeID = e.BloodTypeId
        }).ToListAsync();
    }
    
    [HttpGet("get/{id}")]
    public async Task<BloodTypeDto?> GetBloodTypeById(int id)
    {
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(id);
        if (bloodType == null)
        {
            return null;
        }
        return new BloodTypeDto
        {
            BloodType = bloodType.BloodType,
            BloodTypeID = bloodType.BloodTypeId
        };
    }

    [HttpDelete("delete/{id}")]
    public async Task<bool> DeleteBloodType(int id)
    {
        var bloodType = await _hemoredContext.TblBloodTypes.FindAsync(id);
        if (bloodType == null)
        {
            return false;
        }

        _hemoredContext.TblBloodTypes.Remove(bloodType);
        await _hemoredContext.SaveChangesAsync();
        return true;
    }
}