using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Filters.NotFoundFilter
{
    public interface IIdResolver<TId>
    {
        bool CanResolve(object value);
        TId Resolve(object value);
    }
}
