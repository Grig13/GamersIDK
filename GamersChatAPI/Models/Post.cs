namespace GamersChatAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string PostContent { get; set; }
        public string? PostImage { get; set; }
        public Guid UserId { get; set; }
        public Guid? TimelineId { get; set; }

        public ICollection<PostComment>? PostComments { get; set; }
    }
}
