using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    
    public UsersController(IUserService service)
    {
        _service = service;
    }
    
    // GET: Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        try
        {
            var users = await _service.GetAllUser();
            return Ok(users);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(string id)
    {
        try
        {
            var user = await _service.GetOneUser(id);
            return Ok(user);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, AlterUserDTO user)
    {
        try
        {
            var result = await _service.UpdateUser(id, user);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var result = await _service.DeleteUser(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}