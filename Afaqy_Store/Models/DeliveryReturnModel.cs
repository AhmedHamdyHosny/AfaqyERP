using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class DeliveryReturnModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryReturn/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryReturnModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryReturnViewModel : TransactionView
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
    public class DeliveryReturnIndexViewModel : TransactionView
    {

    }
    public class DeliveryReturnDetailsViewModel : DeliveryReturnViewModel
    {
        public List<DeliveryDetails_DetailsViewModel> DeliveryDetails { get; set; }
        public List<DeliveryItemViewModel> DeliveryDevices { get; set; }
    }
    [Bind(Include = "TransactionId,TransactionTypeId,cmp_seq,POS_ps_code,Warehouse_wa_code,DeliveryRequestId,Customer_aux_id,CustomerName,DolphinCustomerName,CustomerAccountName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,SaleTransactionTypeId,TransactionDateTime,TransactionStatusId,TransactionReference,DolphinTrans_tra_ref_id,DolphinTrans_tra_ref_type,SystemId,WithInstallationService,Note,TransactionDetails,TransactionDevice")]
    public class DeliveryReturnCreateBindModel : Transaction
    {
        public DeliveryNoteViewModel DeliveryNote { get; set; }
        public List<DeliveryDetails_DetailsViewModel> DeliveryNoteDetails { get; set; }
        public it_trans_a DolphinTrans { get; set; }
        public TransactionItemView[] DeliveryDevice { get; set; }
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
    [Bind(Include = "TransactionId,TransactionTypeId,cmp_seq,POS_ps_code,Warehouse_wa_code,DeliveryRequestId,Customer_aux_id,CustomerName,DolphinCustomerName,CustomerAccountName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,SaleTransactionTypeId,TransactionDateTime,TransactionStatusId,TransactionReference,DolphinTrans_tra_ref_id,DolphinTrans_tra_ref_type,SystemId,WithInstallationService,Note,IsBlock,CreateUserId,CreateDate,TransactionDetails,TransactionDevice")]
    public class DeliveryReturnEditBindModel : Transaction
    {
    }
    public class DeliveryReturnEditModel
    {
        public Transaction EditItem { get; set; }
       
    }
}