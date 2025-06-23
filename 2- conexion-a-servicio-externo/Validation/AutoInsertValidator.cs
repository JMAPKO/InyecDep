using BACKEND02.DTOs;
using FluentValidation;

namespace BACKEND02.Validation
{
    public class AutoInsertValidator : AbstractValidator<AutoInserDto>
    {

        public AutoInsertValidator() { 
            
            RuleFor(auto => auto.NombreAuto)
                .NotEmpty().WithMessage("El nombre del auto es obligatorio.")
                .Length(3, 50).WithMessage("El nombre del auto debe tener entre 3 y 50 caracteres.");

        }


    }
}
