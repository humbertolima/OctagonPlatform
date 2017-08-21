using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> All();
        T FindBy(object id);
        void Add(T obj);
        void Edit(T obj);
        void Delete(object id);
        void Dispose();
        void Save();
    }
}
