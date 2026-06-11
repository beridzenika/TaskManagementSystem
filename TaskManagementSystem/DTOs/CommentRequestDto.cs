namespace TaskManagementSystem.DTOs
{
    public class CommentRequestDto
    {
        public string Content { get; set; } = string.Empty;
        public int TaskId { get; set; }
    }
}
