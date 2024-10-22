using BookWebRazor.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static BookWebRazor.Pages.Account.LoginModel;

namespace BookWebRazor.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public string ReturnUrl { get; set; } = string.Empty;
        [TempData]
        public string ErrorMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            public string? Name { get; set; }
        }

        public RegisterModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public void OnGet()
        {
        }
    }
}
