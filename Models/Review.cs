namespace AutodijeloviDemic.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; } // Ocena, npr. od 1 do 5
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public Product Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
