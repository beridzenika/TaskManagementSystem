namespace TaskManagementSystem.Controllers;


using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Services;

[Route("api/[controller]")]
[ApiController]

public class UsersController(IUserService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<List<User>>> GetUsers()
		=> Ok(await service.GetAllUsersAsync());

	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound("Given user id not found.");
        return Ok(user);
    }
}