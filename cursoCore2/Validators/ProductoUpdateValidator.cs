using cursoCore2API.DTOs;
using FluentValidation;

namespace cursoCore2API.Validators
{
    public class ProductoUpdateValidator : AbstractValidator<ProductoUpdateDto>
    {
        public ProductoUpdateValidator() 
        {

            RuleFor(x => x.nombre).NotEmpty().WithMessage("El nombre es obligatorio");


        }
    }
}
