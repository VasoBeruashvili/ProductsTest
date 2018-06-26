using ProductsTest.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductsTest.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private IDbSet<T> _entities;

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public GenericRepository(DataContext context)
        {
            if (_context == null)
                _context = context;
        }

        public T GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public void Delete(T entity)
        {
            if(entity != null)
                Entities.Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity != null)
                _context.Entry(entity).State = EntityState.Modified;
        }

        public void Add(T entity)
        {
            if (entity != null)
                Entities.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}