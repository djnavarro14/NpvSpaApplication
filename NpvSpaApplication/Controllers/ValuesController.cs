using NpvSpaApplication.Helper;
using NpvSpaApplication.Models;
using System.Web.Http;

namespace NpvSpaApplication.Controllers
{
    public class ValuesController : ApiController
    {
        public IHttpActionResult Post([FromBody]NpvObjectModel model)
        {
            var result = new ComputeHelper().NpvCollection(model);
            return Ok(result);
        }
    }
}
