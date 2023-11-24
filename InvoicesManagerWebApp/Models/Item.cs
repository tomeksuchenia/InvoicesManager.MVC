using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesManagerWebApp.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
