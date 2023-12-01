using InvoicesManagerWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManagerWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
