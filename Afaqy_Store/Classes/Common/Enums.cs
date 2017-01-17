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
            Export=5
        }

        
    }

    public class DBEnums
    {
        public enum DeviceStatus
        {
            New = 1,
            In_store = 2,
            Configured_Updated = 3,
            Connted_with_SIM_card = 4,
            Transfered_to_branch = 5,
            With_technical = 6,
            Connected_with_server = 7,
            Disconnected_with_server = 8,
            In_maintenance = 9,
            Missing = 10,
            Damage = 11
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
    }
}