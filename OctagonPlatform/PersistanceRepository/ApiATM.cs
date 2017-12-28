using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OctagonPlatform.PersistanceRepository
{
    public class ApiATM
    {
        private Uri uri;
        private HttpClient client;
        public ApiATM()
        {
            uri = new Uri("http://apiatm.azurewebsites.net/api/");
            client = new HttpClient();
        }
        public async Task<List<JsonLoadCash>> CashLoad(DateTime? start = null, DateTime? end = null, string tn = "", string[] listtn = null)
        {
            try
            {

                HttpResponseMessage response = null;
                if (start != null && end != null)
                {
                    string _start = start.Value.ToString("yyyyMMdd");
                    string _end = end.Value.ToString("yyyyMMdd");

                    string listtn2 = listtn != null ? string.Join(",", listtn) : "0";
                    tn = tn ?? "0";
                    string uriPath = uri + "cash/" + tn + "/" + _start + "/" + _end + "/" + listtn2;
                    response = await client.GetAsync(uriPath);
                }

                List<JsonLoadCash> list = new List<JsonLoadCash>();
                //Checking the response is successful or not which is sent using HttpClient  
                if (response != null && response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cash = await response.Content.ReadAsStringAsync();
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    list = JsonConvert.DeserializeObject<List<JsonLoadCash>>(cash);

                }
                else
                    throw new NullReferenceException("Response is null");
                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<JsonCashManagement>> CashManagement(string tn = "", string[] listtn = null)
        {

            HttpResponseMessage response = null;          

                string listtn2 = listtn != null ? string.Join(",", listtn) : "0";
                tn = tn ?? "0";
                response = await client.GetAsync(uri + "cash/management/" + tn + "/" + listtn2);
           

            List<JsonCashManagement> list = new List<JsonCashManagement>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response != null && response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var cash = await response.Content.ReadAsStringAsync();
                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonCashManagement>>(cash);

            }
          
               
            return list;

        }
        public async Task<List<JsonTerminalStatusReport>> TerminalStatus(string[] listtn = null)
        {
            HttpResponseMessage response = null;
            string listtn2 = listtn != null ? string.Join(",", listtn) : "0";            
            response = await client.GetAsync(uri + "request/terminalstatus/" + listtn2);
            List<JsonTerminalStatusReport> list = new List<JsonTerminalStatusReport>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response != null && response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var status = await response.Content.ReadAsStringAsync();
                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonTerminalStatusReport>>(status);
            }
            return list;
        }
        public async Task<List<JsonDailyTransactionSummary>> DailyTransactionSummary(DateTime? start , DateTime? end, string tn , string[] listtn ,bool surcharge,bool dispensed)
        {

            HttpResponseMessage response = null;
            if (start != null && end != null)
            {
                string _start = start.Value.ToString("yyyyMMdd");
                string _end = end.Value.ToString("yyyyMMdd");

                string listtn2 = listtn != null ? string.Join(",", listtn) : "0";
                tn = tn ?? "0";
                response = await client.GetAsync(uri + "request/transdailysum/" + tn + "/" + _start + "/" + _end + "/" + listtn2 + "/" + surcharge + "/" + dispensed);
            }

            List<JsonDailyTransactionSummary> list = new List<JsonDailyTransactionSummary>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response != null && response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = await response.Content.ReadAsStringAsync();
                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonDailyTransactionSummary>>(result);

            }
            else
                throw new NullReferenceException("Response is null");
            return list;

        }
        public async Task<List<JsonMonthlyTransactionSummary>> MonthlyTransactionSummary(DateTime? start, DateTime? end, string tn, string[] listtn, bool surcharge)
        {

            HttpResponseMessage response = null;
            if (start != null && end != null)
            {
                string _start = start.Value.ToString("yyyyMMdd");
                string _end = end.Value.ToString("yyyyMMdd");

                string listtn2 = listtn != null ? string.Join(",", listtn) : "0";
                tn = tn ?? "0";
                response = await client.GetAsync(uri + "request/transmonthlysum/" + tn + "/" + _start + "/" + _end + "/" + listtn2 + "/" + surcharge );
            }

            List<JsonMonthlyTransactionSummary> list = new List<JsonMonthlyTransactionSummary>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response != null && response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = await response.Content.ReadAsStringAsync();
                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonMonthlyTransactionSummary>>(result);

            }
            else
                throw new NullReferenceException("Response is null");
            return list;

        }
        public async Task<List<JsonCashBalanceClose>> CashBalanceClose(DateTime? start, string[] listtn)
        {

            HttpResponseMessage response = null;

            if (start != null )
            {
                string _start = start.Value.ToString("yyyyMMdd");              

                string listtn2 = listtn != null ? string.Join(",", listtn) : "0";
                response = await client.GetAsync(uri + "cash/atclose/" + listtn2 + "/" + _start);
             }          


            List<JsonCashBalanceClose> list = new List<JsonCashBalanceClose>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response != null && response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var cash = await response.Content.ReadAsStringAsync();
                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonCashBalanceClose>>(cash);

            }


            return list;

        }

    }



}
