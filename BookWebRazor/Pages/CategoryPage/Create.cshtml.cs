using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.CategoryPage
{
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        [BindProperty]
        public Category Category { get; set; } = new Category();

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet()
        {
        }
    }
}
