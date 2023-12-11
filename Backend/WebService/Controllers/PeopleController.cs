using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
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
        [FromQuery] Dictionary<string,string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var (people, metadata)= await _service.GetAllPerson(new Filter(page,pageSize,sortBy,asc,conditions));
            return Ok(new {people,metadata});
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // GET: People/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(string id)
    {
        try
        {
            var person = await _service.GetOnePerson(id);
            return Ok(person);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    // GET: People/ActorsByName/Tom Hanks
    [HttpGet("ActorsByName")]
    public async Task<IActionResult> FindActorsByName([FromQuery] string name)
    {
        try
        {
            var actors = await _service.FindActorsByName(name);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // GET: People/ActorsByMovie/1id1
    [HttpGet("ActorsByMovie")]
    public async Task<IActionResult> FindActorsByMovie([FromQuery] string movieId)
    {
        try
        {
            var actors = await _service.FindActorsByMovie(movieId);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    

    [HttpGet("CoPopularActor")]
    public async Task<IActionResult> GetPopularCoPlayers([FromQuery] string actorName)
    {
        try
        {
            var actors = await _service.GetPopularCoPlayers(actorName);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // PUT: People/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(string id, AlterPersonDTO person)
    {
        try
        {
            var putPerson = await _service.UpdatePerson(id, person);
            return Ok(putPerson);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // POST: People
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostPerson([FromBody] AlterPersonDTO person)
    {
        try
        {
            var postPerson = await _service.AddPerson(person);
            return Ok(postPerson);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // DELETE: People/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(string id)
    {
        try
        {
            var result = await _service.DeletePerson(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
}