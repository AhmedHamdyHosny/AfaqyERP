using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Common
{
    public class Enums
    {
        public enum ModuleType
        {
            None = 0,
            N12 = 1,
            TM2 = 2,
            TM2_FM2 = 3,
            GH = 4,
            AT1000 = 5,
            TM2_FM42 = 6,
            FM11 = 7,
            FM53 = 8,
            TM2_FM42_Spec_101 = 9,
            FM55 = 10,
            FM12 = 11,
            FM33 = 12,
            FM34 = 13,
            FM10 = 14,
            FM36 = 20,
            FM09 = 21,
            FM63 = 22
        }

        public enum Status
        {
            Failed = 0,
            Completed = 1,
            SmsSent = 2,
            SmsDelivered = 3,
            DeviceConnectedToServer = 4,
            FirmwareIsSending = 5,
            Updating = 6,
            FailedDownload = 7,
            FailedFlashing = 8,
            FailedConnectionTimeout = 9,
            FailedLicensing = 10,
            FailedConnectionLost = 11,
            Cancelled = 12,
            Pending = 13,
            ConfigurationSending = 14,
            Configuring = 15,
            None = 127
        }

        public enum Languages
        {
            en=1,
            ar=2
        }

        public enum AlertMessageType
        {
            Success=1,
            Error=2,
            Warning=3,
            info=4
        }

        public enum Transactions
        {
            Create=1,
            Edit=2,
            Delete=3,
            Import=4,
            Export=5,
            Deactive = 6,
            Active = 7
        }

        public class PopupWindowClass
        {
            public const string Meduim_Model = "meduim-Modal";
            public const string Large_Model = "large-Modal";
        }
        
    }

    public class DBEnums
    {
        //public enum UserType
        //{
        //    System_Adminstrator = 1
        //        ,
        //    Technicians_Supervisior = 2,
        //    Warehouse_Supervisior = 3,
        //    Sales_Manager = 4,
        //    Server_Managment_Manager = 5,
        //    Sales_Man = 6
        //}

        public enum Department
        {
            Management = 1,
            Accounting = 2,
            Sales = 3,
            Technical = 4,
            Technical_Support = 5,
            Server_Management = 6,
            Software_Developement = 7
        }

        public enum DeviceStatus
        {
            New = 1,
            In_store = 2,
            Configured_Updated = 3,
            Connted_with_SIM_card = 4,
            Transfered_to_branch = 5,
            In_customer_delivery_phase = 6,
            Spare_to_technician = 7,
            Connected_with_server = 8,
            Disconnected_with_server = 9,
            Disconnected_customer_service = 10,
            Damage = 11,
            In_maintenance = 12,
            Missing = 13

        }

        public enum SIMCardStatus
        {
            New = 1,
            Linked_with_device = 2,
            Spare_to_technical = 3,
            Blanked_to_client = 4,
            Connected_to_server = 5,
            Disconnected_to_server = 6,
            Damag = 7,
            Replacement = 8,
            Missing = 9
        }

        public class SystemServerIp
        {
            public static string AfaqyInfo = "212.70.49.19";
            public static string AfaqyMe = "212.70.49.122";
            public static string AfaqyXYZ = "212.70.49.27";
            public static string Sfac_AfaqyIn = "212.70.49.106";
            public static string AfaqyIn_Old = "212.70.49.114";
            public static string AfaqyIn_New = "212.70.49.108";
            public static string A_AfaqyNet_101 = "212.70.49.101";
            public static string AfaqyNet_20 = "212.70.49.20";
            public static string Waste_AfaqyNet_104 = "212.70.49.104";
            public static string Tatweer_AfaqyNet_119 = "212.70.49.119";

        }

        public enum SystemServer
        {
            AfaqyInfo = 7,
            AfaqyMe = 8
        }

        public enum CustomerServerStatus
        {
            New = 1,
            Working = 2,
            Disable = 3,
            Blocked = 4
        }

        public enum DeliveryRequestStatus
        {
            New = 1,
            Approved = 2,
            Reject = 3,
            Technician_Assignation = 4,
            Technician_Assignation_With_Delay = 5,
            Store_Notified = 6,
            In_the_delivery_phase = 7
        }

        public enum DeliveryStatus
        {
            New = 1,
            Delivery_To_Technician = 2,
            Under_Implementation = 3,
            Delivery_To_Customer = 4
        }

        public enum SaleTransactionType
        {
            Sales = 1,
            Demo = 2
        }

        public enum CustomerStatus
        {
            New = 1,
            Working = 2,
            Disable = 3,
            Blocked = 4
        }

        public enum JobTitle
        {
            Server_Management_Engineer = 1,
            Server_Managment_Manager = 2,
            Storekeeper = 3,
            Sales_Man = 4,
            Branch_Sales_Manager = 5,
            Technician = 6,
            Technicians_General_Manager = 8,
            Branch_Technicians_Manager = 9
        }

        public enum NotificationType
        {
            DeliveryRequest_AssignNotification = 1,
            DeliveryRequest_DeliveryNotification = 2,
            DeliveryRequest_AcceptNotification = 3,
            DeliveryRequest_RejectNotification = 4,
            DeliveryRequest_ApproveNotification = 5,
            DeliveryRequest_StoreReceviedNotification = 6,
            DeliveryRequest_InDeliveryPhaseNotification = 7,
            DeliveryNote_AddedNotification = 8,
            DeliveryNote_ServerReceviedNotification = 9,
            DeliveryNote_ServerAddedNotification = 10,
            DeliveryNote_ServerNamedNotification = 11
        }
        
        public enum UDDeliveryTechnician_DT_ColumnsName
        {
            DeliveryNoteTechnicalId, cmp_seq, DeliveryNoteId, Employee_aux_id, IsBlock, CreateUserId, CreateDate, ModifyUserId, ModifyDate
        }

        public enum UDDeliveryNote_DT_ColumnsName
        {
            DeliveryNoteId, cmp_seq, POS_ps_code, Warehouse_wa_code, DeliveryRequestId, Customer_aux_id, CustomerName, CustomerContact_serial, AlternativeContactName, AlternativeContactTelephone, SaleTransactionTypeId, DeliveryDateTime, DeliveryStatusId, DeliveryNoteReference, DolphinDelivery_tra_ref_id, DolphinDelivery_tra_ref_type, SystemId, WithInstallationService, Note, IsBlock, CreateUserId, CreateDate, ModifyUserId, ModifyDate
        }

        public enum UDDeliveryDetails_DT_ColumnsName
        {
            DeliveryDetailsId, cmp_seq, DeliveryId,ModelType_ia_item_id,Quantity,Note,IsBlock,CreateUserId,CreateDate,ModifyUserId,ModifyDate
        }

        public enum UDDeliveryDevice_DT_ColumnsName
        {
            DeliveryItemId, cmp_seq, DeliveryDetailsId,DeviceId, SerialNumber, IMEI, ModelType_ia_item_id, Employee_aux_id,InstallingDateTime, DeviceNaming, DeviceNamingTypeId, AddToServer, TrackWithTechnician, ServerUpdated, Note,IsBlock,CreateUserId,CreateDate,ModifyUserId,ModifyDate
        }

        //public enum UDNotification_DT_ColumnsName
        //{
        //    NotificationId,NotificationTitle,NotificationContent,StyleClass,ToUserId,CreateDate,IsRead,NotificationTypeId,ReferenceId,ReferenceLink
        //}

    }
}