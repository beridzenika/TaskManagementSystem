namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public interface IUserService
{
    Task<List<UserGetDto>> GetAllUsersAsync();
    Task<UserGetDto?> GetUserByIdAsync(int id);

    Task<User> CreateUserAsync(User user);
    
    Task<User> UpdateUserAsync(int id, User user);
    
    Task<bool> DeleteUserAsync(int id);
}