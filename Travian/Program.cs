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
using Hangfire;

namespace Travian
{
    class Program
    {


        public static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("travian!");




                Console.ReadLine();
            }

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
           
            
            
                
            


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
