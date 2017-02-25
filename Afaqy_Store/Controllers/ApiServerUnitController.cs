using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiServerUnitController : BaseApiController<ServerUnit>
    {
        [HttpGet]
        public IHttpActionResult Clear()
        {
            
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Clear))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            repo.Repo.ExceuteSql("TRUNCATE TABLE [afqy].[ServerUnit]");
            return Content(HttpStatusCode.OK, "Success");
           
        }
    }
}
