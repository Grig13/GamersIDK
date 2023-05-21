using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimelineController : ControllerBase
    {
        private readonly TimelineService _timelineService;
        private readonly PostService _postService;

        public TimelineController(TimelineService timelineService, PostService postService)
        {
            _timelineService = timelineService;
            _postService = postService;
        }

        [HttpGet]
        public IEnumerable<Timeline> Get()
        {
            return this._timelineService.GetTimelines();
        }

        [HttpGet("{id}")]
        public Timeline GetTimelineById(Guid id)
        {
            return this._timelineService.GetTimelineById(id);
        }

        [HttpPost("{timelineId}/add-post")]
        public Timeline AddPostsToTimeline(Guid timelineId, [FromBody] List<Guid> postsId)
        {
            var posts = new List<Post>();
            foreach (var postId in postsId)
            {
                var postToAdd = _postService.GetPostById(postId);
                posts.Add(postToAdd);
            }
            return _timelineService.AddPostsToTimeline(timelineId, posts);
        }

        [HttpPost("{timelineId}/add-post/{postId}")]
        public Timeline AddPostToTimeline(Guid timelineId, Guid postId)
        {
            var post = _postService.GetPostById(postId);
            return this._timelineService.AddPostToTimeline(timelineId, post);
        }

        [HttpPost]
        public Timeline Post(Timeline timeline)
        {
            var timelineToAdd = new Timeline
            {
                Posts = timeline.Posts
            };

            return this._timelineService.AddTimeline(timelineToAdd);
        }

        [HttpDelete("{timelineId}/{postId}")]
        public Timeline RemovePostFromTimeline(Guid timelineId, Guid postId)
        {
            var postToRemove = new Post();
            postToRemove = this._postService.GetPostById(postId);
            return this._timelineService.RemovePostFromTimeline(timelineId, postToRemove);
        }

        [HttpPut("{id}")]
        public Timeline Update(Guid id, [FromBody] Timeline timeline)
        {
            var timelineToEdit = _timelineService.GetTimelineById(id);
            timelineToEdit.Posts = timeline.Posts;
            return this._timelineService.TimelineUpdate(timelineToEdit);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._timelineService.DeleteTimeline(id);
        }
    }
}
