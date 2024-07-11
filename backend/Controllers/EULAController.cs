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
    public class EULAController(HemoRedContext _hemoredContext) : ControllerBase
    {
        // GET: api/EULA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEula>>> GetTblEulas()
        {
            return await _hemoredContext.TblEulas.ToListAsync();
        }

        // GET: api/EULA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EulaDto>> GetTblEula(int id)
        {
            var tblEula = await _hemoredContext.TblEulas.Where(d => d.EulaId == id)
                .Select(d => new EulaDto
                {
                    EulaID = d.EulaId,
                    Content = d.Content,
                    UpdateDate = d.UpdateDate,
                    Version = d.Version
                })
                .FirstOrDefaultAsync();

            if (tblEula == null)
            {
                return NotFound();
            }

            return tblEula;
        }

        // PUT: api/EULA/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEula(int id, EulaDto newEulaDto)
        {
            var eula = await _hemoredContext.TblEulas.FindAsync(id);
            if (eula == null)
            {
                return NotFound();
            }

            // Update the blood bag with the DTO data
            eula.EulaId = newEulaDto.EulaID;
            eula.Content = newEulaDto.Content;
            eula.UpdateDate = newEulaDto.UpdateDate;
            eula.Version = newEulaDto.Version;

            try
            {
                await _hemoredContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEulaExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/EULA
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EulaDto>> PostTblEula(EulaDto eulaDto)
        {
            _hemoredContext.TblEulas.Add(TblEula.FromDto(eulaDto));
            await _hemoredContext.SaveChangesAsync();

            return CreatedAtAction("GetTblEula", new { id = eulaDto.EulaID }, eulaDto);
        }

        // DELETE: api/EULA/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEula(int id)
        {
            var tblEula = await _hemoredContext.TblEulas.FindAsync(id);
            if (tblEula == null)
            {
                return NotFound();
            }

            _hemoredContext.TblEulas.Remove(tblEula);
            await _hemoredContext.SaveChangesAsync();

            return NoContent();
        }
        
        // [HttpPost("PostUserEULA")]
        //
        // public async Task<IActionResult> AddUserEula(UserEulaDto userEulaDto)
        // {
        //     
        // }

        private bool TblEulaExists(int id)
        {
            return _hemoredContext.TblEulas.Any(e => e.EulaId == id);
        }
    }
}
