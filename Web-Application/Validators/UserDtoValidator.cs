using FluentValidation;
using Web_Application.DTOs;

namespace Web_Application.Validators;

public class UserDtoValidator : AbstractValidator<UserDto.UserRequest>
{
    public UserDtoValidator()
    {
        RuleFor(u => u.userName)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(20).WithMessage("El {PropertyName} debe tener como maximo de 20 caracteres.")
            .MinimumLength(6).WithMessage("El {PropertyName} debe tener al menos 6 caracteres.");
        RuleFor(u => u.password)
            .NotEmpty().WithMessage("La {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(20).WithMessage("La {PropertyName} debe tener como maximo de 25 caracteres.")
            .MinimumLength(8).WithMessage("La {PropertyName} debe tener al menos 8 caracteres.");
    }
}
