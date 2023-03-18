namespace GamersChatAPI.Models
{
    public class ProductComment
    {
        public Guid Id { get; set; }
        public string CommentContent { get; set; }
        public int? Grade { get; set; }
        public Guid ProductId { get; set; }

        public Product? Product { get; set; } = null!;
    }
}
