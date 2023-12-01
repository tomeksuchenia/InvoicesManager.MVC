using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoicesManagerWebApp.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
