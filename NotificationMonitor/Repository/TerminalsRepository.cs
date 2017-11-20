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
            Console.WriteLine("Seleccionando terminales...");
            var terminals = DbContext.Terminals
                .Include(c => c.WorkingHours)
                .Include(c => c.TerminalAlertConfigs)
                .Select(c => c).ToList();
            Console.WriteLine("Terminals: " + terminals.Count());
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
                        Console.WriteLine("Send email notification for terminal "+ terminal.TerminalId);
                        OctagonPlatform.Helpers.Email.SendNotification("yasser.osuna@gmail.com","Check terminal Status","Terminal: "+ terminal.TerminalId +" la diferencia es mayor aque la que esta configurado.",null);
                    }
                }

            }
        }
    }

}
