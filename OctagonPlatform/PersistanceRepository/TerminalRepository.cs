using AutoMapper;
using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.Views.ReportsSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalRepository : GenericRepository<Terminal>, ITerminalRepository
    {

        public async Task<List<JsonLoadCash>> GetCashLoad(DateTime start, DateTime end, string terminalId)
        {
            List<JsonLoadCash> list = new List<JsonLoadCash>();
            ApiATM api = new ApiATM();

            list = await api.CashLoad(start, end, terminalId);

            return list;
        }



        public KeyManager GetKey(string messagesId)
        {
            try
            {
                var url = "http://apiatm.azurewebsites.net/api/key/getbyterminal/" + messagesId;
                var json = new WebClient().DownloadString(url);
                var list = JsonConvert.DeserializeObject<KeyManager>(json);

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Key not found. ");
            }
        }

        public BindKeyViewModel SetBindKey(string messagesId, string serial1, string serial2)
        {
            try
            {
                var url = "http://apiatm.azurewebsites.net/api/key/bindkey/" + serial1 + "/" + serial2 + "/" + messagesId;
                var json = new WebClient().DownloadString(url);
                var list = JsonConvert.DeserializeObject<bool>(json);
                var viewModel = new BindKeyViewModel() { Serial1 = "", Serial2 = "", TerminalId = "" };

                if (list)
                {
                    viewModel = new BindKeyViewModel() { TerminalId = messagesId, Serial1 = serial1, Serial2 = serial2 };
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Binded Key not found. ");
            }

        }

        public Terminal GetTerminal(string terminalId)
        {
            try
            {
                var terminal = Table.FirstOrDefault(x => x.TerminalId == terminalId);
                return terminal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Terminal> GetAllTerminals(int partnerId)
        {
            try
            {
                var parent = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (parent == null) throw new Exception("Parent not found. ");

                var terminals = new List<Terminal>();

                var partner = Context.Partners.Where(x => x.Id == partnerId && !x.Deleted)
                    .Include(x => x.Partners)
                    .SingleOrDefault();


                if (partner == null) return terminals;
                {
                    var partnerTerminals = Table.Where(x => x.PartnerId == partnerId && !x.Deleted)
                        .Include(x => x.Partner)
                        .Include(x => x.Country)
                        .Include(x => x.State)
                        .Include(x => x.City)
                        .Include(x => x.LocationType)
                        .ToList();

                    terminals.AddRange(partnerTerminals);

                    foreach (var item in partner.Partners)
                    {
                        if (item.Id != partnerId)
                        {
                            terminals.AddRange(Table.Where(x => x.PartnerId == item.Id && !x.Deleted)
                                .Include(x => x.Partner)
                                .Include(x => x.Country)
                                .Include(x => x.State)
                                .Include(x => x.City)
                                .Include(x => x.LocationType)
                                .ToList());
                        }
                    }
                }

                return terminals;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminals not found. ");
            }
        }

        public IEnumerable<Terminal> Search(string search, int partnerId)
        {
            try
            {
                return Table.Where(x => !x.Deleted && x.PartnerId == partnerId &&
                                        (x.LocationType.Name.Contains(search) || x.Model.Name.Contains(search) ||
                                         x.Make.Name.Contains(search)))
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TerminalFormViewModel RenderTerminalFormViewModel(int partnerId)
        {
            try
            {
                var parent = Context.Partners.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (parent == null) throw new Exception("Parent not found. ");

                return new TerminalFormViewModel()
                {
                    Countries = Context.Countries.ToList(),
                    States = Context.States.Where(x => x.CountryId == 231).ToList(),
                    Status = StatusType.Status.Active,
                    Cities = Context.Cities.Where(x => x.StateId == 3930).ToList(),
                    Partners = Context.Partners.Where(x => (x.Id == partnerId || x.ParentId == partnerId) && !x.Deleted).ToList(),
                    PartnerId = partnerId,
                    Partner = parent,
                    LocationTypes = Context.LocationTypes.ToList(),
                    Makes = Context.Makes.ToList(),
                    Models = Context.Models.ToList(),
                    LocationTypeId = 5,
                    MakeId = 1,
                    ModelId = 1,
                    CommunicationType = CommunicationType.Communication.TcpIp,
                    WhoInitiates = WhoInitiateDayClsed.Who.Host,
                    SurchargeType = SurchargedBy.SurchargeTypes.Greater
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminal not found.");
            }
        }

        public TerminalFormViewModel TerminalToEdit(int id)
        {
            try
            {


                var terminal = Table.Where(x => x.Id == id)
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Make)
                    .Include(x => x.Model)
                    .Include(x => x.LocationType)
                    .SingleOrDefault();
                if (terminal == null) throw new Exception("Terminal does not exist in our records!!!");
                {
                    var terminalViewModel = Mapper.Map<Terminal, TerminalFormViewModel>(terminal);
                    terminalViewModel.Countries = Context.Countries.ToList();
                    terminalViewModel.States = Context.States.Where(x => x.CountryId == terminal.CountryId).ToList();
                    terminalViewModel.Cities = Context.Cities.Where(x => x.StateId == terminal.StateId).ToList();
                    terminalViewModel.Partners = Context.Partners.Where(x => (x.Id == terminal.PartnerId || x.ParentId == terminal.PartnerId) && !x.Deleted).ToList();
                    terminalViewModel.Partner = terminal.Partner;
                    terminalViewModel.LocationTypes = Context.LocationTypes.ToList();
                    terminalViewModel.Makes = Context.Makes.ToList();
                    terminalViewModel.Models = Context.Models.ToList();
                    return terminalViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminal not found.");
            }
        }

        public void SaveTerminal(TerminalFormViewModel viewModel, string action)
        {
            try
            {

                if (action == "Edit")
                {
                    var terminalToEdit = Table.SingleOrDefault(x => x.Id == viewModel.Id && !x.Deleted);
                    if (terminalToEdit == null) throw new Exception("Terminal does not exist in our records!!!");

                    Mapper.Map(viewModel, terminalToEdit);

                    Edit(terminalToEdit);

                }
                else
                {

                    var terminal = Mapper.Map<TerminalFormViewModel, Terminal>(viewModel);


                    terminal.TerminalId = "000000000";
                    Add(terminal);

                    terminal.TerminalId = TerminalIdGenerator.Generator(terminal.Id);

                    Edit(terminal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
            }
        }

        public Terminal TerminalDetails(int id)
        {
            try
            {
                var terminal = Table.Where(x => x.Id == id && !x.Deleted)
                    .FirstOrDefault();

                if (terminal == null) throw new Exception("Terminal not found. ");

                return terminal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTerminal(int id)
        {
            try
            {
                var terminal = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if (terminal == null) throw new Exception("Terminal not found. ");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminal not found.");
            }
        }

        public void CassettesDelete(int cassetteId)
        {
            try
            {
                var cassette = Context.Cassettes.Single(x => x.Id == cassetteId);

                Context.Cassettes.Remove(cassette);
                Context.SaveChanges();
                Context.Dispose();

                //var terminal = Table.Include(x => x.Cassettes).SingleOrDefault(c => c.Cassettes.FirstOrDefault(p => p.Id == cassetteId).Id ==cassetteId);
                //if (terminal == null) throw new Exception("Terminal not found. ");

                //var cassette = terminal.Cassettes.FirstOrDefault(x => x.Id == cassetteId);
                //terminal.Cassettes.Remove(cassette);

                //Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminal not found.");
            }
        }

        public TerminalFormViewModel InitializeNewFormViewModel(TerminalFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found. ");
                viewModel.Countries = Context.Countries.ToList();
                viewModel.States = Context.States.Where(x => x.CountryId == 231).ToList();
                viewModel.Cities = Context.Cities.Where(x => x.StateId == 3930).ToList();
                viewModel.Partners = Context.Partners.Where(x => !x.Deleted).ToList();
                viewModel.Partner = Context.Partners.SingleOrDefault(x => x.Id == viewModel.PartnerId);
                viewModel.LocationTypes = Context.LocationTypes.ToList();
                viewModel.Makes = Context.Makes.ToList();
                viewModel.Models = Context.Models.ToList();
                viewModel.LocationTypeId = 5;
                viewModel.MakeId = 1;
                viewModel.ModelId = 1;
                viewModel.CommunicationType = CommunicationType.Communication.TcpIp;
                viewModel.WhoInitiates = WhoInitiateDayClsed.Who.Host;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Model not found. ");
            }
        }

        public TerminalConfigViewModel GetConfigNotification(int id)
        {
            try
            {
                Terminal terminal = Table
                    .Include(c => c.TerminalAlertConfigs)
                    .Include(c => c.WorkingHours)
                    .FirstOrDefault(c => c.Id == id);
                if (terminal == null) throw new Exception("Terminal not found. ");    //Context.TerminalAlertConfigs.FirstOrDefault(c => c.TerminalId == terminalId);
                if (terminal.TerminalAlertConfigs == null)
                {
                    terminal.TerminalAlertConfigs = new TerminalAlertConfig();
                }
                var terminalAlertConfigViewModel = Mapper.Map<TerminalAlertConfig, TerminalConfigViewModel>(terminal.TerminalAlertConfigs);

                terminalAlertConfigViewModel.WorkingHours = terminal.WorkingHours;
                terminalAlertConfigViewModel.Id = terminal.Id;
                terminalAlertConfigViewModel.TerminalId = terminal.TerminalId;

                return terminalAlertConfigViewModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }

        }

        public TerminalConfigViewModel SetConfiguration(TerminalConfigViewModel terminalConfigVM)
        {
            try
            {
                var terminalAlertConfig = Mapper.Map<TerminalConfigViewModel, TerminalAlertConfig>(terminalConfigVM);

                Terminal terminal = Table
                    .Include(c => c.TerminalAlertConfigs)
                    .FirstOrDefault(c => c.Id == terminalConfigVM.Id);

                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.TerminalAlertConfigs = terminalAlertConfig;

                Edit(terminal);

                TerminalConfigViewModel viewModel = GetConfigNotification(terminal.Id);

                return viewModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Terminal SetWorkingHours(TerminalConfigViewModel terminalConfigViewModel, string workingHoursEdit)
        {
            try
            {
                var startTime = new TimeSpan(terminalConfigViewModel.StartTime, 00, 00);
                var endTime = new TimeSpan(terminalConfigViewModel.EndTime, 00, 00);

                var terminal = Table
                    .Include(c => c.WorkingHours)
                    .FirstOrDefault(c => c.Id == terminalConfigViewModel.Id);

                if (terminal == null) throw new Exception("Terminal not found. ");

                var terminalWorkingHours = terminal.WorkingHours.FirstOrDefault(c => c.Id == Convert.ToInt32(workingHoursEdit));
                if (terminalWorkingHours != null)
                    terminalWorkingHours.StartTime =
                        startTime;
                var firstOrDefault = terminal.WorkingHours.FirstOrDefault(c => c.Id == Convert.ToInt32(workingHoursEdit));
                if (firstOrDefault != null)
                    firstOrDefault.EndTime = endTime;
                var workingHours = terminal.WorkingHours.FirstOrDefault(c => c.Id == Convert.ToInt32(workingHoursEdit));
                if (workingHours != null)
                    workingHours.Day =
                        terminalConfigViewModel.Days;

                Edit(terminal);


                return terminal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }
        }

        public Terminal AddWorkingHours(TerminalConfigViewModel terminalConfigViewModel)
        {


            try
            {
                var startTime = new TimeSpan(terminalConfigViewModel.StartTime, 00, 00);
                var endTime = new TimeSpan(terminalConfigViewModel.EndTime, 00, 00);

                var terminal = Table
                    .FirstOrDefault(c => c.Id == terminalConfigViewModel.Id);

                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.WorkingHours = new List<TerminalWorkingHours>
                {
                    new TerminalWorkingHours
                    {
                        Day = terminalConfigViewModel.Days,
                        StartTime = startTime,
                        EndTime = endTime,
                        TerminalId = terminal.TerminalId
                    }
                };

                Save();
                return terminal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }
        }

        public Terminal DeteteWorkingHours(int terminalId, int WorkingHoursId)
        {
            //pendiente. manipulo la terminal porqur a la hora de aplicar permisos, tengo que validar los permisos que tiene
            //ese usuario para borrar o editar la terminal y los campos que tiene esa terminal. en este caso, workingHours 
            //no lo puedo controlar si el usuario tiene acceso a eliminarlo o no.
            try
            {
                Terminal terminal = TerminalDetails(terminalId);

                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.WorkingHours.Remove(terminal.WorkingHours.FirstOrDefault(c => c.Id == WorkingHoursId));
                Edit(terminal);

                return terminal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }
        }

        public TerminalPicturesVM GetPictures(int id)
        {
            try
            {
                if (id > 0)
                {
                    Terminal terminal = Table.Include(m => m.Pictures).FirstOrDefault(m => m.Id == id);
                    TerminalPicturesVM viewModel = Mapper.Map<Terminal, TerminalPicturesVM>(terminal);
                    return viewModel;
                }
                else
                {
                    return new TerminalPicturesVM();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Terminal SetPictures(int id, HttpPostedFileBase archive, int? pictureId)
        {
            Terminal terminal = new Terminal();
            if (archive != null)
            {
                terminal = TerminalDetails(id);

                if (pictureId == null || pictureId == 0)
                {
                    terminal.Pictures.Add(new Picture
                    {
                        Name = archive.FileName,
                        Archive = ConvertTo.ImageToByteArray(archive),
                    });

                    Save();
                }
            }
            else
            {
                throw new Exception("Need document Attach");
            }
            return terminal;
        }


        public TerminalInterchangeVM GetInterchanges(int id)
        {
            try
            {
                if (id > 0)
                {
                    Terminal terminal = Table
                        .Include(m => m.InterChanges)
                        .Include(m => m.InterChanges.Select(s => s.BankAccount))
                        .FirstOrDefault(m => m.Id == id);

                    TerminalInterchangeVM viewModel = Mapper.Map<Terminal, TerminalInterchangeVM>(terminal);

                    return viewModel;
                }
                else
                {
                    return new TerminalInterchangeVM();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TerminalSurchargeVM GetSurcharges(int id)
        {
            try
            {
                if (id > 0)
                {
                    Terminal terminal = Table
                        .Include(m => m.Surcharges)                 //.Where(a => a.Deleted != false) //para traer los que no estan borrados con soft delete. me da error porque si viene vacio, da error en el bank acount que le sigue
                        .Include(m => m.Surcharges.Select(s => s.BankAccount))      //buenisismo este ejemplo para incluir listas de otra lista en el include
                        .FirstOrDefault(m => m.Id == id && !m.Deleted);

                    TerminalSurchargeVM viewModel = Mapper.Map<Terminal, TerminalSurchargeVM>(terminal);

                    return viewModel;
                }
                else
                {
                    return new TerminalSurchargeVM();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TerminalVaultCashVM GetVaultCash(int id)
        {
            try
            {
                TerminalVaultCashVM viewModel;
                if (id > 0)
                {
                    Terminal terminal = Table
                        .Include(m => m.VaultCash)
                        .Include(m => m.VaultCash.BankAccount)
                        .FirstOrDefault(m => m.Id == id && !m.VaultCash.Deleted);
                    if (terminal != null)
                    {
                        viewModel = Mapper.Map<Terminal, TerminalVaultCashVM>(terminal);
                        return viewModel;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TerminalContactVM GetContacts(int id)
        {
            try
            {
                if (id > 0)
                {
                    Terminal terminal = Table.Include(m => m.TerminalContacts).FirstOrDefault(m => m.Id == id);
                    TerminalContactVM viewModel = Mapper.Map<Terminal, TerminalContactVM>(terminal);
                    return viewModel;
                }
                else
                {
                    return new TerminalContactVM();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TerminalDocumentsVM GetDocuments(int id)
        {
            try
            {
                if (id > 0)
                {
                    Terminal terminal = Table.Include(m => m.Documents).FirstOrDefault(m => m.Id == id);
                    TerminalDocumentsVM viewModel = Mapper.Map<Terminal, TerminalDocumentsVM>(terminal);
                    return viewModel;
                }
                else
                {
                    return new TerminalDocumentsVM();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Terminal SetDocuments(int indexTerminalId, HttpPostedFileBase archive, int? documentId)
        {
            Terminal terminal = new Terminal();
            if (archive != null)
            {
                terminal = TerminalDetails(indexTerminalId);

                if (documentId == null || documentId == 0)
                {
                    terminal.Documents.Add(new Document
                    {
                        Name = archive.FileName,
                        Archive = Helpers.ConvertTo.DocumentToByteArray(archive),
                    });
                    Save();
                }
            }
            else
            {
                throw new Exception("Need document Attach");
            }
            return terminal;
        }

        public Terminal PictureDelete(int id, int pictureId)
        {
            Terminal terminal = Table.Include(m => m.Pictures).FirstOrDefault(m => m.Id == id);

            if (pictureId > 0)
            {
                Context.Pictures.Remove(terminal.Pictures.FirstOrDefault(c => c.Id == pictureId));
                Context.SaveChanges();
            }
            return terminal;
        }

        public Terminal DocumentDelete(int id, int documentId)
        {
            try
            {
                Terminal terminal = Table.Include(m => m.Documents).FirstOrDefault(m => m.Id == id);

                if (documentId > 0)
                {
                    Context.Documents.Remove(terminal.Documents.FirstOrDefault(c => c.Id == documentId));
                    Context.SaveChanges();
                }
                return terminal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TerminalGeneralVM GetGeneralInfo(int id)
        {
            try
            {
                Terminal terminal;
                if (id > 0)
                {
                    terminal = Table
                        .Include(m => m.Partner)
                        .Include(m => m.Country)
                        .Include(m => m.State)
                        .Include(m => m.LocationType)
                        .Include(m => m.Make)
                        .Include(m => m.Model)
                        .FirstOrDefault(m => m.Id == id);
                }
                else
                {
                    terminal = new Terminal();
                }

                TerminalGeneralVM viewModel = Mapper.Map<Terminal, TerminalGeneralVM>(terminal);

                return viewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TerminalNotesVM GetNotes(int id)
        {
            try
            {
                Terminal terminal;
                if (id > 0)
                {
                    terminal = Table
                        .Include(m => m.Notes)
                        .FirstOrDefault(m => m.Id == id);
                }
                else
                {
                    terminal = new Terminal();
                }

                TerminalNotesVM viewModel = Mapper.Map<Terminal, TerminalNotesVM>(terminal);

                return viewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Terminal SetNotes(int id, string note, int? noteId)
        {
            try
            {
                Terminal terminal = Table.Include(m => m.Notes).FirstOrDefault(m => m.Id == id);

                if (noteId == null || noteId == 0)
                {
                    if (terminal != null)
                    {
                        terminal.Notes.Add(new Note { Nota = note });
                        Save();
                    }
                }
                else
                {
                    terminal.Notes.FirstOrDefault(c => c.Id == noteId).Nota = note;
                    Edit(terminal);
                }

                return terminal;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Terminal DeleteNotes(int indexTerminalId, int noteId)
        {

            if (indexTerminalId > 0 || noteId > 0)
            {
                Note note = Context.Notes.Remove(Context.Notes.FirstOrDefault(c => c.Id == noteId));

                Context.SaveChanges();
            }

            return TerminalDetails(indexTerminalId);
        }

        public TerminalCassetteVM GetCassettes(int id)
        {
            TerminalCassetteVM viewModel;

            try
            {
                Terminal terminal = Table
                    .Include(c => c.Cassettes)
                    .FirstOrDefault(c => c.Id == id);

                if (terminal == null) throw new Exception("Terminal not found. ");

                if (terminal.Cassettes == null)
                {
                    viewModel = new TerminalCassetteVM();
                }
                else
                {
                    viewModel = Mapper.Map<Terminal, TerminalCassetteVM>(terminal);

                }

                return viewModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public Terminal CassettesEdit(bool autoRecord, int denomination, int id, int? cassetteId)
        {
            try
            {
                Terminal terminal;
                if (cassetteId != null || cassetteId > 0)
                {
                    terminal = Table.Include(m => m.Cassettes).FirstOrDefault(m => m.Id == id);

                    terminal.Cassettes.FirstOrDefault(m => m.Id == cassetteId).Denomination = denomination;
                    terminal.Cassettes.FirstOrDefault(m => m.Id == cassetteId).AutoRecord = autoRecord;
                    Edit(terminal);
                }
                else
                {
                    terminal = Table.FirstOrDefault(m => m.Id == id);

                    if (terminal != null)
                    {
                        terminal.Cassettes.Add(new Cassette { AutoRecord = autoRecord, Denomination = denomination, TerminalId = id });
                        Save();
                    }
                }
                return terminal;
            }
            catch (Exception)
            {
                //pendiente
                throw;
            }
        }

        public IEnumerable<dynamic> LoadCashList(List<JsonLoadCash> list, StatusType.Status status, int partnerId, int parentId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();
                return list4.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();


            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }

        }

        public IEnumerable<string> GetAllTerminalId(string value,int partnerId)
        {
            try
            {
                IEnumerable<Partner> listpartner = GetPartnerByParentId(partnerId);
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();

                return list4.Where(b => b.TerminalId.ToLower().Contains(value.ToLower())).Select(b => b.TerminalId).ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Terminal> GetTerminalAssociatedGroup(int partnerId, int stateid, int cityid, string zipcode, int? groupId = null)
        {

            try
            {
                return Table.Where(b => b.ReportGroupId == groupId)
              .Where(b => partnerId == 0 || b.PartnerId == partnerId)
              .Where(b => stateid == 0 || b.StateId == stateid)
              .Where(b => cityid == 0 || b.CityId == cityid)
              .Where(b => zipcode == "" || b.Zip.ToString() == zipcode)
              .Include(x => x.Partner).ToList();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<dynamic> GetAllState(string term)
        {
            try
            {
                return Table.Where(b => b.State.Name.Contains(term)).Select(b => new { label = b.State.Name, value = b.State.Id }).Distinct().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<dynamic> GetAllCity(string term)
        {
            try
            {
                return Table.Where(b => b.City.Name.Contains(term)).Select(b => new { label = b.City.Name, value = b.City.Id }).Distinct().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<string> GetAllZipCode(string term)
        {
            try
            {
                return Table.Where(b => b.Zip.ToString().Contains(term)).Select(b => b.Zip.ToString()).Distinct().ToList();
            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public void EditRange(string[] list, int? groupId)
        {
            try
            {

                foreach (string item in list)
                {
                    Terminal tn = FindBy(Int32.Parse(item));
                    tn.ReportGroupId = groupId;
                    Edit(tn);
                }
            }
            catch (Exception e)
            {

                throw new NullReferenceException(e.Message);
            }
        }

        public IEnumerable<dynamic> LoadCashMngList(List<JsonCashManagement> list, StatusType.Status status, int partnerId, int parentId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();

                return list4.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();


            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<TerminalTableVM> GetTerminalsReport(TerminalListViewModel vmodel, string[] listtn, int parentId)
        {

            DateTime? start = null;
            DateTime? end = null;
            if (vmodel.StartDate != null) start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (vmodel.EndDate != null) end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            bool groupfilter = listtn == null ? false : true;
            string[] aux = { "0" };
            listtn = listtn ?? aux;
            int zip = Int32.Parse(vmodel.ZipCode ?? "0");

            IEnumerable<Partner> listpartner = vmodel.PartnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(vmodel.PartnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro


            IEnumerable<Terminal> list4 = (from p in listpartner join m in Table.Include(b => b.TerminalContacts).Include(b => b.Make).Include(b => b.City).Include(b => b.State) on p.Id equals m.PartnerId select m).ToList();

            try
            {
                var list = list4.Where(b => groupfilter == false || listtn.Contains(b.TerminalId))
                .Where(b => vmodel.Status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == vmodel.Status)
                .Where(b => vmodel.AccountId == -1 || b.Partner.BankAccounts.Where(z => z.Id == vmodel.AccountId).Count() > 0)
                .Where(b => vmodel.TerminalId == null || b.TerminalId == vmodel.TerminalId)
                .Where(b => vmodel.CityId == -1 || b.CityId == vmodel.CityId)
                .Where(b => vmodel.StateId == -1 || b.CityId == vmodel.StateId)
                .Where(b => vmodel.ZipCode == null || b.Zip == zip)
                .Where(b => vmodel.ConectionType == CommunicationType.Communication.All ? (b.CommunicationType == CommunicationType.Communication.PhoneLine || b.CommunicationType == CommunicationType.Communication.TcpIp) : b.CommunicationType == vmodel.ConectionType)
                .Where(b => vmodel.StartDate == null || b.DateCreated >= start)
                .Where(b => vmodel.EndDate == null || b.DateCreated <= end)
                .Where(b => b.TerminalContacts.FirstOrDefault() != null && b.Make != null)
                .Select(b => new TerminalTableVM { TerminalID = b.TerminalId, LocationName = b.LocationName, Address = b.Address1 + b.Address2, City = b.City.Name, State = b.State.Name, PostalCode = b.Zip.ToString(), ContactName = b.TerminalContacts.First().Name, ContactPhone = b.TerminalContacts.First().Phone, ATMType = b.Make.Name + " " + b.Make.Name, Connection = b.CommunicationType.ToString(), SurchargeAmount = b.SurchargeAmountFee.ToString(), CreationDate = b.DateCreated.ToString(), EMVStatus = "falta por hacer", DCCStatus = "falta por hacer" })
                .ToList();

                return list;
            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
            // throw new NotImplementedException();
        }

        public IEnumerable<dynamic> TerminalStatus(List<JsonTerminalStatusReport> list, StatusType.Status status, int partnerId, int parentId, int cityid, int stateid, string zipcode)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro

                IEnumerable<Terminal> list4 = (from p in listpartner join m in Table.Include(b => b.TerminalContacts) on p.Id equals m.PartnerId select m).ToList();

                return list4.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => stateid == 0 || b.StateId == stateid)
                .Where(b => cityid == 0 || b.CityId == cityid)
                .Where(b => zipcode == null || b.Zip.ToString() == zipcode)
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Where(b => b.TerminalContacts.FirstOrDefault() != null)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status, b.TerminalContacts.FirstOrDefault().Name, b.TerminalContacts.FirstOrDefault().Phone }).ToList();

            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<dynamic> TransDailyList(List<JsonDailyTransactionSummary> list, int partnerId, int parentId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();
                return list4.Where(b => terminalIds.Contains(b.TerminalId)).Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();

            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<dynamic> TransMonthlyList(List<JsonMonthlyTransactionSummary> list, int partnerId, int parentId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();
                return list4.Where(b => terminalIds.Contains(b.TerminalId)).Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();
            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<dynamic> CashBalanceClose(List<JsonCashBalanceClose> list, int partnerId, int parentId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                IEnumerable<Partner> listpartner = partnerId == -1 ? GetPartnerByParentId(parentId) : GetPartnerByParentId(partnerId);// parentId : terminales del usuario logueado,partnerId: terminales del parnet especifico del filtro
                var list4 = (from q in listpartner join m in Table on q.Id equals m.PartnerId select m).ToList();
                return list4.Where(b => terminalIds.Contains(b.TerminalId)).Select(b => new { b.TerminalId, b.LocationName }).ToList();
            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

    }
}
