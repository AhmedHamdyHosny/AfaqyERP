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
    public class DeliveryNoteModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryNote/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryNoteModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryNoteViewModel : DeliveryNoteView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    public class DeliveryNoteIndexViewModel : DeliveryNoteView
    {

    }
    public class DeliveryNoteDetailsViewModel : DeliveryNoteViewModel
    {
        public List<DeliveryDetails_DetailsViewModel> DeliveryDetailsView { get; set; }
        public List<DeliveryTechnicianViewModel> TechnicianView { get; set; }
    }
    [Bind(Include = "DeliveryNoteId,POSId,WarehouseId,SaleTransactionTypeId,DeliveryRequestId,CustomerId,CustomerContactId,CustomerName,AlternativeContactName,AlternativeContactTelephone,DeliveryDateTime,SystemId,WithInstallationService,Note,DolphinReference,DeliveryDetails,DeliveryDevice")]
    public class DeliveryNoteCreateBindModel : DeliveryNote
    {
        public DeliveryRequestViewModel DeliveryRequestView { get; set; }
        public List<DeliveryRequestTechnicianViewModel> DeliveryRequestTechnician { get; set; }
        public DeliveryDeviceView[] DeliveryDevice { get; set; }
        public string InstallationService
        {
            get
            {
                if(this.WithInstallationService != null)
                {
                    return (bool)this.WithInstallationService ? Resources.Resource.True : Resources.Resource.False;
                }
                else
                {
                    return null;
                }
            }
        }
        internal void ToDeliveryNote(DeliveryRequest deliveryRequest)
        {
            if (deliveryRequest != null)
            {
                this.POSId = deliveryRequest.POSId;
                this.WarehouseId = deliveryRequest.WarehouseId;
                this.CustomerId = deliveryRequest.CustomerId;
                this.CustomerName = deliveryRequest.CustomerName;
                this.CustomerContactId = deliveryRequest.CustomerContactId;
                this.AlternativeContactName = deliveryRequest.AlternativeContactName;
                this.AlternativeContactTelephone = deliveryRequest.AlternativeContactTelephone;
                this.SaleTransactionTypeId = deliveryRequest.SaleTransactionTypeId;
                this.DeliveryDateTime = (DateTime)deliveryRequest.ActualDeliveryDateTime;
                this.SystemId = deliveryRequest.SystemId;
                this.WithInstallationService = deliveryRequest.WithInstallationService;
            }
        }
    }
    [Bind(Include = "DeliveryNoteId,POSId,WarehouseId,SaleTransactionTypeId,DeliveryRequestId,CustomerId,CustomerContactId,CustomerName,AlternativeContactName,AlternativeContactTelephone,DeliveryDateTime,SystemId,WithInstallationService,DeliveryStatusId,Note,DolphinReference,DolphinDeliveryId,DeliveryDetails,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryNoteEditBindModel : DeliveryNote
    {
        //public string DeliveryNoteDate_Str { get; set; }
        //public string DeliveryNoteTime_Str { get; set; }
    }
    public class DeliveryNoteEditModel
    {
        public DeliveryNote EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> PointOfSale { get; set; }
        public IEnumerable<CustomSelectListItem> Warehouse { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerContact { get; set; }
    }
}