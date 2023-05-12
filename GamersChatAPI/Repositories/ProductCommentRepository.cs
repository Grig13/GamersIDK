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
#pragma warning disable CS8603 // Possible null reference return.
            return _dbContext.Set<ProductComment>().Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Update(ProductComment commentToUpdate)
        {
            _dbContext.Entry(commentToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Add(ProductComment productCommentToAdd)
        {
            _dbContext.Set<ProductComment>().Add(productCommentToAdd);
           _dbContext.SaveChanges();
        }

        public IEnumerable<ProductComment> GetCommentsByProductId(Guid productId)
        {
            return _dbContext.Set<ProductComment>().Where(c => c.ProductId == productId).ToList();
        }
    }
}
