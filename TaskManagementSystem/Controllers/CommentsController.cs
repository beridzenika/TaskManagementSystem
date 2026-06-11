namespace TaskManagementSystem.Controllers;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Services;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _commentService.GetAllCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);

        if (comment == null)
            return NotFound("Given id not found.");

        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(
        [FromBody] CommentRequestDto dto)
    {
        try
        {
            var created = await _commentService.CreateCommentAsync(dto);

            return CreatedAtAction(
                nameof(GetComment),
                new { id = created.Id },
                created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(
                ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(
        int id,
        [FromBody] CommentRequestDto dto)
    {
        try
        {
            var updated =
                await _commentService.UpdateCommentAsync(id, dto);

            if (updated == null)
                return NotFound("Given id not found.");

            return Ok(updated);
        }
        catch (ValidationException ex)
        {
            return BadRequest(
                ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            var deleted =
                await _commentService.DeleteCommentAsync(id);

            if (!deleted)
                return NotFound("Given id not found.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}