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

        public Cart AddProductsToCart(Guid cartId, List<Product> productsToAdd)
        {
            var cart = GetById(cartId);
            foreach (var product in productsToAdd)
            {
                cart.Products.Add(product);
            }
            this._dbContext.SaveChanges();
            return cart;
        }

        public Cart AddProductToCart(Guid cartId, Product productToAdd)
        {
            var cart = GetById(cartId);
            cart.Products.Add(productToAdd);
            this._dbContext.SaveChanges();
            return cart;

        }

        public void DeleteById(Guid id)
        {
            var cart = GetById(id);
            _dbContext.Set<Cart>().Remove(cart);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _dbContext.Set<Cart>().Include(a => a.Products).Include(b => b.Quantity).ToList();
        }

        public Cart GetById(Guid id)
        {
            var cartToReturn = _dbContext.Set<Cart>().Where(a => a.Id == id).Include(b => b.Products).Include(c => c.Quantity).FirstOrDefault();
            if (cartToReturn == null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }
            return cartToReturn;
        }

        public Cart RemoveProductFromCart(Guid cartId, Product productToRemove)
        {
            var cart = GetById(cartId);
            cart.Products.Remove(productToRemove);
            this._dbContext.SaveChanges();
            return cart;
        }

        public Cart Update(Cart cartToUpdate)
        {
            _dbContext.Set<Cart>().Update(cartToUpdate);
            _dbContext.SaveChanges();
            return cartToUpdate;
        }

        public Cart Add(Cart cartToAdd)
        {
            var cart = this._dbContext.Set<Cart>().Add(cartToAdd);
            this._dbContext.SaveChanges();
            return cart.Entity;
        }
    }
}
