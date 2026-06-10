namespace TaskManagementSystem.Controllers;


using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Services;
using TaskManagementSystem.DTOs;

[Route("api/[controller]")]
[ApiController]

public class UsersController(IUserService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<List<UserGetDto>>> GetUsers()
		=> Ok(await service.GetAllUsersAsync());

	[HttpGet("{id}")]
	public async Task<ActionResult<UserGetDto>> GetUser(int id)
    {
        var user = await service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound("Given user id not found.");
        return Ok(user);
    }
}