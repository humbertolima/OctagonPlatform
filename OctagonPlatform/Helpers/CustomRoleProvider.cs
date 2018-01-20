using OctagonPlatform.Models;
using System;
using System.Linq;
using System.Web.Security;
using System.Data.Entity;

namespace OctagonPlatform.Helpers
{
    public class CustomRoleProvider:RoleProvider
    {
        private readonly ApplicationDbContext _context;

        public CustomRoleProvider()
        {
            _context = new ApplicationDbContext();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override string[] GetRolesForUser(string username)
        {
            var user = _context.Users
                .Include(m => m.Permissions)
                .Single(x => x.UserName == username);

            var permissions = user.Permissions;

            var result = new string[permissions.Count];

            var index = 0;
            foreach (var permission in permissions)
            {
                result[index] = permission.Name;
                index++;
            }

            return result;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}