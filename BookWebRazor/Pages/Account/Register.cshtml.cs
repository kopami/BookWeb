using BookWebRazor.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.BusinessObjects.Enum;

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
        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = _accountRepository.GetAccount(Input.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already registered.");
                    return Page();
                }

                // Hash the password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Input.Password);

                // Save the user to the database
                var newAccount = new BusinessObjects.Model.Account
                {
                    Email = Input.Email,
                    Password = hashedPassword,
                    Role = RoleEnum.Customer.ToString() // Default role
                };
                _accountRepository.AddAccount(newAccount);

                // Automatically log the user in after registration
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newAccount.Email),
                new Claim(ClaimTypes.Role, newAccount.Role)
            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return LocalRedirect(returnUrl);
            }

            return Page();
        }

    }
}
