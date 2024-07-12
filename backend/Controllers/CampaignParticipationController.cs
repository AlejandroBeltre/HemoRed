using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.DTO;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignParticipationController(HemoRedContext _hemoredContext) : ControllerBase
    {
        // GET: api/CampaignParticipation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignParticipationDto>>> GetTblCampaignParticipations()
        {
            return await _hemoredContext.TblCampaignParticipations
            .Select( c => new CampaignParticipationDto
            {
                CampaignID = c.CampaignId,
                OrganizerID = c.OrganizerId,
                DonationID = c.DonationId
            })
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignParticipationDto>> GetTblCampaignParticipation(int id)
        {
            var campaignParticipation = await _hemoredContext.TblCampaignParticipations
                .Where(c => c.CampaignId == id)
                .Select(c => new CampaignParticipationDto
                {
                    CampaignID = c.CampaignId,
                    OrganizerID = c.OrganizerId,
                    DonationID = c.DonationId
                })
                .FirstOrDefaultAsync();

            if (campaignParticipation == null)
            {
                return NotFound();
            }

            return campaignParticipation;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCampaignParticipation(int id, CampaignParticipationDto campaignParticipationDto)
        {
            if (id != campaignParticipationDto.CampaignID)
            {
                return BadRequest();
            }

            var campaignParticipation = await _hemoredContext.TblCampaignParticipations
                .Where(c => c.CampaignId == id)
                .FirstOrDefaultAsync();

            if (campaignParticipation == null)
            {
                return NotFound();
            }

            campaignParticipation.OrganizerId = campaignParticipationDto.OrganizerID;
            campaignParticipation.DonationId = campaignParticipationDto.DonationID;

            _hemoredContext.Entry(campaignParticipation).State = EntityState.Modified;

            try
            {
                await _hemoredContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCampaignParticipationExists(id))
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

        // POST: api/CampaignParticipation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CampaignParticipationDto>> PostTblCampaignParticipation(CampaignParticipationDto campaignParticipationDto)
        {
            var campaign = await _hemoredContext.TblCampaigns.FindAsync(campaignParticipationDto.CampaignID);
            var organizer = await _hemoredContext.TblOrganizers.FindAsync(campaignParticipationDto.OrganizerID);
            var donation = await _hemoredContext.TblDonations.FindAsync(campaignParticipationDto.DonationID);
            if (campaign == null || organizer == null || donation == null)
            {
                return BadRequest("Invalid campaign, organizer or donation");
            }

            var tblCampaignParticipation = new TblCampaignParticipation
            {
                CampaignId = campaignParticipationDto.CampaignID,
                OrganizerId = campaignParticipationDto.OrganizerID,
                DonationId = campaignParticipationDto.DonationID,
            };

            _hemoredContext.TblCampaignParticipations.Add(tblCampaignParticipation);
            await _hemoredContext.SaveChangesAsync();

            var createdCampaignParticipation = new CampaignParticipationDto
            {
                CampaignID = tblCampaignParticipation.CampaignId,
                OrganizerID = tblCampaignParticipation.OrganizerId,
                DonationID = tblCampaignParticipation.DonationId
            };

            return CreatedAtAction("GetTblCampaignParticipation", new { id = tblCampaignParticipation.CampaignId }, createdCampaignParticipation);
        }

        // DELETE: api/CampaignParticipation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCampaignParticipation(int id)
        {
            var tblCampaignParticipation = await _hemoredContext.TblCampaignParticipations
                .Where(c => c.CampaignId == id)
                .FirstOrDefaultAsync();

            if (tblCampaignParticipation == null)
            {
                return NotFound();
            }

            _hemoredContext.TblCampaignParticipations.Remove(tblCampaignParticipation);
            await _hemoredContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCampaignParticipationExists(int id)
        {
            return _hemoredContext.TblCampaignParticipations.Any(e => e.CampaignId == id);
        }
    }
}
