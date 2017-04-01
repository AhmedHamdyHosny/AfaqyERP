using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiDeliveryNoteController : BaseApiController<DeliveryNote>
    {
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Post(DeliveryNote value)
        {
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Post))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            DeliveryNote result = null;
            using (var context = new AfaqyStoreEntities())
            {
                result = context.DeliveryNote.Add(value);

                var UserId = value.CreateUserId;
                //update status of each device in Delivery
                foreach (var item in value.DeliveryDetails.SelectMany(x => x.DeliveryDevice))
                {
                    
                    var device = context.Device.SingleOrDefault(x => x.DeviceId == item.DeviceId);
                    device.DeviceStatusId = (int)Classes.Common.DBEnums.DeviceStatus.In_customer_delivery_phase;
                    //insert new device status history
                    //context.DeviceStatusHistory.Add(new DeviceStatusHistory() { DeviceId = device.DeviceId, DeviceStatusId = device.DeviceStatusId, BranchId = device.BranchId, CreateUserId = UserId, CreateDate = DateTime.Now });
                    context.Entry(device).State = System.Data.Entity.EntityState.Modified;
                }

                context.SaveChanges();
            }
            return Content(HttpStatusCode.OK, result);
            
        }
        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryNoteViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiDeliveryNoteViewController : BaseApiController<DeliveryNoteView>
    {

    }
}
