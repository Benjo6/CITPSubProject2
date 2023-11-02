using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubProject2.Models;

namespace SubProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchhistoriesController : ControllerBase
    {
        private readonly Cit02Context _context;

        public SearchhistoriesController(Cit02Context context)
        {
            _context = context;
        }

        // GET: api/Searchhistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Searchhistory>>> GetSearchhistories()
        {
          if (_context.Searchhistories == null)
          {
              return NotFound();
          }
            return await _context.Searchhistories.ToListAsync();
        }

        // GET: api/Searchhistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Searchhistory>> GetSearchhistory(int id)
        {
          if (_context.Searchhistories == null)
          {
              return NotFound();
          }
            var searchhistory = await _context.Searchhistories.FindAsync(id);

            if (searchhistory == null)
            {
                return NotFound();
            }

            return searchhistory;
        }

        // PUT: api/Searchhistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchhistory(int id, Searchhistory searchhistory)
        {
            if (id != searchhistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(searchhistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchhistoryExists(id))
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

        // POST: api/Searchhistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Searchhistory>> PostSearchhistory(Searchhistory searchhistory)
        {
          if (_context.Searchhistories == null)
          {
              return Problem("Entity set 'Cit02Context.Searchhistories'  is null.");
          }
            _context.Searchhistories.Add(searchhistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchhistory", new { id = searchhistory.Id }, searchhistory);
        }

        // DELETE: api/Searchhistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchhistory(int id)
        {
            if (_context.Searchhistories == null)
            {
                return NotFound();
            }
            var searchhistory = await _context.Searchhistories.FindAsync(id);
            if (searchhistory == null)
            {
                return NotFound();
            }

            _context.Searchhistories.Remove(searchhistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SearchhistoryExists(int id)
        {
            return (_context.Searchhistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
