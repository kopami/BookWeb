using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class ShoppingCartDAO : BaseDAO<ShoppingCart>
    {
        private static ShoppingCartDAO? _instance;

        public static ShoppingCartDAO Instance
        {
            get => _instance ?? new ShoppingCartDAO(new ApplicationDbContext());
        }

        public ShoppingCartDAO(ApplicationDbContext context) : base(context)
        { }
    }
}
