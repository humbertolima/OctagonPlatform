
using OctagonPlatform.Models;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebApiOctagon.Repository.InterfacesRepository;

namespace WebApiOctagon.Repository.PersistanceRepository
{
    public class TerminalAlertRepo : GenericRepository<TerminalAlert>, ITerminalAlertRepo
    {
        public void SaveAlerts(List<KeyValuePair<string, string>> alerts)
        {
            TerminalAlert terminalAlert = new TerminalAlert();

            foreach (var item in alerts)
            {
                terminalAlert.GetType().GetProperty(item.Key).SetValue(terminalAlert, item.Value);
            }
            Table.Add(terminalAlert);

            Save();

            NotifAlertToUser(terminalAlert);
        }

        
        public void Add(TerminalAlert obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TerminalAlert> All()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TerminalAlert FindBy(object id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void NotifAlertToUser(TerminalAlert terminalAlert)
        {
            try
            {
                //enviar el correo a los que tengan configurado que les llegue las alertas.
                //no se puede enviar acorreo de alertas que esten ingnoradas.
                var query = Context.Terminals
                    .Include("Users")
                    .Include("TerminalAlertConfigs")
                    .Include("TerminalAlerts")
                    .FirstOrDefault(c => c.TerminalId == terminalAlert.TerminalId);

                WebMail.EnableSsl = true;
                WebMail.From = "luisrafael.gamez@outlook.com";
                WebMail.SmtpPort = 25;
                WebMail.UserName = "luisrafael.gamez@outlook.com";
                WebMail.SmtpServer = "smtp.live.com";
                WebMail.Password = "Vv19477002";
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.Send("yasser.osuna@gmail.com", "Alerta de terminal", "Esto es una prueba");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}