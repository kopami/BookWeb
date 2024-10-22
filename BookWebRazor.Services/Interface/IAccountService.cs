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
        public Account? Login(string email, string password, out string message);
        public Account? Register(string email, string password, string? name, out string message);
    }
}
