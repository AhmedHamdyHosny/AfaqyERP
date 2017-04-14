using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericApiController.Utilities;

namespace Afaqy_Store.Controllers
{
    public class ApiCustomerController : BaseApiController<rpaux>
    {
        public ApiCustomerController()
        {
            var dataContrains = GetDataConstrains();
            if(dataContrains == null)
            {
                dataContrains = x => x.salecode != null;
            }
            else
            {
                dataContrains.AndAlso(x => x.salecode != null);
            }
            SetDataConstrains(dataContrains);
            
        }
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiCustomerViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiCustomerViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiCustomerViewController : BaseApiController<CustomerView>
    {

    }
}
