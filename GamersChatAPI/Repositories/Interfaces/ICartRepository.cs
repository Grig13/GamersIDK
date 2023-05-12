using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Cart GetById(Guid id);
        public IEnumerable<Cart> GetAll();
        public Cart GetCartByUserId(Guid userId);
        public void AddToCart(Guid userId, Guid productId);
        public void RemoveFromCart(Guid userId, Guid productId);
        public void ClearCart(Guid userId);
    }
}
