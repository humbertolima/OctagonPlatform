using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;

namespace OctagonPlatform.PersistanceRepository
{
    public class AccountRepository: GenericRepository<User>, IAccountRepository
    {
        public UserLoginViewModel Login(UserLoginViewModel userLogin)
        {
            var user = Table.Where(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password && !u.Deleted && !u.IsLocked)
                .Include(x => x.Partner).SingleOrDefault();
            if (user != null)
            {
                
                    return new UserLoginViewModel()
                    {
                        UserName = userLogin.UserName,
                        Logo = user.Partner.Logo,
                        Partner = user.Partner,
                        BusinessName = user.Partner.BusinessName
                    };
               
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

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}