namespace TaskManagementSystem.Services;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

public class ProjectService: IProjectService
{
    private readonly AppDbContext _context;
    private readonly IValidator<ProjectRequestDto> _validator;

    public ProjectService(AppDbContext context, IValidator<ProjectRequestDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    //GET: api/projects
    public async Task<List<ProjectResponseDto>> GetAllProjectsAsync()
    {
        return await _context.Projects.Select(p => new ProjectResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description
        }).ToListAsync();
    }

    //GET: api/projects/{id}
    public async Task<ProjectResponseDto?> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return null;

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    //POST: api/projects
    public async Task<ProjectResponseDto> CreateProjectAsync(ProjectRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    // PUT: api/projects/{id}
    public async Task<ProjectResponseDto?> UpdateProjectAsync(int id, ProjectRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var project = await _context.Projects.FindAsync(id);
        if (project == null) return null;

        project.Name = dto.Name;
        project.Description = dto.Description;

        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    // DELETE: api/projects/{id}
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}
