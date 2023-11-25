using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public Task<Invoice> GetById(int id)
        {
            return _invoiceRepository.GetById(id);
        }
        public Task<IEnumerable<Invoice>> GetAll()
        {
            return _invoiceRepository.GetAll();
        }
        public async Task Add(Invoice invoice)
        {
            await _invoiceRepository.Add(invoice);
        }

        public async Task Update(Invoice invoice)
        {
            var invoiceFromDb = await _invoiceRepository.GetById(invoice.Id);

            invoiceFromDb.InvoiceDate = invoice.InvoiceDate;
            invoiceFromDb.Notes = invoice.Notes;
            invoiceFromDb.Status = invoice.Status;
            invoiceFromDb.Currency = invoice.Currency;
            invoiceFromDb.Items = invoice.Items;
            invoiceFromDb.PaymentMethod = invoice.PaymentMethod;
            invoiceFromDb.Total = 0;

            foreach (var item in invoice.Items)
            {
                var priceWithTax = item.Price * (decimal)(1 + item.Vat / 100);
                invoiceFromDb.Total += priceWithTax * item.Quantity;
            }

            await _invoiceRepository.Update(invoice);
        }
        public async Task Delete(Invoice invoice)
        {
            await _invoiceRepository.Delete(invoice);
        }
    }
}
