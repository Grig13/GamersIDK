using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productRepository.GetAll();
        }

        public Product GetProductById(Guid productId)
        {
            return this.productRepository.GetById(productId);
        }

        public void AddProduct(Product productToAdd)
        {
             this.productRepository.Add(productToAdd);
        }

        public Product ProductUpdate(Product productToUpdate)
        {
             return this.productRepository.Update(productToUpdate);
        }

        public void DeleteProduct(Guid productId)
        {
            this.productRepository.DeleteById(productId);
        }

    }
}
