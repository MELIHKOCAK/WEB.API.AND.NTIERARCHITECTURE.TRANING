using AutoMapper;
using App.Repositories.EFCORE.Products;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Category.Create;

namespace App.Services.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequestDto,Product>();
            CreateMap<CreateCategoryRequestDto, Repositories.EFCORE.Categories.Category>();
        }
    }
}
