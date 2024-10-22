using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class OrderDetailDAO : BaseDAO<OrderDetail>
    {
        private static OrderDetailDAO? _instance;

        public static OrderDetailDAO Instance
        {
            get => _instance ?? new OrderDetailDAO(new ApplicationDbContext());
        }

        public OrderDetailDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}
