using Microsoft.AspNetCore.Mvc;

namespace InvoicesManagerWebApp.Controllers
{
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
