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
    
    public partial class TransactionDetailsView
    {
        public int TransactionDetailsId { get; set; }
        public int TransactionId { get; set; }
        public decimal ModelType_ia_item_id { get; set; }
        public string ia_name { get; set; }
        public string ia_shname { get; set; }
        public string ia_altname { get; set; }
        public string ia_shaltname { get; set; }
        public string fa_name { get; set; }
        public string fa_shname { get; set; }
        public string fa_altname { get; set; }
        public string fa_shaltname { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public bool IsBlock { get; set; }
        public string fa_code { get; set; }
        public Nullable<decimal> DolphinTransB_trb_serial_id { get; set; }
        public Nullable<int> ReferenceTransactionDetailsId { get; set; }
    }
}