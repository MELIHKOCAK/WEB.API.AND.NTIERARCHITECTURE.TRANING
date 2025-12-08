using App.Services;
using App.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controller
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
            => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber,pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequestDto createDto) => CreateActionResult(await productService.CreateAsync(createDto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await productService.DeleteAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequestDto updateDto)
        { 
            return CreateActionResult(await productService.UpdateAsync(updateDto)); 
        }

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(int id, int quantity)
        {
            return CreateActionResult(await productService.UpdateStockAsync(id, quantity));
        }
    }
}