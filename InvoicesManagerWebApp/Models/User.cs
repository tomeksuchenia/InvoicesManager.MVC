﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesManagerWebApp.Models
{
    public class User : IdentityUser
    {
        public string? EmailAddress { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [DisplayName("Company name")]
        public string? CompanyName { get; set; }
        [DisplayName("Tax Id")]
        public string? TaxId { get; set; }
        [DisplayName("Telephone number")]
        public string? TelephoneNumber { get; set; }
        ICollection<Invoice>? Invoices { get; set; }

    }
}
