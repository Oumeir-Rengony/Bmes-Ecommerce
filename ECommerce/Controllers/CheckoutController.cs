using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(ICheckoutService checkoutService, UserManager<IdentityUser> userManager)
        {
            _checkoutService = checkoutService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            await _checkoutService.ProcessCheckout();

            return RedirectToAction("Receipt");
        }

        public IActionResult Receipt()
        {
            return View();
        }
    }
}
