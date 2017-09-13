using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Domain to Dto
            CreateMap<User,UserFormViewModel>();

            // Domain to Dto
            CreateMap<UserFormViewModel, UserEditFormViewModel>();

            //Dto to Domain
            CreateMap<UserEditFormViewModel, UserFormViewModel>().ForMember(c => c.Password, opt => opt.AllowNull());

        }

    }
}