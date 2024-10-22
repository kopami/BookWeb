using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class OrderHeaderDAO : BaseDAO<OrderHeader>
    {
        private static OrderHeaderDAO? _instance;

        public static OrderHeaderDAO Instance
        {
            get => _instance ?? new OrderHeaderDAO(new ApplicationDbContext());
        }

        public OrderHeaderDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}
