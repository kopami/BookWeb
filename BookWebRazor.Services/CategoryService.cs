using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool Add(Category category) => _categoryRepository.Add(category);

        public IEnumerable<Category> GetAll() => _categoryRepository.GetAll();

        public Category? GetById(int? id) => id == null ? null : _categoryRepository.Get(c => c.Id == id);

        public bool Update(Category category) => _categoryRepository.Update(category);
    }
}
