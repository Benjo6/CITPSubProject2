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
    public class BookmarkpersonalitiesController : ControllerBase
    {
        private readonly Cit02Context _context;

        public BookmarkpersonalitiesController(Cit02Context context)
        {
            _context = context;
        }

        // GET: api/Bookmarkpersonalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookmarkpersonality>>> GetBookmarkpersonalities()
        {
          if (_context.Bookmarkpersonalities == null)
          {
              return NotFound();
          }
            return await _context.Bookmarkpersonalities.ToListAsync();
        }

        // GET: api/Bookmarkpersonalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmarkpersonality>> GetBookmarkpersonality(int id)
        {
          if (_context.Bookmarkpersonalities == null)
          {
              return NotFound();
          }
            var bookmarkpersonality = await _context.Bookmarkpersonalities.FindAsync(id);

            if (bookmarkpersonality == null)
            {
                return NotFound();
            }

            return bookmarkpersonality;
        }

        // PUT: api/Bookmarkpersonalities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmarkpersonality(int id, Bookmarkpersonality bookmarkpersonality)
        {
            if (id != bookmarkpersonality.UserId)
            {
                return BadRequest();
            }

            _context.Entry(bookmarkpersonality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkpersonalityExists(id))
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

        // POST: api/Bookmarkpersonalities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookmarkpersonality>> PostBookmarkpersonality(Bookmarkpersonality bookmarkpersonality)
        {
          if (_context.Bookmarkpersonalities == null)
          {
              return Problem("Entity set 'Cit02Context.Bookmarkpersonalities'  is null.");
          }
            _context.Bookmarkpersonalities.Add(bookmarkpersonality);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkpersonalityExists(bookmarkpersonality.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookmarkpersonality", new { id = bookmarkpersonality.UserId }, bookmarkpersonality);
        }

        // DELETE: api/Bookmarkpersonalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmarkpersonality(int id)
        {
            if (_context.Bookmarkpersonalities == null)
            {
                return NotFound();
            }
            var bookmarkpersonality = await _context.Bookmarkpersonalities.FindAsync(id);
            if (bookmarkpersonality == null)
            {
                return NotFound();
            }

            _context.Bookmarkpersonalities.Remove(bookmarkpersonality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkpersonalityExists(int id)
        {
            return (_context.Bookmarkpersonalities?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
