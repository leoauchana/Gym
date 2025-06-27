using FluentValidation;
using Web_Application.DTOs;

namespace Web_Application.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(40).WithMessage("El {PropertyName} debe tener como maximo de 40 caracteres.")
            .MinimumLength(6).WithMessage("El {PropertyName} debe tener al menos 6 caracteres.");
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("La {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(25).WithMessage("La {PropertyName} debe tener como maximo de 25 caracteres.")
            .MinimumLength(8).WithMessage("La {PropertyName} debe tener al menos 8 caracteres.");
    }
}
