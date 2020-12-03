using Microsoft.AspNetCore.Mvc;

namespace Parusnik.Pcs.MqWebProxy.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
