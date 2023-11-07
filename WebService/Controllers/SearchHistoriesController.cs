using Common.Domain;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SearchHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchHistory>>> GetSearchHistories()
        {
            if (_context.SearchHistories == null)
            {
                return NotFound();
            }

            return await _context.SearchHistories.ToListAsync();
        }

        // GET: api/SearchHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SearchHistory>> GetSearchHistory(int id)
        {
            if (_context.SearchHistories == null)
            {
                return NotFound();
            }

            var searchHistory = await _context.SearchHistories.FindAsync(id);

            if (searchHistory == null)
            {
                return NotFound();
            }

            return searchHistory;
        }

        // PUT: api/SearchHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchHistory(int id, SearchHistory searchHistory)
        {
            if (id != searchHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(searchHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchHistoryExists(id))
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

        // POST: api/SearchHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SearchHistory>> PostSearchHistory(SearchHistory searchHistory)
        {
            if (_context.SearchHistories == null)
            {
                return Problem("Entity set 'Cit02Context.SearchHistories'  is null.");
            }

            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchHistory", new { id = searchHistory.Id }, searchHistory);
        }

        // DELETE: api/SearchHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchHistory(int id)
        {
            if (_context.SearchHistories == null)
            {
                return NotFound();
            }

            var searchHistory = await _context.SearchHistories.FindAsync(id);
            if (searchHistory == null)
            {
                return NotFound();
            }

            _context.SearchHistories.Remove(searchHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SearchHistoryExists(int id)
        {
            return (_context.SearchHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}