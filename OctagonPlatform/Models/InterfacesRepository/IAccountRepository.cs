using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IAccountRepository
    {
        User Login(UserLoginViewModel userLogin);

        void Logout();
    }
}
