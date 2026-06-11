namespace TaskManagementSystem.Validators;

using FluentValidation;
using TaskManagementSystem.DTOs;

public class ProjectCreateValidator : AbstractValidator<ProjectRequestDto>
{
    public ProjectCreateValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is Required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
