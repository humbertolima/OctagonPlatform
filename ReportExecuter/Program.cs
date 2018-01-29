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

                    ErrorCtrl.save(()=> {

                        if(NeedToBeSend(auser, asuscription))
                        {
                            string ReportName = "";
                            string email = asuscription.Email;
                            List<KeyValuePair<string, string>> FilterNames = new List<KeyValuePair<string, string>>();
                            foreach (ReportFilter reportFilter in asuscription.ReportFilters)
                            {
                                FilterModel fm = reportFilter.Filter;
                                ReportModel rm = reportFilter.Report;

                                ReportName = rm.Name.Replace(" ", string.Empty);
                                FilterNames.Add(new KeyValuePair<string, string>(fm.Name, reportFilter.Value));

                            }

                            ExecRunReport(ReportName, FilterNames, email, auser, asuscription);
                        }                        

                    });                   

                };

            };

        }

        private static bool NeedToBeSend(User auser, SubscriptionModel asuscription)
        {
            return true;
        }

        private static void ExecRunReport(string reportName, List<KeyValuePair<string, string>> filterNames, string email, User auser, SubscriptionModel asuscription)
        {
            string url_base = "http://localhost:51141";

            string format = "pdf";

            ErrorCtrl.save( () =>
            {

                HttpClient client = new HttpClient();
                //var values = new Dictionary<string, string> { };
                string adata = "";

                foreach(KeyValuePair<string, string> pair in filterNames)
                {                  

                    if (adata != "")
                        adata += "&";

                    adata += pair.Key + "=" + pair.Value;
                }

                //values.Add("format", "pdf");

                string url = url_base + "/" + reportName + "/"+ reportName;
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



           

            });
        }

 
    }
}
