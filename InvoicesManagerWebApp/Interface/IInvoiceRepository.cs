using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Interface
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll();
        Task<Invoice> GetById(int id);
        Task<IEnumerable<Invoice>> GetAllUserInvoice();
        Task<Invoice> GetInvoiceUserById(int id);
        Task<IEnumerable<Invoice>> GetInvoicesListForMonth(int month);
        Task Add(Invoice invoice);

        Task Update(Invoice invoice);

        Task Delete(Invoice invoice);
    }
}
