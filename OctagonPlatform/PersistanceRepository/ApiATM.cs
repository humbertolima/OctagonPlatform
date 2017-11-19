using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
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
        public async Task<List<JsonLoadCash>> CashLoad(string tn, DateTime start, DateTime end)
        {
            string _start = start.ToString("yyyyMMdd");
            string _end = end.ToString("yyyyMMdd");
            HttpResponseMessage response = client.GetAsync(uri+ "cash/"+tn+"/"+_start+"/"+_end).Result;
            List<JsonLoadCash> list = new List<JsonLoadCash>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var cash = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                list = JsonConvert.DeserializeObject<List<JsonLoadCash>>(cash);

            }
            return list;

        }


    }

   

}
