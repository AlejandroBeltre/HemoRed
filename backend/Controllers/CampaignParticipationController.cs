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
        public async Task<ActionResult<IEnumerable<TblCampaignParticipation>>> GetTblCampaignParticipations()
        {
            return await _hemoredContext.TblCampaignParticipations.ToListAsync();
        }

        // GET: api/CampaignParticipation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCampaignParticipation>> GetTblCampaignParticipation(int id)
        {
            var tblCampaignParticipation = await _hemoredContext.TblCampaignParticipations.FindAsync(id);

            if (tblCampaignParticipation == null)
            {
                return NotFound();
            }

            return tblCampaignParticipation;
        }

        // PUT: api/CampaignParticipation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCampaignParticipation(int id, TblCampaignParticipation tblCampaignParticipation)
        {
            if (id != tblCampaignParticipation.CampaignId)
            {
                return BadRequest();
            }

            _hemoredContext.Entry(tblCampaignParticipation).State = EntityState.Modified;

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
        public async Task<ActionResult<TblCampaignParticipation>> PostTblCampaignParticipation([FromForm]CampaignParticipationDto campaignParticipationDto)
        {
            var campaign = await _hemoredContext.TblCampaigns.FindAsync(campaignParticipationDto.CampaignID);
            var organizer = await _hemoredContext.TblOrganizers.FindAsync(campaignParticipationDto.OrganizerID);
            var donation = await _hemoredContext.TblDonations.FindAsync(campaignParticipationDto.DonationID);
            if (campaign == null || organizer == null || donation == null)
            {
                return BadRequest("Invalid campaign, organizer or donation");
            }
            var campaignParticipation = new TblCampaignParticipation
            {
                CampaignId= campaignParticipationDto.CampaignID,
                OrganizerId = campaignParticipationDto.OrganizerID,
                DonationId = campaignParticipationDto.DonationID,
                Campaign = campaign,
                Donation = donation,
                Organizer = organizer
            };
            _hemoredContext.TblCampaignParticipations.Add(campaignParticipation);
            await _hemoredContext.SaveChangesAsync();
            var createdCampaignParticipation = new CampaignParticipationDto()
            {
                CampaignID= campaignParticipation.CampaignId,
                OrganizerID = campaignParticipation.OrganizerId,
                DonationID = campaignParticipation.DonationId
            };

            return CreatedAtAction("GetTblCampaignParticipation", new { id = campaignParticipationDto }, createdCampaignParticipation);
        }

        // DELETE: api/CampaignParticipation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCampaignParticipation(int id)
        {
            var tblCampaignParticipation = await _hemoredContext.TblCampaignParticipations.FindAsync(id);
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
