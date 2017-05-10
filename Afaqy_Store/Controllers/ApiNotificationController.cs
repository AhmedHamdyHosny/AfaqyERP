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
    public class ApiNotificationController : BaseApiController<Notification>
    {
        //public ApiNotificationController()
        //{
        //    //filter by user
        //    var dataContrains = GetDataConstrains();
        //    if (dataContrains == null)
        //    {
        //        dataContrains = x => x.ToUserId == ;
        //    }
        //    else
        //    {
        //        dataContrains.AndAlso(x => x.ToUserId == );
        //    }
        //    SetDataConstrains(dataContrains);

        //}
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiNotificationViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiNotificationViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiNotificationViewController : BaseApiController<NotificationView>
    {
        //public ApiNotificationViewController()
        //{
        //    //filter by user
        //    var dataContrains = GetDataConstrains();
        //    if (dataContrains == null)
        //    {
        //        dataContrains = x => x.ToUserId == ;
        //    }
        //    else
        //    {
        //        dataContrains.AndAlso(x => x.ToUserId == );
        //    }
        //    SetDataConstrains(dataContrains);

        //}
    }
}
