namespace TaskManagementSystem.Services;
using TaskManagementSystem.Models;

public class UserService : IUserService
{
    static List<User> users =
    [
        new User { Id = 1, FirstName = "Alice", LastName = "Doe", Email = "AliceDoe@gmail.com" },
        new User { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "BobSmith@gmail.com" },
        new User { Id = 3, FirstName = "Charlie", LastName = "Brown", Email = "CharlieBrown@gmail.com" }
    ];

    public async Task<List<User>> GetAllUsersAsync()
        => await Task.FromResult(users);

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var result = users.FirstOrDefault(u => u.Id == id);
        return await Task.FromResult(result);
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