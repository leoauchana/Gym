using FluentValidation;
using Web_Application.DTOs;

namespace Web_Application.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto.EmployeeRequest>
{
    public EmployeeDtoValidator()
    {
        RuleFor(e => e.name)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(25).WithMessage("El {PropertyName} debe tener como máximo 40 caracteres.")
            .MinimumLength(3).WithMessage("El {PropertyName} debe tener al menos 8 caracteres.");
        RuleFor(e => e.lastName)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacio.")
            .MaximumLength(25).WithMessage("El {PropertyName} debe tener como máximo 40 caracteres.")
            .MinimumLength(3).WithMessage("El {PropertyName} debe tener al menos 8 caracteres.");
        RuleFor(e => e.age)
            .NotEmpty().WithMessage("La {PropertyName} del empleado no puede estar vacía.")
            .InclusiveBetween(18, 65).WithMessage("La {PropertyName} del empleado debe estar entre 18 y 65 años.");
        RuleFor(e => e.gmail)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacio.")
            .EmailAddress().WithMessage("El {PropertyName} del empleado debe ser un correo electrónico válido.")
            .MaximumLength(40).WithMessage("El {PropertyName} debe tener como máximo 40 carácteres.");
        RuleFor(e => e.domicile)
            .NotEmpty().WithMessage("El {PropertyName} del empleado no puede estar vacío.")
            .MaximumLength(50).WithMessage("La {PropertyName} debe tener como maximo 50 caracteres.")
            .MinimumLength(10).WithMessage("La {PropertyName} debe tener como minimo 10 caracteres.");
        RuleFor(e => e.typeEmployee)
            .NotEmpty().WithMessage("El {PropertyName} no debe ser nulo.");
        RuleFor(e => e.description)
            .NotEmpty().WithMessage("La {PropertyName} no debe estar vacia")
            .MaximumLength(150).WithMessage("La {PropertyName} debe tener como máximo 150 caracteres");
    }
}
