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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> GetTblMunicipalities()
        {
            return await context.TblMunicipalities
                .Select(municipality => new MunicipalityDto{
                    MunicipalityID = municipality.MunicipalityId,
                    MunicipalityName = municipality.MunicipalityName,
                    ProvinceID = municipality.ProvinceId
                }).ToListAsync();
        }

        // GET: api/Municipality/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MunicipalityDto>> GetTblMunicipality(int id)
        {
            var municipality = await context.TblMunicipalities.Where(m => m.MunicipalityId == id).Select(m => new MunicipalityDto
            {
                MunicipalityID = m.MunicipalityId,
                MunicipalityName = m.MunicipalityName,
                ProvinceID = m.ProvinceId
            }).FirstOrDefaultAsync();

            if (municipality == null)
            {
                return NotFound();
            }

            return municipality;
        }

        // PUT: api/Municipality/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMunicipality(int id)
        {
            var municipality = await context.TblMunicipalities.FindAsync(id);
            if (municipality == null)
            {
                return NotFound();
            }

            context.TblMunicipalities.Remove(municipality);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}