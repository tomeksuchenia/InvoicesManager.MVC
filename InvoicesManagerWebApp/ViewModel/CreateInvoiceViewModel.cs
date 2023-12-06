using InvoicesManagerWebApp.Data.Enum;
using InvoicesManagerWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoicesManagerWebApp.ViewModel
{
    public class CreateInvoiceViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Invoice Code")]
        public string? InvoiceCode { get; set; }
        public string? Notes { get; set; }
        public decimal Total { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public ICollection<Item>? Items { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
