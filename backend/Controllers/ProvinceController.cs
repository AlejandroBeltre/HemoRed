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
        public async Task<ActionResult<IEnumerable<ProvinceDto>>> GetTblProvinces()
        {
            return await context.TblProvinces.Select(p => new ProvinceDto{
                ProvinceID = p.ProvinceId,
                ProvinceName = p.ProvinceName
            }).ToListAsync();
        }

        // GET: api/Province/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinceDto>> GetTblProvince(int id)
        {
            var province = await context.TblProvinces.Where(p => p.ProvinceId == id).Select(p => new ProvinceDto{
                ProvinceID = p.ProvinceId,
                ProvinceName = p.ProvinceName
            }).FirstOrDefaultAsync();

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }
        private bool TblProvinceExists(int id)
        {
            return context.TblProvinces.Any(e => e.ProvinceId == id);
        }
    }
}
