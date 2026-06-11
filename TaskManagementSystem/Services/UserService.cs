namespace TaskManagementSystem.Services;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class UserService(
    AppDbContext context,
    IValidator<UserRequestDto> validator) : IUserService
{
    // GET: api/users
    public async Task<List<UserResponseDto>> GetAllUsersAsync()
        => await context.Users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email
        }).ToListAsync();

    // GET: api/users/{id}
    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var result = await context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).FirstOrDefaultAsync();
        return result;
    }

    // POST: api/users
    public async Task<UserResponseDto> CreateUserAsync(UserRequestDto user)
    {
        var validationResult = await validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email
        };
    }

    // PUT: api/users/{id}
    public async Task<UserResponseDto?> UpdateUserAsync(int id, UserRequestDto user)
    {
        var validationResult = await validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var editedUser = await context.Users.FindAsync(id);

        if (editedUser == null)
        {
            return null;
        }

        editedUser.FirstName = user.FirstName;
        editedUser.LastName = user.LastName;
        editedUser.Email = user.Email;

        await context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = editedUser.Id,
            FirstName = editedUser.FirstName,
            LastName = editedUser.LastName,
            Email = editedUser.Email
        };
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return true;
    }
}