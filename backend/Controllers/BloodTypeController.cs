using backend.Context;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodTypeController(ApplicationContext applicationContext) : ControllerBase
{
    // GET
    [HttpGet("get")]
    public async Task<IEnumerable<BloodTypeDto>> GetBloodTypes()
    {
        return await applicationContext.tblBloodType.Select(e => new BloodTypeDto
        {
            BloodType = e.bloodType,
            BloodTypeID = e.bloodTypeID
        }).ToListAsync();
    }

    [HttpGet("get{id}")]
    public async Task<BloodTypeDto?> GetBloodTypeById(int id)
    {
        var bloodType = await applicationContext.tblBloodType.FindAsync(id);
        if (bloodType == null)
        {
            return null;
        }
        return new BloodTypeDto
        {
            BloodType = bloodType.bloodType,
            BloodTypeID = bloodType.bloodTypeID
        };
    }

    [HttpDelete("delete{id}")]
    public async Task<bool> DeleteBloodType(int id)
    {
        var bloodType = await applicationContext.tblBloodType.FindAsync(id);
        if (bloodType == null)
        {
            return false;
        }

        applicationContext.tblBloodType.Remove(bloodType);
        await applicationContext.SaveChangesAsync();
        return true;
    }
}