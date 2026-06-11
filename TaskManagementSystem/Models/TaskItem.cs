namespace TaskManagementSystem.Models;

public class TaskItem
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool Status { get; set; }
	public int ProjectId { get; set; }
	public int AssignedUserId { get; set; }

	public Project Project { get; set; }
    public User AssignedUser { get; set; }
    public List<Comment> Comments { get; set; } = new();

}
