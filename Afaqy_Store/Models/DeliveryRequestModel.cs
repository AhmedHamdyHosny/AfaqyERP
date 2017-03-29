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
    public class DeliveryRequestIndexViewModel : DeliveryRequestView
    {

    }
    public class DeliveryRequestDetailsViewModel : DeliveryRequestViewModel
    {
        public List<RequestDetails_DetailsViewModel> DeliveryRequestDetails { get; set; }
        public List<DeliveryRequestTechnician> Technician { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POSId,WarehouseId,SaleTransactionTypeId,CustomerId,CustomerContactId,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,Note,DeliveryRequestDetails")]
    public class DeliveryRequestCreateBindModel : DeliveryRequest
    {
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POSId,WarehouseId,SaleTransactionTypeId,CustomerId,CustomerContactId,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,DeliveryRequestStatusId,Note,DeliveryRequestDetails,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryRequestEditBindModel : DeliveryRequest
    {
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }
    }
    public class DeliveryRequestEditModel
    {
        public DeliveryRequest EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> PointOfSale { get; set; }
        public IEnumerable<CustomSelectListItem> Warehouse { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerContact { get; set; }
    }
}