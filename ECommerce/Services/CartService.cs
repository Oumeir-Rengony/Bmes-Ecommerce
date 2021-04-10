using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Cart;
using ECommerce.Repositories;
using ECommerce.Repositories.Contracts;
using ECommerce.Services.Contracts;
using ECommerce.ViewModels.Cart;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services
{
    public class CartService : ICartService
    {
        private const string UniqueCartIdSessionKey = "UniqueCartId";
        private readonly HttpContext _httpContext;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public CartService(IHttpContextAccessor httpContextAccessor,
                            ICartRepository cartRepository,
                            ICartItemRepository cartItemRepository,
                            IProductRepository productRepository)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public string UniqueCartId()
        {
            if (!string.IsNullOrWhiteSpace(_httpContext.Session.GetString(UniqueCartIdSessionKey)))
            {
                return _httpContext.Session.GetString(UniqueCartIdSessionKey);
            }
            else
            {
                var uniqueId = Guid.NewGuid().ToString();
                _httpContext.Session.SetString(UniqueCartIdSessionKey, uniqueId);

                return _httpContext.Session.GetString(UniqueCartIdSessionKey);
            }
        }

        public Cart GetCart()
        {
            var uniqueId = UniqueCartId();
            var cart = _cartRepository.GetAllCarts().FirstOrDefault(c => c.UniqueCartId == uniqueId);
            return cart;
        }

        public void AddToCart(AddToCartViewModel addToCartViewModel)
        {
            var cart = GetCart();

            if (cart != null)
            {
                var existingCartItem = _cartItemRepository.FindCartItemsByCartId(cart.Id)
                                                          .FirstOrDefault(c => c.ProductId == addToCartViewModel.ProductId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                    _cartItemRepository.UpdateCartItem(existingCartItem);
                }
                else
                {
                    var product = _productRepository.FindProductById(addToCartViewModel.ProductId);

                    if (product != null)
                    {
                        var cartItem = new CartItem
                        {
                            CartId = cart.Id,
                            Cart = cart,
                            ProductId = addToCartViewModel.ProductId,
                            Product = product,
                            Quantity = 1
                        };
                        _cartItemRepository.SaveCartItem(cartItem);
                    }
                }
            }
            else
            {
                var product = _productRepository.FindProductById(addToCartViewModel.ProductId);
                if (product != null)
                {
                    var newCart = new Cart
                    {
                        UniqueCartId = UniqueCartId(),
                        CartStatus = CartStatus.Open
                    };

                    _cartRepository.SaveCart(newCart);

                    var cartItem = new CartItem
                    {
                        CartId = newCart.Id,
                        Cart = newCart,
                        ProductId = addToCartViewModel.ProductId,
                        Product = product,
                        Quantity = 1
                    };

                    _cartItemRepository.SaveCartItem(cartItem);

                }
            }
        }

        public void RemoveFromCart(RemoveFromCartViewModel removeFromCartViewModel)
        {
            var cartItem = _cartItemRepository.FindCartItemById(removeFromCartViewModel.CartItemId);
            _cartItemRepository.DeleteCartItem(cartItem);
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            IList<CartItem> cartItems = new List<CartItem>();

            var cart = GetCart();

            if (cart != null)
            {
                cartItems = _cartItemRepository.FindCartItemsByCartId(cart.Id).ToArray();
            }

            return cartItems;
        }

        public int CartItemsCount()
        {

            var count = 0;
            var cartItems = GetCartItems();

            foreach (var cartItem in cartItems)
            {
                count += cartItem.Quantity;
            }

            return count;
        }


        public decimal GetCartTotal()
        {
            decimal total = 0;

            var cartItems = GetCartItems();

            foreach (var cartItem in cartItems)
            {
                var product = _productRepository.FindProductById(cartItem.ProductId);
                total += cartItem.Quantity * product.Price;
            }

            return total;
        }
    }
}
