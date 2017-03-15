using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class DeliveryRequestModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryRequest/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryRequestModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryRequestViewModel : DeliveryRequestView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    public class DeliveryRequestIndexViewModel : DeliveryRequest
    {

    }
    public class DeliveryRequestDetailsViewModel : DeliveryRequestViewModel
    {

    }
    [Bind(Include = "DeliveryRequestId,POSId,DeliveryRequestStatusId,CustomerId,SaleTransactionTypeId,DeliveryDate_Str,DeliveryTime_Str,DeliveryDateTime,Note,DeliveryRequestDetails")]
    public class DeliveryRequestCreateBindModel : DeliveryRequest
    {
        public string DeliveryDate_Str { get; set; }
        public string DeliveryTime_Str { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POSId,DeliveryRequestStatusId,CustomerId,SaleTransactionTypeId,DeliveryDate_Str,DeliveryTime_Str,DeliveryDateTime,Note,DeliveryRequestDetails,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryRequestEditBindModel : DeliveryRequest
    {
        public string DeliveryDate_Str { get; set; }
        public string DeliveryTime_Str { get; set; }
    }
    public class DeliveryRequestEditModel
    {
        public DeliveryRequest EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
    }
}