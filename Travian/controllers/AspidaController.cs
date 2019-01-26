using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace Travian.controllers
{
   
    public class AspidaController : ApiController
    {   [Route("login")]
        public string Get(string login, string password)
        {
           var c = RestClient.Instance;
            return c.Login(login,password);
        }
        public string Get(int id)
        {
            return "value";
        }
    }
}
