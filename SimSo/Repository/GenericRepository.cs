using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimSo.Models.App.Repository
{
    public class GenericRepository<T> : IDisposable where T : class
    {
        private AppDbContext context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            context = new AppDbContext();
            table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T Get(int id)
        {
            return table.Find(id);
        }

        public T Insert(T entity)
        {
            return table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T entity = table.Find(id);
            table.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}