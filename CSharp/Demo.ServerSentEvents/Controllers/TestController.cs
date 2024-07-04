using Microsoft.AspNetCore.Mvc;

namespace Demo.ServerSentEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.CacheControl = "no-cache";
            Response.Headers.Connection = "keep-alive";

            for (var i = 1; i <= 20; i++)
            {
                await Response.WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");
                await Response.Body.FlushAsync();

                await Task.Delay(500);
            }
        }
    }
}
