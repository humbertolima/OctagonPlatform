using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IAccountRepository
    {
        UserLoginViewModel Login(UserLoginViewModel userLogin);

        string GetPermissions(int userId);

        void Logout();
    }
}
