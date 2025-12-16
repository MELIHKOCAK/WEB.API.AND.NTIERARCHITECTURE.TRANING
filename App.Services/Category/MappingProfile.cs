using App.Repositories.EFCORE.Products;
using App.Services.Category.Create;
using App.Services.Products;
using App.Services.Products.Create;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Category
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequestDto, Repositories.EFCORE.Categories.Category>();
            CreateMap<Repositories.EFCORE.Categories.Category, CategoryDto>().ReverseMap();
            CreateMap<Repositories.EFCORE.Categories.Category, CategoryWithProductsDto>().ReverseMap();
        }
    }
}
