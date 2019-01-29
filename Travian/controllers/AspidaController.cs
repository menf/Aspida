using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Travian.models;

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

        [Route("sendResources")]
        [HttpPost]
        public IHttpActionResult SendResources([FromBody] ResourceBody model)
        {
            Console.WriteLine(model);
            var c = RestClient.Instance;
            c.SendResources(model);
            return Ok(model);
        }
        [Route("sendTroops")]
        [HttpPost]
        public IHttpActionResult SendTroops([FromBody] TroopsBody model,string v)
        {

            Console.WriteLine(model);
            var c = RestClient.Instance;
            // c.SendResources(model);
            c.SendTroops(model,v);
            return Ok(v);
        }
    }
}
