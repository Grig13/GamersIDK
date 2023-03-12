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

        public Product AddCommentsToProduct(Guid productId, List<ProductComment> comments)
        {
            return this.productRepository.AddCommentsToProduct(productId, comments);
        }

        public Product AddCommentToProduct(Guid productId, ProductComment comment)
        {
            return this.productRepository.AddCommentToProduct(productId, comment);
        }

        public Product AddProduct(Product productToAdd)
        {
            return this.productRepository.Add(productToAdd);
        }

        public Product ProductUpdate(Product productToUpdate)
        {
            return this.productRepository.Update(productToUpdate);
        }

        public Product RemoveCommentFromProduct(Guid productId, ProductComment comment)
        {
            return this.productRepository.RemoveCommentFromProduct(productId, comment);
        }

        public void DeleteProduct(Guid productId)
        {
            this.productRepository.DeleteById(productId);
        }

    }
}
