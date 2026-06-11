namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;
public interface ITaskItemService
{
    Task<List<TaskItemResponseDto>> GetAllTaskItemsAsync();
    Task<TaskItemResponseDto?> GetTaskItemByIdAsync(int id);

    Task<TaskItemResponseDto> CreateTaskItemAsync(TaskItemRequestDto taskItem);

    Task<TaskItemResponseDto?> UpdateTaskItemAsync(int id, TaskItemRequestDto taskItem);

    Task<bool> DeleteTaskItemAsync(int id);

}
