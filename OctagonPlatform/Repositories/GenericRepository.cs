using Microsoft.AspNet.Identity;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        public DbSet<T> Table;

        protected GenericRepository()
        {
            _context = new ApplicationDbContext();
            Table = _context.Set<T>();
        }

        protected GenericRepository(ApplicationDbContext db)
        {
            _context = db;
            Table = db.Set<T>();
        }
        public IEnumerable<T> All()
        {
            return Table.ToList();
        }
        public T FindBy(object id)
        {
            return Table.Find(id);
        }
        public void Add(T obj)
        {
            Table.Add(obj);
            if (obj is IAuditEntity)
            {
                var date = DateTime.Now;
                var user = HttpContext.Current.User;
                ((IAuditEntity) obj).CreatedAt = date;
                ((IAuditEntity) obj).CreatedBy = user.Identity.GetUserName(); 

            }
            Save();
        }
        public void Edit(T obj)
        {
            Table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            if (obj is IAuditEntity)
            {
                var date = DateTime.Now;
                var user = HttpContext.Current.User;
                ((IAuditEntity) obj).CreatedAt = date;
                ((IAuditEntity) obj).CreatedBy = user.Identity.GetUserName();

            }
            Save();
        }
        public void Delete(object id)
        {
            var existing = Table.Find(id);
            if (existing is ISoftDeleted)
            {
                ((ISoftDeleted)existing).Deleted = true;
                if (existing is IAuditEntity)
                {
                    var date = DateTime.Now;
                    var user = HttpContext.Current.User;
                    ((IAuditEntity) existing).DeletedAt= date;
                    ((IAuditEntity) existing).DeletedBy = user.Identity.GetUserName();

                }

            }
            else
            {
                if (existing != null) Table.Remove(existing);
            }
            Save();
        }

        public void Save()
        {

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}