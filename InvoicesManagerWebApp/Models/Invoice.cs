using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using InvoicesManagerWebApp.Data.Enum;

namespace InvoicesManagerWebApp.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? InvoiceCode { get; set; }
        public string? Notes { get; set; }
        public decimal Total { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public ICollection<Item>? Items { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}