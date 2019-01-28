using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travian.models;

namespace Travian
{


class RestClient
    {
        private enum BuildingsCategories
        {
            INFRASTRUCTURE = 1,
            MILITARY = 2,
            RESOURCES = 3
        }
        private string serverUrl = "";
        private string resourceUrl = "dorf1.php";
        private string villageUrl = "dorf2.php";
        private string buildUrl = "build.php?";
        private string adventureUrl = "hero_adventure.php";
        private string adventureStart = "a2b.php";
        private RestSharp.RestClient client;


        private static readonly Lazy<RestClient> rest =
       new Lazy<RestClient>(() => new RestClient());

        public static RestClient Instance { get { return rest.Value; } }

        private RestClient()
        {
            client = new RestSharp.RestClient();
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36";

        }
        public string Login(string login, string password, string server)
        {
            serverUrl = server;
            client.BaseUrl = new Uri(serverUrl);
            client.CookieContainer = new System.Net.CookieContainer();
            var request = new RestSharp.RestRequest("login.php", Method.POST);
            request.AddParameter("ft", "a4"); // adds to POST or URL querystring based on Method
            request.AddParameter("user", login); // adds to POST or URL querystring based on Method
            request.AddParameter("pw", password); // adds to POST or URL querystring based on Method
            request.AddParameter("s2", "Go"); // adds to POST or URL querystring based on Method
            request.AddParameter("pw_servertime", "5c4b5a8c112d5"); // adds to POST or URL querystring based on Method
           
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                var user= User.Instance;
                user.SetUser(login, password);
                Console.WriteLine("Login OK : "+ Program.GetUserName(doc));
                GetPlayerInfo();
                return JsonConvert.SerializeObject(user.Villages);
            }
            return "";
        }
        public HtmlDocument GetVillagePage(string id)
        {
            var request = new RestSharp.RestRequest(resourceUrl, Method.GET);
            request.AddParameter("newdid", id);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetVillagePage()
        {
            var request = new RestSharp.RestRequest(villageUrl, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetSmith(string id)
        {
            var request = new RestSharp.RestRequest(id, Method.GET);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetResourcePage()
        {
            var request = new RestSharp.RestRequest(resourceUrl, Method.GET);
            request.AddHeader("Accept","*/*");
           // request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
           
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetAdventuresPage()
        {
            var request = new RestSharp.RestRequest(adventureUrl, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetBuildingPage(string id)
        {
            var request = new RestSharp.RestRequest(buildUrl, Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("category", "1");

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetBuildingUpgradePage(Building building)
        {
            var request = new RestSharp.RestRequest(building.url, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetBuildingUpgrade(Building building)
        {
            var request = new RestSharp.RestRequest(building.upgradeUrl, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument GetBuilding(string map, string building, string captcha)
        {
            var request = new RestSharp.RestRequest(villageUrl, Method.GET);
            request.AddParameter("?%D0%B0", "{"+ building+"}");
            request.AddParameter("id", "{" + map + "}");
            request.AddParameter("c", "{" + captcha + "}");

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }
        public HtmlDocument PostSendResources(string wood, string clay, string iron, string crops, string market, string target)
        {
            var request = new RestSharp.RestRequest(buildUrl, Method.POST);
            request.AddParameter("id", market);
            request.AddParameter("ft", "mk1");
            request.AddParameter("send3", "1");
            request.AddParameter("r1", wood);
            request.AddParameter("r2", clay);
            request.AddParameter("r3", iron);
            request.AddParameter("r4", crops);
            request.AddParameter("getwref", target);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }


        public HtmlDocument PostTrainUnit(string unit, string building, string count)
        {
            var request = new RestSharp.RestRequest(buildUrl, Method.POST);
            request.AddParameter("id", "{" + building + "}");
            request.AddParameter("ft", "t1");
            request.AddParameter("{" + unit + "}", "{" + count + "}");

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            return null;
        }

        #region HELPERS
        public void GetPlayerInfo()
        {
            var resourcesPage = GetResourcePage();
            var villages = GetVillageList(resourcesPage);
            var user = User.Instance;
            user.Villages = villages;
            FetchVillageData(ref user);

        }
        public void FetchVillageData(ref User user)
        {
            foreach(Village village in user.Villages)
            {
                if (!village.active)
                {
                    GetVillagePage(village.id);
                    foreach(Village _village in user.Villages)
                    {
                        _village.active = false;
                    }
                    village.active = true;
                    Console.WriteLine("Active village: " + village.name);
                }

                var resourcesPage = GetResourcePage();
                var villagePage = GetVillagePage();
                List<Building> buildingQueue = new List<Building>();
                List<Building> buildings = new List<Building>();
                HtmlNodeCollection nodes = resourcesPage.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[10]/ul");
                if (nodes !=null)
                {
                    var _buildingQueue = nodes[0].ChildNodes;

                    foreach (var el in _buildingQueue)
                    {
                        string name = el.ChildNodes[1].ChildNodes[0].InnerHtml;
                        string level = (el.ChildNodes[1].ChildNodes[1].InnerHtml).Replace("level ", "");
                        string buildDuration = el.ChildNodes[2].InnerText.Substring(0, 8);
                        Building b = new Building(name, level, buildDuration);
                        buildingQueue.Add(b);
                    }
                }
               
                HtmlNodeCollection nodes2 = resourcesPage.DocumentNode.SelectNodes("//*[@id=\"rx\"]");
              
                foreach (var el in nodes2[0].ChildNodes)
                {
                    if (el.Name == "area")
                    {
                        string href = el.GetAttributeValue("href", "");
                        string id = href.Replace("build.php?id=", "");
                        string title = el.GetAttributeValue("title", null);
                        string name,level;
                        List<string> resources = new List<string>();
                        if (title!=null && title !="buildings")
                        {
                            var doc = new HtmlDocument();
                            doc.LoadHtml(title);
                            name = doc.DocumentNode.ChildNodes[0].ChildNodes[0].InnerHtml;

                            if(name == "The field is in the maximum level")
                            {
                                level = "max";

                            }
                           else level = doc.DocumentNode.ChildNodes[0].ChildNodes[1].InnerHtml.Replace("level ","");
                            foreach (var node in doc.DocumentNode.ChildNodes)
                            {
                                if (node.Name == "span")
                                {
                                    resources.Add(node.InnerText.Trim(' '));
                                }
                            }
                            buildings.Add(new Building(id,name, level, href, resources));
                        }
                        
 
                    }
                }

                HtmlNodeCollection nodes3 = villagePage.DocumentNode.SelectNodes("//*[@id=\"clickareas\"]");
                var nodes4 = villagePage.DocumentNode.Descendants("img").Where(d => d.GetAttributeValue("class", "").Contains("building"));
                HtmlNodeCollection nodes5 = villagePage.DocumentNode.SelectNodes("//*[@id=\"village_map\"]");
                var ss =nodes5[0].SelectNodes("//img");
                foreach (var el in nodes3[0].ChildNodes)
                {
                    if (el.Name == "area")
                    {
                        string href = el.GetAttributeValue("href", "");
                        string id = href.Replace("build.php?id=", "");
                        string title = el.GetAttributeValue("title", null);
                        string name, level="max";
                        List<string> resources = new List<string>();

                        if (id == "39" )
                        {
                            var _name = villagePage.DocumentNode.Descendants("img").Where(d => d.GetAttributeValue("class", "").Contains("dx1"));
                          //  _name.ElementAt(0).GetAttributeValue("alt", "").Split(new[] { " Level " }, StringSplitOptions.None);
                            name = "Rally Point";
                        }
                      else  if (id == "40")
                        {
                            var node = villagePage.DocumentNode.Descendants("img").Where(d => d.GetAttributeValue("class", "").Contains("wall"));

                            if (node.Any())
                            {
                                var _name = node.ElementAt(0).GetAttributeValue("alt", "").Split(' ');
                                name = _name[0] + " " + _name[1];
                                level = _name[3];
                            }
                            else { name = "City Wall"; level = "0"; }
                        }
                        else
                        {

                            var _name = nodes4.ElementAt(int.Parse(id) - 19).GetAttributeValue("alt", "").Split(new[] { " Level " }, StringSplitOptions.None);

                            name = _name[0];
                            level = _name.Length>1 ? _name[1]:"max";
                            if (title != null && title != "buildings" &&title != "Building is fully upgraded")
                            {

                                var doc = new HtmlDocument();
                                doc.LoadHtml(title);


                                foreach (var node in doc.DocumentNode.ChildNodes)
                                {
                                    if (node.Name == "span")
                                    {
                                        resources.Add(node.InnerText.Trim(' '));
                                    }
                                }
                            }
                        }

                            buildings.Add(new Building(id, name, level, href, resources));
                        


                    }
                }



                village.buildings = buildings;
            }
        }
        public List<Village> GetVillageList(HtmlDocument doc)
        {
             string xPath = "//*[@id=\"sidebarBoxVillagelist\"]/div[2]/div[2]/ul";
              var villages = new List<Village>();

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xPath);

            foreach (HtmlNode node in nodes[0].ChildNodes)
            {
                if (node.Name.Equals("li"))
                {
                    var href = node.ChildNodes[1].Attributes[0].Value;
                    int pos = href.LastIndexOf("=") + 1;
                    var id = href.Substring(pos, href.Length - pos - 1);

                    var name = node.ChildNodes[1].ChildNodes[3].InnerHtml;
                    var x1 = node.ChildNodes[1].ChildNodes[5].ChildNodes[0].InnerHtml;
                    var y1 = node.ChildNodes[1].ChildNodes[5].ChildNodes[2].InnerHtml;
                    var x = x1.Substring(1);
                    var y = y1.Substring(0, 2);
                    var active = false;
                    foreach (var att in node.Attributes)
                    {
                        if (att.Name.Equals("class") && att.Value.Equals(" active "))
                        {
                            active = true;
                        }
                    }
                    villages.Add(new Village(id, href, active, name, x, y));

                }
            }
            return villages;
        }

        private string GetBuildingName(string id)
        {
            switch (id)
            {
                case "19":
                    return "g26";
                    break;
                case "20":
                    return "g10";
                    break;
                case "21":
                    return "g11";
                    break;
                case "22":
                    return "g41";
                    break;
                case "24":
                    return "g24";
                    break;
                case "25":
                    return "g14";
                    break;
                case "27":
                    return "g17";
                    break;
                case "28":
                    return "g18";
                    break;
                case "29":
                    return "g21";
                    break;
                case "30":
                    return "g20";
                    break;
                case "32":
                    return "g19";
                    break;
                case "33":
                    return "g12";
                    break;
                case "34":
                    return "g7";
                    break;
                case "35":
                    return "g6";
                    break;
                case "36":
                    return "g22";
                    break;
                case "38":
                    return "g8";
                    break;
                case "39":
                    return "g16";
                    break;
                default: return "";
            }
        }
        #endregion


    }
}