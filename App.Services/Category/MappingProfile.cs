using App.Services.Category.Create;
using AutoMapper;

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
