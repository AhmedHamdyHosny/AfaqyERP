using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class JsonResponse 
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}