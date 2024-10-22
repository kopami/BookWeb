using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class ProductImageDAO : BaseDAO<ProductImage>
    {
        private static ProductImageDAO? _instance;

        public static ProductImageDAO Instance
        {
            get => _instance ?? new ProductImageDAO(new ApplicationDbContext());
        }

        public ProductImageDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}
