using Common.Domain;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatingHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RatingHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RatingHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingHistory>>> GetRatingHistories()
        {
            if (_context.RatingHistories == null)
            {
                return NotFound();
            }

            return await _context.RatingHistories.ToListAsync();
        }

        // GET: api/RatingHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingHistory>> GetRatingHistory(int id)
        {
            if (_context.RatingHistories == null)
            {
                return NotFound();
            }

            var ratingHistory = await _context.RatingHistories.FindAsync(id);

            if (ratingHistory == null)
            {
                return NotFound();
            }

            return ratingHistory;
        }

        // PUT: api/RatingHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatingHistory(int id, RatingHistory ratingHistory)
        {
            if (id != ratingHistory.UserId)
            {
                return BadRequest();
            }

            _context.Entry(ratingHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingHistoryExists(id))
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

        // POST: api/RatingHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatingHistory>> PostRatingHistory(RatingHistory ratingHistory)
        {
            if (_context.RatingHistories == null)
            {
                return Problem("Entity set 'Cit02Context.RatingHistories'  is null.");
            }

            _context.RatingHistories.Add(ratingHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RatingHistoryExists(ratingHistory.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRatingHistory", new { id = ratingHistory.UserId }, ratingHistory);
        }

        // DELETE: api/RatingHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatingHistory(int id)
        {
            if (_context.RatingHistories == null)
            {
                return NotFound();
            }

            var ratingHistory = await _context.RatingHistories.FindAsync(id);
            if (ratingHistory == null)
            {
                return NotFound();
            }

            _context.RatingHistories.Remove(ratingHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingHistoryExists(int id)
        {
            return (_context.RatingHistories?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}