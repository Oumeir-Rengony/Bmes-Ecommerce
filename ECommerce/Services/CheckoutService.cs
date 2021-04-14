using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Models.Customer;
using ECommerce.Models.Order;
using ECommerce.Repositories.Contracts;
using ECommerce.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartService _cartService;

        public CheckoutService(UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            ICartService cartService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
        }

        public async Task ProcessCheckout()
        {

            var cart = _cartService.GetCart();

            if (cart != null)
            {
                var cartItems = _cartItemRepository.FindCartItemsByCartId(cart.Id).ToList();
                var cartTotal = _cartService.GetCartTotal();
                decimal shippingCharge = 0;
                var orderTotal = cartTotal + shippingCharge;

                /*var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);*/
                var customer = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);


                var order = new Order
                {
                    OrderTotal = orderTotal,
                    OrderItemTotal = cartTotal,
                    ShippingCharge = shippingCharge,
                    CustomerId = customer.Id,
                    Customer = (Customer)customer,
                    OrderStatus = OrderStatus.Submitted
                };

                 _orderRepository.SaveOrder(order);

                foreach (var cartItem in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = cartItem.Quantity,
                        Order = order,
                        OrderId = order.Id,
                        Product = cartItem.Product,
                        ProductId = cartItem.ProductId
                    };

                     _orderItemRepository.SaveOrderItem(orderItem);
                }

                _cartRepository.DeleteCart(cart);
            }
        }
    }
}
