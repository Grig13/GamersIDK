namespace GamersChatAPI.Models
{
    public class News
    {
        private string title;

        public Guid Id { get; set; }
        public string Title { get => title; set => title = value; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public string? Attachment { get; set; }
    }
}
