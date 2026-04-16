using CRUD.DTOs;
using FluentValidation;

namespace CRUD.Validator
{
    public class BookInsertValidator: AbstractValidator<BookInsertDto>
    {

        public BookInsertValidator() { 

        RuleFor(x => x.Title).NotEmpty().WithMessage("El title es obligatorio")
                .Length(2,20).WithMessage("El titulo debe tener de al menos 2, maximo 20 caracteres");

            RuleFor(x => x.Description).NotEmpty().WithMessage("La descripcion es obligatoria")
                .Length(10, 50).WithMessage("La descripcion debe tener al menos 10 caracteres, maximo 50 caracteres");

            RuleFor(x => x.Page).NotEmpty().WithMessage("El numero de paginas es obligatorio")
                .GreaterThan(2).WithMessage("El titulo debe tener de 2 a 20 caracteres")
                .LessThanOrEqualTo(3200).WithMessage("El maximo de paginas es de 3200");

        }
    }
}
