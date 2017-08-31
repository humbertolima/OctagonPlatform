using OctagonPlatform.Models.FormsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Helpers
{
    public class MapFrom<T>
    {
        public UserFormViewModel ToUserFormView(T obj) {

            var viewModel = new UserFormViewModel();

            if (obj is UserEditFormViewModel)
            {
                viewModel.Email = (obj as UserEditFormViewModel).Email;
                viewModel.Id = (obj as UserEditFormViewModel).Id;
                viewModel.IsLocked = (obj as UserEditFormViewModel).IsLocked;
                viewModel.LastName = (obj as UserEditFormViewModel).LastName;
                viewModel.Name = (obj as UserEditFormViewModel).Name;
                viewModel.PartnerId = (obj as UserEditFormViewModel).PartnerId;
                viewModel.Partners = (obj as UserEditFormViewModel).Partners;
                viewModel.Password = (obj as UserEditFormViewModel).Password;
                viewModel.Phone = (obj as UserEditFormViewModel).Phone;
                viewModel.Status = (obj as UserEditFormViewModel).Status;
                viewModel.UserName = (obj as UserEditFormViewModel).UserName;
            }
            return viewModel;
        }
    }
}