using Common;
using Common.DataTransferObjects;
using Common.Identity;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPeopleService _service;
    public PeopleController(IPeopleService service)
    {
        _service = service;
    }

    // GET: People
    [HttpGet]
    public async Task<IActionResult> GetPeople(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string, string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var people = await _service.GetAllPerson(new Filter(page, pageSize, sortBy, asc, conditions));
            var listUri = Url.Action("GetPeople", new { page = page, pageSize = pageSize, conditions = conditions, sortBy = sortBy, asc = asc });
            var peopleWithUris = people.Select(p => new
            {
                person = p,
                uri = Url.Action("GetPerson", new { id = p.Id })
            });

            return Ok(new { people = peopleWithUris, uri = listUri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: People/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(string id)
    {
        try
        {
            var person = await _service.GetOnePerson(id);
            var uri = Url.Action("GetPerson", new { id = id });
            return Ok(new { person = person, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: People/ActorsByName/TomHanks
    [HttpGet("ActorsByName")]
    public async Task<IActionResult> FindActorsByName([FromQuery] string name)
    {
        try
        {
            var actors = await _service.FindActorsByName(name);
            var uri = Url.Action("FindActorsByName", new { name = name });
            return Ok(new { actors = actors, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: People/ActorsByMovie/1id1
    [HttpGet("ActorsByMovie")]
    public async Task<IActionResult> FindActorsByMovie([FromQuery] string movieId)
    {
        try
        {
            var actors = await _service.FindActorsByMovie(movieId);
            var uri = Url.Action("FindActorsByMovie", new { movieId = movieId });
            return Ok(new { actors = actors, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("CoPopularActor")]
    public async Task<IActionResult> GetPopularCoPlayers([FromQuery] string actorName)
    {
        try
        {
            var actors = await _service.GetPopularCoPlayers(actorName);
            var uri = Url.Action("GetPopularCoPlayers", new { actorName = actorName });
            return Ok(new { actors = actors, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: People/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> PutPerson(string id, AlterPersonDTO person)
    {
        try
        {
            var putPerson = await _service.UpdatePerson(id, person);
            var uri = Url.Action("PutPerson", new { id = id });
            return Ok(new { person = putPerson, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: People
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> PostPerson([FromBody] AlterPersonDTO person)
    {
        try
        {
            var postPerson = await _service.AddPerson(person);
            var uri = Url.Action("PostPerson");
            return Ok(new { person = postPerson, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: People/5
    [HttpDelete("{id}")]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> DeletePerson(string id)
    {
        try
        {
            var result = await _service.DeletePerson(id);
            var uri = Url.Action("DeletePerson", new { id = id });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
