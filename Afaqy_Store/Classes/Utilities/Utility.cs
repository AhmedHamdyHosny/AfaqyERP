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
        public string MessageContent { get; set; }

        
    }
}