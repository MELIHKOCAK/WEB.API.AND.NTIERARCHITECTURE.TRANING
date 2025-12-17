using FluentValidation;

namespace App.Services.Products.Update;
public class UpdateProductRequestDtoValidator : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductRequestDtoValidator()
    {
        RuleFor(x => x.price)
            .NotNull().NotEmpty().WithMessage("Ürünün Fiyat Alanı Boş Olamaz.")
            .GreaterThan(0).WithMessage("Ürünün Fiyatı 0'dan Büyük Olmalıdır.");

        RuleFor(x => x.stock)
            .NotNull().NotEmpty().WithMessage("Ürünün Fiyat Alanı Boş Olamaz.")
            .GreaterThan(0).WithMessage("Ürünün Fiyatı 0'dan Büyük Olmalıdır.")
             .InclusiveBetween(1, 500).WithMessage("Ürün Stock Adeti 1 İle 500 Arasında Olmalıdır."); ;

        RuleFor(x => x.name)
             .NotNull().NotEmpty().WithMessage("Ürünün Fiyat Alanı Boş Olamaz.")
             .Length(3, 10).WithMessage("Ürün İsmi 3 ile 10 karakter arasında olmalıdır.");
    }
}
