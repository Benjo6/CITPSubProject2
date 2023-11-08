using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class AliasesController : ControllerBase
{
    private readonly IAliasesService _service;
    
    public AliasesController(IAliasesService service)
    {
        _service = service;
    }
    
    // GET: Aliases
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AliasDTO>>> GetAliases()
    {
        try
        {
            var aliases = await _service.GetAllAliases();
            return Ok(aliases);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Aliases/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AliasDTO>> GetAlias(string id)
    {
        try
        {
            var alias = await _service.GetOneAlias(id);
            return Ok(alias);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Aliases/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<AliasDTO>> PutAlias(string id, AlterAliasDTO alias)
    {
        try
        {
            var putAlias = await _service.UpdateAlias(id, alias);
            return Ok(putAlias);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Aliases
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AliasDTO>> PostAlias(AlterAliasDTO alias)
    {
        try
        {
            var postAlias = await _service.AddAlias(alias);
            return Ok(postAlias);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: Aliases/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlias(string id)
    {
        try
        {
            var result = await _service.DeleteAlias(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}