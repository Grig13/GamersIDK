namespace GamersChatAPI.Models
{
    public class Timeline
    {
        public Guid Id { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
