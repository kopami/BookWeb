using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _repo { get; set; }

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public Account? Login(string email, string password, out string message)
        {
            var account =  _repo.GetAccount(email);

            if (account == null)
            {
                message = "Email not found";
            }
            else if (account.Password != password)
            {
                message = "Password is incorrect";
            }
            else
            {
                message = "Login success";
                return account;
            }

            return null;
        }
    }
}
