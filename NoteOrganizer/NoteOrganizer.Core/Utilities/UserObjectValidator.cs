using FluentValidation;
using NoteOrganizer.Core.DTO;

namespace NoteOrganizer.Core.Utilities
{
    public class UserObjectValidator : AbstractValidator<UserDto>
    {
        public UserObjectValidator()
        {
            RuleFor(UserDto => UserDto.UserName)
                .NotEmpty().WithMessage("the username must not be empty").NotNull().WithMessage("the user must have a username");
            RuleFor(UserDto => UserDto.Password)
                .NotNull().WithMessage("Password is required")
                .NotEmpty().WithMessage("Password cant be an empty string")
                .MinimumLength(6).WithMessage("Password must contain at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }
    }
}
