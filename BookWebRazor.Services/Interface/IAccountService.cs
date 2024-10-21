using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services.Interface
{
    public interface IAccountService
    {
        public Account? GetAccount(string username, string password);
    }
}
