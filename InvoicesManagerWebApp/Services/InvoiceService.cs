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

        public async Task<IEnumerable<Invoice>> GetAllUserInvoices()
        {
            return await _invoiceRepository.GetAllUserInvoice();
        }

        public async Task<Invoice> GetInvoiceUserById(int id)
        {
            return await _invoiceRepository.GetInvoiceUserById(id);
        }

        public async Task Add(Invoice invoice)
        {
            var invoices = await _invoiceRepository.GetUserInvoicesListForMonth(invoice.InvoiceDate.Month);
            invoice.InvoiceCode = $"{invoices.Count() + 1}/{invoice.InvoiceDate.Month}/{invoice.InvoiceDate.Year}";

            foreach (var item in invoice.Items)
            {
                var priceWithTax = item.Price * (decimal)(1 + item.Vat / 100);
                invoice.Total += priceWithTax * item.Quantity;
            }

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
            invoiceFromDb.Customer = invoice.Customer;
            invoiceFromDb.Total = 0;

            foreach (var item in invoice.Items)
            {
                var priceWithTax = item.Price * (decimal)(1 + item.Vat / 100);
                invoiceFromDb.Total += priceWithTax * item.Quantity;
            }

            await _invoiceRepository.Update(invoiceFromDb);
        }
        public async Task Delete(Invoice invoice)
        {
            await _invoiceRepository.Delete(invoice);
        }

    }
}
