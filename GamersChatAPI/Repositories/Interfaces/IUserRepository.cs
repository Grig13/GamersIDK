using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public IEnumerable<User> GetNormalUsers();
        public IEnumerable<User> GetAdminUsers();
        public void DeleteById(Guid id);
        public User Add(User userToAdd);
        public User Update(User userToUpdate);
        public User GetById(Guid id);
    }
}
