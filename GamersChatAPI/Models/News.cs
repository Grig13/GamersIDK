namespace GamersChatAPI.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public string? Attachment { get; set; }
    }
}
