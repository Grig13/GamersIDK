using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository repository)
        {
            this._cartRepository = repository;
        }

        public IEnumerable<Cart> GetAllProductsFromCart()
        {
            return this._cartRepository.GetAll();
        }

        public Cart GetCartByUserId(Guid userId)
        {
            return this._cartRepository.GetCartByUserId(userId);
        }

        public void AddToCart(Guid userId, Guid productId)
        {
            _cartRepository.AddToCart(userId, productId);
        }

        public void RemoveFromCart(Guid userId, Guid productId)
        {
            _cartRepository.RemoveFromCart(userId, productId);
        }

        public Cart GetCartById(Guid id)
        {
            return this._cartRepository.GetById(id);
        }

        public void ClearCart(Guid userId)
        {
            _cartRepository.ClearCart(userId);
        }
    }
}
