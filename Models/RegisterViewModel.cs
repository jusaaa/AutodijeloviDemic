using System.ComponentModel.DataAnnotations;

namespace AutodijeloviDemic.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Unesite važeću email adresu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [Phone(ErrorMessage = "Unesite važeći broj telefona.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Unesite grad.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Unesite državu.")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrda lozinke je obavezna.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string ConfirmPassword { get; set; }

    }
}
