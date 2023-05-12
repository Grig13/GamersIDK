using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly GamersChatDbContext _dbContext;

        public CartItemRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CartItem> GetCartItemsByUserId(Guid userId)
        {
            return _dbContext.Set<CartItem>()
                           .Include(ci => ci.Product)
                           .Where(ci => ci.Cart.UserId == userId)
                           .ToList();
        }

        public void AddCartItem(CartItem cartItem)
        {
            _dbContext.Set<CartItem>().Add(cartItem);
            _dbContext.SaveChanges();
        }

        public void RemoveCartItem(Guid cartItemId)
        {
            var cartItem = _dbContext.Set<CartItem>().Find(cartItemId);
            if (cartItem != null)
            {
                _dbContext.Set<CartItem>().Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }
    }
}
