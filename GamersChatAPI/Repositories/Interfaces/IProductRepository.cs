using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public Product AddCommentToProduct(Guid productId, ProductComment commentToAdd);
        public Product RemoveCommentFromProduct(Guid productId, ProductComment commentToRemove);
        public Product Add(Product productToAdd);
        public void DeleteById(Guid id);
        public Product Update(Product productToUpdate);
        public Product GetById(Guid id);
    }
}
