using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using WebApiOctagon.Repository.InterfacesRepository;

namespace WebApiOctagon.Controllers
{
    [RoutePrefix("api/Terminals")]
    public class TerminalAlertsController : ApiController
    {
        private ITerminalAlertRepo _repository;
        public TerminalAlertsController(ITerminalAlertRepo repository)
        {
            _repository = repository;
        }

        [Route("PostAlert")]
        public HttpResponseMessage PostTerminalAlert(HttpRequestMessage alerts)
        {
            try
            {

                var p = alerts.Content.ReadAsStringAsync().Result;
                if (String.IsNullOrEmpty(p))
                {
                    List<KeyValuePair<string, string>> prueba = new List<KeyValuePair<string, string>>();
                    prueba.Add(new KeyValuePair<string, string>("TerminalId", "TR024019"));
                    prueba.Add(new KeyValuePair<string, string>("CashAvailable", "345"));
                    prueba.Add(new KeyValuePair<string, string>("AlarmChestdooropen", "True"));
                    prueba.Add(new KeyValuePair<string, string>("AlarmTopdooropen", "True"));
                    prueba.Add(new KeyValuePair<string, string>("AlarmSupervisoractive", "True"));
                    prueba.Add(new KeyValuePair<string, string>("Receiptprinterpaperstatus", "Low"));
                    prueba.Add(new KeyValuePair<string, string>("ReceiptPrinterRibbonStatus", "Out"));
                    prueba.Add(new KeyValuePair<string, string>("JournalPrinterPaperStatus", "Low"));
                    _repository.SaveAlerts(prueba);
                }
                else
                {
                    List<KeyValuePair<string, string>> list = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(p);
                    _repository.SaveAlerts(list);
                }

                HttpResponseMessage respo = new HttpResponseMessage(HttpStatusCode.OK);
                return respo;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                WebMail.EnableSsl = true;
                WebMail.From = "luisrafael.gamez@outlook.com";
                WebMail.SmtpPort = 25;
                WebMail.UserName = "luisrafael.gamez@outlook.com";
                WebMail.SmtpServer = "smtp.live.com";
                WebMail.Password = "Vv19477002";
                WebMail.SmtpUseDefaultCredentials = true;

                WebMail.Send("yasser.osuna@gmail.com", "Error en el Api ", ex.Message + " " + ex.InnerException.Message);

                return response;
                throw;
            }
        }
        [Route("CompareSurcharge")]
        public void PostSendSurcharge(HttpRequestMessage alerts)
        {
            var p = alerts.Content.ReadAsStringAsync().Result;
            //esto devuelve el surcharge de la terminal. tiene que conicidir con lo que me manda el ATM
        }


        [Route("prueba")]
        public string GetPrueba()
        {
            return "fff";
        }
    }
}
