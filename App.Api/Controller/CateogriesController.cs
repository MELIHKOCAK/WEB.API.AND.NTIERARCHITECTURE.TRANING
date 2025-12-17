using App.Services.Category;
using App.Services.Category.Create;
using App.Services.Category.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller
{
    public class CateogriesController(ICategoryService service) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
           => CreateActionResult(await service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
           => CreateActionResult(await service.GetByIdAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetWithProducts()
           => CreateActionResult(await service.GetCategoryAllWithProductAsync());

        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetByIdWithProducts([FromRoute] int id)
           => CreateActionResult(await service.GetCategoryByIdWithProductAsync(id));

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll([FromRoute] int pageNumber, [FromRoute] int pageSize)
           => CreateActionResult(await service.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto requestDto)
           => CreateActionResult(await service.CreateAsync(requestDto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestDto requestDto)
           => CreateActionResult(await service.UpdateAsync(requestDto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
           => CreateActionResult(await service.DeleteAsync(id));

    }
}
