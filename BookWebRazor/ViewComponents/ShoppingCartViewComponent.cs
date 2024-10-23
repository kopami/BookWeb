using BookWebRazor.BusinessObjects.Constants;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookWebRazor.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartViewComponent(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                if (HttpContext.Session.GetInt32(Consts.SessionCart) is null)
                {
                    int accountId = int.Parse(userId);
                    HttpContext.Session.SetInt32(Consts.SessionCart,
                    _shoppingCartService.Count(cart => cart.AccountId == accountId));
                }
                return View(HttpContext.Session.GetInt32(Consts.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
