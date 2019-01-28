using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace Travian.controllers
{
   
    public class AspidaController : ApiController
    {[Route("login")]
        [HttpGet]
        public string Login(string login, string password, string world)
        {
            Console.WriteLine("Logowanie");
           var c = RestClient.Instance;
            return c.Login(login, password, world);             
        }

        [Route("refresh")]
        [HttpGet]
        public string RefreshData()
        {
            var c = RestClient.Instance;
            return c.Refresh();
        }
    }
}
