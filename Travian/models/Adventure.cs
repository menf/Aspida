using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian.models
{
    class Adventure
    {
    public string location;
    public string moveTime;
    public string difficult;
    public int id;

        public Adventure(string location, string moveTime, string difficult, int id)
        {
            this.location = location;
            this.moveTime = moveTime;
            this.difficult = difficult;
            this.id = id;
        }
    }
}
