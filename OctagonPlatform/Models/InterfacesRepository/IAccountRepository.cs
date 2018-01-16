using OctagonPlatform.Models.FormsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IAccountRepository
    {
        UserLoginViewModel Login(UserLoginViewModel userLogin);

        List<Permission> GetPermissions(int userId);

        void Logout();
    }
}
