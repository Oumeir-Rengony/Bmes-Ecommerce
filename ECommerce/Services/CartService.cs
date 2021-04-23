using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models.Domain.Cart;
using ECommerce.Models.Domain.Customer;
using ECommerce.Models.Domain.Product;
using ECommerce.Repositories;
using ECommerce.Repositories.Contracts;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services
{
    public class CartService : ICartService
    {
        private const string UniqueCartIdSessionKey = "UniqueCartId"; 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public CartService(IHttpContextAccessor httpContextAccessor,
                            UserManager<IdentityUser> userManager,
                            ICartRepository cartRepository,
                            ICartItemRepository cartItemRepository,
                            IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        /*public string UniqueCartId()
        {
            if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(UniqueCartIdSessionKey)))
            {
                return _httpContextAccessor.HttpContext.Session.GetString(UniqueCartIdSessionKey);
            }
            else
            {
                var uniqueId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext.Session.SetString(UniqueCartIdSessionKey, uniqueId);

                return _httpContextAccessor.HttpContext.Session.GetString(UniqueCartIdSessionKey);
            }
        }*/

        public async Task<string> UniqueCartId()
        {
            var customer = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var cart = _cartRepository.GetAllCarts().FirstOrDefault(c => c.CustomerId == customer.Id);

            if (cart == null)
                return _httpContextAccessor.HttpContext.Session.GetString(UniqueCartIdSessionKey);

            return cart.UniqueCartId;

        }

        public async Task<Cart> GetCart()
        {
            var customer = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (customer == null)
                return null;

            var cart = _cartRepository.GetAllCarts()
                                                .FirstOrDefault(c => c.CustomerId == customer.Id);
            return cart;
        }

        /* public Cart GetCart()
         {
             var uniqueId = UniqueCartId();
             var cart = _cartRepository.GetAllCarts().FirstOrDefault(c => c.UniqueCartId == uniqueId);
             return cart;
         }*/


        public async Task AddToCart(AddToCartViewModel addToCartViewModel)
        {
            var cart = await GetCart();

            if (cart != null)
            {
                //if product exists update quantity
                var existingCartItem = GetExistingCartItem(cart, addToCartViewModel.ProductId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                    _cartItemRepository.UpdateCartItem(existingCartItem);
                    return;
                }
            }

            else
            {
                cart = await CreateNewCart();
            }

            //add Product
            var product = _productRepository.FindProductById(addToCartViewModel.ProductId);

            if (product != null)
                AddCartItem(cart, product);
        }

        public async Task<Cart> CreateNewCart()
        {
            var customer = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var newCart = new Cart
            {
                UniqueCartId = await UniqueCartId(),
                CartStatus = CartStatus.Open,
                CustomerId = customer.Id,
                Customer = (Customer) customer
            };

            _cartRepository.SaveCart(newCart);

            return newCart;
        }

        public CartItem GetExistingCartItem(Cart cart, long productId)
        {
            return _cartItemRepository
                        .FindCartItemsByCartId(cart.Id)
                        .FirstOrDefault(c => c.ProductId == productId);
        }

        public void AddCartItem(Cart cart, Product product)
        {
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                Cart = cart,
                ProductId = product.Id,
                Product = product,
                Quantity = 1
            };

            _cartItemRepository.SaveCartItem(cartItem);
        }

        public void RemoveFromCart(RemoveFromCartViewModel removeFromCartViewModel)
        {
            var cartItem = _cartItemRepository.FindCartItemById(removeFromCartViewModel.CartItemId);
            _cartItemRepository.DeleteCartItem(cartItem);
        }

        public async Task<IEnumerable<CartItem>> GetCartItems()
        {
            IList<CartItem> cartItems = new List<CartItem>();

            var cart = await GetCart();

            if (cart != null)
            {
                cartItems = _cartItemRepository.FindCartItemsByCartId(cart.Id).ToList();
            }

            return cartItems;
        }

        public async Task<int> CartItemsCount()
        {
            var count = 0;
            var cartItems = await GetCartItems();

            foreach (var cartItem in cartItems)
            {
                count += cartItem.Quantity;
            }

            return count;
        }


        public async Task<decimal> GetCartTotal()
        {
            decimal total = 0;

            var cartItems = await GetCartItems();

            foreach (var cartItem in cartItems)
            {
                var product = _productRepository.FindProductById(cartItem.ProductId);
                total += cartItem.Quantity * product.Price;
            }

            return total;
        }
    }
}
