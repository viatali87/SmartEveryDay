using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartEveryDay.Controllers
{
    public class LightController : ApiController
    {
        // GET: api/Light
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Light/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Light
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Light/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Light/5
        public void Delete(int id)
        {
        }
    }
}
