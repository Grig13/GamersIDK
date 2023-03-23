using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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
            var productComment = GetById(id);
            if (productComment != null)
            {
                _dbContext.ProductComments.Remove(productComment);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<ProductComment> GetAll()
        {
            return _dbContext.ProductComments.ToList();
        }

        public ProductComment GetById(Guid id)
        {
            return _dbContext.ProductComments.SingleOrDefault(p => p.Id == id);
        }

        public ProductComment Update(ProductComment commentToUpdate)
        {
            _dbContext.Set<ProductComment>().Update(commentToUpdate);
            _dbContext.SaveChanges();
            return commentToUpdate;
        }

        public void Add(ProductComment productCommentToAdd)
        {
           _dbContext.ProductComments.Add(productCommentToAdd);
           _dbContext.SaveChanges();
        }
    }
}
