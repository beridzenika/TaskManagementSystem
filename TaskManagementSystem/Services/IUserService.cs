namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(int id);

    Task<UserResponseDto> CreateUserAsync(UserRequestDto user);
    
    Task<UserResponseDto?> UpdateUserAsync(int id, UserRequestDto user);
    
    Task<bool> DeleteUserAsync(int id);
}