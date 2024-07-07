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
    public class EULAController(HemoRedContext context) : ControllerBase
    {
        // GET: api/EULA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEula>>> GetTblEulas()
        {
            return await context.TblEulas.ToListAsync();
        }

        // GET: api/EULA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEula>> GetTblEula(int id)
        {
            var tblEula = await context.TblEulas.FindAsync(id);

            if (tblEula == null)
            {
                return NotFound();
            }

            return tblEula;
        }

        // PUT: api/EULA/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEula(int id, TblEula tblEula)
        {
            if (id != tblEula.EulaId)
            {
                return BadRequest();
            }

            context.Entry(tblEula).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEulaExists(id))
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

        // POST: api/EULA
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblEula>> PostTblEula(EulaDto eulaDto)
        {
            context.TblEulas.Add(TblEula.FromDto(eulaDto));
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTblEula", new { id = eulaDto.EulaID }, eulaDto);
        }

        // DELETE: api/EULA/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEula(int id)
        {
            var tblEula = await context.TblEulas.FindAsync(id);
            if (tblEula == null)
            {
                return NotFound();
            }

            context.TblEulas.Remove(tblEula);
            await context.SaveChangesAsync();

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
            return context.TblEulas.Any(e => e.EulaId == id);
        }
    }
}
