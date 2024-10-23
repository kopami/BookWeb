using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.BusinessObjects.ViewModel;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace BookWebRazor.Pages.Product
{
    public class UpsertModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ProductVM ViewModel { get; set; } = new();

        public UpsertModel(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            IEnumerable<SelectListItem> categoryList = _categoryService.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            ViewModel = new ProductVM
            {
                CategoryList = categoryList,
                Product = new BusinessObjects.Model.Product()
            };

            if (id == null || id == 0)
            {
                //Create
                return Page();
            }

            ViewModel.Product = _productService.GetProductById(id ?? 0, includeProperties: "ProductImages") ?? new ();
            return Page();
        }

        public IActionResult OnPost(List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    if (ViewModel.Product.Id == 0)
                    {
                        _productService.CreateProduct(ViewModel.Product, files, wwwRootPath);
                    }
                    else
                    {
                        _productService.UpdateProduct(ViewModel.Product, files, wwwRootPath);
                    }
                }

                TempData["success"] = "Product created/updated successfully";
                return Redirect(Url.Content("~/product"));
            }
            else
            {//if modelState is invalid because of some invalid fields
             //make sure CategoryList is set to an instance
                ViewModel.CategoryList =_categoryService.GetAll()
                                    .Select(u => new SelectListItem
                                    {
                                        Text = u.Name,
                                        Value = u.Id.ToString()
                                    });
            }
            return Page();
        }

    }
}
