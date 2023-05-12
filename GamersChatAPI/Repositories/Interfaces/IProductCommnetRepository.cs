using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IProductCommentRepository
    {
        public IEnumerable<ProductComment> GetCommentsByProductId(Guid productId);
        public void DeleteById(Guid id);
        public void Update(ProductComment commentToUpdate);
        public ProductComment GetById(Guid id);
        public void Add(ProductComment commentToADD);
    }
}
