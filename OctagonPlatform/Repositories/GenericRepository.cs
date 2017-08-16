using Microsoft.AspNet.Identity;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OctagonPlatform.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        public DbSet<T> Table;

        protected GenericRepository()
        {
            this._context = new ApplicationDbContext();
            Table = _context.Set<T>();
        }

        protected GenericRepository(ApplicationDbContext db)
        {
            this._context = db;
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
                var userName = user.Identity.GetUserName();
                (obj as IAuditEntity).CreatedAt = date;
                (obj as IAuditEntity).CreatedBy = _context.Users.Single(u => u.UserName == userName);

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
                var userName = user.Identity.GetUserName();
                (obj as IAuditEntity).CreatedAt = date;
                (obj as IAuditEntity).CreatedBy = _context.Users.Single(u => u.UserName == userName);

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
                    var userName = user.Identity.GetUserName();
                    (existing as IAuditEntity).DeletedAt= date;
                    (existing as IAuditEntity).DeletedBy = _context.Users.Single(u => u.UserName == userName);

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