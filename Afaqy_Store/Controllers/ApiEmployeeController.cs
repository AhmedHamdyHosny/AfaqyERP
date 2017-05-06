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
    public class ApiRpauxEmployeeController : BaseApiController<rpaux>
    {
        public ApiRpauxEmployeeController()
        {
            var dataContrains = GetDataConstrains();
            if (dataContrains == null)
            {
                dataContrains = x => x.employee == 1;
            }
            else
            {
                dataContrains.AndAlso(x => x.employee == 1);
            }
            SetDataConstrains(dataContrains);

        }
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiRpauxEmployeeViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiRpauxEmployeeViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiRpauxEmployeeViewController : BaseApiController<RpauxEmployeeView>
    {

    }

    public class ApiEmployeeController : BaseApiController<Employee>
    {
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiEmployeeViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiEmployeeViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiEmployeeViewController : BaseApiController<EmployeeView>
    {
        
    }
}
