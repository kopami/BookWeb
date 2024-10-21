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

        public Account? GetAccount(string username, string password) => _repo.GetAccount(username, password);
    }
}
