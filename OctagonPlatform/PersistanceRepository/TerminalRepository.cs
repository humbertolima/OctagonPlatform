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

namespace OctagonPlatform.PersistanceRepository
{
    public class TerminalRepository : GenericRepository<Terminal>, ITerminalRepository
    {

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
            catch (Exception ex)
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
                    .Include(x => x.Partner)
                    .Include(x => x.Country)
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.LocationType)
                    .Include(x => x.Cassettes)
                    .Include(x => x.Contracts)
                    .Include(x => x.Documents)
                    .Include(x => x.InterChanges)
                    .Include(x => x.Make)
                    .Include(x => x.Model)
                    .Include(x => x.Users)
                    .Include(x => x.VaultCash)
                    .Include(x => x.Surcharges)
                    .Include(x => x.Notes)
                    .Include(x => x.TerminalContacts)
                    .Include(x => x.TerminalPictures)
                    .Include(x => x.Cassettes)
                    .Include(x => x.Disputes)
                    .Include(x => x.TerminalAlertConfigs)
                    .Include(x => x.WorkingHours)
                    .FirstOrDefault();
                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.InterChanges = Context.InterChanges.Where(x => x.TerminalId == terminal.Id && !x.Deleted)
                    .Include(x => x.BankAccount).ToList();
                return terminal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Terminal not found.");
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

        public TerminalAlertIngnoredViewModel GetConfigNotification(int id)
        {
            try
            {
                var terminal = Table
                    .Include(c => c.TerminalAlertConfigs)
                    .Include(c => c.WorkingHours)
                    .FirstOrDefault(c => c.Id == id);
                if (terminal == null) throw new Exception("Terminal not found. ");    //Context.TerminalAlertConfigs.FirstOrDefault(c => c.TerminalId == terminalId);
                if (terminal.TerminalAlertConfigs == null)
                {
                    terminal.TerminalAlertConfigs = new TerminalAlertConfig();
                }
                var terminalAlertConfigViewModel = Mapper.Map<TerminalAlertConfig, TerminalAlertIngnoredViewModel>(terminal.TerminalAlertConfigs);

                terminalAlertConfigViewModel.WorkingHours = terminal.WorkingHours;
                terminalAlertConfigViewModel.TerminalId = terminal.TerminalId;

                return terminalAlertConfigViewModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }

        }

        public Terminal SetConfigNotification(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel)
        {
            try
            {
                var terminalAlertConfig = Mapper.Map<TerminalAlertIngnoredViewModel, TerminalAlertConfig>(terminalAlertIngnoredViewModel);

                var terminal = Table
                    .Include(c => c.TerminalAlertConfigs)
                    .FirstOrDefault(c => c.TerminalId == terminalAlertIngnoredViewModel.TerminalId);
                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.TerminalAlertConfigs = terminalAlertConfig;

                Edit(terminal);


                return terminal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }

        }

