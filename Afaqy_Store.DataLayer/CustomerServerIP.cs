//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerServerIP
    {
        public int CustomerServerId { get; set; }
        public int CustomerId { get; set; }
        public int SystemServerId { get; set; }
        public string AccountUserName { get; set; }
        public string AccountPassword { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual SystemServerIP SystemServerIP { get; set; }
    }
}
