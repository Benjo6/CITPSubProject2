using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string, string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var users = await _service.GetAllUser(new Filter(page, pageSize, sortBy, asc, conditions));
            var listUri = Url.Action("GetUsers", new { page = page, pageSize = pageSize, conditions = conditions, sortBy = sortBy, asc = asc });
            var usersWithUris = users.Select(u => new
            {
                user = u,
                uri = Url.Action("GetUser", new { id = u.Id })
            });

            return Ok(new { users = usersWithUris, uri = listUri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Users/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(string id)
    {
        try
        {
            var user = await _service.GetOneUser(id);
            var uri = Url.Action("GetUser", new { id = id });
            return Ok(new { user = user, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Users/5
    [HttpGet("ByUsername/{username}")]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        try
        {
            var user = await _service.GetUserByUsername(username);
            var uri = Url.Action("GetUserByUsername", new { username = username });
            return Ok(new { user = user, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: Users/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, AlterUserDTO user)
    {
        try
        {
            var result = await _service.UpdateUser(id, user);
            var uri = Url.Action("PutUser", new { id = id });
            return Ok(new { user = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    // DELETE: api/Users/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var result = await _service.DeleteUser(id);
            var uri = Url.Action("DeleteUser", new { id = id });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
