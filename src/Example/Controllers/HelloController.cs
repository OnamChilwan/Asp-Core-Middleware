namespace Example.Controllers
{
    using Microsoft.AspNet.Mvc;

    [Route("api")]
    public class HelloController : Controller
    {
        [HttpGet("hello/world")]
        public IActionResult Get()
        {
            return this.Ok("Hello World");
        }
    }
}