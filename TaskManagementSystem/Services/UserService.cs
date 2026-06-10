namespace TaskManagementSystem.Services;

using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<List<UserGetDto>> GetAllUsersAsync()
        => await context.Users.Select(u => new UserGetDto
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email
        }).ToListAsync();

    public async Task<UserGetDto?> GetUserByIdAsync(int id)
    {
        var result = await context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserGetDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).FirstOrDefaultAsync();
        return result;
    }
    public Task<User> CreateUserAsync(User user)
    {
        // Implementation to create a new user
        throw new NotImplementedException();
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