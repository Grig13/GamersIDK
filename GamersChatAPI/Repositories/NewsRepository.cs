using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly GamersChatDbContext _dbContext;
        public NewsRepository(GamersChatDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public News GetById(Guid id)
        {
            var newsToReturn = _dbContext.Set<News>().Where(p => p.Id == id).FirstOrDefault();

            if (newsToReturn == null)
            {
                throw new KeyNotFoundException("News article not found.");
            }

            return newsToReturn;
        }

        public News Add(News newsToAdd)
        {
            var news = this._dbContext.Set<News>().Add(newsToAdd);
            _dbContext.SaveChanges();
            return news.Entity;
        }

        public void DeleteById(Guid id)
        {
            var toDelete = GetById(id);
            _dbContext.Set<News>().Remove(toDelete);
            _dbContext.SaveChanges();
        }

        public IEnumerable<News> GetAll()
        {
            return _dbContext.Set<News>().ToList();
        }

        public News Update(News newsToUpdate)
        {
            _dbContext.Set<News>().Update(newsToUpdate);
            _dbContext.SaveChanges();
            return newsToUpdate;
        }
    }
}
