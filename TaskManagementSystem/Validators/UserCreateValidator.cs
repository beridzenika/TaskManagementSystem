namespace TaskManagementSystem.Validators;

using FluentValidation;
using TaskManagementSystem.DTOs;

public class UserCreateValidator: AbstractValidator<UserRequestDto>
{
    public UserCreateValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required.");
        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
