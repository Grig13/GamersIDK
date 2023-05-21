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

        public void Add(Product productToAdd)
        {
            _dbContext.Set<Product>().Add(productToAdd);
            _dbContext.SaveChanges();
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

        public Product Update(Product productToUpdate)
        {
            var existingProduct = GetById((Guid)productToUpdate.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"News with id: {productToUpdate.Id} not found.");
            }
            existingProduct.Name = productToUpdate.Name;
            existingProduct.Price = productToUpdate.Price;
            existingProduct.Description = productToUpdate.Description;
            existingProduct.ImageUrl = productToUpdate.ImageUrl;
            existingProduct.Category = productToUpdate.Category;
            _dbContext.SaveChanges();
            return existingProduct;
        }
    }
}
