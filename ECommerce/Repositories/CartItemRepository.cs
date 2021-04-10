using ECommerce.Data;
using ECommerce.Models.Cart;
using ECommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Bmes.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CartItem FindCartItemById(long id)
        {
            var cartItem = _context.CartItems.Find(id);

            return cartItem;
        }

        public IEnumerable<CartItem> FindCartItemsByCartId(long cartId)
        {
            var cartItems = _context.CartItems.Where(cartItem => cartItem.CartId == cartId);
            return cartItems;
        }

        public void SaveCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
    }
}
