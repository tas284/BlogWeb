using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : TypeFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyFilter))
    {
    }
}