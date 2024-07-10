using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.DTO;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController(HemoRedContext context) : ControllerBase
    {
        // GET: api/Municipality
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<TblMunicipality>>> GetTblMunicipalities()
        {
            return await context.TblMunicipalities.ToListAsync();
        }

        // GET: api/Municipality/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<TblMunicipality>> GetTblMunicipality(int id)
        {
            var tblMunicipality = await context.TblMunicipalities.FindAsync(id);

            if (tblMunicipality == null)
            {
                return NotFound();
            }

            return tblMunicipality;
        }

        // PUT: api/Municipality/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMunicipality(int id, MunicipalityDto municipalityDto)
        {
            if (id != municipalityDto.MunicipalityID)
            {
                return BadRequest();
            }

            context.Entry(municipalityDto).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMunicipalityExists(id))
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

        private bool TblMunicipalityExists(int id)
        {
            return context.TblMunicipalities.Any(e => e.MunicipalityId == id);
        }
    }
}
