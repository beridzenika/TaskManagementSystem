namespace TaskManagementSystem.Controllers;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Services;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskItems()
    {
        var tasks = await _taskItemService.GetAllTaskItemsAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskItem(int id)
    {
        var task = await _taskItemService.GetTaskItemByIdAsync(id);

        if (task == null)
        {
            return NotFound("Given id not found.");
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskItem([FromBody] TaskItemRequestDto dto)
    {
        try
        {
            var created = await _taskItemService.CreateTaskItemAsync(dto);
            return CreatedAtAction(
                nameof(GetTaskItem),
                new { id = created.Id },
                created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] TaskItemRequestDto dto)
    {
        try
        {
            var updated = await _taskItemService.UpdateTaskItemAsync(id, dto);

            if (updated == null)
            {
                return NotFound("Given id not found.");
            }
            return Ok(updated);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        try
        {
            var deleted = await _taskItemService.DeleteTaskItemAsync(id);
            if (!deleted)
            {
                return NotFound("Given id not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}