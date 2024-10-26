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
        IEnumerable<ShoppingCart> GetAll(string accountId, string? includeProperties = null);
        ShoppingCart? Get(Expression<Func<ShoppingCart, bool>> filter, string? includeProperties = null);
        bool Update(ShoppingCart cart);
        bool Remove(ShoppingCart cartFromDb);
    }
}
