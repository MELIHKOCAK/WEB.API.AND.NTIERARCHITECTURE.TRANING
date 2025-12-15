using App.Services.Category;
using App.Services.Category.Create;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller
{
    public class CateogriesController(ICategoryService service):CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto requestDto)
        {
            return CreateActionResult(await service.CreateAsync(requestDto));
        }
    }
}
