using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Travian.models;

namespace Travian
{


class RestClient
    {

        private string serverUrl = "";
        private string resourceUrl = "dorf1.php";
        private string villageUrl = "dorf2.php";
        private string buildUrl = "build.php";
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
                user.messages.Add("Logowanie...");
                var msg = "Login OK : " + Program.GetUserName(doc);
                user.messages.Add(msg);
                Console.WriteLine(msg);
                GetPlayerInfo();
                var res = JsonConvert.SerializeObject(user);
                user.ResetMessages();
                return res;
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
                    village.active = true;;
                }
                user.messages.Add("Sprawdzam wioske: " + village.name);
                var resourcesPage = GetResourcePage();
                var villagePage = GetVillagePage();

                List<string> resourcesVillage = new List<string>();
                List<Unit> units = new List<Unit>();
                List<Building> buildingQueue = new List<Building>();
                List<Building> buildings = new List<Building>();

                user.messages.Add("Pobieram surowce");
                resourcesVillage.Add(resourcesPage.DocumentNode.SelectSingleNode("//*[@id=\"l1\"]").InnerHtml);
                resourcesVillage.Add(resourcesPage.DocumentNode.SelectSingleNode("//*[@id=\"l2\"]").InnerHtml);
                resourcesVillage.Add(resourcesPage.DocumentNode.SelectSingleNode("//*[@id=\"l3\"]").InnerHtml);
                resourcesVillage.Add(resourcesPage.DocumentNode.SelectSingleNode("//*[@id=\"l4\"]").InnerHtml);
                village.resources = resourcesVillage;

                user.messages.Add("Pobieram wojsko");
                HtmlNodeCollection unitsNode = resourcesPage.DocumentNode.SelectNodes("//*[@id=\"troops\"]/tbody");              
                if (unitsNode!=null)
                {
                    var unitNodes = unitsNode[0].Descendants("tr");
                    if (unitNodes.ElementAt(0).ChildNodes[0].InnerHtml != "None")
                    {
                        foreach (var _node in unitNodes)
                        {

                            var count = _node.ChildNodes[1].InnerHtml;
                            var name = _node.ChildNodes[2].InnerHtml;
                            var id = _node.ChildNodes[0].ChildNodes[0].ChildNodes[0].Attributes[0].Value.Replace("unit", "").Replace('u', 't').Trim();
                            if (id == "thero") id = "t11";
                            units.Add(new Unit(id, name, count));
                        }
                        village.units = units;
                    }

                }

                user.messages.Add("Pobieram kolejke budowy");
                HtmlNodeCollection buildingQueueNode = resourcesPage.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[10]/ul");
                if (buildingQueueNode != null)
                {
                    var _buildingQueue = buildingQueueNode[0].ChildNodes;

                    foreach (var el in _buildingQueue)
                    {
                        string name = el.ChildNodes[1].ChildNodes[0].InnerHtml;
                        string level = (el.ChildNodes[1].ChildNodes[1].InnerHtml).Replace("level ", "");
                        string buildDuration = el.ChildNodes[2].InnerText.Substring(0, 8);
                        Building b = new Building(name, level, buildDuration);
                        buildingQueue.Add(b);
                    }
                    village.buildingsQueue = buildingQueue;
                }

                user.messages.Add("Pobieram pola surowcow");
                HtmlNodeCollection resourcesNode = resourcesPage.DocumentNode.SelectNodes("//*[@id=\"rx\"]");
              
