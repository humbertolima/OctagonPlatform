using Microsoft.AspNet.Identity;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        public  ApplicationDbContext Context;
        public DbSet<T> Table;

        protected GenericRepository()
        {
            Context = new ApplicationDbContext();
            Table = Context.Set<T>();
        }

        protected GenericRepository(ApplicationDbContext context)
        {
            Context = context;

            Table = Context.Set<T>();
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
                var userName = user.Identity.GetUserId();
                (obj as IAuditEntity).CreatedAt = date;
                (obj as IAuditEntity).UpdatedBy = Context.Users.Single(x => x.UserName == userName).Id;

            }
            Save();
        }

        public void Edit(T obj)
        {
            Table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
            if (obj is IAuditEntity)
            {
                var date = DateTime.Now;
                var user = HttpContext.Current.User;
                var userName = user.Identity.GetUserId();
                (obj as IAuditEntity).UpdatedAt = date;
                (obj as IAuditEntity).UpdatedBy = Context.Users.Single(x => x.UserName == userName).Id;

            }
            Save();
        }

        public void Delete(object id)
        {
            var existing = Table.Find(id);
            if (existing is ISoftDeleted)
            {
                ((ISoftDeleted) existing).Deleted = true;
                if (existing is IAuditEntity)
                {
                    var date = DateTime.Now;
                    var user = HttpContext.Current.User;
                    var userName = user.Identity.GetUserId();
                    (existing as IAuditEntity).DeletedAt = date;
                    (existing as IAuditEntity).DeletedBy = Context.Users.Single(x => x.UserName == userName).Id;

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

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
