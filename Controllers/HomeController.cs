using BlogWeb.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    [ApiController]
    [Route("/v1")]
    [ApiKey]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get() => Ok();
    }
}
