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

            //@tra_cmp_seq int = null, @tra_ref_id numeric = null, @tra_ref_type int = null, @tra_user_id varchar(15), 
            //     @tra_invt_id int = null, @tra_cura_seq int = null, @tra_aux_id int = null, @tra_sal_aux_id int = null, @tra_ps_code char(3) = null,
            //     @tra_wa_code char(3) = null, @tra_rate float = null, @tra_rate_sl float = null, @tra_date datetime = null, @tra_gross_before_disc float = null,
            //     @tra_gross float = null, @tra_disc float = null, @tra_add_disc float = null, @tra_vat float = null, @tra_net float = null,
            //     @tra_po_number varchar(15) = null, @tra_sup_doc_date smalldatetime = null, @tra_bat_code varchar(8) = null, @tra_bat_seq numeric = null,
            //     @tra_remark text = null, @tra_status varchar(255) = null, @tra_wa_code_to char(3) = null, @tra_trf_flag int = null, @tra_shipment varchar(10) = null,
            //     @tra_act_date smalldatetime = null, @tra_sup_ref varchar(15) = null, @tra_cancel numeric = null, @tra_flag_exp int = null, @tra_consign int = null,
            //     @tra_parent int = null, @tra_posted int = null, @tra_jvdocref varchar(17) = null, @tra_add_chg float = null, @tra_vat_chk int = null,
            //     @tra_wa_code_till char(3) = null, @tra_disc2 float = null, @tra_acc_cmp_seq int = null, @tra_extraname varchar(75) = null, @tra_workflow int = null,
            //     @tra_exp_id numeric = null, @tra_revision int = null, @tra_quotcode varchar(1) = null, @tra_filtercur int = null, @tra_purcontract varchar(15) = null,
            //     @tra_ttc int = null, @tra_add_discttc float = null, @tra_driver_name varchar(20) = null, @tra_truck_name varchar(20) = null,
            //     @tra_gsn_ref_id numeric = null, @tra_purchaseforeign int = null, @tra_flg_res int = null, @fk_serial int = null, @tra_book_date datetime = null,
            //     @dblPuType as numeric = null, @tra_assignto_user varchar(10) = null


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
                    //get current status of device
                    var lastStatus = device.DeviceStatusId;
                    //update device status
                    device.DeviceStatusId = (int)Classes.Common.DBEnums.DeviceStatus.In_customer_delivery_phase;
                    //if device has new status insert it to status history
                    if (lastStatus != device.DeviceStatusId)
                    {
                        //insert new status in status history
                        var CurrentUser = new Models.UserViewModel().GetUserFromSession();
                        var userId = CurrentUser.UserId;
                        context.DeviceStatusHistory.Add(new DeviceStatusHistory() { DeviceId = device.DeviceId, DeviceStatusId = device.DeviceStatusId, BranchId = device.BranchId, Note = value.Note, CreateUserId = userId, CreateDate = DateTime.Now });
                    }
                    
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
