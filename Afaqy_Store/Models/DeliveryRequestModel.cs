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
        public string InstallationService
        {
            get
            {
                if (this.WithInstallationService != null)
                {
                    return (bool)this.WithInstallationService ? Resources.Resource.True : Resources.Resource.False;
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class DeliveryRequestIndexViewModel : DeliveryRequestView
    {

    }
    public class DeliveryRequestDetailsViewModel : DeliveryRequestViewModel
    {
        public List<RequestDetails_DetailsViewModel> DeliveryRequestDetails { get; set; }
        public List<DeliveryRequestTechnicianViewModel> DeliveryRequestTechnician { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POS_ps_code,Warehouse_wa_code,SaleTransactionTypeId,Customer_aux_id,CustomerName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,WithInstallationService,Note,DeliveryRequestDetails")]
    public class DeliveryRequestCreateBindModel : DeliveryRequest
    {
        //public string POSId { get; set; }
        //public string WarehouseId { get; set; }
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POS_ps_code,Warehouse_wa_code,SaleTransactionTypeId,Customer_aux_id,CustomerName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,WithInstallationService,DeliveryRequestStatusId,cmp_seq,Note,DeliveryRequestDetails,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryRequestEditBindModel : DeliveryRequest
    {
        //public string POSId { get; set; }
        //public string WarehouseId { get; set; }
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }
    }
    public class DeliveryRequestEditModel
    {
        public DeliveryRequest EditItem { get; set; }
        public List<DeliveryRequestDetailsView> DeliveryRequestDetails { get; set; }
        public IEnumerable<CustomSelectListItem> PointOfSale { get; set; }
        public IEnumerable<CustomSelectListItem> Warehouse { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerContact { get; set; }
    }
}