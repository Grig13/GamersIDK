using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public ProductRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product AddCommentsToProduct(Guid productId, List<ProductComment> comments)
        {
            var product = GetById(productId);
            foreach (var comment in comments)
            {
                product.Comments.Add(comment);
            }
            this._dbContext.SaveChanges();
            return product;
        }

        public Product AddCommentToProduct(Guid productId, ProductComment commentToAdd)
        {
            var product = GetById(productId);
            product.Comments.Add(commentToAdd);
            this._dbContext.SaveChanges();
            return product;
        }

        public Product Add(Product productToAdd)
        {
            var product = this._dbContext.Add<Product>(productToAdd);
            _dbContext.SaveChanges();
            return product.Entity;
        }

        public void DeleteById(Guid id)
        {
            var product = GetById(id);
            _dbContext.Set<Product>().Remove(product);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Set<Product>().ToList();
        }

        public Product GetById(Guid id)
        {
            var productToReturn = _dbContext.Set<Product>().Where(a => a.Id == id).FirstOrDefault();
            if (productToReturn == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            return productToReturn;
        }

        public Product RemoveCommentFromProduct(Guid productId, ProductComment commentToRemove)
        {
            var product = GetById(productId);
            product.Comments.Remove(commentToRemove);
            this._dbContext.SaveChanges();
            return product;
        }

        public Product Update(Product productToUpdate)
        {
            _dbContext.Set<Product>().Update(productToUpdate);
            _dbContext.SaveChanges();
            return productToUpdate;
        }
    }
}
