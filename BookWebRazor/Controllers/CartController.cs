using BookWebRazor.BusinessObjects.Constants;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWebRazor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public CartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("plus/{cartId}")]
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _shoppingCartService.Get(c => c.Id == cartId);
            if (cartFromDb != null)
            {
                cartFromDb.Count += 1;
                _shoppingCartService.Update(cartFromDb);
            }
            return RedirectToPage("/ShoppingCart/Index");
        }

        [HttpGet("minus/{cartId}")]
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _shoppingCartService.Get(c => c.Id == cartId);
            if (cartFromDb != null)
            {
                if (cartFromDb.Count <= 1)
                {
                    //remove that from cart
                    _shoppingCartService.Remove(cartFromDb);
                    //update session
                    var cartCount = _shoppingCartService.Count(cart => cart.AccountId == cartFromDb.AccountId);
                    HttpContext.Session.SetInt32(Consts.SessionCart, cartCount);
                }
                else
                {
                    cartFromDb.Count -= 1;
                    _shoppingCartService.Update(cartFromDb);
                }
            }

            return RedirectToPage("/ShoppingCart/Index");
        }

        [HttpGet("remove/{cartId}")]
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _shoppingCartService.Get(c => c.Id == cartId);
            if (cartFromDb != null)
            {
                //remove that from cart
                _shoppingCartService.Remove(cartFromDb);
                //update session
                var cartCount = _shoppingCartService.Count(cart => cart.AccountId == cartFromDb.AccountId);
                HttpContext.Session.SetInt32(Consts.SessionCart, cartCount);
            }
            return RedirectToPage("/ShoppingCart/Index");
        }
    }
}
