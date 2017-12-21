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
                var date = DateTime.UtcNow;
                var userName = HttpContext.Current.User.Identity.Name;
             
                (obj as IAuditEntity).CreatedAt = date;
                (obj as IAuditEntity).CreatedBy = Context.Users.Single(x => x.UserName == userName && !x.Deleted).Id;
                (obj as IAuditEntity).CreatedByName = userName;
            }   
            Save();
        }

        public void Edit(T obj)
        {
            Table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
            if (obj is IAuditEntity)
            {
                var date = DateTime.UtcNow;
                var user = HttpContext.Current.User;
                var userName = user.Identity.Name;
                (obj as IAuditEntity).UpdatedAt = date;
                (obj as IAuditEntity).UpdatedBy = Context.Users.Single(x => x.UserName == userName && !x.Deleted).Id;
                (obj as IAuditEntity).UpdatedByName = userName;
            }
            Save();
        }

        public void Delete(object id)
        {
            try
            {
                var existing = Table.Find(id);
                if (existing is ISoftDeleted)
                {
                    ((ISoftDeleted)existing).Deleted = true;
                    if (existing is IAuditEntity)
                    {
                        var date = DateTime.UtcNow;
                        var user = HttpContext.Current.User;
                        var userName = user.Identity.Name;
                        (existing as IAuditEntity).DeletedAt = date;
                        (existing as IAuditEntity).DeletedBy = Context.Users.Single(x => x.UserName == userName && !x.Deleted).Id;
                        (existing as IAuditEntity).DeletedByName = userName;
                    }

                }
                else
                {
                    if (existing != null) Table.Remove(existing);
                }
                Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
          
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
