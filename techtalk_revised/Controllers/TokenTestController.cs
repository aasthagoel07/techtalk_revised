using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace techtalk_revised.Controllers
{
    public class TokenTestController : ApiController
    {
        [Authorize]
        public IHttpActionResult Authorize()
        {
            return Ok("Authorized");
        }
    }
}
