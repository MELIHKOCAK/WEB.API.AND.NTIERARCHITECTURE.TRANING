using App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
namespace App.Services.Filters;

public class NotFoundFilter<T, TId> : Attribute, IAsyncActionFilter where T : class where TId:struct
{
    private readonly IGenericRepositoryBase<T, TId> _genericRepository;

    public NotFoundFilter(IGenericRepositoryBase<T, TId> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();
        if (idValue == null)
        {
            await next();
            return;
        }

        if(idValue is not TId id)
        {
            await next();
            return;
        }

        var anyEntity = await _genericRepository.AnyAsync(id);
        
        if (!anyEntity)
        {
            var entityName = typeof(T).Name;
            var actionName = context.ActionDescriptor.DisplayName;
            var result = ServiceResult.Fail($"data bulunumamıştır({entityName})({actionName}).");

            context.Result = new NotFoundObjectResult(result);
            return;
        }

        await next();
    }
}
