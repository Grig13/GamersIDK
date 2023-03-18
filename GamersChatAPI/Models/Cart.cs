namespace GamersChatAPI.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int? TotalPrice { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
