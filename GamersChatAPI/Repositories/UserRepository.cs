using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public UserRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Add(User userToAdd)
        {
            var user = this._dbContext.Add<User>(userToAdd);
            this._dbContext.SaveChanges();
            return user.Entity;
        }

        public void DeleteById(Guid id)
        {
            var user = GetById(id);
            _dbContext.Set<User>().Remove(user);
            _dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Set<User>().Include(a => a.Name).Include(b => b.Role).ToList();
        }

        public User GetById(Guid id)
        {
            var user = _dbContext.Set<User>().Where(a => a.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return user;
        }

        public User Update(User userToUpdate)
        {
            _dbContext.Set<User>().Update(userToUpdate);
            _dbContext.SaveChanges();
            return userToUpdate;
        }
    }
}
