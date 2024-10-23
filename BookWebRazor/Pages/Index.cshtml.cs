using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;

        [BindProperty]
        public IEnumerable<BusinessObjects.Model.Product> Products { get; set; } = new List<BusinessObjects.Model.Product>();

        public IndexModel(ILogger<IndexModel> logger, IProductService productService, IProductImageService productImageService)
        {
            _logger = logger;
            _productService = productService;
            _productImageService = productImageService;
        }

        public void OnGet()
        {
            Products = _productService.GetProducts(includeProperties: "Category,ProductImages");
        }
    }
}
