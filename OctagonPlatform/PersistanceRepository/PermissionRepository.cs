using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class PermissionRepository : GenericRepository<Permission>, IPermission
    {
        public IEnumerable<Permission> GetAllPermissions()
        {
            try
            {
                List<Permission> permissions = Table.Include(p => p.Parent).ToList();

                return permissions;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Permission> ToSelectControlPermissions()
        {
            try
            {
                var permissions = Table.AsEnumerable();
                return permissions;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

