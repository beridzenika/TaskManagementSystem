namespace TaskManagementSystem.Services;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class TaskItemService : ITaskItemService
{
    private readonly AppDbContext _context;
    private readonly IValidator<TaskItemRequestDto> _validator;

    public TaskItemService(AppDbContext context, IValidator<TaskItemRequestDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    // GET: api/taskitems
    public async Task<List<TaskItemResponseDto>> GetAllTaskItemsAsync()
    {
        return await _context.TaskItems
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ProjectId = t.ProjectId,
                AssignedUserId = t.AssignedUserId,
                Status = t.Status
            })
            .ToListAsync();
    }

    // GET: api/taskitems/{id}
    public async Task<TaskItemResponseDto?> GetTaskItemByIdAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return null;

        return new TaskItemResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            Status = task.Status
        };
    }

    // POST: api/taskitems
    public async Task<TaskItemResponseDto> CreateTaskItemAsync(TaskItemRequestDto dto)
    {
        //validation
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        // FK 
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists)
            throw new KeyNotFoundException($"Project with id {dto.ProjectId} not found.");

        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.AssignedUserId);
        if (!userExists)
            throw new KeyNotFoundException($"User with id {dto.AssignedUserId} not found.");

        // create
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            ProjectId = dto.ProjectId,
            AssignedUserId = dto.AssignedUserId,
            Status = false
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return new TaskItemResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            Status = task.Status
        };
    }

    // PUT: api/taskitems/{id}
    public async Task<TaskItemResponseDto?> UpdateTaskItemAsync(int id, TaskItemRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return null;

        // FK 
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists)
            throw new KeyNotFoundException($"Project with id {dto.ProjectId} not found.");

        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.AssignedUserId);
        if (!userExists)
            throw new KeyNotFoundException($"User with id {dto.AssignedUserId} not found.");

        // update
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.ProjectId = dto.ProjectId;
        task.AssignedUserId = dto.AssignedUserId;

        await _context.SaveChangesAsync();

        return new TaskItemResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            Status = task.Status
        };
    }

    // DELETE: api/taskitems/{id}
    public async Task<bool> DeleteTaskItemAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}