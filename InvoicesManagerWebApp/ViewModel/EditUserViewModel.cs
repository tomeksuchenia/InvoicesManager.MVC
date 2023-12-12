using InvoicesManagerWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoicesManagerWebApp.ViewModel
{
    public class EditUserViewModel
    {
        public string? UserId { get; set; }
        public string? EmailAddress { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [DisplayName("Company name")]
        public string? CompanyName { get; set; }
        [DisplayName("Tax Id")]
        public string? TaxId { get; set; }
        [DisplayName("Telephone number")]
        [Required]
        public string? TelephoneNumber { get; set; }
    }
}
