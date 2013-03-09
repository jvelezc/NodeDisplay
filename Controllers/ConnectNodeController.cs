using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NodeDisplay.Controllers
{
    public class ConnectNodeController : ApiController
    {
        // GET api/connectnode
        public IEnumerable<int> Get()
        {
            return new int[] { 1, 2 };
        }

        // GET api/connectnode/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/connectnode
        public void Post([FromBody]string value)
        {
        }

        // PUT api/connectnode/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/connectnode/5
        public void Delete(int id)
        {
        }
    }
}
