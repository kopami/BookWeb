using BookWebRazor.BusinessObjects.Constants;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddToCart(ShoppingCart cart)
        {
            var context = _httpContextAccessor.HttpContext;
            ShoppingCart? cartFromDb = _shoppingCartRepository.Get(c => c.AccountId == cart.AccountId && c.ProductId == cart.ProductId);
            
            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += cart.Count;
                _shoppingCartRepository.Update(cartFromDb);
            }
            else
            {
                //add cart record
                _shoppingCartRepository.Add(cart);
                context.Session.SetInt32(Consts.SessionCart, _shoppingCartRepository.GetAll(u => u.AccountId == cart.AccountId).Count());
            }
        }

        public int Count(Expression<Func<ShoppingCart, bool>> filter)
        {
            return _shoppingCartRepository.Count(filter);
        }

        public ShoppingCart? Get(Expression<Func<ShoppingCart, bool>> filter, string? includeProperties = null)
        {
            return _shoppingCartRepository.Get(filter, includeProperties);
        }

        public IEnumerable<ShoppingCart> GetAll(string accountId, string? includeProperties = null)
        {
            if (int.TryParse(accountId, out int accountIdInt))
            {
                return _shoppingCartRepository.GetAll(u => u.AccountId == accountIdInt, includeProperties);
            }
            return new List<ShoppingCart>();
        }

        public bool Remove(ShoppingCart cartFromDb) => _shoppingCartRepository.Delete(cartFromDb);

        public bool Update(ShoppingCart cart) => _shoppingCartRepository.Update(cart);
    }
}
