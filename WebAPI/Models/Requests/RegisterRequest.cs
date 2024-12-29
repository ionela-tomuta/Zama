using System.ComponentModel.DataAnnotations;
using Zama.API.Models.DTOs;

namespace Zama.API.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Parola trebuie să conțină cel puțin o literă mare, o literă mică, un număr și un caracter special")]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
