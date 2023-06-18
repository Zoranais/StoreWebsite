using Microsoft.AspNetCore.Mvc;

namespace TestStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
