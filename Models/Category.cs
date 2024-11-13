using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutodijeloviDemic.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        // Byte[] za skladištenje slike u bazi (opciono)
        public byte[]? ImageData { get; set; }

        // Tip MIME slike (opciono)
        public string? ImageMimeType { get; set; }

        // Relacija sa proizvodima (opciono)
        public ICollection<Product>? Products { get; set; }

        // Koristi se samo za upload slike (nije deo baze)
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
