using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs;
using BookWebRazor.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account? GetAccount(string email) => AccountDAO.Instance.Get(u => u.Email == email);
    }
}
