using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Infrastructure;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class EpisodesController : ControllerBase
{
    private readonly IEpisodesService _service;

    public EpisodesController(IEpisodesService service)
    {
        _service = service;
    }
    
    // GET: Episodes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EpisodeDTO>>> GetEpisodes()
    {
        try
        {
            var episodes = await _service.GetAllEpisodes();
            return Ok(episodes);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Episodes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EpisodeDTO>> GetEpisode(string id)
    {
        try
        {
            var episode = await _service.GetOneEpisode(id);
            return Ok(episode);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Episodes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<EpisodeDTO>> PutEpisode(string id ,AlterEpisodeDTO episode)
    {
        try
        {
            var putEpisode = await _service.UpdateEpisode(id, episode);
            return Ok(putEpisode);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Episodes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<EpisodeDTO>> PostEpisode([FromBody] AlterEpisodeDTO episode)
    {
        try
        {
            var postEpisode = await _service.AddEpisode(episode);
            return Ok(postEpisode);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Episodes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEpisode(string id)
    {
        try
        {
            var result = await _service.DeleteEpisode(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}