using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetEpisodes(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string, string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var episodes = await _service.GetAllEpisodes(new Filter(page, pageSize, sortBy, asc, conditions));
            var uri = Url.Action("GetEpisodes", new { page = page, pageSize = pageSize, conditions = conditions, sortBy = sortBy, asc = asc });
            return Ok(new { episodes = episodes, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Episodes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEpisode(string id)
    {
        try
        {
            var episode = await _service.GetOneEpisode(id);
            var uri = Url.Action("GetEpisode", new { id = id });
            return Ok(new { episode = episode, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/Episodes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEpisode(string id, AlterEpisodeDTO episode)
    {
        try
        {
            var putEpisode = await _service.UpdateEpisode(id, episode);
            var uri = Url.Action("PutEpisode", new { id = id });
            return Ok(new { episode = putEpisode, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/Episodes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostEpisode([FromBody] AlterEpisodeDTO episode)
    {
        try
        {
            var postEpisode = await _service.AddEpisode(episode);
            var uri = Url.Action("PostEpisode");
            return Ok(new { episode = postEpisode, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/Episodes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEpisode(string id)
    {
        try
        {
            var result = await _service.DeleteEpisode(id);
            var uri = Url.Action("DeleteEpisode", new { id = id });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
