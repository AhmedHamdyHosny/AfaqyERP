using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class AfaqyTacker
    {
        public List<ServerUnit> AfaqyTackers { get; set; }
    }

    public class Afaqy_Info_Me
    {
        public partial class AfaqyAccount
        {
            public string ssid { get; set; }
            public string eid { get; set; }
        }
        public class AfaqySearchUnit
        {
            public class SearchSpec
            {
                public string itemsType { get; set; }
                public string propName { get; set; }
                public string propValueMask { get; set; }
                public string sortType { get; set; }
            }

            public class Item
            {
                public class Pos
                {
                    public string t { get; set; }
                    public string f { get; set; }
                    public string y { get; set; }
                    public string x { get; set; }
                    public string z { get; set; }
                    public string s { get; set; }
                    public string c { get; set; }
                    public string sc { get; set; }
                }

                public string nm { get; set; }
                public string cls { get; set; }
                public string id { get; set; }
                public string crt { get; set; }
                public string bact { get; set; }
                public string uid { get; set; }
                public string hw { get; set; }
                public string ph { get; set; }
                public string psw { get; set; }
                public Pos pos { get; set; }

                public ServerUnit ToUnit()
                {
                    ServerUnit unit = null;

                    if (this != null)
                    {
                        unit = new ServerUnit();
                        unit.gsm_number = this.ph;
                        //unit.sim_serial = 
                        unit.device_imei = this.uid;
                        //unit.device_serial = 
                        //unit.device_status =
                        //unit.device_deleted = 
                        unit.device_name_at_Server = this.nm;
                        unit.lastConnection = this.pos != null ? this.pos.t : null;
                        //unit.server_ip_address = 
                        unit.client_id = this.bact;
                        //unit.client_name = 
                        //unit.client_status = 

                    }

                    return unit;
                }
            }

            public SearchSpec searchSpec { get; set; }
            public int dataFlags { get; set; }
            public int totalItemsCount { get; set; }
            public int indexFrom { get; set; }
            public int indexTo { get; set; }
            public List<Item> items { get; set; }
        }
    }
    public class AfaqyInfo : Afaqy_Info_Me
    {
        //public class AfaqyInfoAccount
        //{
        //    public string ssid { get; set; }
        //}
        public new class AfaqySearchUnit : Afaqy_Info_Me.AfaqySearchUnit
        {
            //public class SearchSpec
            //{
            //    public string itemsType { get; set; }
            //    public string propName { get; set; }
            //    public string propValueMask { get; set; }
            //    public string sortType { get; set; }
            //}

            public new class Item : Afaqy_Info_Me.AfaqySearchUnit.Item
            {
                public new class Pos :  Afaqy_Info_Me.AfaqySearchUnit.Item
                {
                    public class P
                    {
                        public string gsm { get; set; }
                        public string pwr_ext { get; set; }
                        public string param68 { get; set; }
                        public string pwr_int { get; set; }
                        public string battery_charge { get; set; }
                        public string hdop { get; set; }
                        public string odo { get; set; }
                        public string adc1 { get; set; }
                        public string adc11 { get; set; }
                        public string adc2 { get; set; }
                        public string adc12 { get; set; }
                        public string adc3 { get; set; }
                        public string adc13 { get; set; }
                        public string adc4 { get; set; }
                        public string adc14 { get; set; }
                        public string adc5 { get; set; }
                        public string adc15 { get; set; }
                        public string adc6 { get; set; }
                        public string adc16 { get; set; }
                        public string adc7 { get; set; }
                        public string adc17 { get; set; }
                        public string adc8 { get; set; }
                        public string adc18 { get; set; }
                        public string mcc { get; set; }
                        public string mnc { get; set; }
                        public string lac { get; set; }
                        public string cell_id { get; set; }
                        public string ta { get; set; }
                        public string gsm_lvl { get; set; }
                        public string dallas_id_end { get; set; }
                    }

                    //public string t { get; set; }
                    //public string f { get; set; }
                    public string tp { get; set; }
                    public string l { get; set; }
                    //public string y { get; set; }
                    //public string x { get; set; }
                    //public string z { get; set; }
                    //public string s { get; set; }
                    //public string c { get; set; }
                    //public string sc { get; set; }
                    public string y2 { get; set; }
                    public string x2 { get; set; }
                    public string i { get; set; }
                    public P p { get; set; }
                    public string dr { get; set; }
                }

                //public string nm { get; set; }
                //public string cls { get; set; }
                //public string id { get; set; }
                //public string crt { get; set; }
                //public string bact { get; set; }
                //public string uid { get; set; }
                //public string hw { get; set; }
                //public string ph { get; set; }
                //public string psw { get; set; }
                public new Pos pos { get; set; }
                //public string lmsg { get; set; } sometime equal to string like "lmsg":"dup" and sometime equal to array "lmsg":{"t":1431376086,"f":0,"tp":"ud","p":{"f0":160}}

                //public Unit ToUnit()
                //{
                //    Unit unit = null;

                //    if (this != null)
                //    {
                //        unit = new Unit();
                //        unit.gsm_number = this.ph;
                //        //unit.sim_serial = 
                //        unit.device_imei = this.uid;
                //        //unit.device_serial = 
                //        //unit.device_status =
                //        //unit.device_deleted = 
                //        unit.device_name_at_Server = this.nm;
                //        unit.lastConnection = this.pos != null ? this.pos.t : null;
                //        //unit.server_ip_address = 
                //        unit.client_id = this.bact;
                //        //unit.client_name = 
                //        //unit.client_status = 

                //    }

                //    return unit;
                //}
            }

            //public SearchSpec searchSpec { get; set; }
            //public int dataFlags { get; set; }
            //public int totalItemsCount { get; set; }
            //public int indexFrom { get; set; }
            //public int indexTo { get; set; }
            public new List<Item> items { get; set; }
        }
    }

    public class AfaqyMe : Afaqy_Info_Me
    {
        //public class AfaqyAccount
        //{
        //    public string eid { get; set; }
        //}
        
        public new class AfaqySearchUnit : Afaqy_Info_Me.AfaqySearchUnit
        {
            public new class SearchSpec : Afaqy_Info_Me.AfaqySearchUnit.SearchSpec
            {
                //public string itemsType { get; set; }
                //public string propName { get; set; }
                //public string propValueMask { get; set; }
                //public string sortType { get; set; }
                public string propType { get; set; }
                public string or_logic { get; set; }
            }

            public new class Item : Afaqy_Info_Me.AfaqySearchUnit.Item
            {
                public new class Pos : Afaqy_Info_Me.AfaqySearchUnit.Item.Pos
                {
                    //public string t { get; set; }
                    //public string f { get; set; }
                    public string lc { get; set; }
                    //public string y { get; set; }
                    //public string x { get; set; }
                    //public string z { get; set; }
                    //public string s { get; set; }
                    //public string c { get; set; }
                    //public string sc { get; set; }
                }

                //public string nm { get; set; }
                //public string cls { get; set; }
                //public string id { get; set; }
                //public string crt { get; set; }
                //public string bact { get; set; }
                public string mu { get; set; }
                //public string uid { get; set; }
                public string uid2 { get; set; }
                //public string hw { get; set; }
                //public string ph { get; set; }
                public string ph2 { get; set; }
                //public string psw { get; set; }
                public new Pos pos { get; set; }
                //public Lmsg lmsg { get; set; }
                public string uacl { get; set; }

                
            }

            //public class Pos2
            //{
            //    public string y { get; set; }
            //    public string x { get; set; }
            //    public string z { get; set; }
            //    public string s { get; set; }
            //    public string c { get; set; }
            //    public string sc { get; set; }
            //}

            //public class P
            //{
            //    public string param180 { get; set; }
            //    public string param240 { get; set; }
            //    public string gsm { get; set; }
            //    public string param239 { get; set; }
            //    public string param199 { get; set; }
            //    public string param72 { get; set; }
            //    public string battery_charge { get; set; }
            //}

            //public class Lmsg
            //{
            //    public int t { get; set; }
            //    public int f { get; set; }
            //    public string tp { get; set; }
            //    public Pos2 pos { get; set; }
            //    public int i { get; set; }
            //    public int lc { get; set; }
            //    public P p { get; set; }
            //}

            public new SearchSpec searchSpec { get; set; }
            //public int dataFlags { get; set; }
            //public int totalItemsCount { get; set; }
            //public int indexFrom { get; set; }
            //public int indexTo { get; set; }
            public new List<Item> items { get; set; }
        }

    }
    
    public class ServerUnit
    {
        public string gsm_number { get; set; }
        public string sim_serial { get; set; }
        public string device_imei { get; set; }
        public string device_serial { get; set; }
        public string device_status { get; set; }
        public string device_deleted { get; set; }
        public string device_name_at_Server { get; set; }
        public string lastConnection { get; set; }
        public string server_ip_address { get; set; }
        public string client_id { get; set; }
        public string client_name { get; set; }
        public string client_status { get; set; }

        
    }

    public class ServerCustomer
    {
        public string client_id { get; set; }
        public string client_name { get; set; }
        public string client_status { get; set; }
        public List<ServerUnit> Units { get; set; }
    }
}