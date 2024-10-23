using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.Product
{
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public IEnumerable<BusinessObjects.Model.Product> Products { get; set; } = new List<BusinessObjects.Model.Product>();

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public void OnGet()
        {
            Products = _productService.GetProducts(includeProperties: "Category");
        }       
    }
}
