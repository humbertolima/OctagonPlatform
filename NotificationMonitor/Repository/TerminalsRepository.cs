using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMonitor.Repository
{
    public class TerminalsRepository
    {
        public void CheckTerminalsStatus()
        {
            ApplicationDbContext DbContext = new ApplicationDbContext();

            var terminals = DbContext.Terminals
                .Include(c => c.WorkingHours)
                .Include(c => c.TerminalAlertConfigs)
                .Select(c => c).ToList();

            foreach (var terminal in terminals)
            {
                var messages = OctagonPlatform.Helpers.Terminals.GetTerminalMessages(terminal.TerminalId);
                if (messages.Count() > 0)
                {

                    DateTime lastMessages = messages.OrderByDescending(c => c.Date).FirstOrDefault().Date;
                    TimeSpan timeToIgnore = new TimeSpan(terminal.TerminalAlertConfigs.IgnoreHoursInactive, 00, 00);

                    DateTime dateNow = DateTime.Now;

                    TimeSpan diff = dateNow - lastMessages;

                    if (diff > timeToIgnore)    //la diferencia es mayor que lo que esta configurado.
                    {
                        //enviar el correo 
                    }
                }

            }
        }
    }

}
