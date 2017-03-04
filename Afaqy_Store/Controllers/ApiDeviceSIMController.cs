using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiDeviceSIMController : BaseApiController<DeviceSIM>
    {

        public override IHttpActionResult Import(List<DeviceSIM> entities)
        {
            using (AfaqyStoreEntities Context = new AfaqyStoreEntities())
            {
                //get userid
                var userId = entities[0].CreateUserId;
                //add connection between devices and sim cards
                Context.DeviceSIM.AddRange(entities);
                //get all device ids
                var deviceIds = entities.Select(x => x.DeviceId).Distinct();
                //get all devices to update status
                var Devices = Context.Device.Where(x => deviceIds.Contains(x.DeviceId)).ToList();
                
                //update status
                Devices = Devices.Select(x => { x.DeviceStatusId = (int)Classes.Common.DBEnums.DeviceStatus.Connted_with_SIM_card; x.ModifyUserId = userId; x.ModifyDate = DateTime.Now; return x; }).ToList();
                foreach (var device in Devices)
                {
                    //Context.Entry(device).State = EntityState.Detached;
                    Context.Device.Attach(device);
                    Context.Entry(device).State = System.Data.Entity.EntityState.Modified;
                }

                //get all simcard ids
                var simCardIds = entities.Select(x => x.SIMCardId).Distinct();
                //get all Sim cards to update status
                var simCards = Context.SIMCard.Where(x => simCardIds.Contains(x.SIMCardId)).ToList();
                //update status
                simCards = simCards.Select(x => { x.SIMCardStatusId = (int)Classes.Common.DBEnums.SIMCardStatus.Linked_with_device; x.ModifyUserId = userId; x.ModifyDate = DateTime.Now; return x; }).ToList();
                foreach (var simcard in simCards)
                {
                    //Context.Entry(simcard).State = EntityState.Detached;
                    Context.SIMCard.Attach(simcard);
                    Context.Entry(simcard).State = System.Data.Entity.EntityState.Modified;
                }

                Context.SaveChanges();
            }
            return Content(HttpStatusCode.OK, "Success");
        }
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeviceSIMViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeviceSIMViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiDeviceSIMViewController : BaseApiController<DeviceSIMView>
    {

    }
}
