using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.BusinessObjects.ViewModel
{
    public class ShoppingCartVM
    {
        public List<ShoppingCart> ShoppingCartList { get; set; } = new List<ShoppingCart>();
        public OrderHeader OrderHeader { get; set; } = new();
    }
}
