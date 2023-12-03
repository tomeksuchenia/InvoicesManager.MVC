using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Interface
{
    public interface IInvoiceService
    {
        Task<Invoice> GetById(int id);
        Task<IEnumerable<Invoice>> GetAll();
        Task<IEnumerable<Invoice>> GetAllUserInvoices();
        Task<Invoice> GetInvoiceUserById(int id);
        Task Add(Invoice invoice);
        Task Update(Invoice invoice);
        Task Delete(Invoice invoice);
    }
}
