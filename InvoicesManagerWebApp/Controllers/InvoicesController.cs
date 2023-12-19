using InvoicesManagerWebApp.Extensions;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using InvoicesManagerWebApp.Services;
using InvoicesManagerWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesManagerWebApp.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        
        private readonly IInvoiceService _invoiceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvoicesController(IInvoiceService inoviceService, IHttpContextAccessor httpContextAccessor)
        {
            _invoiceService = inoviceService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllUserInvoices();
            return View(invoices);
        }

        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceService.GetInvoiceUserById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        public async Task<IActionResult> Create()
        {
            var createInvoiceViewModel = new CreateInvoiceViewModel
            {
                InvoiceDate = DateTime.UtcNow,
                UserId = _httpContextAccessor.HttpContext?.User.GetUserId()
                
            };
            return View(createInvoiceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceViewModel invoiceVM)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice
                {
                    InvoiceDate = invoiceVM.InvoiceDate,
                    InvoiceCode = invoiceVM.InvoiceCode,
                    Notes = invoiceVM.Notes,
                    Status = invoiceVM.Status,
                    Currency = invoiceVM.Currency,
                    PaymentMethod = invoiceVM.PaymentMethod,
                    Items = invoiceVM.Items,
                    Customer = invoiceVM.Customer,
                    UserId = invoiceVM.UserId
                };
                await _invoiceService.Add(invoice);
                return RedirectToAction("Index");
            }
            return View(invoiceVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _invoiceService.GetInvoiceUserById(id);
            if (invoice == null) { return NotFound();}

            var invoiceVM = new EditInvoiceViewModel
            {
                InvoiceDate = invoice.InvoiceDate,
                InvoiceCode = invoice.InvoiceCode,
                Notes = invoice.Notes,
                Status = invoice.Status,
                Currency = invoice.Currency,
                PaymentMethod = invoice.PaymentMethod,
                Items = invoice.Items,
                Customer = invoice.Customer,
                UserId = invoice.UserId
            };

            return View(invoiceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditInvoiceViewModel invoiceVM)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice
                {
                    Id = id,
                    InvoiceDate = invoiceVM.InvoiceDate,
                    InvoiceCode = invoiceVM.InvoiceCode,
                    Notes = invoiceVM.Notes,
                    Status = invoiceVM.Status,
                    Currency = invoiceVM.Currency,
                    PaymentMethod = invoiceVM.PaymentMethod,
                    Items = invoiceVM.Items,
                    Customer = invoiceVM.Customer,
                    UserId = invoiceVM.UserId
                };

                await _invoiceService.Update(invoice);
                return RedirectToAction("Index");
            }
            return View(invoiceVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _invoiceService.GetInvoiceUserById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            if (invoice == null) return RedirectToAction("Index");
            await _invoiceService.Delete(invoice);
            return RedirectToAction("Index");
        }

    }
}
