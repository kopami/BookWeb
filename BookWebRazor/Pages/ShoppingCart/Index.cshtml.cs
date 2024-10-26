using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.BusinessObjects.ViewModel;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BookWebRazor.Pages.ShoppingCart
{
    [Authorize(Roles = nameof(RoleEnum.Customer))]
    public class IndexModel : PageModel
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductImageService _productImageService;

        [BindProperty]
        public ShoppingCartVM? ShoppingCartVM { get; set; }

        public IndexModel(IShoppingCartService shoppingCartService, IProductImageService productImageService)
        {
            _shoppingCartService = shoppingCartService;
            _productImageService = productImageService;
        }
        public void OnGet()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _shoppingCartService.GetAll(userId ?? string.Empty, includeProperties: "Product").ToList(),
                OrderHeader = new OrderHeader()
            };
            IEnumerable<ProductImage> productImages = _productImageService.GetAll();
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Product.ProductImages = productImages.Where(image => image.ProductId == cart.ProductId).ToList();
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
        }
        private double GetPriceBasedOnQuantity(BusinessObjects.Model.ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            if (shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            return shoppingCart.Product.Price100;
        }
    }
}
