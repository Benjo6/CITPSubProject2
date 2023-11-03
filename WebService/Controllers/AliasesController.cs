using Common.Domain;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AliasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AliasesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Aliases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alias>>> GetAliases()
        {
            if (_context.Aliases == null)
            {
                return NotFound();
            }

            return await _context.Aliases.ToListAsync();
        }

        // GET: api/Aliases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alias>> GetAlias(int id)
        {
            if (_context.Aliases == null)
            {
                return NotFound();
            }

            var @alias = await _context.Aliases.FindAsync(id);

            if (@alias == null)
            {
                return NotFound();
            }

            return @alias;
        }

        // PUT: api/Aliases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlias(int id, Alias @alias)
        {
            if (id != @alias.Id)
            {
                return BadRequest();
            }

            _context.Entry(@alias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AliasExists(id))
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

        // POST: api/Aliases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alias>> PostAlias(Alias @alias)
        {
            if (_context.Aliases == null)
            {
                return Problem("Entity set 'Cit02Context.Aliases'  is null.");
            }

            _context.Aliases.Add(@alias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlias", new { id = @alias.Id }, @alias);
        }

        // DELETE: api/Aliases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlias(int id)
        {
            if (_context.Aliases == null)
            {
                return NotFound();
            }

            var @alias = await _context.Aliases.FindAsync(id);
            if (@alias == null)
            {
                return NotFound();
            }

            _context.Aliases.Remove(@alias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AliasExists(int id)
        {
            return (_context.Aliases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}