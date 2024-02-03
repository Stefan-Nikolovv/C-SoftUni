using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Web.Infrastructure.ModelBinders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal))
            {
                return new ModelBinderDecimal();
            }
            return null;
        }
    }
}
