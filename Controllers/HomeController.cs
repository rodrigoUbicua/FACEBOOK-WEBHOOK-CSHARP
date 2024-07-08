using Microsoft.AspNetCore.Mvc;

namespace FACEBOOK_WEBHOOK_C.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the Facebook Webhook API!");
        }
    }
}
