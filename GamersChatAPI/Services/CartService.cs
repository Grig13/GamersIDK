using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class CartService
    {
        private readonly ICartRepository repository;

        public CartService(ICartRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Cart> GetAllProductsFromCart()
        {
            return this.repository.GetAll();
        }

        public Cart AddProductsToCart(Guid cartId, List<Product> productsToAdd)
        {
            return this.repository.AddProductsToCart(cartId, productsToAdd);
        }

        public Cart AddProductToCart(Guid cartId, Product productToAdd)
        {
            return this.repository.AddProductToCart(cartId, productToAdd);
        }

        public Cart AddCart(Cart cartToAdd)
        {
            return this.repository.Add(cartToAdd);
        }

        public Cart RemoveProductFromCart(Guid cartId, Product productToRemove)
        {
            return this.repository.RemoveProductFromCart(cartId, productToRemove);
        }

        public Cart GetCartById(Guid id)
        {
            return this.repository.GetById(id);
        }

        public Cart UpdateCart(Cart cartToUpdate)
        {
            return this.repository.Update(cartToUpdate);
        }

        public void DeleteCart(Guid id)
        {
            this.repository.DeleteById(id);
        }
    }
}
