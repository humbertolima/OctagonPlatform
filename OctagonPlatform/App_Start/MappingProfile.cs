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
            CreateMap<User, UserFormViewModel>();

            // Domain to Dto
            CreateMap<UserFormViewModel, UserEditFormViewModel>();

            //Dto to Domain
            CreateMap<UserEditFormViewModel, UserFormViewModel>().ForMember(c => c.Password, opt => opt.AllowNull());

            CreateMap<User, UserBAViewModel>();

            #region BankAccount Mapping

            // Domain to Dto
            CreateMap<BankAccount, BAccountFVModel>();
            //Dto to Domain
            CreateMap<BAccountFVModel, BankAccount>();

            CreateMap<BAEditFVModel, BankAccount>();

            CreateMap<BankAccount, BAEditFVModel>();

            CreateMap<BankAccount, UserBAViewModel>();
            #endregion



            #region Partner Mapping

            CreateMap<Partner, PartnerFormViewModel>();

            CreateMap<PartnerFormViewModel, PartnerFormViewModel>();
            //Dto to Domain
            CreateMap<PartnerFormViewModel, Partner>();

            #endregion



            #region PartnerContact Mapping

            CreateMap<PartnerContact, PartnerFormViewModel>();

            CreateMap<PartnerContactFormViewModel, PartnerFormViewModel>();
            //Dto to Domain
            CreateMap<PartnerFormViewModel, PartnerContact>();

            #endregion



            #region Terminal Mapping

            CreateMap<Terminal, TerminalFormViewModel>();

            CreateMap<TerminalFormViewModel, TerminalFormViewModel>();
            //Dto to Domain
            CreateMap<TerminalFormViewModel, Terminal>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion



            #region TerminalContact Mapping

            CreateMap<TerminalContact, TerminalContactFormViewModel>();

            CreateMap<TerminalContactFormViewModel, TerminalContactFormViewModel>();
            //Dto to Domain
            CreateMap<TerminalContactFormViewModel, TerminalContact>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion

        }

    }
}