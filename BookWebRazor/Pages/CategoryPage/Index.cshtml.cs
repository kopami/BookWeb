using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWebRazor.Pages.CategoryPage;

[Authorize(Roles = nameof(RoleEnum.Admin))]
public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    [BindProperty]
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public void OnGet()
    {
        Categories = _categoryService.GetAll();
    }
}
