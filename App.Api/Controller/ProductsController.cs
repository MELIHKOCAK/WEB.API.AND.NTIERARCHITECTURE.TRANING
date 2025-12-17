using App.Services;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controller
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => CreateActionResult(await productService.GetAllAsync());

        [HttpGet("TopPrice/{count:int}")]
        public async Task<IActionResult> GetTopPrice([FromRoute]int count)
            => CreateActionResult(await productService.GetTopPriceProductsAsync(count));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
            => CreateActionResult(await productService.GetByIdAsync(id));

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll([FromRoute]int pageNumber, [FromRoute]int pageSize)
            => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequestDto createDto) 
            => CreateActionResult(await productService.CreateAsync(createDto));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequestDto updateDto)
            => CreateActionResult(await productService.UpdateAsync(updateDto));

        [HttpPatch("stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromRoute] int quantity)
            => CreateActionResult(await productService.UpdateStockAsync(id, quantity));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id) 
            => CreateActionResult(await productService.DeleteAsync(id));
    }
}