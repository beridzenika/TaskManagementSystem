namespace TaskManagementSystem.Validators;

using FluentValidation;
using TaskManagementSystem.DTOs;
public class CommentCreateValidator: AbstractValidator<CommentRequestDto>
{
    public CommentCreateValidator()
    {
        RuleFor(c => c.Content)
           .NotEmpty().WithMessage("Content should not be empty.");
    }
}
