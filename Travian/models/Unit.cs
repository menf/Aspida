using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian.models
{
    class Unit
    {
        public int id; 
    public string title;
    public bool available;
    public Building building;

        public Unit(int id, string title, bool available, Building building)
        {
            this.id = id;
            this.title = title;
            this.available = available;
            this.building = building;
        }
    }
}
