using FashionNet.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext dbContext;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            dbContext = context;
            this.dbSet = context.Set<T>();
        }
        public void Agregar(T entity)
        {
            dbSet.Add(entity);
        }

        public void EliminarPorEntidad(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> MostrarTodos(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includePropertie in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropertie);
                }
            }
            return query.ToList();
        }

        public T ObtenerPorId(int id)
        {
            return dbSet.Find(id);
        }
    }
}

