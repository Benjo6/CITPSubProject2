using Common.Domain;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookmarkPersonalitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookmarkPersonalitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BookmarkPersonalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookmarkPersonality>>> GetBookmarkPersonalities()
        {
            if (_context.BookmarkPersonalities == null)
            {
                return NotFound();
            }

            return await _context.BookmarkPersonalities.ToListAsync();
        }

        // GET: api/BookmarkPersonalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkPersonality>> GetBookmarkPersonality(int id)
        {
            if (_context.BookmarkPersonalities == null)
            {
                return NotFound();
            }

            var bookmarkPersonality = await _context.BookmarkPersonalities.FindAsync(id);

            if (bookmarkPersonality == null)
            {
                return NotFound();
            }

            return bookmarkPersonality;
        }

        // PUT: api/BookmarkPersonalities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmarkPersonality(int id, BookmarkPersonality bookmarkPersonality)
        {
            if (id != bookmarkPersonality.UserId)
            {
                return BadRequest();
            }

            _context.Entry(bookmarkPersonality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkPersonalityExists(id))
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

        // POST: api/BookmarkPersonalities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookmarkPersonality>> PostBookmarkPersonality(
            BookmarkPersonality bookmarkPersonality)
        {
            if (_context.BookmarkPersonalities == null)
            {
                return Problem("Entity set 'Cit02Context.BookmarkPersonalities'  is null.");
            }

            _context.BookmarkPersonalities.Add(bookmarkPersonality);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkPersonalityExists(bookmarkPersonality.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookmarkPersonality", new { id = bookmarkPersonality.UserId },
                bookmarkPersonality);
        }

        // DELETE: api/BookmarkPersonalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmarkPersonality(int id)
        {
            if (_context.BookmarkPersonalities == null)
            {
                return NotFound();
            }

            var bookmarkPersonality = await _context.BookmarkPersonalities.FindAsync(id);
            if (bookmarkPersonality == null)
            {
                return NotFound();
            }

            _context.BookmarkPersonalities.Remove(bookmarkPersonality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkPersonalityExists(int id)
        {
            return (_context.BookmarkPersonalities?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}