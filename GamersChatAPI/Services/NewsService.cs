using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class NewsService
    {

        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IEnumerable<News> GetNews()
        {
            return this._newsRepository.GetAll();
        }

        public News GetNewsById(Guid id)
        {
            return this._newsRepository.GetById(id);
        }

        public News AddNews(News newsToAdd)
        {
            return this._newsRepository.Add(newsToAdd);
        }

        public News UpdateNews(News newsToUpdate)
        {
            return this._newsRepository.Update(newsToUpdate);
        }

        public void DeleteNews(Guid id)
        {
            this._newsRepository.DeleteById(id);
        }
    }
}
