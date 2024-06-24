using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.Models;
using backend.DTO;

namespace backend.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AddressController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: api/Address
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GettblAddress()
    {
        return await _context.tblAddress
            .Select(a => new AddressDto
            {
                AddressID = a.addressID,
                MunicipalityID = a.municipalityID,
                ProvinceID = a.provinceID,
                Street = a.street,
                BuildingNumber = a.buildingNumber
            })
            .ToListAsync();
    }

    // GET: api/Address/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressDto>> GettblAddress(int id)
    {
        var address = await _context.tblAddress
            .Where(a => a.addressID == id)
            .Select(a => new AddressDto
            {
                AddressID = a.addressID,
                MunicipalityID = a.municipalityID,
                ProvinceID = a.provinceID,
                Street = a.street,
                BuildingNumber = a.buildingNumber
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
        var address = await _context.tblAddress.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        // Update the address with the DTO data
        address.municipalityID = newAddressDto.MunicipalityID;
        address.provinceID = newAddressDto.ProvinceID;
        address.street = newAddressDto.Street;
        address.buildingNumber = newAddressDto.BuildingNumber;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!tblAddressExists(id))
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
    public async Task<ActionResult<AddressDto>> PosttblAddress(newAddressDTO newAddressDto)
    {
        var address = new tblAddress
        {
            municipalityID = newAddressDto.MunicipalityID,
            provinceID = newAddressDto.ProvinceID,
            street = newAddressDto.Street,
            buildingNumber = newAddressDto.BuildingNumber,
            tblMunicipality = null,
            tblProvince = null
        };

        _context.tblAddress.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GettblAddress", new { id = address.addressID }, newAddressDto);
    }

    // DELETE: api/Address/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletetblAddress(int id)
    {
        var tblAddress = await _context.tblAddress.FindAsync(id);
        if (tblAddress == null)
        {
            return NotFound();
        }

        _context.tblAddress.Remove(tblAddress);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool tblAddressExists(int id)
    {
        return _context.tblAddress.Any(e => e.addressID == id);
    }
}
}
