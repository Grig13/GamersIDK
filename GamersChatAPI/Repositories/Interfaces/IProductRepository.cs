using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public void Add(Product productToAdd);
        public void DeleteById(Guid id);
        public Product Update(Product productToUpdate);
        public Product GetById(Guid id);
    }
}
