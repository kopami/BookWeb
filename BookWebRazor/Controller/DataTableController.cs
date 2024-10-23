using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWebRazor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTableController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DataTableController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("get-products")]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts(includeProperties: "Category").ToList();
            return new JsonResult(new { data = products }); // Fix the Json method call
        }

        [HttpDelete("product")]
        public IActionResult DeleteProducts(int? id)
        {
            var productToBeDeleted = _productService.GetProductById(id ?? 0);
            if (productToBeDeleted == null)
            {
                return new JsonResult(new { success = false, message = "error while deleting" });
            }

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


            //delete product
            _productService.DeleteProduct(productToBeDeleted);
            return new JsonResult(new { success = true, message = "Delete Successful" });
        }
    }
}
