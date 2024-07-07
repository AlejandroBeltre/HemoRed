using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.DTO;
using backend.Models;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserEulaController(HemoRedContext _hemoredContext) : ControllerBase
{
    // GET: api/UserEula
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TblUserEula>>> GetTblUserEulas()
    {
        return await _hemoredContext.TblUserEulas.ToListAsync();
    }

    // GET: api/UserEula/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TblUserEula>> GetTblUserEula(int id)
    {
        var tblUserEula = await _hemoredContext.TblUserEulas.FindAsync(id);

        if (tblUserEula == null)
        {
            return NotFound();
        }

        return tblUserEula;
    }

    // PUT: api/UserEula/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTblUserEula(int id,[FromForm] UserEulaDto userEulaDto)
    {
        if (id != userEulaDto.EulaID)
        {
            return BadRequest();
        }

        _hemoredContext.Entry(userEulaDto).State = EntityState.Modified;

        try
        {
            await _hemoredContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TblUserEulaExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // POST: api/UserEula
    [HttpPost]
    public async Task<ActionResult<UserEulaDto>> PostUserEula([FromForm] NewUserEulaDTO newUserEulaDto)
    {
        var userDocumento = await _hemoredContext.TblUsers.FindAsync(newUserEulaDto.UserDocument);
        var eula = await _hemoredContext.TblEulas.FindAsync(newUserEulaDto.EulaID);
        if (eula == null || userDocumento == null)
        {
            return BadRequest("Invalid EulaId or UserDocument");
        }
        var userEulaDto = new TblUserEula
        {
            AcceptedStatus = newUserEulaDto.AcceptedStatus,
            EulaId = newUserEulaDto.EulaID,
            UserDocument = newUserEulaDto.UserDocument,
            Eula = eula,
            UserDocumentNavigation = userDocumento
        };
        _hemoredContext.TblUserEulas.Add(userEulaDto);
        await _hemoredContext.SaveChangesAsync();
        var createdUserEula = new UserEulaDto
        {
            AcceptedStatus = userEulaDto.AcceptedStatus,
            EulaID = userEulaDto.EulaId,
            UserDocument = userEulaDto.UserDocument
        };
        
        return CreatedAtAction("GetTblUserEula", new { id = newUserEulaDto.EulaID }, createdUserEula);
    }
        

    // DELETE: api/UserEula/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTblUserEula(int id)
    {
        var tblUserEula = await _hemoredContext.TblUserEulas.FindAsync(id);
        if (tblUserEula == null)
        {
            return NotFound();
        }

        _hemoredContext.TblUserEulas.Remove(tblUserEula);
        await _hemoredContext.SaveChangesAsync();

        return NoContent();
    }

    private bool TblUserEulaExists(int id)
    {
        return _hemoredContext.TblUserEulas.Any(e => e.EulaId == id);
    }
}