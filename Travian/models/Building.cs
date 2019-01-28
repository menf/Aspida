using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian.models
{

    class Building
    {
        private string[] Level20 = { "Cropland", "Iron Mine", "Clay Pit", "Woodcutter", "Marketplace", "Granary", "Rally Point", "Earth Wall", "Marketplace", "Smithy", "Heromansion", "Main Building" };
        private string[] Level10 = { "Warehouse", "Academy", "Treasury", "Cranny" };
        private string[] LevelUnkown = { "Residence", "Embassy" };
        public List<string> resources;
        public string name = "";
        public string duration = "";
        public string url = "";
        public string upgradeUrl = "";
       // public string[] unitsUrl = new string[20];
        public string level = "";
        public string maxLevel = "";
        public string id = "";
        private string buildDuration = "";

        public Building(string name, string level, string buildDuration)
        {
            this.name = name;
            this.level = level;
            this.buildDuration = buildDuration;
        }
        public Building(string id,string name, string level, string url,List<string> resources)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.url = url;
            this.resources = resources;
            SetMaxLevel();
        }

        private void SetMaxLevel()
        {
            if (Level20.Contains(name)) maxLevel = "20";
            else if (Level10.Contains(name)) maxLevel = "10";
            else maxLevel = "varies";
        }
    }
}
