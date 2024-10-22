﻿using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs;
using BookWebRazor.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository() : base(ProductImageDAO.Instance)
        {
        }
    }
}