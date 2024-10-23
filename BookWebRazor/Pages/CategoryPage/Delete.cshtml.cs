using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.CategoryPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        [BindProperty]
        public Category Category { get; set; } = new Category();

        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            Category? category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            bool isDeleted = _categoryService.Delete(category);
            if (isDeleted)
            {
                TempData["success"] = "Category deleted successfully";
                return Redirect(Url.Content("~/category"));
            }
            return Page();
        }
    }
}
