using System.Collections.Generic;
using ECommerce.Models.Domain.Cart;

namespace ECommerce.Repositories.Contracts
{
    public interface ICartRepository
    {
        Cart FindCartById(long id);
        IEnumerable<Cart> GetAllCarts();
        void SaveCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
    }
}
