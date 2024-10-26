using BookWebRazor.BusinessObjects.Constants;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BookWebRazor.Pages.Product
{
    public class DetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        [BindProperty]
        public BusinessObjects.Model.ShoppingCart Cart { get; set; } = new ();

        public DetailModel(IProductService productService, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }
        public void OnGet(int productId)
        {
            Cart = new()
            {
                Product = _productService.GetProductById(productId, includeProperties: "Category,ProductImages") ?? new(),
                ProductId = productId,
                Count = 1
            };
        }

        public IActionResult OnPost()
        {
            if (!(User.Identity?.IsAuthenticated ?? false)) // Check if the user is authenticated
            {
                return RedirectToPage("/Account/Login");
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Cart.AccountId = int.Parse(userId ?? string.Empty);

            _shoppingCartService.AddToCart(Cart);

            TempData["success"] = "Cart updated successfully";

            return RedirectToPage("/Index");
        }
    }
}
