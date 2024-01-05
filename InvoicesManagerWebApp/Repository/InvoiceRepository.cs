using InvoicesManagerWebApp.Data;
using InvoicesManagerWebApp.Extensions;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManagerWebApp.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InvoiceRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Invoice> GetById(int id)
            => await _context.Invoices.Include(x => x.Items).Include(x => x.Customer).Include(x => x.Customer.Address).FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<Invoice>> GetAll()
            => await _context.Invoices.ToListAsync();

        public async Task<IEnumerable<Invoice>> GetAllUserInvoice()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userInvoices = await _context.Invoices.Where(x => x.UserId == curUser.ToString()).OrderByDescending(x => x.InvoiceDate).ToListAsync();
            return userInvoices;
        }

        public async Task<Invoice> GetInvoiceUserById(int id)
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var invoice = await _context.Invoices.Include(x => x.Items).Include(x => x.Customer).Include(x => x.Customer.Address).Include(x => x.User).Where(x => x.UserId == curUser.ToString()).FirstOrDefaultAsync(x => x.Id == id);
            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetUserInvoicesListForMonth(int month)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            return await _context.Invoices.Where(x => x.UserId == userId).Where(x => x.InvoiceDate.Month == month).ToListAsync();
        }
        public async Task Add(Invoice invoice)
        {
            _context.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Invoice invoice)
        {
            _context.Update(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
