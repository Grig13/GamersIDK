using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public TimelineRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Timeline AddPostsToTimeline(Guid timelineId, List<Post> posts)
        {
            var timeline = GetById(timelineId);
            foreach (var post in posts)
            {
                timeline.Posts.Add(post);
            }
            this._dbContext.SaveChanges();
            return timeline;
        }

        public Timeline AddPostToTimeline(Guid timelineId, Post postToAdd)
        {
            var timeline = GetById(timelineId);
            timeline.Posts.Add(postToAdd);
            this._dbContext.SaveChanges();
            return timeline;
        }

        public void DeleteById(Guid id)
        {
            var timelineToDelete = GetById(id);
            _dbContext.Set<Timeline>().Remove(timelineToDelete);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Timeline> GetAll()
        {
            return _dbContext.Set<Timeline>().Include(a => a.Posts).ToList();
        }

        public Timeline GetById(Guid id)
        {
            var timelineToReturn = _dbContext.Set<Timeline>().Include(t => t.Posts).Where(a => a.Id == id).FirstOrDefault();
            if (timelineToReturn == null)
            {
                throw new KeyNotFoundException("Timeline not found.");
            }
            return timelineToReturn;
        }

        public Timeline RemovePostFromTimeline(Guid timelineId, Post postToRemove)
        {
            var timeline = GetById(timelineId);
            timeline.Posts.Remove(postToRemove);
            this._dbContext.SaveChanges();
            return timeline;
        }

        public Timeline Update(Timeline timelineToUpdate)
        {
            _dbContext.Set<Timeline>().Update(timelineToUpdate);
            _dbContext.SaveChanges();
            return timelineToUpdate;
        }

        public Timeline Add(Timeline timelineToAdd)
        {
            var timeline = this._dbContext.Add<Timeline>(timelineToAdd);
            this._dbContext.SaveChanges();
            return timeline.Entity;
        }
    }
}
