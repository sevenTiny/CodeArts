using Microsoft.AspNetCore.Mvc;

namespace Demo.ServerSentEvents.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.CacheControl = "no-cache";
            Response.Headers.Connection = "keep-alive";

            for (var i = 1; i <= 5; i++)
            {
                await Response.WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");
                await Response.Body.FlushAsync();

                await Task.Delay(500);
            }
        }
    }
}
