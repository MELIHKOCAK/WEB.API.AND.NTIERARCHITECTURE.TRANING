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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequestDto createDto) => CreateActionResult(await productService.CreateAsync(createDto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult<Task<ServiceResult>>(await productService.DeleteAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequestDto updateDto)
        { 
            return CreateActionResult<Task<ServiceResult>>(await productService.UpdateAsync(updateDto)); 
        }
    }
}