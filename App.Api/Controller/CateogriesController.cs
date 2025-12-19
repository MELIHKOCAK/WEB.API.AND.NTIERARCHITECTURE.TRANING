using App.Repositories.EFCORE.Categories;
using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Filters.NotFoundFilter;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller
{
    public class CateogriesController(ICategoryService service) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
           => CreateActionResult(await service.GetAllAsync());

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
           => CreateActionResult(await service.GetByIdAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetWithProducts()
           => CreateActionResult(await service.GetCategoryAllWithProductAsync());

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetByIdWithProducts([FromRoute] int id)
           => CreateActionResult(await service.GetCategoryByIdWithProductAsync(id));

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll([FromRoute] int pageNumber, [FromRoute] int pageSize)
           => CreateActionResult(await service.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto requestDto)
           => CreateActionResult(await service.CreateAsync(requestDto));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestDto requestDto)
           => CreateActionResult(await service.UpdateAsync(requestDto));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
           => CreateActionResult(await service.DeleteAsync(id));

    }
}
