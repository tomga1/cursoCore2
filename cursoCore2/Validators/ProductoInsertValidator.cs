using cursoCore2API.DTOs;
using FluentValidation;

namespace cursoCore2API.Validators
{
    public class ProductoInsertValidator : AbstractValidator<ProductoInsertDto>
    {
        public ProductoInsertValidator() 
        {
            RuleFor(x => x.nombre).NotEmpty().WithMessage("El nombre es obligatorio"); 
        }
    }
}
