using BACKEND02.DTOs;
using FluentValidation;

namespace BACKEND02.Validation
{
    public class MarcaInsertValidator : AbstractValidator<MarcaDto>
    {

        public MarcaInsertValidator()
        {
            RuleFor(marca => marca.NombreMarca).NotEmpty();
        }

    }
}
