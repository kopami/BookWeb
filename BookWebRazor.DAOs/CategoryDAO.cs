using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class CategoryDAO : BaseDAO<Category>
    {
        private static CategoryDAO? _instance;

        public static CategoryDAO Instance
        {
            get => _instance ?? new CategoryDAO(new ApplicationDbContext());
        }

        public CategoryDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}
