﻿using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Repositories.Interface
{
    public interface IAccountRepository
    {
        public Account? GetAccount(string email, string password);
    }
}