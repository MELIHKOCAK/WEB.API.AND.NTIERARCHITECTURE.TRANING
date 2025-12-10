using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Products.Create
{
    public class CreateProductRequestDTOValidator:AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Ürün ismi null olamaz.")
                .Length(3,10).WithMessage("Ürün İsmi 3 ile 10 karakter arasında olmalıdır.");

            RuleFor(x => x.Price)
               .GreaterThan(0).WithMessage("Ürün Fiyatı Sıfırdan Büyük Olmalıdır.");

            RuleFor(x => x.Stock)
               .InclusiveBetween(1, 500).WithMessage("Ürün Stock Adeti 1 İle 500 Arasında Olmalıdır.");


        }
    }
}
