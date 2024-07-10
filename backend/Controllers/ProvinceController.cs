using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.DTO;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController(HemoRedContext context) : ControllerBase
    {
        // GET: api/Province
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProvince>>> GetTblProvinces()
        {
            return await context.TblProvinces.ToListAsync();
        }

        // GET: api/Province/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProvince>> GetTblProvince(int id)
        {
            var tblProvince = await context.TblProvinces.FindAsync(id);

            if (tblProvince == null)
            {
                return NotFound();
            }

            return tblProvince;
        }

        // PUT: api/Province/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutTblProvince(int id, ProvinceDto provinceDto)
        {
            if (id != provinceDto.ProvinceID)
            {
                return BadRequest();
            }

            context.Entry(provinceDto).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProvinceExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        private bool TblProvinceExists(int id)
        {
            return context.TblProvinces.Any(e => e.ProvinceId == id);
        }
    }
}
