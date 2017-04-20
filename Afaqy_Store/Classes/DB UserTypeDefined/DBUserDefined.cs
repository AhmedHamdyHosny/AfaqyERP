using Classes.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Classes.DB_UserTypeDefined
{

    public class UDDeliveryTechnician
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryTechnician()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.DeliveryNoteTechnicalId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.DeliveryNoteId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.Employee_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryTechnician_DT_ColumnsName.ModifyDate.ToString());
        }
    }

    public class UDDeliveryNote
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryNote()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryNoteId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.POS_ps_code.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.Warehouse_wa_code.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryRequestId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.Customer_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.CustomerName.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.CustomerContact_serial.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.AlternativeContactName.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.AlternativeContactTelephone.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.SaleTransactionTypeId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryDateTime.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryStatusId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DeliveryNoteReference.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DolphinDelivery_tra_ref_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.DolphinDelivery_tra_ref_type.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.SystemId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.WithInstallationService.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryNote_DT_ColumnsName.ModifyDate.ToString());
        }
    }

    public class UDDeliveryDetails
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryDetails()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.DeliveryDetailsId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.DeliveryId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.ModelType_ia_item_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.Quantity.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDetails_DT_ColumnsName.ModifyDate.ToString());
        }
    }

    public class UDDeliveryDevice
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryDevice()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.DeliveryItemId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.DeliveryDetailsId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.DeviceId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.SerialNumber.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.IMEI.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.ModelType_ia_item_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.Employee_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.InstallingDateTime.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.DeviceNaming.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.DeviceNamingTypeId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.AddToServer.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.TrackWithTechnician.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.ServerUpdated.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDDeliveryDevice_DT_ColumnsName.ModifyDate.ToString());
        }
    }

    //public class UDNotification
    //{
    //    public DataTable dataTable { get; set; }
    //    public UDNotification()
    //    {
    //        dataTable = new DataTable();
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.NotificationId.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.NotificationTitle.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.NotificationContent.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.StyleClass.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.ToUserId.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.CreateDate.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.IsRead.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.NotificationTypeId.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.ReferenceId.ToString());
    //        dataTable.Columns.Add(DBEnums.UDNotification_DT_ColumnsName.ReferenceLink.ToString());
    //    }
    //}
}