using Microsoft.AspNetCore.Mvc;

namespace Helix.Hcs.MqWebProxy.Service.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
