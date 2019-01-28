using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian.models
{
    class Unit
    {
        public string id; 
        public string name;
        public string count;
        public Building building;


        public Unit(string id, string name, string count)
        {
            this.id = id;
            this.name = name;
            this.count = count;
        }

        public Unit(string id, string name, string count, Building building)
        {
            this.id = id;
            this.name = name;
            this.count = count;
            this.building = building;
        }
    }
}
