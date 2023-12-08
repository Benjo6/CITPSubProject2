using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var (episodes, metadata) = await _service.GetAllEpisodes(new Filter(page,pageSize,sortBy,asc,conditions));
            
            return Ok(new {episodes, metadata});
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Episodes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEpisode(string id)
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
    public async Task<IActionResult> PutEpisode(string id ,AlterEpisodeDTO episode)
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
    public async Task<IActionResult> PostEpisode([FromBody] AlterEpisodeDTO episode)
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