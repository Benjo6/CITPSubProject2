using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Infrastructure;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<GetAllPersonDTO>>> GetPeople()
    {
        try
        {
            var people = await _service.GetAllPerson();
            return Ok(people);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: People/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetOnePersonDTO>> GetPerson(string id)
    {
        try
        {
            var person = await _service.GetOnePerson(id);
            return Ok(person);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: People/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdatePersonDTO>> PutPerson(string id, AlterPersonDTO person)
    {
        try
        {
            var putPerson = await _service.UpdatePerson(id, person);
            return Ok(putPerson);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ActorsByName")]
    public async Task<ActionResult<IEnumerable<ActorBy>>> FindActorsByName([FromQuery] string name)
    {
        try
        {
            var actors = await _service.FindActorsByName(name);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ActorsByMovie")]
    public async Task<ActionResult<IEnumerable<ActorBy>>> FindActorsByMovie([FromQuery] string movieId)
    {
        try
        {
            var actors = await _service.FindActorsByMovie(movieId);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PopularActor")]
    public async Task<ActionResult<IEnumerable<PopularActor>>> GetPopGetPopularActorsInMovie([FromQuery] string movieId)
    {
        try
        {
            var actors = await _service.GetPopularActorsInMovie(movieId);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CoPopularActor")]
    public async Task<ActionResult<IEnumerable<PopularCoPlayer>>> GetPopularCoPlayers([FromQuery] string actorName)
    {
        try
        {
            var actors = await _service.GetPopularCoPlayers(actorName);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ActorWords")]
    public async Task<ActionResult<IEnumerable<PersonWord>>> PersonWords([FromQuery] string word, int frequency)
    {
        try
        {
            var actors = await _service.PersonWords(word, frequency);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // POST: People
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson([FromBody] AlterPersonDTO person)
    {
        try
        {
            var postPerson = await _service.AddPerson(person);
            return Ok(postPerson);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }
}