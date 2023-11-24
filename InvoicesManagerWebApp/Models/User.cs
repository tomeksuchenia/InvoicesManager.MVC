using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesManagerWebApp.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string? EmailAddress { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        ICollection<Invoice>? Invoices { get; set; }

    }
}
