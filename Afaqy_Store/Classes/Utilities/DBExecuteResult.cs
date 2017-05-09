using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class DBExecuteResult
    {
        public enum ExecuteResult
        {
            Success = 1,
            Fail = 2
        }

        public ExecuteResult Result { get; set; }
        public string Message { get; set; }
    }
    public class DBExecuteResult<TResultModel> : DBExecuteResult
    {
        
        public TResultModel ResultModel { get; set; }

    }


}