using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.Models;
using backend.DTO;

namespace backend.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AddressController(HemoRedContext context) : ControllerBase
{
    // GET: api/Address
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses()
    {
        return await context.TblAddresses
            .Select(a => new AddressDto
            {
                AddressID = a.AddressId,
                MunicipalityID = a.MunicipalityId,
                ProvinceID = a.ProvinceId,
                Street = a.Street,
                BuildingNumber = a.BuildingNumber
            })
            .ToListAsync();
    }

    // GET: api/Address/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressDto>> GetAddresses(int id)
    {
        var address = await context.TblAddresses
            .Where(a => a.AddressId == id)
            .Select(a => new AddressDto
            {
                AddressID = a.AddressId,
                MunicipalityID = a.MunicipalityId,
                ProvinceID = a.ProvinceId,
                Street = a.Street,
                BuildingNumber = a.BuildingNumber
            })
            .FirstOrDefaultAsync();

        if (address == null)
        {
            return NotFound();
        }

        return address;
    }

    // PUT: api/Address/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PuttblAddress(int id, newAddressDTO newAddressDto)
    {
        var address = await context.TblAddresses.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        // Update the address with the DTO data
        address.MunicipalityId = newAddressDto.MunicipalityID;
        address.ProvinceId= newAddressDto.ProvinceID;
        address.Street= newAddressDto.Street;
        address.BuildingNumber = newAddressDto.BuildingNumber;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TblAddressExists(id))
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

    // POST: api/Address
    [HttpPost]
    public async Task<ActionResult<AddressDto>> PostAddress(newAddressDTO newAddressDto)
    {
        var address = new TblAddress
        {
            MunicipalityId= newAddressDto.MunicipalityID,
            ProvinceId= newAddressDto.ProvinceID,
            Street= newAddressDto.Street,
            BuildingNumber = newAddressDto.BuildingNumber,
            
        };

        context.TblAddresses.Add(address);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetAddresses", new { id = address.AddressId }, newAddressDto);
    }

    // DELETE: api/Address/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletetblAddress(int id)
    {
        var tblAddress = await context.TblAddresses.FindAsync(id);
        if (tblAddress == null)
        {
            return NotFound();
        }

        context.TblAddresses.Remove(tblAddress);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TblAddressExists(int id)
    {
        return context.TblAddresses.Any(e => e.AddressId == id);
    }
}
}
