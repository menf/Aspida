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
        public string href = "";
        public bool active;
        public List<Building> buildings = new List<Building>();
        public string name = "";
        public string x = "";
        public string y = "";

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
