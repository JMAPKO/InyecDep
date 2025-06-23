using BACKEND02.DTOs;
using FluentValidation;

namespace BACKEND02.Validation
{
    public class MarcaInsertValidator : AbstractValidator<MarcaInsertDto>
    {

        public MarcaInsertValidator()
        {
            RuleFor(marca => marca.NombreMarca).NotEmpty();
        }

    }
}
