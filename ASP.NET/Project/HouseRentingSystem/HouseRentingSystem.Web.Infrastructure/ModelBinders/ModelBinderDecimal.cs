using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Globalization;



namespace HouseRentingSystem.Web.Infrastructure.ModelBinders
{
    public class ModelBinderDecimal : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
           if(bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            
           ValueProviderResult valueResult = 
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if(valueResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueResult.FirstValue))
            {
                decimal parsedValue = 0m;
                bool success = false;

                try
                {
                    string formDecimalValue = valueResult.FirstValue;
                    formDecimalValue = formDecimalValue.Replace(",",
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    formDecimalValue = formDecimalValue.Replace(".",
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    parsedValue = Convert.ToDecimal(formDecimalValue);
                    success = true; 
                }
                catch (FormatException FE)
                {

                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, FE, bindingContext.ModelMetadata);
                    throw;
                }
                if(success)
                {
                    bindingContext.Result = ModelBindingResult.Success(parsedValue);

                }
            }
            return Task.CompletedTask;
        }
    }
}
