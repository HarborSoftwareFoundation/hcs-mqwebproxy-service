using Microsoft.AspNetCore.Mvc;

namespace Harbor.Hcs.MqWebProxy.Service.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
