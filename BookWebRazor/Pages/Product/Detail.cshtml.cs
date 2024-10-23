using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.Product
{
    public class DetailModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public ShoppingCart Cart { get; set; } = new ShoppingCart();

        public DetailModel(IProductService productService)
        {
            _productService = productService;
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
    }
}
