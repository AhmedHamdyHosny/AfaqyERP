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
    
    public partial class it_trans_a
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public it_trans_a()
        {
            this.it_trans_b = new HashSet<it_trans_b>();
        }
    
        public int tra_cmp_seq { get; set; }
        public decimal tra_ref_id { get; set; }
        public int tra_ref_type { get; set; }
        public string tra_user_id { get; set; }
        public Nullable<int> tra_invt_id { get; set; }
        public Nullable<int> tra_cura_seq { get; set; }
        public Nullable<int> tra_aux_id { get; set; }
        public Nullable<int> tra_sal_aux_id { get; set; }
        public Nullable<decimal> tra_jv_no { get; set; }
        public string tra_ps_code { get; set; }
        public string tra_wa_code { get; set; }
        public Nullable<double> tra_rate { get; set; }
        public Nullable<double> tra_rate_sl { get; set; }
        public Nullable<System.DateTime> tra_date { get; set; }
        public Nullable<double> tra_gross_before_disc { get; set; }
        public Nullable<double> tra_gross { get; set; }
        public Nullable<double> tra_disc { get; set; }
        public Nullable<double> tra_add_disc { get; set; }
        public Nullable<double> tra_vat { get; set; }
        public Nullable<double> tra_net { get; set; }
        public string tra_po_number { get; set; }
        public Nullable<System.DateTime> tra_sup_doc_date { get; set; }
        public byte[] tra_tstamp { get; set; }
        public string tra_bat_code { get; set; }
        public Nullable<decimal> tra_bat_seq { get; set; }
        public string tra_remark { get; set; }
        public string tra_status { get; set; }
        public string tra_wa_code_to { get; set; }
        public Nullable<int> tra_trf_flag { get; set; }
        public string tra_shipment { get; set; }
        public Nullable<System.DateTime> tra_act_date { get; set; }
        public string tra_sup_ref { get; set; }
        public Nullable<decimal> tra_Cancel { get; set; }
        public Nullable<int> tra_flag_exp { get; set; }
        public Nullable<int> tra_consign { get; set; }
        public System.DateTime tra_sysdate { get; set; }
        public Nullable<int> tra_retail_batch { get; set; }
        public Nullable<int> tra_parent { get; set; }
        public Nullable<int> tra_posted { get; set; }
        public string tra_jvdocref { get; set; }
        public Nullable<double> tra_add_chg { get; set; }
        public Nullable<int> tra_vat_chk { get; set; }
        public string tra_albaran { get; set; }
        public string tra_wa_code_till { get; set; }
        public string tra_AddUser { get; set; }
        public Nullable<System.DateTime> tra_AddDate { get; set; }
        public string tra_UpdUser { get; set; }
        public Nullable<System.DateTime> tra_UpdDate { get; set; }
        public Nullable<double> tra_disc2 { get; set; }
        public Nullable<int> tra_acc_cmp_seq { get; set; }
        public string tra_ExtraName { get; set; }
        public Nullable<int> tra_workflow { get; set; }
        public Nullable<decimal> tra_Exp_Id { get; set; }
        public Nullable<decimal> tra_costjvno { get; set; }
        public Nullable<int> tra_pack_flag { get; set; }
        public Nullable<int> tra_revision { get; set; }
        public Nullable<int> tra_trf_flag_PAC { get; set; }
        public string tra_Quotcode { get; set; }
        public Nullable<decimal> tra_inoutjvno { get; set; }
        public Nullable<int> tra_print_flag { get; set; }
        public Nullable<int> tra_FilterCur { get; set; }
        public Nullable<int> tra_rfq { get; set; }
        public string tra_Purcontract { get; set; }
        public string tra_driver_name { get; set; }
        public string tra_truck_name { get; set; }
        public int tra_ttc { get; set; }
        public double tra_add_discttc { get; set; }
        public Nullable<decimal> fk_aca_seq { get; set; }
        public int tra_Purchaseforeign { get; set; }
        public Nullable<decimal> tra_gsn_ref_id { get; set; }
        public int tra_flg_res { get; set; }
        public Nullable<int> fk_serial { get; set; }
        public Nullable<System.DateTime> tra_book_date { get; set; }
        public Nullable<decimal> tra_deduct_amt { get; set; }
        public Nullable<decimal> tra_ret_cash { get; set; }
        public Nullable<int> tra_sister_closed { get; set; }
        public Nullable<System.DateTime> tra_deliv_date { get; set; }
        public Nullable<decimal> fk_tra_ref_id { get; set; }
        public Nullable<decimal> fk_tra_ref_type { get; set; }
        public string tra_assignto_user { get; set; }
        public Nullable<int> fk_fl_step_seq { get; set; }
        public Nullable<decimal> fk_ppa_seq { get; set; }
        public Nullable<int> tra_ReadyPurchCosting { get; set; }
        public Nullable<int> tra_use_internal_trx { get; set; }
        public Nullable<int> tra_cash { get; set; }
        public string tra_PostSalesDocRefInv { get; set; }
        public Nullable<int> tra_only_qty { get; set; }
        public Nullable<int> tra_sup_id { get; set; }
        public Nullable<int> tra_AssInProcess { get; set; }
        public Nullable<int> tra_checked { get; set; }
        public Nullable<decimal> fk_jba_code { get; set; }
        public Nullable<int> fk_cmp_seq_job { get; set; }
        public Nullable<decimal> tra_jv_no_cls { get; set; }
        public Nullable<int> tra_sistercmpseq { get; set; }
        public string tra_sisterdatabase { get; set; }
        public Nullable<int> tra_ready_invoicing { get; set; }
        public Nullable<int> tra_statusWG { get; set; }
        public Nullable<int> fk_cta_seq { get; set; }
        public Nullable<int> tra_user_locked { get; set; }
        public Nullable<System.DateTime> tra_user_lock_time { get; set; }
        public Nullable<decimal> tra_put_id { get; set; }
        public Nullable<double> tra_withHoldTax { get; set; }
        public Nullable<int> fk_dr_seq { get; set; }
        public string tra_user_lock_name { get; set; }
        public Nullable<System.DateTime> tra_date_lostPost { get; set; }
        public Nullable<System.DateTime> tra_date_cancel { get; set; }
        public Nullable<System.DateTime> tra_date_closed { get; set; }
        public Nullable<System.DateTime> tra_date_approve { get; set; }
        public Nullable<byte> tra_flag_invoiced { get; set; }
    
        public virtual im_points im_points { get; set; }
        public virtual im_warehouse im_warehouse { get; set; }
        public virtual im_warehouse im_warehouse1 { get; set; }
        public virtual im_warehouse im_warehouse2 { get; set; }
        public virtual rpauxname rpauxname { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<it_trans_b> it_trans_b { get; set; }
    }
}