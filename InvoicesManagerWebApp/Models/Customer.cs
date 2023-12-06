using System.ComponentModel;

namespace InvoicesManagerWebApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        [DisplayName("Tax Id")]
        public string? TaxId { get; set; }
        public string? Email { get; set; }
    }
}
