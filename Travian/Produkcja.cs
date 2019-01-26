using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Travian
{
    public static class Produkcja
    {
        private static string Drewno { get; } = "//*[@id=\"production\"]/tbody/tr[1]/td[3]";
        private static string Glina { get; } = "//*[@id=\"production\"]/tbody/tr[2]/td[3]";
        private static string Żelazo { get; } = "//*[@id=\"production\"]/tbody/tr[3]/td[3]";
        private static string Zboże { get; } = "//*[@id=\"production\"]/tbody/tr[4]/td[3]";



        public static string GetProdukcjaDrewno(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Drewno);
            return ParseInnerHtml(nodes[0].InnerHtml);
        }
        public static string GetProdukcjaGlina(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Glina);
            return ParseInnerHtml(nodes[0].InnerHtml);
        }
        public static string GetProdukcjaŻelazo(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Żelazo);
            return ParseInnerHtml(nodes[0].InnerHtml);
        }
        public static string GetProdukcjaZboże(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Zboże);
            return ParseInnerHtml(nodes[0].InnerHtml);
        }

        private static string ParseInnerHtml(string node)
        {
            return Regex.Replace(node, @"\s+", "");
        }
    }
}
