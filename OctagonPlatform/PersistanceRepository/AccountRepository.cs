using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.PersistanceRepository
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        public List<Permission> GetPermissions(int userId)
        {
            try
            {       //pendiente validar si viene el Id
                    //List<Permission> permissions = Context.Permissions.ToList();
                var permissions = Context.Permissions
                    .Include(u=>u.Users).ToList();

                return permissions;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UserLoginViewModel Login(UserLoginViewModel userLogin)
        {
            try
            {
                var user = Table.Where(u => u.UserName == userLogin.UserName && !u.Deleted && !u.IsLocked)
                    .Include(x => x.Partner).SingleOrDefault();
                if (user == null) throw new Exception("User not found. ");
                {
                    var key = user.Key;
                    var hash = Cryptography.EncodePassword(userLogin.Password, key);

                    if (user.Password == hash)
                    {
                        return new UserLoginViewModel()
                        {
                            Id = user.Id,
                            UserName = userLogin.UserName,
                            Logo = user.Partner.Logo,
                            Partner = user.Partner,
                            BusinessName = user.Partner.BusinessName,
                            Name = user.Name,
                            UserId = user.Id
                        };
                    }
                }

                var userTrying = Table.SingleOrDefault(u => u.UserName == userLogin.UserName && !u.Deleted);
                if (userTrying == null)
                    return userLogin;

                if (userTrying.IsLocked)
                {
                    userLogin.IsLocked = true;
                    return userLogin;
                }


                if (userLogin.TriesToLogin <= 3)
                {
                    userLogin.TriesToLogin++;
                    return userLogin;
                }

                userTrying.IsLocked = true;
                Save();
                userLogin.IsLocked = true;
                return userLogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Logout()
        {
            try
            {

                System.Web.Security.Roles.DeleteCookie();
                System.Web.Security.FormsAuthentication.SignOut();

                var roles = System.Web.Security.Roles.GetRolesForUser("admin02");
                string[] users = { "admin02" };

                //System.Web.Security.Roles.RemoveUserFromRoles("admin02", roles);
                    
                //.RemoveUsersFromRoles(users, roles);

                //UserManager pepe = new UserManager<UserManagerExtensions();
                //UserManager.>
                //    Roles.RemoveUsersFromRoles()
                //var roles = Microsoft.AspNet.Identity.UserManager.GetRoles(userid);
                // UserManager.RemoveFromRoles(userid, roles.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}