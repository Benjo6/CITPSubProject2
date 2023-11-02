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
    public class BookmarkmoviesController : ControllerBase
    {
        private readonly Cit02Context _context;

        public BookmarkmoviesController(Cit02Context context)
        {
            _context = context;
        }

        // GET: api/Bookmarkmovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookmarkmovie>>> GetBookmarkmovies()
        {
          if (_context.Bookmarkmovies == null)
          {
              return NotFound();
          }
            return await _context.Bookmarkmovies.ToListAsync();
        }

        // GET: api/Bookmarkmovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmarkmovie>> GetBookmarkmovie(int id)
        {
          if (_context.Bookmarkmovies == null)
          {
              return NotFound();
          }
            var bookmarkmovie = await _context.Bookmarkmovies.FindAsync(id);

            if (bookmarkmovie == null)
            {
                return NotFound();
            }

            return bookmarkmovie;
        }

        // PUT: api/Bookmarkmovies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmarkmovie(int id, Bookmarkmovie bookmarkmovie)
        {
            if (id != bookmarkmovie.UserId)
            {
                return BadRequest();
            }

            _context.Entry(bookmarkmovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkmovieExists(id))
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

        // POST: api/Bookmarkmovies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookmarkmovie>> PostBookmarkmovie(Bookmarkmovie bookmarkmovie)
        {
          if (_context.Bookmarkmovies == null)
          {
              return Problem("Entity set 'Cit02Context.Bookmarkmovies'  is null.");
          }
            _context.Bookmarkmovies.Add(bookmarkmovie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkmovieExists(bookmarkmovie.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookmarkmovie", new { id = bookmarkmovie.UserId }, bookmarkmovie);
        }

        // DELETE: api/Bookmarkmovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmarkmovie(int id)
        {
            if (_context.Bookmarkmovies == null)
            {
                return NotFound();
            }
            var bookmarkmovie = await _context.Bookmarkmovies.FindAsync(id);
            if (bookmarkmovie == null)
            {
                return NotFound();
            }

            _context.Bookmarkmovies.Remove(bookmarkmovie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkmovieExists(int id)
        {
            return (_context.Bookmarkmovies?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
