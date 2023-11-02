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
    public class RatinghistoriesController : ControllerBase
    {
        private readonly Cit02Context _context;

        public RatinghistoriesController(Cit02Context context)
        {
            _context = context;
        }

        // GET: api/Ratinghistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ratinghistory>>> GetRatinghistories()
        {
          if (_context.Ratinghistories == null)
          {
              return NotFound();
          }
            return await _context.Ratinghistories.ToListAsync();
        }

        // GET: api/Ratinghistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ratinghistory>> GetRatinghistory(int id)
        {
          if (_context.Ratinghistories == null)
          {
              return NotFound();
          }
            var ratinghistory = await _context.Ratinghistories.FindAsync(id);

            if (ratinghistory == null)
            {
                return NotFound();
            }

            return ratinghistory;
        }

        // PUT: api/Ratinghistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatinghistory(int id, Ratinghistory ratinghistory)
        {
            if (id != ratinghistory.UserId)
            {
                return BadRequest();
            }

            _context.Entry(ratinghistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatinghistoryExists(id))
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

        // POST: api/Ratinghistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ratinghistory>> PostRatinghistory(Ratinghistory ratinghistory)
        {
          if (_context.Ratinghistories == null)
          {
              return Problem("Entity set 'Cit02Context.Ratinghistories'  is null.");
          }
            _context.Ratinghistories.Add(ratinghistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RatinghistoryExists(ratinghistory.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRatinghistory", new { id = ratinghistory.UserId }, ratinghistory);
        }

        // DELETE: api/Ratinghistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatinghistory(int id)
        {
            if (_context.Ratinghistories == null)
            {
                return NotFound();
            }
            var ratinghistory = await _context.Ratinghistories.FindAsync(id);
            if (ratinghistory == null)
            {
                return NotFound();
            }

            _context.Ratinghistories.Remove(ratinghistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatinghistoryExists(int id)
        {
            return (_context.Ratinghistories?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
