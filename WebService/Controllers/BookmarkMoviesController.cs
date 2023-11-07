using Common.Domain;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookmarkMoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookmarkMoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookmarkmovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookmarkMovie>>> GetBookmarkmovies()
        {
            if (_context.BookmarkMovies == null)
            {
                return NotFound();
            }

            return await _context.BookmarkMovies.ToListAsync();
        }

        // GET: api/BookmarkMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkMovie>> GetBookmarkMovie(int id)
        {
            if (_context.BookmarkMovies == null)
            {
                return NotFound();
            }

            var bookmarkMovie = await _context.BookmarkMovies.FindAsync(id);

            if (bookmarkMovie == null)
            {
                return NotFound();
            }

            return bookmarkMovie;
        }

        // PUT: api/BookmarkMovies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmarkMovie(int id, BookmarkMovie bookmarkMovie)
        {
            if (id != bookmarkMovie.UserId)
            {
                return BadRequest();
            }

            _context.Entry(bookmarkMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkMovieExists(id))
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

        // POST: api/BookmarkMovies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookmarkMovie>> PostBookmarkMovie(BookmarkMovie bookmarkMovie)
        {
            if (_context.BookmarkMovies == null)
            {
                return Problem("Entity set 'Cit02Context.BookmarkMovies'  is null.");
            }

            _context.BookmarkMovies.Add(bookmarkMovie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkMovieExists(bookmarkMovie.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookmarkMovie", new { id = bookmarkMovie.UserId }, bookmarkMovie);
        }

        // DELETE: api/BookmarkMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmarkMovie(int id)
        {
            if (_context.BookmarkMovies == null)
            {
                return NotFound();
            }

            var bookmarkMovie = await _context.BookmarkMovies.FindAsync(id);
            if (bookmarkMovie == null)
            {
                return NotFound();
            }

            _context.BookmarkMovies.Remove(bookmarkMovie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkMovieExists(int id)
        {
            return (_context.BookmarkMovies?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}