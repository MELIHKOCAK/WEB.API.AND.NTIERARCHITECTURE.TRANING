using AutoMapper;
using App.Repositories.EFCORE.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;

namespace App.Services.Products
{
    public partial class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequestDto,Product>();
            CreateMap<UpdateProductRequestDto,Product>();
        }
    }
}
