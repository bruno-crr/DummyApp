using Dominio.Entity;
using FluentValidation;

namespace Dominio.Validation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Title).NotNull().NotEmpty().Length(0, 10).WithMessage("Title inválido");

        }        
    }

}
