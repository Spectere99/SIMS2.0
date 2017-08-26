using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SIMSService.DataActions;

namespace SIMSService.Controllers
{
    public class LookupTypeController : ApiController
    {
        // GET: api/LookupType
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            LookupActions lookupActions = new LookupActions();

            return Ok(lookupActions.GetLookupTypes());
        }

        // GET: api/LookupType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LookupType
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LookupType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LookupType/5
        public void Delete(int id)
        {
        }
    }
}
