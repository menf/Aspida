using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian
{
    class RestClient
    {
        private RestSharp.RestClient client;
        private static readonly Lazy<RestClient> rest =
       new Lazy<RestClient>(() => new RestClient());

        public static RestClient Instance { get { return rest.Value; } }

        private RestClient()
        {
            client = new RestSharp.RestClient("http://www.x5000000.aspidanetwork.com");

        }
        public string Login(string login, string password)
        {
            var request = new RestSharp.RestRequest("login.php", Method.POST);
            request.AddParameter("ft", "a4"); // adds to POST or URL querystring based on Method
            request.AddParameter("user", login); // adds to POST or URL querystring based on Method
            request.AddParameter("pw", password); // adds to POST or URL querystring based on Method
            request.AddParameter("s2", "Go"); // adds to POST or URL querystring based on Method
            request.AddParameter("pw_servertime", "5c4b5a8c112d5"); // adds to POST or URL querystring based on Method
            // easily add HTTP Headers
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                var user= User.Instance;
                user.SetUser(login, password);
                
                return (Program.GetUserName(doc));

            }
            return "";
        }
    }
}