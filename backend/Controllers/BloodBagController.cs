// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using backend.Context;
// using backend.Models;
// using backend.DTO;
//
// namespace backend.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class BloodBagController : ControllerBase
//     {
//         private readonly ApplicationContext _context;
//
//         public BloodBagController(ApplicationContext context)
//         {
//             _context = context;
//         }
//
//         // GET: api/BloodBag
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<BloodBagDto>>> GetBloodBags()
//         {
//             return await _context.tblBloodBag
//                 .Select(b => new BloodBagDto
//                 {
//                     BagID = b.bagID,
//                     BloodTypeID = b.bloodTypeID,
//                     BloodBankID = b.bloodBankID,
//                     DonationID = b.donationID,
//                     ExpirationDate = b.expirationDate,
//                     IsReserved = b.isReserved
//                 })
//                 .ToListAsync();
//         }
//
//         // GET: api/BloodBag/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<BloodBagDto>> GetBloodBag(int id)
//         {
//             var bloodBag = await _context.tblBloodBag
//                 .Where(b => b.bagID == id)
//                 .Select(b => new BloodBagDto
//                 {
//                     BagID = b.bagID,
//                     BloodTypeID = b.bloodTypeID,
//                     BloodBankID = b.bloodBankID,
//                     DonationID = b.donationID,
//                     ExpirationDate = b.expirationDate,
//                     IsReserved = b.isReserved
//                 })
//                 .FirstOrDefaultAsync();
//
//             if (bloodBag == null)
//             {
//                 return NotFound();
//             }
//
//             return bloodBag;
//         }
//
//         // PUT: api/BloodBag/5
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutBloodBag(int id, NewBloodBagDTO newBloodBagDto)
//         {
//             var bloodBag = await _context.tblBloodBag.FindAsync(id);
//             if (bloodBag == null)
//             {
//                 return NotFound();
//             }
//
//             // Update the blood bag with the DTO data
//             bloodBag.bloodTypeID = newBloodBagDto.BloodTypeID;
//             bloodBag.bloodBankID = newBloodBagDto.BloodBankID;
//             bloodBag.donationID = newBloodBagDto.DonationID;
//             bloodBag.expirationDate = newBloodBagDto.ExpirationDate;
//             bloodBag.isReserved = newBloodBagDto.IsReserved;
//
//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!BloodBagExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }
//
//             return NoContent();
//         }
//
//         // POST: api/BloodBag
//         [HttpPost]
//         public async Task<ActionResult<BloodBagDto>> PostBloodBag(NewBloodBagDTO newBloodBagDto)
//         {
//             // Fetch related entities
//             var bloodType = await _context.tblBloodType.FindAsync(newBloodBagDto.BloodTypeID);
//             var bloodBank = await _context.tblBloodBank.FindAsync(newBloodBagDto.BloodBankID);
//             var donation = await _context.tblDonation.FindAsync(newBloodBagDto.DonationID);
//
//             if (bloodType == null || bloodBank == null || donation == null)
//             {
//                 return BadRequest("Invalid BloodTypeID, BloodBankID, or DonationID");
//             }
//
//             var bloodBag = new tblBloodBag
//             {
//                 bloodTypeID = newBloodBagDto.BloodTypeID,
//                 bloodBankID = newBloodBagDto.BloodBankID,
//                 donationID = newBloodBagDto.DonationID,
//                 expirationDate = newBloodBagDto.ExpirationDate,
//                 isReserved = newBloodBagDto.IsReserved,
//                 tblBloodType = bloodType,
//                 tblBloodBank = bloodBank,
//                 tblDonation = donation
//             };
//
//             _context.tblBloodBag.Add(bloodBag);
//             await _context.SaveChangesAsync();
//
//             var createdBagDto = new BloodBagDto
//             {
//                 BagID = bloodBag.bagID,
//                 BloodTypeID = bloodBag.bloodTypeID,
//                 BloodBankID = bloodBag.bloodBankID,
//                 DonationID = bloodBag.donationID,
//                 ExpirationDate = bloodBag.expirationDate,
//                 IsReserved = bloodBag.isReserved
//             };
//
//             return CreatedAtAction("GetBloodBag", new { id = bloodBag.bagID }, createdBagDto);
//         }
//
//         // DELETE: api/BloodBag/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteBloodBag(int id)
//         {
//             var bloodBag = await _context.tblBloodBag.FindAsync(id);
//             if (bloodBag == null)
//             {
//                 return NotFound();
//             }
//
//             _context.tblBloodBag.Remove(bloodBag);
//             await _context.SaveChangesAsync();
//
//             return NoContent();
//         }
//
//         private bool BloodBagExists(int id)
//         {
//             return _context.tblBloodBag.Any(e => e.bagID == id);
//         }
//     }
// }