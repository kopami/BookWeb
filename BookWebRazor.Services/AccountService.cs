using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Account? Login(string email, string password, out string message)
        {
            var account = _accountRepository.Get(a => a.Email == email);
            if (account == null)
            {
                message = "Email not found";
            }
            else if (!BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                message = "Invalid Password";
            }
            else
            {
                message = "Login successful";
                return account;
            }

            return null;
        }

        public Account? Register(string email, string password, string? name, out string message)
        {
            var account = _accountRepository.Get(a => a.Email == email);
            if (account != null)
            {
                message = "Email already exists";
                return null;
            }
            // Save the user to the database
            var newAccount = new Account
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Name = name,
                Role = RoleEnum.Customer.ToString()
            };
            bool isSuccess = _accountRepository.Add(newAccount);

            if (isSuccess)
            {
                message = "Registration successful";
                return newAccount;
            }

            message = "Registration failed";
            return null;
        }
    }
}
