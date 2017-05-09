using Afaqy_Store.DataLayer;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class DeliveryItemModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryItem/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryItemModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryItemViewModel : TransactionItemView
    {

    }

    public class DeliveryItemDetailsViewModel : DeliveryItemViewModel
    {

    }
    
    [Bind(Include = "TransactionItemId,cmp_seq,TransactionDetailsId,ModelType_ia_item_id,DeviceId,Employee_aux_id,InstallingDateTime,DeviceNaming_en,DeviceNaming_ar,DeviceNamingTypeId,AddToServer,TrackWithTechnician,ServerUpdated,TechnicalApproval,IsReturn,Note,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryDeviceEditBindModel : TransactionItem
    {
        
    }

}