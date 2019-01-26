using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travian.models;

namespace Travian
{
    class User
    {
       
        private static readonly Lazy<User> user =
       new Lazy<User>(() => new User());
        public static User Instance { get { return user.Value; } }


        public string login;
        public string password;
        public Dictionary<string, Village> villages = new Dictionary<string, Village>();
        public Dictionary<string, Adventure> adventures = new Dictionary<string, Adventure>();
        private User()
        {
        }
        public void SetUser(string login,string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
