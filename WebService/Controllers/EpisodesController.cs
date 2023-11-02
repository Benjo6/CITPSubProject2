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
    public class EpisodesController : ControllerBase
    {
        private readonly Cit02Context _context;

        public EpisodesController(Cit02Context context)
        {
            _context = context;
        }

        // GET: api/Episodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodes()
        {
          if (_context.Episodes == null)
          {
              return NotFound();
          }
            return await _context.Episodes.ToListAsync();
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Episode>> GetEpisode(string id)
        {
          if (_context.Episodes == null)
          {
              return NotFound();
          }
            var episode = await _context.Episodes.FindAsync(id);

            if (episode == null)
            {
                return NotFound();
            }

            return episode;
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpisode(string id, Episode episode)
        {
            if (id != episode.Id)
            {
                return BadRequest();
            }

            _context.Entry(episode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
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

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Episode>> PostEpisode(Episode episode)
        {
          if (_context.Episodes == null)
          {
              return Problem("Entity set 'Cit02Context.Episodes'  is null.");
          }
            _context.Episodes.Add(episode);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EpisodeExists(episode.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEpisode", new { id = episode.Id }, episode);
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(string id)
        {
            if (_context.Episodes == null)
            {
                return NotFound();
            }
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpisodeExists(string id)
        {
            return (_context.Episodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
