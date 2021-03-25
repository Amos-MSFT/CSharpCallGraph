using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonConsole
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly string tokenRequestURL = "https://accounts.accesscontrol.windows.net/2e83cc45-652e-419b-a85a-80c981c30c09/tokens/oauth/2";
        private static readonly HttpRequestMessage tokenRequest = new HttpRequestMessage();
       
        private static readonly FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
{
    new KeyValuePair<string, string>("grant_type", "client_credentials"),
    new KeyValuePair<string, string>("client_id", "4b864129-1df3-4db8-8621-230bd6de1130@2e83cc45-652e-419b-a85a-80c981c30c09"),
            new KeyValuePair<string, string>("client_secret", "/vf10fyVMSYh9KqGk9fV5pbJRF9fOLBwBF0NHvkY8CQ="),
            new KeyValuePair<string, string>("resource", "00000003-0000-0ff1-ce00-000000000000/haiqiwu.sharepoint.com@2e83cc45-652e-419b-a85a-80c981c30c09")
});
        static async Task Main(string[] args)
        {
            await ProcessRequest();

        }
        private static async Task ProcessRequest()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

            tokenRequest.Method = new HttpMethod("POST");
            tokenRequest.RequestUri = new Uri(tokenRequestURL);

            

            //tokenRequest.Headers.Add("Accept", "application/json;odata=verbose;charset=utf-8");
            //tokenRequest.Headers.Add("content_type", "application/json;odata=verbose;charset=utf-8");

            var stringTask = client.PostAsync(tokenRequestURL, formContent);

            var msg = await stringTask;
            Console.Write(stringTask.Result);
            Console.Write("-------------------------");
            Console.Write(msg);
            Console.ReadLine();
        }
    }
}
