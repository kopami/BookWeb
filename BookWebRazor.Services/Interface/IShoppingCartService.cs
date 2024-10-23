using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services.Interface
{
    public interface IShoppingCartService
    { 
        void AddToCart(ShoppingCart cart);
        int Count(Expression<Func<ShoppingCart, bool>> filter);
    }
}
