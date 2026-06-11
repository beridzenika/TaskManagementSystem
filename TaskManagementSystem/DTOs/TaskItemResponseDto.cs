namespace TaskManagementSystem.DTOs
{
    public class TaskItemResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
    }
}
