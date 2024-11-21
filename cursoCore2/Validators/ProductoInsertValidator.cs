using cursoCore2API.DTOs;
using FluentValidation;

namespace cursoCore2API.Validators
{
    public class ProductoInsertValidator : AbstractValidator<ProductoInsertDto>
    {
        public ProductoInsertValidator() 
        {
            RuleFor(x => x.nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            //RuleFor(x => x.nombre).Length(2, 20).WithMessage("El nombre es obligatorio y debe medir de 2 a 20 caracteres"); 
        }
    }
}
