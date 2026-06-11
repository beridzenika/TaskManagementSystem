namespace TaskManagementSystem.Services;

using TaskManagementSystem.DTOs;

public interface IProjectService
{
    Task<List<ProjectResponseDto>> GetAllProjectsAsync();
    Task<ProjectResponseDto?> GetProjectByIdAsync(int id);

    Task<ProjectResponseDto> CreateProjectAsync(ProjectRequestDto project);
    
    Task<ProjectResponseDto?> UpdateProjectAsync(int id, ProjectRequestDto project);
    
    Task<bool> DeleteProjectAsync(int id);
}