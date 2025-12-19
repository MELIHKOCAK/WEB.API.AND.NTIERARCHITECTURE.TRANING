using App.Repositories.EFCORE.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;

namespace App.Services.Categories
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequestDto, Category>();
            CreateMap<UpdateCategoryRequestDto, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
        }
    }
}
