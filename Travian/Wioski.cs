using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian
{
    class Wioski
    {

        private static readonly string ListaWiosek = "//*[@id=\"sidebarBoxVillagelist\"]/div[2]/div[2]/ul";
        public static Wioska AktywnaWioska;
        public static List<Wioska> Wioskii = new List<Wioska>();


        public static void GetWioski(HtmlDocument doc)
        {
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(ListaWiosek);

            foreach (HtmlNode node in nodes[0].ChildNodes)
            {
                if (node.Name.Equals("li"))
                {
                            var href = node.ChildNodes[1].Attributes[0].Value;
                            var nazwa = node.ChildNodes[1].ChildNodes[3].InnerHtml;
                            var x1 = node.ChildNodes[1].ChildNodes[5].ChildNodes[0].InnerHtml;
                            var y1 = node.ChildNodes[1].ChildNodes[5].ChildNodes[2].InnerHtml;
                            var x = x1.Substring(1);
                            var y = y1.Substring(0, 2);
                            var wioska = new Wioska(true, href, nazwa, Convert.ToInt32(y), Convert.ToInt32(x));

                            Wioskii.Add(wioska);
                    foreach(var att in node.Attributes)
                    {
                        if(att.Name.Equals("class") && att.Value.Equals(" active "))
                        {
                            AktywnaWioska = wioska;
                        }
                    }

                }
            }

        }
        public static void WypiszAktywnaWioske(HtmlDocument doc)
        {
            Console.WriteLine("Aktywna wioska: " +AktywnaWioska.Nazwa) ;
        }
        public static void WypiszWioski(HtmlDocument doc)
        {

            Console.WriteLine("Lista Wiosek");
            foreach(Wioska w in Wioskii)
            {
                Console.WriteLine(w.Nazwa);
            }
        }
    }
}
