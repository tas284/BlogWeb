using BlogWeb.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace BlogWeb.Attributes
{
    public class ApiKeyFilter : IAsyncActionFilter
    {
        private readonly ApiConfiguration _apiConfiguration;
        
        public ApiKeyFilter(IOptions<ApiConfiguration> options)
        {
            _apiConfiguration = options.Value;
        }

        public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Query.TryGetValue(_apiConfiguration.ApiKeyName, out var extractedApiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    Content = $"ApiKey is required"
                };

                return;
            }

            if (!_apiConfiguration.ApiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = $"ApiKey not found"
                };
                return;
            }

            await next();
        }
    }
}
