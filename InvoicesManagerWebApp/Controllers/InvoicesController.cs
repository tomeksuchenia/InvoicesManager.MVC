using InvoicesManagerWebApp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesManagerWebApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _inoviceService;
        public InvoicesController(IInvoiceService inoviceService)
        {
            _inoviceService = inoviceService;
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _inoviceService.GetAll();
            return View(invoices);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
