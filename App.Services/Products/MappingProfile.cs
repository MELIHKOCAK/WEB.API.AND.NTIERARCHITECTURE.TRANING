using AutoMapper;
using App.Repositories.EFCORE.Products;
using App.Services.Products.Create;

namespace App.Services.Products
{
    public partial class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequestDto,Product>();
        }
    }
}
