using BACKEND02.DTOs;
using FluentValidation;

namespace BACKEND02.Validation
{
    public class AutoUpdateValidation : AbstractValidator<AutoUpdateDto>
    {

        public AutoUpdateValidation() {
            
            /*RuleFor(auto => auto.Id)
                .NotEmpty().WithMessage("El ID del auto es obligatorio.")
                .GreaterThan(0).WithMessage("El ID del auto debe ser mayor que cero.");*/
            
            RuleFor(auto => auto.NombreAuto).NotEmpty();
            
            RuleFor(auto => auto.Precio)
                .GreaterThan(0).WithMessage("El precio del auto debe ser mayor que cero.");

            RuleFor(a => a.IdMarca).GreaterThan(0);

        }

    }
}
