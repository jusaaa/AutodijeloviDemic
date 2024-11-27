using System.ComponentModel.DataAnnotations;

namespace AutodijeloviDemic.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Unesite važeću email adresu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } 
    }
}
