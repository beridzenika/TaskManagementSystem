namespace TaskManagementSystem.Services;

using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class UserService(AppDbContext context) : IUserService
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

    public Task<User> UpdateUserAsync(int id, User user)
    {
        // Implementation to update an existing user
        throw new NotImplementedException();
    }
    public Task<bool> DeleteUserAsync(int id)
    {
        // Implementation to delete a user by ID
        throw new NotImplementedException();
    }
}