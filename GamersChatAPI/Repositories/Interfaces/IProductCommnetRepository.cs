using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IProductCommentRepository
    {
        public IEnumerable<ProductComment> GetAll();
        public void DeleteById(Guid id);
        public ProductComment Update(ProductComment commentToUpdate);
        public ProductComment GetById(Guid id);
        public ProductComment Add(ProductComment commentToADD);
    }
}
