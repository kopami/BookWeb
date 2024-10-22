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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository() : base(ProductDAO.Instance)
        {
        }
    }
}
