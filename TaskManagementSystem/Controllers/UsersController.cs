namespace TaskManagementSystem.Controllers;


using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Services;
using TaskManagementSystem.DTOs;
using FluentValidation;

[Route("api/[controller]")]
[ApiController]

public class UsersController(
    IUserService service,
    IValidator<UserRequestDto> validator) : ControllerBase
{
    [HttpGet]
	public async Task<ActionResult<List<UserResponseDto>>> GetUsers()
		=> Ok(await service.GetAllUsersAsync());

	[HttpGet("{id}")]
	public async Task<ActionResult<UserResponseDto>> GetUser(int id)
    {
        var user = await service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound("Given user id not found.");
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserRequestDto user)
    {
        var validationResult = await validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var createdUser = await service.CreateUserAsync(user);

        return CreatedAtAction(
            nameof(GetUser),
            new { id = createdUser.Id },
            createdUser);
    }


}