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
            var commentToDelete = GetById(id);
            _dbContext.Set<ProductComment>().Remove(commentToDelete);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ProductComment> GetAll()
        {
            return _dbContext.Set<ProductComment>().ToList();
        }

        public ProductComment GetById(Guid id)
        {
            var productCommToReturn = _dbContext.Set<ProductComment>().Where(a => a.Id == id).FirstOrDefault();
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
            using (var scope = new TransactionScope())
            {
                var productId = productCommentToAdd.ProductId;
                var product = _dbContext.Set<Product>().FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                var productComment = this._dbContext.Set<ProductComment>().Add(productCommentToAdd);
                _dbContext.SaveChanges();

                scope.Complete();
                return productComment.Entity;
            }
        }
    }
}
