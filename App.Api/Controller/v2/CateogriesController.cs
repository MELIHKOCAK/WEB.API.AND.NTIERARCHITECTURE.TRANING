using App.Repositories.EFCORE.Categories;
using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Filters.NotFoundFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace App.Api.Controller.v2
{
    [EnableRateLimiting("Token")]
    public class CateogriesController(ICategoryService service) : CustomBaseController
    {
        [EnableRateLimiting("Concurrency")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
           => CreateActionResult(await service.GetAllAsync());
    }
}
