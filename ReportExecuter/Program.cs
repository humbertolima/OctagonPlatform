using OctagonPlatform.Controllers.Reports;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReportExecuter
{
    class Program
    {
        public static DateTime time_run ;

        static void Main(string[] args)
        {
          

            //var container = new UnityContainer();

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            IEnumerable<User> users =  (new UserRepository()).GetAllUsersSubscription();
            int count = users.Cast<Object>().Count();

            foreach( User auser in users)
            {
                foreach(SubscriptionModel asuscription in auser.Subscriptions)
                {
                    time_run = Utils.ToTimeZoneTime(DateTime.Now, asuscription.Schedule.User.TimeZoneInfo);
                    ErrorCtrl.save(()=> {

                        if(NeedToBeSend(auser, asuscription))
                        {
                            string ReportName = "";
                            string email = asuscription.Email;
                            List<Tuple<string, string,string>> FilterNames = new List<Tuple<string, string, string>>();
                            foreach (ReportFilter reportFilter in asuscription.ReportFilters)
                            {
                                FilterModel fm = reportFilter.Filter;
                                ReportModel rm = reportFilter.Report;

                                ReportName = rm.Name.Replace(" ", string.Empty);
                                FilterNames.Add(new Tuple<string, string, string>(fm.Name, reportFilter.Value, fm.Type));

                            }
                            Console.WriteLine("Ejecutando Report");
                            ExecRunReport(ReportName, FilterNames, email, auser, asuscription);
                            Console.WriteLine("Termino de ejecutar Report");
                        }                        

                    });                   

                };

            };

        }

        private static bool NeedToBeSend(User auser, SubscriptionModel asuscription)
        {
           DateTime? date = Utils.NextRunDateSchedule(asuscription.Schedule);
            if(date == null)
            {
                return false;
            }

            TimeSpan span = date.Value - time_run;

            if (span.TotalMinutes >=0 && span.TotalMinutes < 30)
            {
                return true;
            }

            return false;
        }

        private static void ExecRunReport(string reportName, List<Tuple<string, string, string>> filterNames, string email, User auser, SubscriptionModel asuscription)
        {
            //string ss = System.Web.HttpContext.Current.Server.MapPath("~/");
            string url_base = "http://localhost:51141";
            string format = asuscription.Format;
           
                ErrorCtrl.Save( () =>
            {
                HttpClient client = new HttpClient();               
                string adata = "";

                foreach(Tuple<string, string, string> pair in filterNames)
                {
                    string field_name = pair.Item1;
                    string field_value = pair.Item2;
                    string field_type = pair.Item3;

                    if(field_type == "date")
                    {                      
                        
                            int value = Convert.ToInt32(field_value) * -1; //restarle esa cantidad de dias
                            DateTime datetime = time_run.AddDays(value);
                            field_value = datetime.ToString("MM/dd/yyyy");                      

                    }
                    //preparar las variables del view model que hay que pasarle a RunReport
                    if (adata != "")
                        adata += "&";

                    adata += field_name + "=" + field_value;
                }


                if (adata != "")
                    adata += "&";

                adata += "format=" + format;

                if (adata != "")
                    adata += "&";

                adata += "UserId=" + asuscription.Schedule.User.Id;

                #region Ejecutar RunReport

                string url = url_base + "/" + reportName + "/RunReport";
                Console.WriteLine(url);

             
                // Create a request using a URL that can receive a post.   
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.  
                request.Method = "POST";
                // Create POST data and convert it to a byte array.  
                string postData = adata ; // "This is a test that posts this string to a Web server.";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.  
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.  
                request.ContentLength = byteArray.Length;
                // Get the request stream.  
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.  
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.  
                dataStream.Close();
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Console.WriteLine(responseFromServer);
                // Clean up the streams.  
                reader.Close();
                dataStream.Close();
                response.Close();

                #endregion



            });
        }

 
    }
}
