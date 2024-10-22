using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs;
using BookWebRazor.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BaseDAO<T> _dao;
        public Repository(BaseDAO<T> dao) { _dao = dao; }
        public bool Add(T entity) => _dao.Add(entity);

        public bool AddRange(IEnumerable<T> entities) => _dao.AddRange(entities);

        public bool Delete(int id)
        {
            var entity = _dao.Get(x => x.Id == id);
            if (entity == null) return false;
            return _dao.Delete(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            return _dao.Get(filter, includeProperties, tracked);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            return _dao.GetAll(filter, includeProperties);
        }

        public bool Update(T entity)
        {
            return _dao.Update(entity);
        }
    }
}
