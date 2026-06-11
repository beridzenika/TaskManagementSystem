namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(int id);

    Task<UserResponseDto> CreateUserAsync(UserRequestDto user);
    
    Task<User> UpdateUserAsync(int id, User user);
    
    Task<bool> DeleteUserAsync(int id);
}