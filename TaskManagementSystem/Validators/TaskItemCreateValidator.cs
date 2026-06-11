namespace TaskManagementSystem.Validators;

using FluentValidation;
using TaskManagementSystem.DTOs;
public class TaskItemCreateValidator : AbstractValidator<TaskItemRequestDto>
{
    public TaskItemCreateValidator()
    {
        RuleFor(T => T.Title)
            .NotEmpty().WithMessage("Title is required.");
        RuleFor(t => t.ProjectId)
            .GreaterThan(0).WithMessage("Valid ProjectId is required.");
        RuleFor(t => t.AssignedUserId)
            .GreaterThan(0).WithMessage("Valid AssignedUserId is required.");
    }
}
