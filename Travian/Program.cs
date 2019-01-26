using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyConsole;
using Microsoft.Owin.Hosting;
using System.Net.Http;

namespace Travian
{
    class Program
    {


        public static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient clients = new HttpClient();

                var response = clients.GetAsync(baseAddress + "login").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("travian!");
            
            var client = new RestSharp.RestClient("http://www.x5000000.aspidanetwork.com");
            var request = new RestRequest("login.php", Method.POST);
            request.AddParameter("ft", "a4"); // adds to POST or URL querystring based on Method
            request.AddParameter("user", "menfis"); // adds to POST or URL querystring based on Method
            request.AddParameter("pw", "123123"); // adds to POST or URL querystring based on Method
            request.AddParameter("s2", "Go"); // adds to POST or URL querystring based on Method
            request.AddParameter("pw_servertime", "5c4b5a8c112d5"); // adds to POST or URL querystring based on Method
            // easily add HTTP Headers
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");
            //request.AddHeader("header", "value");

            // execute the request
            for (int i = 0; i < 3; i++)
            {
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var content = response.Content; // raw content as string
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(content);              
                    Console.WriteLine("Zalogowano jako :" + GetUserName(doc));
                    Wioski.GetWioski(doc);
                    var pattern = @"ajaxToken\s*=\s*\'(\w+)\'";
                    var m1 = Regex.Match(content, pattern);
                    Wioski.WypiszWioski(doc);
                    Wioski.WypiszAktywnaWioske(doc);

                    Console.WriteLine(m1.Value);

                    Console.WriteLine(Produkcja.GetProdukcjaDrewno(doc));
                    Console.WriteLine(Produkcja.GetProdukcjaGlina(doc));
                    Console.WriteLine(Produkcja.GetProdukcjaŻelazo(doc));
                    Console.WriteLine(Produkcja.GetProdukcjaZboże(doc));

                    System.Threading.Thread.Sleep(10000);
                }
            }
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        private static string GetServerTime(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectNodes("//*[contains(@id,'servertime')]")[0].LastChild.InnerHtml;
        }
        public static string GetUserName(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id=\"sidebarBoxHero\"]/ div[2]/div[1]/div");
            return nodes[0].ChildNodes[2].InnerHtml;
        }


        private static string ParseInnerHtml(string node)
        {
            return Regex.Replace(node, @"\s+", "");
        }
    }
}
