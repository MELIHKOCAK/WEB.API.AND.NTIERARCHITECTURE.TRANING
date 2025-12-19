using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace App.Services.Filters.NotFoundFilter
{
    public class EntityIdResolver<TId> : IIdResolver<TId>
    {
        public bool CanResolve(object value) => value.GetType().IsClass;
        public TId Resolve(object value)
        {
            var prop = value.GetType().GetProperty("id",
              BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            var id = prop?.GetValue(value);
            return (TId)Convert.ChangeType(id, typeof(TId))!;
        }
    }
}
