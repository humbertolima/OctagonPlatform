using OctagonPlatform.Models.DetailsViewModels;
using OctagonPlatform.Models.FormsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        UserFormViewModel RenderUserFormViewModel();

        void SaveUser(UserFormViewModel viewModel, string action);

        UserFormViewModel UserToEdit(int id);

        UserDetailsViewModel UserDetails(int id);

        void DeleteUser(int id);
    }
}
