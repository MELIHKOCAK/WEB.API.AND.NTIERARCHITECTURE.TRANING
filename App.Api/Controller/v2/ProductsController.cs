using App.Repositories.EFCORE.Products;
using App.Services;
using App.Services.Filters.NotFoundFilter;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;

namespace App.Api.Controller.v2
{
    [EnableRateLimiting("Token")]
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [EnableRateLimiting("Concurrency")]
        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => CreateActionResult(await productService.GetAllAsync());

    }
}