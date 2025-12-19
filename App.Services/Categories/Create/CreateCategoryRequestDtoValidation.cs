using FluentValidation;
namespace App.Services.Categories.Create
{
    public class CreateCategoryRequestDtoValidation:AbstractValidator<CreateCategoryRequestDto>
    {
        public CreateCategoryRequestDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Kategori İsmi Boş Geçilemez")
                .MinimumLength(5).WithMessage("Minumum 5 Karakter Yazılmalıdır.");
                
        }
    }
}
