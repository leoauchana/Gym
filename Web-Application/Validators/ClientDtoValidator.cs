using FluentValidation;
using Web_Application.DTOs;

namespace Web_Application.Validators;

public class ClientDtoValidator : AbstractValidator<ClientDto.ClientRequest>
{
    public ClientDtoValidator()
    {
        RuleFor(c => c.name)
            .NotEmpty().WithMessage("El {PropertyName} del cliente no puede estar vacío.")
            .MaximumLength(25).WithMessage("El {PropertyName} debe tener como maximo de 40 caracteres.")
            .MinimumLength(3).WithMessage("El {PropertyName} debe tener al menos 6 caracteres.");
        RuleFor(c => c.lastName)
            .NotEmpty().WithMessage("El {PropertyName} del cliente no puede estar vacío.")
            .MaximumLength(25).WithMessage("La {PropertyName} debe tener como maximo de 25 caracteres.")
            .MinimumLength(3).WithMessage("La {PropertyName} debe tener al menos 8 caracteres.");
        RuleFor(c => c.dni)
            .NotEmpty().WithMessage("El {PropertyName} del cliente no puede estar vacío.")
            .Must(d => d.ToString().Length == 7 || d.ToString().Length == 8).WithMessage("El {PropertyName} debe tener exactamente 7 o 8 caracteres.");
        RuleFor(c => c.age)
            .NotEmpty().WithMessage("La {PropertyName} del cliente no puede estar vacía.")
            .InclusiveBetween(18, 65).WithMessage("La {PropertyName} debe estar entre 18 y 65 años.");
        RuleFor(c => c.domicile)
            .NotEmpty().WithMessage("El {PropertyName} del cliente no puede estar vacío.")
            .MaximumLength(50).WithMessage("El {PropertyName} debe tener como maximo de 50 caracteres.")
            .MinimumLength(10).WithMessage("El {PropertyName} debe tener al menos 10 caracteres.");
    }
}
