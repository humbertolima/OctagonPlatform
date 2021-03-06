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

            #region Terminal General Info

            CreateMap<Terminal, TerminalGeneralVM>(); //.ForMember(c => c.Contacts, opt => opt.MapFrom(m => m.TerminalContacts));
            CreateMap<TerminalGeneralVM, Terminal>();

            #endregion

            #region Terminal Interchanges

            CreateMap<Terminal, TerminalInterchangeVM>(); //.ForMember(c => c.Contacts, opt => opt.MapFrom(m => m.TerminalContacts));
            CreateMap<TerminalInterchangeVM, Terminal>();

            #endregion

            #region Terminal Sourcharge

            CreateMap<Terminal, TerminalSurchargeVM>(); //.ForMember(c => c.Contacts, opt => opt.MapFrom(m => m.TerminalContacts));
            CreateMap<TerminalSurchargeVM, Terminal>();

            #endregion

            #region BankAccount Mapping

            // Domain to Dto
            CreateMap<BankAccount, BAccountFVModel>();
            //Dto to Domain
            CreateMap<BAccountFVModel, BankAccount>();

            CreateMap<BAEditFVModel, BankAccount>().ForMember(x => x.Id, opt => opt.Ignore());

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

            //CreateMap<TerminalFormViewModel, TerminalFormViewModel>();
            //Dto to Domain
            CreateMap<TerminalFormViewModel, Terminal>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion

            #region Terminal Alert Ignnored Mapped

            CreateMap<TerminalAlertConfig, TerminalConfigViewModel>();

            CreateMap<TerminalConfigViewModel, TerminalAlertConfig>();
            //Dto to Domain
            #endregion

            #region TerminalContact Mapping

            CreateMap<TerminalContact, TerminalContactFormViewModel>();
            CreateMap<TerminalContactFormViewModel, TerminalContactFormViewModel>();
            //Dto to Domain
            CreateMap<TerminalContactFormViewModel, TerminalContact>().ForMember(c => c.Id, opt => opt.Ignore());
          
            #endregion

            #region Terminal Contacts

            CreateMap<Terminal, TerminalContactVM>().ForMember( c=> c.Contacts, opt => opt.MapFrom(m=> m.TerminalContacts));
            CreateMap<TerminalContactVM, Terminal>();

            #endregion

            #region Terminal VaultCash

            CreateMap<Terminal, TerminalVaultCashVM>(); //.ForMember(c => c.Contacts, opt => opt.MapFrom(m => m.TerminalContacts));
            CreateMap<TerminalVaultCashVM, Terminal>();

            #endregion

            #region VaultCash Mapping

            CreateMap<VaultCash, VaultCashFormViewModel>();

            CreateMap<VaultCashFormViewModel, VaultCashFormViewModel>();
            //Dto to Domain
            CreateMap<VaultCashFormViewModel, VaultCash>();

            #endregion

            #region Surcharge Mapping

            CreateMap<Surcharge, SurchargeFormViewModel>();

            CreateMap<SurchargeFormViewModel, SurchargeFormViewModel>();
            //Dto to Domain
            CreateMap<SurchargeFormViewModel, Surcharge>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion

            #region InterChange Mapping

            CreateMap<InterChange, InterChangeFormViewModel>();

            CreateMap<InterChangeFormViewModel, InterChangeFormViewModel>();
            //Dto to Domain
            CreateMap<InterChangeFormViewModel, InterChange>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion

            #region Dispute

            CreateMap<Dispute, DisputeViewModel>();

            CreateMap<DisputeViewModel, Dispute>();
            //Dto to Domain
            //CreateMap<InterChangeFormViewModel, InterChange>().ForMember(c => c.Id, opt => opt.Ignore());

            #endregion

            #region Dispute Represents

            CreateMap<DisputeRepresent, DisputeRepresentVM>();

            CreateMap<DisputeRepresentVM, DisputeRepresent>();
            #endregion

            #region Terminals Cassettes

            CreateMap<Terminal, TerminalCassetteVM>();

            CreateMap<TerminalCassetteVM, Cassette>();
            #endregion

            #region Terminals Notes

            CreateMap<Terminal, TerminalNotesVM>();

            CreateMap<TerminalNotesVM, Terminal>();
            #endregion

            #region Terminals Documents

            CreateMap<Terminal, TerminalDocumentsVM>();

            CreateMap<TerminalDocumentsVM, Terminal>();
            #endregion

            #region Terminals Pictures

            CreateMap<Terminal, TerminalPicturesVM>();

            CreateMap<TerminalPicturesVM, Terminal>();
            #endregion

















        }

    }
}