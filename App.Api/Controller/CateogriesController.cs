using App.Services.Category;
using App.Services.Category.Create;
using App.Services.Category.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller
{
    public class CateogriesController(ICategoryService service) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto requestDto) =>
            CreateActionResult(await service.CreateAsync(requestDto));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
    CreateActionResult(await service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id) =>
            CreateActionResult(await service.GetByIdAsync(id));

        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetCategoryByIdWithProducts([FromRoute] int id) =>
            CreateActionResult(await service.GetCategoryByIdWithProductAsync(id));

        [HttpGet("{pageSize:int}/{pageNumber:int}")]
        public async Task<IActionResult> GetCategoryByIdWithProducts([FromRoute] int pageSize, [FromRoute] int pageNumber) =>
          CreateActionResult(await service.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpGet("/products")]
        public async Task<IActionResult> GetCategoryWithProducts() =>
           CreateActionResult(await service.GetCategoryAllWithProductAsync());

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequestDto requestDto) =>
            CreateActionResult(await service.UpdateAsync(requestDto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id) =>
            CreateActionResult(await service.DeleteAsync(id));

    }
}
