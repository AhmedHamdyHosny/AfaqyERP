using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classes.Common;

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

    public class DeliveryNoteViewModel : TransactionView
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
    public class DeliveryNoteIndexViewModel : TransactionView
    {

    }
    public class DeliveryNoteDetailsViewModel : DeliveryNoteViewModel
    {
        public List<DeliveryDetails_DetailsViewModel> DeliveryDetails { get; set; }
        public List<DeliveryTechnicianViewModel> DeliveryTechnicians { get; set; }
        public List<DeliveryItemViewModel> DeliveryDevices { get; set; }
    }
    [Bind(Include = "TransactionId,TransactionTypeId,cmp_seq,POS_ps_code,Warehouse_wa_code,DeliveryRequestId,Customer_aux_id,CustomerName,DolphinCustomerName,CustomerAccountName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,SaleTransactionTypeId,TransactionDateTime,TransactionStatusId,TransactionReference,DolphinTrans_tra_ref_id,DolphinTrans_tra_ref_type,SystemId,WithInstallationService,Note,TransactionDetails,TransactionDevice")]
    public class DeliveryNoteCreateBindModel : Transaction
    {
        public it_trans_a DolphinTrans { get; set; }
        public DeliveryRequestViewModel DeliveryRequestView { get; set; }
        public List<DeliveryRequestDetailsView> DeliveryRequestDetails { get; set; }
        public List<DeliveryRequestTechnicianViewModel> DeliveryRequestTechnician { get; set; }
        public TransactionItemView[] DeliveryDevice { get; set; }
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
                this.cmp_seq = deliveryRequest.cmp_seq;
                this.POS_ps_code = deliveryRequest.POS_ps_code;
                this.Warehouse_wa_code = deliveryRequest.Warehouse_wa_code;
                this.Customer_aux_id = deliveryRequest.Customer_aux_id;
                this.CustomerName = deliveryRequest.CustomerName;
                this.CustomerContact_serial = deliveryRequest.CustomerContact_serial;
                this.AlternativeContactName = deliveryRequest.AlternativeContactName;
                this.AlternativeContactTelephone = deliveryRequest.AlternativeContactTelephone;
                this.SaleTransactionTypeId = deliveryRequest.SaleTransactionTypeId;
                this.TransactionDateTime = (DateTime)deliveryRequest.ActualDeliveryDateTime;
                this.SystemId = deliveryRequest.SystemId;
                this.WithInstallationService = deliveryRequest.WithInstallationService;
            }
        }

        internal string GetNewDeliveryReference(string warehouseCode)
        {
            string deliveryReference = "";
            switch (warehouseCode)
            {
                case "WHS": //riyadh
                    deliveryReference = "Riy";
                    break;
                case "jed": //jeddah 
                    deliveryReference = "Jed";
                    break;
                case "dam": //dammam
                    deliveryReference = "Dam";
                    break;
                case "05": //Demo
                    deliveryReference = "Demo";
                    break;
                default:
                    deliveryReference = "None";
                    break;
            }
            var requestBody = new GenericApiController.Utilities.GenericDataFormat();
            var filters = new List<GenericApiController.Utilities.GenericDataFormat.FilterItems>();
            filters.Add(new GenericApiController.Utilities.GenericDataFormat.FilterItems() { Property = "Warehouse_wa_code", Operation = GenericApiController.Utilities.GenericDataFormat.FilterOperations.Equal, Value = warehouseCode, LogicalOperation = GenericApiController.Utilities.GenericDataFormat.LogicalOperations.And });
            requestBody.Filters = filters;
            requestBody.Paging = new GenericApiController.Utilities.GenericDataFormat.PagingItem() { PageNumber = 1, PageSize = 1 };
            requestBody.Sorts = new List<GenericApiController.Utilities.GenericDataFormat.SortItems>();
            requestBody.Sorts.Add(new GenericApiController.Utilities.GenericDataFormat.SortItems() { Property = "TransactionId", SortType = GenericApiController.Utilities.GenericDataFormat.SortType.Desc });
            var result = new DeliveryNoteModel<Transaction>().Get(requestBody);
            if (result != null && result.Count == 1)
            {
                int serial = int.Parse(System.Text.RegularExpressions.Regex.Match(result[0].TransactionReference, @"\d+").Value);
                serial++;
                deliveryReference += " " + serial.ToString();
            }
            else
            {
                deliveryReference += " 20000";
            }

            return deliveryReference;
        }
    }
    [Bind(Include = "TransactionId,TransactionTypeId,cmp_seq,POS_ps_code,Warehouse_wa_code,DeliveryRequestId,Customer_aux_id,CustomerName,DolphinCustomerName,CustomerAccountName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,SaleTransactionTypeId,TransactionDateTime,TransactionStatusId,TransactionReference,DolphinTrans_tra_ref_id,DolphinTrans_tra_ref_type,SystemId,WithInstallationService,Note,IsBlock,CreateUserId,CreateDate,TransactionDetails,TransactionDevice")]
    public class DeliveryNoteEditBindModel : Transaction
    {
        internal static void UpdateStatus(string deliveryNoteId, int transactionsStatusId, string note, int userId)
        {
            //get delivery note
            var deliveryNote = new DeliveryNoteModel<Transaction>().Get(deliveryNoteId);
            if(!string.IsNullOrEmpty(note) && deliveryNote.Note != note)
            {
                deliveryNote.Note = note;
                deliveryNote.ModifyUserId = userId;
                deliveryNote.ModifyDate = DateTime.Now;
            }
            deliveryNote.TransactionStatusId = transactionsStatusId;
            deliveryNote = new DeliveryNoteModel<Transaction>().Update(deliveryNote, deliveryNoteId);
            //add new status to status history
            TransactionStatusHistoryModel<TransactionStatusHistory> historyModel = new TransactionStatusHistoryModel<TransactionStatusHistory>();
            historyModel.Insert(new TransactionStatusHistory() { TransactionId = deliveryNote.TransactionId, TransactionStatusId = deliveryNote.TransactionStatusId, Note = deliveryNote.Note, CreateUserId = userId, CreateDate = DateTime.Now});
        }
    }
    public class DeliveryNoteEditModel
    {
        public Transaction EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> PointOfSale { get; set; }
        public IEnumerable<CustomSelectListItem> Warehouse { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerContact { get; set; }
    }
}