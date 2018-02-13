using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IPermission: IGenericRepository<Permission>
    {
        IEnumerable<Permission> GetAllPermissions();

        IEnumerable<Permission> ToSelectControlPermissions();
    }
}
