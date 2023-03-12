using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class ProductCommentRepository : IProductCommentRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public ProductCommentRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteById(Guid id)
        {
            var commentToDelete = GetById(id);
            _dbContext.Set<ProductComment>().Remove(commentToDelete);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ProductComment> GetAll()
        {
            return _dbContext.Set<ProductComment>().Include(a => a.CommentContent).Include(b => b.Grade).ToList();
        }

        public ProductComment GetById(Guid id)
        {
            var productCommToReturn = _dbContext.Set<ProductComment>().Where(a => a.Id == id).Include(b => b.CommentContent).Include(c => c.Grade).FirstOrDefault();
            if (productCommToReturn == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }
            return productCommToReturn;
        }

        public ProductComment Update(ProductComment commentToUpdate)
        {
            _dbContext.Set<ProductComment>().Update(commentToUpdate);
            _dbContext.SaveChanges();
            return commentToUpdate;
        }

        public ProductComment Add(ProductComment productCommentToAdd)
        {
            var productComment = this._dbContext.Set<ProductComment>().Add(productCommentToAdd);
            _dbContext.SaveChanges();
            return productComment.Entity;
        }
    }
}