        public Terminal SetWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel, string workingHoursEdit)
        {
            try
            {
                var startTime = new TimeSpan(terminalAlertIngnoredViewModel.StartTime, 00, 00);
                var endTime = new TimeSpan(terminalAlertIngnoredViewModel.EndTime, 00, 00);

                var terminal = Table
                    .Include(c => c.WorkingHours)
                    .FirstOrDefault(c => c.TerminalId == terminalAlertIngnoredViewModel.TerminalId);

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
                        terminalAlertIngnoredViewModel.Days;

                Edit(terminal);


                return terminal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Terminal not found.");
            }
        }
        public Terminal AddWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel)
        {


            try
            {
                var startTime = new TimeSpan(terminalAlertIngnoredViewModel.StartTime, 00, 00);
                var endTime = new TimeSpan(terminalAlertIngnoredViewModel.EndTime, 00, 00);

                var terminal = Table
                    .FirstOrDefault(c => c.TerminalId == terminalAlertIngnoredViewModel.TerminalId);

                if (terminal == null) throw new Exception("Terminal not found. ");

                terminal.WorkingHours = new List<TerminalWorkingHours>
                {
                    new TerminalWorkingHours
                    {
                        Day = terminalAlertIngnoredViewModel.Days,
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

        public Terminal CassettesSet(bool autoRecord, int denomination, int terminalId)
        {
            try
            {
                var terminal = Table.Include("Cassettes").FirstOrDefault(c => c.Id == terminalId);
                if (terminal != null)
                {
                    terminal.Cassettes.Add(new Cassette { AutoRecord = autoRecord, Denomination = denomination, TerminalId = terminalId });
                    Save();
                }
                return terminal;
            }
            catch (Exception)
            {
                //pendiente
                throw;
            }
        }
        public Terminal CassettesEdit(bool autoRecord, int denomination, int terminalId, int cassetteId)
        {
            try
            {
                var terminal = Table.Include("Cassettes").FirstOrDefault(c => c.Id == terminalId);
                if (terminal != null)
                {
                    terminal.Cassettes.FirstOrDefault(c => c.Id == cassetteId).Denomination = denomination;
                    terminal.Cassettes.FirstOrDefault(c => c.Id == cassetteId).AutoRecord = autoRecord;

                    Edit(terminal);
                }
                return terminal;
            }
            catch (Exception)
            {
                //pendiente
                throw;
            }
        }



        public IEnumerable<dynamic> LoadCashList(List<JsonLoadCash> list, StatusType.Status status, int partnerid)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                return Table.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => partnerid == -1 || b.PartnerId == partnerid)
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();


            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }

        }

        public IEnumerable<string> GetAllTerminalId(string value)
        {
            try
            {
                return Table.Where(b => b.TerminalId.Contains(value)).Select(b => b.TerminalId).ToList();
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

                throw new Exception(e.Message);
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

        public IEnumerable<dynamic> LoadCashMngList(List<JsonCashManagement> list, StatusType.Status status, int partnerId)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                return Table.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => partnerId == -1 || b.PartnerId == partnerId)
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();


            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<TerminalTableVM> GetTerminalsReport(TerminalListViewModel vmodel, string[] listtn)
        {

            DateTime? start = null;
            DateTime? end = null;
            if (vmodel.StartDate != null) start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (vmodel.EndDate != null) end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            bool groupfilter = listtn == null ? false : true;
            string[] aux = { "0" };
            listtn = listtn ?? aux;
            int zip = Int32.Parse(vmodel.ZipCode ?? "0");
            try
            {
                var list = Table.Where(b => groupfilter == false || listtn.Contains(b.TerminalId))
                .Where(b => vmodel.PartnerId == -1 || b.PartnerId == vmodel.PartnerId)
                .Where(b => vmodel.Status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == vmodel.Status)
                .Where(b => vmodel.AccountId == -1 || b.Partner.BankAccounts.Where(z => z.Id == vmodel.AccountId).Count() > 0)
                .Where(b => vmodel.TerminalId == null || b.TerminalId == vmodel.TerminalId)
                .Where(b => vmodel.CityId == -1 || b.CityId == vmodel.CityId)
                .Where(b => vmodel.StateId == -1 || b.CityId == vmodel.StateId)
                .Where(b => vmodel.ZipCode == null || b.Zip == zip)
                .Where(b => vmodel.ConectionType == CommunicationType.Communication.All ? (b.CommunicationType == CommunicationType.Communication.PhoneLine || b.CommunicationType == CommunicationType.Communication.TcpIp) : b.CommunicationType == vmodel.ConectionType)
                .Where(b => vmodel.StartDate == null || b.DateCreated >= start)
                .Where(b => vmodel.EndDate == null || b.DateCreated <= end)
                .Select(b => new TerminalTableVM { TerminalID = b.TerminalId, LocationName = b.LocationName, Address = b.Address1 + b.Address2, City = b.City.Name, State = b.State.Name, PostalCode = b.Zip.ToString(), ContactName = b.TerminalContacts.FirstOrDefault().Name, ContactPhone = b.TerminalContacts.FirstOrDefault().Phone, ATMType = b.Make.Name + " " + b.Model.Name, Connection = b.CommunicationType.ToString(), SurchargeAmount = b.SurchargeAmountFee.ToString(), CreationDate = b.DateCreated.ToString(), EMVStatus = "falta por hacer", DCCStatus = "falta por hacer" })
                .ToList();

                return list;
            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
            // throw new NotImplementedException();
        }

        public IEnumerable<dynamic> TerminalStatus(List<JsonTerminalStatusReport> list, StatusType.Status status, int partnerId, int cityid, int stateid, string zipcode)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                return Table.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => partnerId == -1 || b.PartnerId == partnerId)
                .Where(b => stateid == 0 || b.StateId == stateid)
                .Where(b => cityid == 0 || b.CityId == cityid)
                .Where(b => zipcode == null || b.Zip.ToString() == zipcode)
                .Where(b => status == StatusType.Status.All ? (b.Status == StatusType.Status.Active || b.Status == StatusType.Status.Inactive || b.Status == StatusType.Status.Incomplete) : b.Status == status)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status, b.TerminalContacts.FirstOrDefault().Name, b.TerminalContacts.FirstOrDefault().Phone }).ToList();

            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }

        public IEnumerable<dynamic> TransDailyList(List<JsonDailyTransactionSummary> list, int partnerid)
        {
            try
            {
                var terminalIds = list.Select(s => s.TerminalId).ToList();
                return Table.Where(b => terminalIds.Contains(b.TerminalId))
                .Where(b => partnerid == -1 || b.PartnerId == partnerid)
                .Select(b => new { b.TerminalId, b.LocationName, b.Status }).ToList();


            }
            catch (Exception e)
            {

                throw new Exception("Error database " + e.Message);
            }
        }
    }
}
