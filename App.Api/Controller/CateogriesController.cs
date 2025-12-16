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



    }
}
