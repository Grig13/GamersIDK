using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly GamersChatDbContext _dbContext;

        public CartRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart GetCartByUserId(Guid userId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _dbContext.Set<Cart>()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void DeleteById(Guid id)
        {
            var cart = GetById(id);
            _dbContext.Set<Cart>().Remove(cart);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _dbContext.Set<Cart>().ToList();
        }

        public Cart GetById(Guid id)
        {
            var cartToReturn = _dbContext.Set<Cart>().Where(a => a.Id == id).FirstOrDefault();
            if (cartToReturn == null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }
            return cartToReturn;
        }

        public void AddToCart(Guid userId, Guid productId)
        {
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _dbContext.Set<Cart>().Add(cart);
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                var product = _dbContext.Set<Product>().Find(productId);
                if (product != null)
                {
                    cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1 });
                }
            }

            _dbContext.SaveChanges();
        }

        public void RemoveFromCart(Guid userId, Guid productId)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    _dbContext.Set<CartItem>().Remove(cartItem);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void ClearCart(Guid userId)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                cart.CartItems.Clear();
                _dbContext.SaveChanges();
            }
        }
    }
}
