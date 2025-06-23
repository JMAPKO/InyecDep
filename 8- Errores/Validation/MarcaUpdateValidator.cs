using System.Data;
using BACKEND02.DTOs;
using FluentValidation;

namespace BACKEND02.Validation
{
    public class MarcaUpdateValidator : AbstractValidator<MarcaUpdateDto>
    {
        public MarcaUpdateValidator()
        {
            RuleFor(marca => marca.NombreMarca).NotEmpty();
            RuleFor(marca => marca.IdMarca).GreaterThan(0)
                .WithMessage("El Id de la marca debe ser mayor que 0.");
        }
    }
}
