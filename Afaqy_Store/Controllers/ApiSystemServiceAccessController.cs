using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Security.DataLayer;

namespace Afaqy_Store.Controllers
{
    public class ApiSystemServiceAccessController : BaseSecurityApiController<ServiceAccess>
    {
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiSystemServiceAccessViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
    }

    public class ApiSystemServiceAccessViewController : BaseSecurityApiController<ServiceAccessView>
    {
    }
}
