namespace TaskManagementSystem.Services;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;
    private readonly IValidator<CommentRequestDto> _validator;

    public CommentService(
        AppDbContext context,
        IValidator<CommentRequestDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<List<CommentResponseDto>> GetAllCommentsAsync()
    {
        return await _context.Comments
            .Select(c => new CommentResponseDto
            {
                Id = c.Id,
                Content = c.Content,
                TaskId = c.TaskId,
            })
            .ToListAsync();
    }

    public async Task<CommentResponseDto?> GetCommentByIdAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
            return null;

        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskId = comment.TaskId
        };
    }

    public async Task<CommentResponseDto> CreateCommentAsync(CommentRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var taskExists = await _context.TaskItems
            .AnyAsync(t => t.Id == dto.TaskId);

        if (!taskExists)
            throw new KeyNotFoundException(
                $"TaskItem with id {dto.TaskId} not found.");

        var comment = new Comment
        {
            Content = dto.Content,
            TaskId = dto.TaskId,
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskId = comment.TaskId,
        };
    }

    public async Task<CommentResponseDto?> UpdateCommentAsync(
        int id,
        CommentRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
            return null;

        var taskExists = await _context.TaskItems
            .AnyAsync(t => t.Id == dto.TaskId);

        if (!taskExists)
            throw new KeyNotFoundException(
                $"TaskItem with id {dto.TaskId} not found.");

        comment.Content = dto.Content;
        comment.TaskId = dto.TaskId;

        await _context.SaveChangesAsync();

        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskId = comment.TaskId
        };
    }

    public async Task<bool> DeleteCommentAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
            return false;

        _context.Comments.Remove(comment);

        await _context.SaveChangesAsync();

        return true;
    }
}