                foreach (var el in resourcesNode[0].ChildNodes)
                {
                    if (el.Name == "area")
                    {
                        string href = el.GetAttributeValue("href", "");
                        string id = href.Replace("build.php?id=", "");
                        string title = el.GetAttributeValue("title", null);
                        string name,level;
                        List<string> _resources = new List<string>();
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
                                    _resources.Add(node.InnerText.Trim(' '));
                                }
                            }
                            buildings.Add(new Building(id,name, level, href, _resources));
                        }
                        
 
                    }
                }


                user.messages.Add("Pobieram budynki");
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
                        List<string> _resources = new List<string>();

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
                                        _resources.Add(node.InnerText.Trim(' '));
                                    }
                                }
                            }
                        }
                            buildings.Add(new Building(id, name, level, href, _resources));                     
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
                    var y = y1.Replace(")","");
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
        public void AutoUpgrade()
        {
            var user = User.Instance;
            user.messages.Add("Rozpoczynam automatyczną rozbudowe");
            UpgradeVillages(ref user);
        }
        public void UpgradeVillages(ref User user)
        {
            foreach (Village village in user.Villages)
            {


            }
        }
        public void SendResources(ResourceBody model)
        {
            
            var request = new RestSharp.RestRequest(buildUrl, Method.POST);
            request.AddParameter("ft", "check"); // adds to POST or URL querystring based on Method
            request.AddParameter("id", model.id); // adds to POST or URL querystring based on Method
            request.AddParameter("r1", model.r1); // adds to POST or URL querystring based on Method
            request.AddParameter("r2", model.r2); // adds to POST or URL querystring based on Method
            request.AddParameter("r3", model.r3); // adds to POST or URL querystring based on Method
            request.AddParameter("r4", model.r4); // adds to POST or URL querystring based on Method
            request.AddParameter("x", model.x); // adds to POST or URL querystring based on Method
            request.AddParameter("y", model.y); // adds to POST or URL querystring based on Method
            request.AddParameter("send3", "1"); // adds to POST or URL querystring based on Method
            request.AddParameter("s1", "ok"); // adds to POST or URL querystring based on Method

            var user = User.Instance;
            
            var xxx = GetVillagePage(model.villageid);
            user.messages.Add("Wysyłam surowce");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                var node = doc.DocumentNode.SelectSingleNode("//*[@id=\"build\"]/form/input[4]");

                if (node != null)
                {
                    var request2 = new RestSharp.RestRequest(buildUrl, Method.POST);
                    request2.AddParameter("ft", "mk1"); // adds to POST or URL querystring based on Method
                    request2.AddParameter("id", model.id); // adds to POST or URL querystring based on Method
                    request2.AddParameter("r1", model.r1); // adds to POST or URL querystring based on Method
                    request2.AddParameter("r2", model.r2); // adds to POST or URL querystring based on Method
                    request2.AddParameter("r3", model.r3); // adds to POST or URL querystring based on Method
                    request2.AddParameter("r4", model.r4); // adds to POST or URL querystring based on Method
                    request2.AddParameter("x", model.x); // adds to POST or URL querystring based on Method
                    request2.AddParameter("y", model.y); // adds to POST or URL querystring based on Method
                    request2.AddParameter("send3", "1"); // adds to POST or URL querystring based on Method
                    request2.AddParameter("s1", "ok"); // adds to POST or URL querystring based on Method
                    request2.AddParameter("getwref", node.Attributes[2].Value); // adds to POST or URL querystring based on Method
                    user.messages.Add("Wysyłam surowce..");
                    IRestResponse response2 = client.Execute(request2);
                    if (response.IsSuccessful)
                    {
                        user.messages.Add("Wysyłano surowce");
                        content = response.Content; // raw content as string
                         doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(content);
                        GetVillagePage();
                    }
                }
            }
        }
        public void SendTroops(TroopsBody model,string villageid)
        {
            var request = new RestSharp.RestRequest("a2b.php", Method.POST);
            var user = User.Instance;
            user.messages.Add("Wysyłam wojska");
            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains('t') && propertyInfo.Name!="type" )
                {
                    if(propertyInfo.GetValue(model)!=null)
                    request.AddParameter(propertyInfo.Name, propertyInfo.GetValue(model));
                }
            }
            request.AddParameter("x", model.x); // adds to POST or URL querystring based on Method
            request.AddParameter("y", model.y); // adds to POST or URL querystring based on Method
            request.AddParameter("c", model.type); // adds to POST or URL querystring based on Method
            request.AddParameter("s1", "ok"); // adds to POST or URL querystring based on Method
            var xxx = GetVillagePage(villageid);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = response.Content; // raw content as string
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                var node = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/form");
                var _nodes = node.Descendants("input");
                if (_nodes != null)
                {
                    var request2 = new RestSharp.RestRequest("a2b.php", Method.POST);
                    foreach (var el in _nodes)
                    {
                        request2.AddParameter(el.GetAttributeValue("name",""), el.GetAttributeValue("value","")); // adds to POST or URL querystring based on Method
                    }
                    user.messages.Add("Wysyłam wojska..");
                    IRestResponse response2 = client.Execute(request2);
                    if (response.IsSuccessful)
                    {

                        user.messages.Add("Wysyłano wojska");
                        content = response.Content; // raw content as string
                        doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(content);
                        GetVillagePage();
                    }
                }
            }
        }
        public string Refresh()
        {
            var user = User.Instance;
            user.messages.Add("Odswiezanie danych");
            Console.WriteLine("Odswiezanie danych");
            GetPlayerInfo();
            var res = JsonConvert.SerializeObject(user);
            user.ResetMessages();
            return res;
        }
        #endregion


    }
}