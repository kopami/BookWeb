using BookWebRazor.BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        bool Add(Category category);
        Category? GetById(int? id);
        bool Update(Category category);
        bool Delete(Category category);
    }
}
