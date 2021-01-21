using System;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
namespace CSharpCallGraph
{
    class Program
    {
        static  void Main(string[] args)
        {
            string clientID = "cde921c5-862c-4264-cccc-6daceb46fec5"; // Put the Application ID from above here.
            string clientSecret = "tdy~UUxbGCWXfD-5r.cccc.i9D3~479p2"; // Put the Client Secret from above here.

            string graphApiResource = "https://graph.microsoft.com";
            Uri microsoftLogin = new Uri("https://login.microsoftonline.com/");
            string tenantID = "2e83cc45-652e-419b-a85a-80c981c30c09"; // Put the Azure AD Tenant ID from above here.

            // The authority to ask for a token: your azure active directory.
            string authority = new Uri(microsoftLogin, tenantID).AbsoluteUri;
            AuthenticationContext authenticationContext = new AuthenticationContext(authority);
            ClientCredential clientCredential = new ClientCredential(clientID, clientSecret);

            // Picks up the bearer token.
            AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(graphApiResource, clientCredential).Result;

            GraphServiceClient graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async (requestMessage) =>
                {
                    // This is adding a bearer token to the httpclient used in the requests.
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);
                }));

            IGraphServiceUsersCollectionPage users = graphClient.Users.Request().GetAsync().Result;
            IListColumnsCollectionPage columns = graphClient.Sites["b57886ef-4c2a-4d56-cccc-27266638ac3b,b62d1450-8e6f-4be7-84a3-f6600fd6cc14"].Lists["538191ae-7802-43b5-90ec-c566b4c954b3"].Columns.Request().GetAsync().Result;
            //_ = graphClient.Sites["b57886ef-4c2a-4d56-cccc-27266638ac3b,b62d1450-8e6f-4be7-84a3-f6600fd6cc14"].Lists["538191ae-7802-43b5-90ec-c566b4c954b3"].Columns;
            // I don't have many users, else I'd have to request the next page of results.
            //foreach (var user in users.CurrentPage)
            //{
            //    Console.WriteLine("DisplayName: {0}", user.DisplayName);
            //}
            Console.WriteLine(columns);
            Console.ReadLine();
        }
    }
}
