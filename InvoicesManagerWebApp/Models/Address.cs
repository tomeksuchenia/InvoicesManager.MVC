using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoicesManagerWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [DisplayName("Zip code")]
        public int ZipCode { get; set; }
    }
}