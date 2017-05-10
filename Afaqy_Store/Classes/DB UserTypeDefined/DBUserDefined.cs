using Classes.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Afaqy_Store.Models;
using Afaqy_Store.DataLayer;

namespace Classes.DB_UserTypeDefined
{

    public class UDDeliveryTechnician
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryTechnician()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.TransactionTechnicianId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.TransactionId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.Employee_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionTechnician_DT_ColumnsName.ModifyDate.ToString());
        }

        internal DataRow BindDataRow(TransactionTechnician technician, string SqlServerDateTimeFormat)
        {
            DataRow row = this.dataTable.NewRow();
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.TransactionTechnicianId.ToString()] = technician.TransactionTechnicianId;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.cmp_seq.ToString()] = technician.cmp_seq;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.Employee_aux_id.ToString()] = technician.Employee_aux_id;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.IsBlock.ToString()] = technician.IsBlock;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.CreateUserId.ToString()] = technician.CreateUserId;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.CreateDate.ToString()] = technician.CreateDate.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.ModifyUserId.ToString()] = technician.ModifyUserId;
            row[DBEnums.UDTransactionTechnician_DT_ColumnsName.ModifyDate.ToString()] = technician.ModifyDate != null ? ((DateTime)technician.ModifyDate).ToString(SqlServerDateTimeFormat) : null;

            return row;
        }
    }

    public class UDTransaction
    {
        public DataTable dataTable { get; set; }
        public UDTransaction()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.TransactionId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.POS_ps_code.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.Warehouse_wa_code.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.DeliveryRequestId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.Customer_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.CustomerName.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.DolphinCustomerName.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.CustomerAccountName.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.CustomerContact_serial.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactName.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactTelephone.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.SaleTransactionTypeId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.TransactionDateTime.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.TransactionStatusId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.TransactionReference.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_type.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.SystemId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.WithInstallationService.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.ReferenceTransactionId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransaction_DT_ColumnsName.ModifyDate.ToString());
        }

        internal DataRow BindDataRow(DeliveryNoteCreateBindModel value, string SqlServerDateTimeFormat)
        {
            var row = this.dataTable.NewRow();
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionId.ToString()] = value.TransactionId;
            row[DBEnums.UDTransaction_DT_ColumnsName.cmp_seq.ToString()] = value.cmp_seq;
            row[DBEnums.UDTransaction_DT_ColumnsName.POS_ps_code.ToString()] = value.POS_ps_code;
            row[DBEnums.UDTransaction_DT_ColumnsName.Warehouse_wa_code.ToString()] = value.Warehouse_wa_code;
            row[DBEnums.UDTransaction_DT_ColumnsName.DeliveryRequestId.ToString()] = value.DeliveryRequestId;
            row[DBEnums.UDTransaction_DT_ColumnsName.Customer_aux_id.ToString()] = value.Customer_aux_id;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerName.ToString()] = value.CustomerName;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinCustomerName.ToString()] = value.DolphinCustomerName;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerAccountName.ToString()] = value.CustomerAccountName;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerContact_serial.ToString()] = value.CustomerContact_serial;
            row[DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactName.ToString()] = value.AlternativeContactName;
            row[DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactTelephone.ToString()] = value.AlternativeContactTelephone;
            row[DBEnums.UDTransaction_DT_ColumnsName.SaleTransactionTypeId.ToString()] = value.SaleTransactionTypeId;
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionDateTime.ToString()] = value.TransactionDateTime.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionStatusId.ToString()] = value.TransactionStatusId;
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionReference.ToString()] = value.TransactionReference;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_id.ToString()] = value.DolphinTrans_tra_ref_id;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_type.ToString()] = value.DolphinTrans_tra_ref_type;
            row[DBEnums.UDTransaction_DT_ColumnsName.SystemId.ToString()] = value.SystemId;
            row[DBEnums.UDTransaction_DT_ColumnsName.WithInstallationService.ToString()] = value.WithInstallationService;
            row[DBEnums.UDTransaction_DT_ColumnsName.ReferenceTransactionId.ToString()] = value.ReferenceTransactionId;
            row[DBEnums.UDTransaction_DT_ColumnsName.Note.ToString()] = value.Note;
            row[DBEnums.UDTransaction_DT_ColumnsName.IsBlock.ToString()] = value.IsBlock;
            row[DBEnums.UDTransaction_DT_ColumnsName.CreateUserId.ToString()] = value.CreateUserId;
            row[DBEnums.UDTransaction_DT_ColumnsName.CreateDate.ToString()] = value.CreateDate.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransaction_DT_ColumnsName.ModifyUserId.ToString()] = value.ModifyUserId;
            row[DBEnums.UDTransaction_DT_ColumnsName.ModifyDate.ToString()] = value.ModifyDate != null ? ((DateTime)value.ModifyDate).ToString(SqlServerDateTimeFormat) : null;

            return row;
        }

        internal DataRow BindDataRow(DeliveryReturnCreateBindModel value, string SqlServerDateTimeFormat)
        {
            var row = this.dataTable.NewRow();
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionId.ToString()] = value.TransactionId;
            row[DBEnums.UDTransaction_DT_ColumnsName.cmp_seq.ToString()] = value.cmp_seq;
            row[DBEnums.UDTransaction_DT_ColumnsName.POS_ps_code.ToString()] = value.POS_ps_code;
            row[DBEnums.UDTransaction_DT_ColumnsName.Warehouse_wa_code.ToString()] = value.Warehouse_wa_code;
            row[DBEnums.UDTransaction_DT_ColumnsName.DeliveryRequestId.ToString()] = value.DeliveryRequestId;
            row[DBEnums.UDTransaction_DT_ColumnsName.Customer_aux_id.ToString()] = value.Customer_aux_id;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerName.ToString()] = value.CustomerName;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinCustomerName.ToString()] = value.DolphinCustomerName;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerAccountName.ToString()] = value.CustomerAccountName;
            row[DBEnums.UDTransaction_DT_ColumnsName.CustomerContact_serial.ToString()] = value.CustomerContact_serial;
            row[DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactName.ToString()] = value.AlternativeContactName;
            row[DBEnums.UDTransaction_DT_ColumnsName.AlternativeContactTelephone.ToString()] = value.AlternativeContactTelephone;
            row[DBEnums.UDTransaction_DT_ColumnsName.SaleTransactionTypeId.ToString()] = value.SaleTransactionTypeId;
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionDateTime.ToString()] = value.TransactionDateTime.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionStatusId.ToString()] = value.TransactionStatusId;
            row[DBEnums.UDTransaction_DT_ColumnsName.TransactionReference.ToString()] = value.TransactionReference;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_id.ToString()] = value.DolphinTrans_tra_ref_id;
            row[DBEnums.UDTransaction_DT_ColumnsName.DolphinTrans_tra_ref_type.ToString()] = value.DolphinTrans_tra_ref_type;
            row[DBEnums.UDTransaction_DT_ColumnsName.SystemId.ToString()] = value.SystemId;
            row[DBEnums.UDTransaction_DT_ColumnsName.WithInstallationService.ToString()] = value.WithInstallationService;
            row[DBEnums.UDTransaction_DT_ColumnsName.ReferenceTransactionId.ToString()] = value.ReferenceTransactionId;
            row[DBEnums.UDTransaction_DT_ColumnsName.Note.ToString()] = value.Note;
            row[DBEnums.UDTransaction_DT_ColumnsName.IsBlock.ToString()] = value.IsBlock;
            row[DBEnums.UDTransaction_DT_ColumnsName.CreateUserId.ToString()] = value.CreateUserId;
            row[DBEnums.UDTransaction_DT_ColumnsName.CreateDate.ToString()] = value.CreateDate.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransaction_DT_ColumnsName.ModifyUserId.ToString()] = value.ModifyUserId;
            row[DBEnums.UDTransaction_DT_ColumnsName.ModifyDate.ToString()] = value.ModifyDate != null ? ((DateTime)value.ModifyDate).ToString(SqlServerDateTimeFormat) : null;

            return row;
        }
    }

    public class UDDeliveryDetails
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryDetails()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.TransactionDetailsId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.TransactionId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.ModelType_ia_item_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.Quantity.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.ReferenceTransactionDetailsId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionDetails_DT_ColumnsName.ModifyDate.ToString());
        }

        internal DataRow BindDataRow(TransactionDetails details, string SqlServerDateTimeFormat, int detialsRowSerial)
        {
            DataRow row = this.dataTable.NewRow();
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.TransactionDetailsId.ToString()] = detialsRowSerial;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.cmp_seq.ToString()] = details.cmp_seq;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.ModelType_ia_item_id.ToString()] = details.ModelType_ia_item_id;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.Quantity.ToString()] = details.Quantity;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.ReferenceTransactionDetailsId.ToString()] = details.ReferenceTransactionDetailsId;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.Note.ToString()] = details.Note;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.IsBlock.ToString()] = details.IsBlock;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.CreateUserId.ToString()] = details.CreateUserId;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.CreateDate.ToString()] = details.CreateDate.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.ModifyUserId.ToString()] = details.ModifyUserId;
            row[DBEnums.UDTransactionDetails_DT_ColumnsName.ModifyDate.ToString()] = details.ModifyDate != null ? ((DateTime)details.ModifyDate).ToString(SqlServerDateTimeFormat) : null;

            return row;
        }
    }

    public class UDDeliveryItem
    {
        public DataTable dataTable { get; set; }
        public UDDeliveryItem()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.TransactionItemId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.cmp_seq.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.TransactionDetailsId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.DeviceId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.SerialNumber.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.IMEI.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.ModelType_ia_item_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.Employee_aux_id.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.InstallingDateTime.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNaming_en.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNaming_ar.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNamingTypeId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.AddToServer.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.TrackWithTechnician.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.ServerUpdated.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.TechnicalApproval.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.ServerNamed.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.IsReturn.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.Note.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.IsBlock.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.CreateUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.CreateDate.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.ModifyUserId.ToString());
            dataTable.Columns.Add(DBEnums.UDTransactionItem_DT_ColumnsName.ModifyDate.ToString());
        }

        internal DataRow BindDataRow(TransactionItemView device, int cmp_seq, int CreateUserId, DateTime CreateDate, int? ModifyUserId, DateTime? ModifyDate, string SqlServerDateTimeFormat)
        {
            DataRow row = this.dataTable.NewRow();
            row[DBEnums.UDTransactionItem_DT_ColumnsName.TransactionItemId.ToString()] = device.TransactionItemId;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.cmp_seq.ToString()] = cmp_seq;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.DeviceId.ToString()] = device.DeviceId;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.SerialNumber.ToString()] = device.SerialNumber;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.IMEI.ToString()] = device.IMEI;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.ModelType_ia_item_id.ToString()] = device.ModelType_ia_item_id;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.Employee_aux_id.ToString()] = device.Employee_aux_id;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.InstallingDateTime.ToString()] = device.InstallingDateTime != null ? ((DateTime)device.InstallingDateTime).ToString(SqlServerDateTimeFormat) : null;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNaming_en.ToString()] = device.DeviceNaming_en;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNaming_ar.ToString()] = device.DeviceNaming_ar;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.DeviceNamingTypeId.ToString()] = device.DeviceNamingTypeId;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.AddToServer.ToString()] = device.AddToServer;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.TrackWithTechnician.ToString()] = device.TrackWithTechnician;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.ServerUpdated.ToString()] = device.ServerUpdated;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.TechnicalApproval.ToString()] = device.TechnicalApproval;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.ServerNamed.ToString()] = device.ServerNamed;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.IsReturn.ToString()] = device.IsReturn;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.Note.ToString()] = device.Note;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.IsBlock.ToString()] = device.IsBlock;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.CreateUserId.ToString()] = CreateUserId;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.CreateDate.ToString()] = CreateDate.ToString(SqlServerDateTimeFormat);
            row[DBEnums.UDTransactionItem_DT_ColumnsName.ModifyUserId.ToString()] = ModifyUserId;
            row[DBEnums.UDTransactionItem_DT_ColumnsName.ModifyDate.ToString()] = ModifyDate != null ? ((DateTime)ModifyDate).ToString(SqlServerDateTimeFormat) : null;

            return row;
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