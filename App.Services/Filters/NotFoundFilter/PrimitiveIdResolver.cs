using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Filters.NotFoundFilter
{
    public class PrimitiveIdResolver<TId> : IIdResolver<TId>
    {
        public bool CanResolve(object value) => value.GetType().IsPrimitive;

        public TId Resolve(object value)
        {
            return (TId)Convert.ChangeType(value, typeof(TId));
        }
    }
}
