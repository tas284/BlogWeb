using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogWeb.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
        }
    }
}
