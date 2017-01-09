using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Classes.Common.Enums;

namespace Classes.Utilities
{
    public class Utility
    {
        
    }

    public class AlertMessage
    {

        public AlertMessageType MessageType { get; set; }
        private string _MessageContent;
        public string MessageContent
        {
            get
            {
                if(_MessageContent == null)
                {
                    _MessageContent = GetAlertMessage();
                }

                return _MessageContent;
            }
            set
            {
                _MessageContent = value;
            }
        }
        public int? TransactionCount { get; set; }
        public Transactions Transaction { get; set; }


        internal string GetAlertMessage()
        {
            var message = "";
            switch (this.Transaction)
            {
                case Transactions.Create:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Edit:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Delete:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Import:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Export:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return message;
        }
    }
}