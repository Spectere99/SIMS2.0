using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SIMSService.DataActions;

namespace SIMSService.Controllers
{
    public class LookupController : ApiController
    {
        // GET: api/Lookup
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            LookupActions lookupActions = new LookupActions();

            return Ok(lookupActions.GetLookups());
        }

        // GET: api/Lookup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lookup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Lookup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lookup/5
        public void Delete(int id)
        {
        }
    }
}
