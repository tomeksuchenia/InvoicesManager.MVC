using InvoicesManagerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManagerWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Address> Address { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
