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
        try
        {
            var createdUser = await service.CreateUserAsync(user);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = createdUser.Id },
                createdUser);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, UserRequestDto user)
    {
        try
        {
            var updatedUser = await service.UpdateUserAsync(id, user);

            if (updatedUser == null)
            {
                return NotFound("Given user id not found.");
            }

            return Ok(updatedUser);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            var result = await service.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound("Given user id not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}