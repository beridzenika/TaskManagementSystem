namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;

public interface ICommentService
{
    Task<List<CommentResponseDto>> GetAllCommentsAsync();
    Task<CommentResponseDto?> GetCommentByIdAsync(int id);

    Task<CommentResponseDto> CreateCommentAsync(CommentRequestDto comment);

    Task<CommentResponseDto?> UpdateCommentAsync(int id, CommentRequestDto comment);

    Task<bool> DeleteCommentAsync(int id);
}
