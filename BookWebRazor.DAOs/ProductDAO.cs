using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class ProductDAO : BaseDAO<Product>
    {
        private static ProductDAO? _instance;

        public static ProductDAO Instance
        {
            get => _instance ?? new ProductDAO(new ApplicationDbContext());
        }

        public ProductDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}
