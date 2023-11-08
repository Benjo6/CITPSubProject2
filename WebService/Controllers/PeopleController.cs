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