using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.BusinessObjects.ViewModel;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.Product
{
    public class DeleteImageModel : PageModel
    {
        private readonly IProductImageService _productImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteImageModel(IProductImageService productImageService, IWebHostEnvironment webHostEnvironment)
        {
            _productImageService = productImageService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int imageId)
        {
            int productId = _productImageService.Remove(imageId, _webHostEnvironment.WebRootPath);

            if (productId != 0)
            {
                TempData["success"] = "Delete successfully";
                return RedirectToPage("Upsert", new { id = productId });
            }
            return RedirectToPage("Index");
        }
    }
}
