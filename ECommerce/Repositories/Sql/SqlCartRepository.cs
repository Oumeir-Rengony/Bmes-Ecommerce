using System.Collections.Generic;
using System.Linq;
using ECommerce.Data;
using ECommerce.Models.Domain.Cart;
using ECommerce.Repositories.Contracts;

namespace ECommerce.Repositories.Sql
{
    public class SqlCartRepository : ICartRepository
    {
        private ApplicationDbContext _context;

        public SqlCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart FindCartById(long id)
        {
            var cart = _context.Carts.Find(id);
            return cart;
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            var carts = _context.Carts;
            return carts;
        }

        public void SaveCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}
