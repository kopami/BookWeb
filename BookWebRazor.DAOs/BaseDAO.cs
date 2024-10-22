using BookWebRazor.DAOs.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs
{
    public class BaseDAO<T> where T : class
    {
        protected ApplicationDbContext? _context;
        internal DbSet<T>? _dbSet;

        public BaseDAO(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public bool Add(T entity)
        {
            bool result = false;
            try
            {
                if (entity != null)
                {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public bool Update(T entity)
        {
            bool result = false;
            try
            {
                if (entity != null)
                {
                    _dbSet.Update(entity);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public bool Delete(T entity)
        {
            bool result = false;
            try
            {
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
                //no tracking : Database will not be updated if the Update method is not called before the Save method                
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
    }
}
