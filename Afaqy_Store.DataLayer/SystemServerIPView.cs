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
    
    public partial class SystemServerIPView
    {
        public int SystemServerId { get; set; }
        public string ServerIP { get; set; }
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public int CompanyId { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUser_FirstName_en { get; set; }
        public string CreateUser_FirstName_ar { get; set; }
        public string CreateUser_LastName_en { get; set; }
        public string CreateUser_LastName_ar { get; set; }
        public string CreateUser_FullName_en { get; set; }
        public string CreateUser_FullName_ar { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUser_FirstName_en { get; set; }
        public string ModifyUser_FirstName_ar { get; set; }
        public string ModifyUser_LastName_en { get; set; }
        public string ModifyUser_LastName_ar { get; set; }
        public string ModifyUser_FullName_en { get; set; }
        public string ModifyUser_FullName_ar { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
