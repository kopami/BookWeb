using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class AccountDAO : BaseDAO<Account>
    {
        private static AccountDAO? _instance;

        public static AccountDAO Instance
        {
            get => _instance ?? new AccountDAO();            
        }

        public AccountDAO() : base()
        {
        }
    }
}
