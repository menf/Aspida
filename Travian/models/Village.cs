using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian.models
{
    class Village
    {
        public string id="";
        public string name = "";
        public string x = "";
        public string y = "";
        public string href = "";
        public bool active;
        public List<string> resources = new List<string>();
        public List<Unit> units = new List<Unit>();
        public List<Building> buildings = new List<Building>();
        public List<Building> buildingsQueue = new List<Building>();


        public Village(string id, string href, bool active, string name, string x, string y)
        {
            this.id = id;
            this.href = href;
            this.active = active;
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }
}
