namespace GamersChatAPI.Models
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? PostId { get; set; }
        public string CommentContent { get; set; }
        public Post? Post { get; set; }
    }
}
