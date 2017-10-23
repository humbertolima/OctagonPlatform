
using OctagonPlatform.Models;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using WebApiOctagon.Repository.InterfacesRepository;

namespace WebApiOctagon.Repository.PersistanceRepository
{
    public class TerminalAlertRepo : GenericRepository<TerminalAlert>, ITerminalAlertRepo
    {
        public void SaveAlerts(List<KeyValuePair<string, string>> alerts)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }
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

                #region Parse Message to notified
                if (!String.IsNullOrEmpty(terminalAlert.AlarmChestdooropen))
                {
                    if (terminalAlert.AlarmChestdooropen.Contains("True") && (!query.TerminalAlertConfigs.IgnoreChestDoorOpen))
                        SendEmailTo(query.Users, null);
                }

                if (!String.IsNullOrEmpty(terminalAlert.AlarmTopdooropen))
                {
                    if (terminalAlert.AlarmTopdooropen.Contains("True") && (!query.TerminalAlertConfigs.IgnoreTopDoorOpen))
                        SendEmailTo(query.Users, null);
                }
                //if (!String.IsNullOrEmpty())
                //{
                //if (terminalAlert.AlarmSupervisoractive.Contains("True") && (!query.TerminalAlertConfigs.))
                //{
                //    SendEmailTo(query.Users);
                //}
                //}
                if (!String.IsNullOrEmpty(terminalAlert.Receiptprinterpaperstatus))
                {
                    if ((terminalAlert.Receiptprinterpaperstatus.Contains("Low") || terminalAlert.Receiptprinterpaperstatus.Contains("Out")) && (!query.TerminalAlertConfigs.IgnoreReceiptPaper))
                        SendEmailTo(query.Users, terminalAlert.Receiptprinterpaperstatus);
                }
                if (!String.IsNullOrEmpty(terminalAlert.ReceiptPrinterRibbonStatus))
                {
                    if ((terminalAlert.ReceiptPrinterRibbonStatus.Contains("Low") || terminalAlert.ReceiptPrinterRibbonStatus.Contains("Out") || terminalAlert.ReceiptPrinterRibbonStatus.Contains("Thermal")) && (!query.TerminalAlertConfigs.IgnoreReceiptRibbon))
                        SendEmailTo(query.Users, terminalAlert.Receiptprinterpaperstatus);
                }
                if (!String.IsNullOrEmpty(terminalAlert.JournalPrinterPaperStatus))
                {
                    if ((terminalAlert.JournalPrinterPaperStatus.Contains("Low") || terminalAlert.JournalPrinterPaperStatus.Contains("Out")) && (!query.TerminalAlertConfigs.IgnoreJournalPaper))
                        SendEmailTo(query.Users, terminalAlert.JournalPrinterPaperStatus);
                }
                if (!String.IsNullOrEmpty(terminalAlert.JournalPrinterRibbonStatus))
                {
                    if ((terminalAlert.JournalPrinterRibbonStatus == "Low" || terminalAlert.JournalPrinterRibbonStatus.Contains("Out") || terminalAlert.JournalPrinterRibbonStatus.Contains("Thermal")) && (!query.TerminalAlertConfigs.IgnoreJournalRibbon))
                        SendEmailTo(query.Users, terminalAlert.JournalPrinterRibbonStatus);
                }

                if (!String.IsNullOrEmpty(terminalAlert.NoteStatusDispenser))
                {
                    if ((terminalAlert.NoteStatusDispenser.Contains("Low") || terminalAlert.NoteStatusDispenser.Contains("Out")) && (!query.TerminalAlertConfigs.IgnoreCassetteNotes))
                        SendEmailTo(query.Users, terminalAlert.NoteStatusDispenser);
                }
                if (!String.IsNullOrEmpty(terminalAlert.ReceiptPrinter))
                {
                    if ((terminalAlert.ReceiptPrinter.Contains("Needs attention") || terminalAlert.ReceiptPrinter.Contains("Out of service")) && (!query.TerminalAlertConfigs.IgnoreReceiptPaper))
                        SendEmailTo(query.Users, terminalAlert.ReceiptPrinter);
                }
                if (!String.IsNullOrEmpty(terminalAlert.JournalPrinter))
                {
                    if ((terminalAlert.JournalPrinter.Contains("Needs attention") || terminalAlert.JournalPrinter.Contains("Out of service")) && (!query.TerminalAlertConfigs.IgnoreJournalPaper))
                        SendEmailTo(query.Users, terminalAlert.JournalPrinter);
                }
                if (!String.IsNullOrEmpty(terminalAlert.Dispenser))
                {
                    if ((terminalAlert.Dispenser.Contains("Needs attention") || terminalAlert.Dispenser.Contains("Out of service")) && (!query.TerminalAlertConfigs.IgnoreBillDispenserNeedAttention))
                        SendEmailTo(query.Users, terminalAlert.Dispenser);
                }
                if (!String.IsNullOrEmpty(terminalAlert.CommunicationsSystem))
                {
                    if ((terminalAlert.CommunicationsSystem.Contains("Needs attention") || terminalAlert.CommunicationsSystem.Contains("Out of service")) && (!query.TerminalAlertConfigs.IgnoreCommNeedAttention))
                        SendEmailTo(query.Users, terminalAlert.CommunicationsSystem);
                }
                if (!String.IsNullOrEmpty(terminalAlert.CardReader))
                {
                    if ((terminalAlert.CardReader.Contains("Needs attention") || terminalAlert.CardReader.Contains("Out of service")) && (!query.TerminalAlertConfigs.IgnoreCardReaderNeedAttention))
                        SendEmailTo(query.Users, terminalAlert.CardReader);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void SendEmailTo(ICollection<User> users, string toBody)
        {

            WebMail.EnableSsl = true;
            WebMail.From = "luisrafael.gamez@outlook.com";
            WebMail.SmtpPort = 25;
            WebMail.UserName = "luisrafael.gamez@outlook.com";
            WebMail.SmtpServer = "smtp.live.com";
            WebMail.Password = "Vv19477002";
            WebMail.SmtpUseDefaultCredentials = true;

            foreach (var item in users)
            {

                WebMail.Send(item.Email.ToString(), "Alerta de terminal", "Esto es una prueba." + toBody);

            }
        }
    }
}