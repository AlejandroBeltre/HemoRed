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
    public class BloodBankController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BloodBankController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/BloodBank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodBankDto>>> GetBloodBanks()
        {
            return await _context.tblBloodBank
                .Select(b => new BloodBankDto
                {
                    BloodBankID = b.bloodBankID,
                    AddressID = b.addressID,
                    BloodBankName = b.bloodBankName,
                    AvailableHours = b.availableHours,
                    Phone = b.phone,
                    Image = b.image
                })
                .ToListAsync();
        }

        // GET: api/BloodBank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodBankDto>> GetBloodBank(int id)
        {
            var bloodBank = await _context.tblBloodBank
                .Where(b => b.bloodBankID == id)
                .Select(b => new BloodBankDto
                {
                    BloodBankID = b.bloodBankID,
                    AddressID = b.addressID,
                    BloodBankName = b.bloodBankName,
                    AvailableHours = b.availableHours,
                    Phone = b.phone,
                    Image = b.image
                })
                .FirstOrDefaultAsync();

            if (bloodBank == null)
            {
                return NotFound();
            }

            return bloodBank;
        }

        // POST: api/BloodBank
        [HttpPost]
        public async Task<ActionResult<BloodBankDto>> PostBloodBank([FromForm]NewBloodBankDTO newBloodBankDto, [FromForm]IFormFile image)
        {
            string imagePath = null;
            if (image != null && image.Length > 0)
            {
                var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if(!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                imagePath = Path.Combine(uploadFolderPath, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }
            var address = await _context.tblAddress.FindAsync(newBloodBankDto.AddressID);
            var bloodBank = new tblBloodBank
            {
                addressID = newBloodBankDto.AddressID,
                bloodBankName = newBloodBankDto.BloodBankName,
                availableHours = newBloodBankDto.AvailableHours,
                phone = newBloodBankDto.Phone,
                image = imagePath,
                tblAddress = address
            };

            _context.tblBloodBank.Add(bloodBank);
            await _context.SaveChangesAsync();

            var createdBankDto = new BloodBankDto
            {
                BloodBankID = bloodBank.bloodBankID,
                AddressID = bloodBank.addressID,
                BloodBankName = bloodBank.bloodBankName,
                AvailableHours = bloodBank.availableHours,
                Phone = bloodBank.phone,
                Image = bloodBank.image
            };

            return CreatedAtAction("GetBloodBank", new { id = bloodBank.bloodBankID }, createdBankDto);
        }

// PUT: api/BloodBank/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodBank(int id, [FromForm] NewBloodBankDTO newBloodBankDto, [FromForm] IFormFile image)
        {
            var bloodBank = await _context.tblBloodBank.FindAsync(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            string imagePath = bloodBank.image; // Keep the existing image path if no new image is uploaded
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

            bloodBank.addressID = newBloodBankDto.AddressID;
            bloodBank.bloodBankName = newBloodBankDto.BloodBankName;
            bloodBank.availableHours = newBloodBankDto.AvailableHours;
            bloodBank.phone = newBloodBankDto.Phone;
            bloodBank.image = imagePath;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodBankExists(id))
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
        public async Task<IActionResult> DeleteBloodBank(int id)
        {
            var bloodBank = await _context.tblBloodBank.FindAsync(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            _context.tblBloodBank.Remove(bloodBank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloodBankExists(int id)
        {
            return _context.tblBloodBank.Any(e => e.bloodBankID == id);
        }
    }
}