using BookWebRazor.BusinessObjects.ViewModel;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int? id)
        {
            var productToBeDeleted = _productService.GetProductById(id ?? 0);
            if (productToBeDeleted == null)
            {
                return new JsonResult(new { success = false, message = "error while deleting" });
            }

            DeleteImage(id);

            //delete product
            _productService.DeleteProduct(productToBeDeleted);
            return new JsonResult(new { success = true, message = "Delete Successful" });
        }

        private void DeleteImage(int? id)
        {
            string productPath = Path.Combine("images", "products", $"product-{id}");
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
        }
    }
}
