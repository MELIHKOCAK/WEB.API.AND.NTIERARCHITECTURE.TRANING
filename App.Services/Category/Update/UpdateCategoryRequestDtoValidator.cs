using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Category.Update
{
    public class UpdateCategoryRequestDtoValidator:AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Kategori İsmi Boş Geçilemez")
               .MinimumLength(5).WithMessage("Minumum 5 Karakter Yazılmalıdır.");
        }
    }
}
