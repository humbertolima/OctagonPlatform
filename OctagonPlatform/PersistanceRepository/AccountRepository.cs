using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Linq;
using System.Web.Security;

namespace OctagonPlatform.PersistanceRepository
{
    public class AccountRepository: GenericRepository<User>, IAccountRepository
    {
        public User Login(UserLoginViewModel userLogin)
        {
            var user = Table.SingleOrDefault(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password && !u.Deleted);
            if (user != null) return user;
            {
                var userTrying = Table.SingleOrDefault(u => u.UserName == userLogin.UserName && !u.Deleted);
                if (userTrying != null)
                {
                    userLogin.TriesToLogin++;
                    if (userLogin.TriesToLogin >= 3)
                        userTrying.IsLocked = true;
                }
                Save();

                return userTrying;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}