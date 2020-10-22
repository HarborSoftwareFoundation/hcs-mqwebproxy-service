using Microsoft.AspNetCore.Mvc;

namespace Harbor.Hcs.MqWebProxy.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
