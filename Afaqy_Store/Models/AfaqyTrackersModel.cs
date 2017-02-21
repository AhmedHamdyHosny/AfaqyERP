using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class AfaqyTacker
    {
        public List<CustServerUnit> AfaqyTackers { get; set; }
    }

    public abstract class Afaqy_Info_Me
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
                //public Dictionary<string, CustomField> flds { get; set; }
                private bool _isCorrect = true;
                public bool IsCorrect
                {
                    get
                    {
                        return _isCorrect;
                    }
                    set
                    {
                        _isCorrect = value;
                    }
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
                public new class Pos :  Afaqy_Info_Me.AfaqySearchUnit.Item.Pos
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

                public class CustomField 
                {
                    public int id { get; set; }
                    public string nm { get; set; }
                    public string vl { get; set; }
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
                public Dictionary<string, CustomField> flds { get; set; }
                public CustServerUnit ToUnit()
                {
                    CustServerUnit unit = null;
                    if (this != null)
                    {
                        unit = new CustServerUnit();
                        unit.gsm_number = this.ph;
                        //unit.sim_serial = GetSIMSerial(unit.gsm_number,this.flds);
                        unit.device_imei = this.uid;
                        //unit.device_serial = GetDeviceSerial(unit.device_imei, this.flds);
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

                public DataLayer.ServerUnit ToServerUnit()
                {
                    DataLayer.ServerUnit unit = null;

                    if (this != null)
                    {
                        unit = new DataLayer.ServerUnit();
                        unit.GSMNumber = this.ph;
                        //unit.sim_serial = GetSIMSerial(unit.gsm_number,this.flds);
                        unit.DeviceIMEI = this.uid;
                        //unit.device_serial = GetDeviceSerial(unit.device_imei, this.flds);
                        //unit.device_status =
                        //unit.device_deleted = 
                        unit.DeviceServerName = this.nm;
                        unit.LastConnection = this.pos != null ? Classes.Utilities.Utility.ParseDateTime(this.pos.t) : (DateTime?)null;
                        //unit.server_ip_address = 
                        unit.ClientId = int.Parse(this.bact);
                        //unit.client_name = 
                        //unit.client_status = 

                    }
                    return unit;
                }
                
                public virtual string GetSIMSerial(string gsm_number, Dictionary<string, CustomField> fields)
                {
                    string simSerial = null;
                    if(gsm_number !=null)
                    {
                        var filters = new List<GenericApiController.Utilities.GenericDataFormat.FilterItems>();
                        filters.Add(new GenericApiController.Utilities.GenericDataFormat.FilterItems() { Property = "GSM", Operation = GenericApiController.Utilities.GenericDataFormat.FilterOperations.Equal, Value=gsm_number });
                        var orders = new List<GenericApiController.Utilities.GenericDataFormat.SortItems>();
                        orders.Add(new GenericApiController.Utilities.GenericDataFormat.SortItems() { Property = "SIMCardId", SortType = GenericApiController.Utilities.GenericDataFormat.SortType.Desc });
                        var request = new GenericApiController.Utilities.GenericDataFormat() { Filters = filters, Sorts = orders};
                        var SIMs = new SIMCardModel<DataLayer.SIMCard>().Get(request);
                        if(SIMs != null && SIMs.Count > 0)
                        {
                            simSerial = SIMs[0].SerialNumber;
                        }
                    }
                    //if (fields != null)
                    //{
                    //    var sim_field = flds.Where(x => x.Value.nm.ToLower().Contains("sim")
                    //        || x.Value.vl.StartsWith("89966")
                    //        || x.Value.vl.StartsWith("4211")).SingleOrDefault();

                    //    if (sim_field.Value != null)
                    //    {
                    //        simSerial = sim_field.Value.vl;
                    //    }
                    //}
                    return simSerial;
                }

                private string GetDeviceSerial(string device_imei, Dictionary<string, CustomField> flds)
                {
                    string deviceSerial = null;
                    if(device_imei != null)
                    {
                        var filters = new List<GenericApiController.Utilities.GenericDataFormat.FilterItems>();
                        filters.Add(new GenericApiController.Utilities.GenericDataFormat.FilterItems() { Property = "IMEI", Operation = GenericApiController.Utilities.GenericDataFormat.FilterOperations.Equal, Value = device_imei });
                        var request = new GenericApiController.Utilities.GenericDataFormat() { Filters = filters, };
                        var devices = new DeviceModel<DataLayer.Device>().Get(request);
                        if (devices != null && devices.Count == 1)
                        {
                            deviceSerial = devices[0].SerialNumber;
                        }
                    }
                    return deviceSerial;
                }

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
                public class CustomField 
                {
                    public int id { get; set; }
                    public string n { get; set; }
                    public string v { get; set; }
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
                public Dictionary<string, CustomField> flds { get; set; }
                //public Lmsg lmsg { get; set; }
                public string uacl { get; set; }

                public CustServerUnit ToUnit()
                {
                    CustServerUnit unit = null;

                    if (this != null)
                    {
                        unit = new CustServerUnit();
                        unit.gsm_number = this.ph;
                        //unit.sim_serial = GetSIMSerial(this.flds);
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

                public DataLayer.ServerUnit ToServerUnit()
                {
                    DataLayer.ServerUnit unit = null;

                    if (this != null)
                    {
                        unit = new DataLayer.ServerUnit();
                        unit.GSMNumber = this.ph;
                        //unit.sim_serial = GetSIMSerial(unit.gsm_number,this.flds);
                        unit.DeviceIMEI = this.uid;
                        //unit.device_serial = GetDeviceSerial(unit.device_imei, this.flds);
                        //unit.device_status =
                        //unit.device_deleted = 
                        unit.DeviceServerName = this.nm;
                        unit.LastConnection = this.pos != null ? Classes.Utilities.Utility.ParseDateTime(this.pos.t) : (DateTime?)null;
                        //unit.server_ip_address = 
                        unit.ClientId = int.Parse(this.bact);
                        //unit.client_name = 
                        //unit.client_status = 

                    }
                    return unit;
                }

                public virtual string GetSIMSerial(Dictionary<string, CustomField> fields)
                {
                    string simSerial = null;
                    if (fields != null)
                    {
                        var sim_field = flds.Where(x => x.Value.n.ToLower().Contains("sim")
                        || x.Value.v.StartsWith("89966")
                        || x.Value.v.StartsWith("4211")).SingleOrDefault();

                        if (sim_field.Value != null)
                        {
                            simSerial = sim_field.Value.v;
                        }
                    }
                    return simSerial;
                }
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
    
    public class CustServerUnit
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

        internal DataLayer.ServerUnit ToServerUnit()
        {
            return new DataLayer.ServerUnit()
            {
                DeviceIMEI = this.device_imei,
                DeviceSerial = this.device_serial,
                SIMSerial = this.sim_serial,
                GSMNumber = this.gsm_number,
                DeviceStatus = string.IsNullOrEmpty(this.device_status) ? (int?)null : int.Parse(this.device_status),
                IsDeleted = string.IsNullOrEmpty(this.device_deleted) ? (bool?)null : Classes.Utilities.Utility.ParseBool(this.device_deleted),
                DeviceServerName = this.device_name_at_Server,
                LastConnection = Classes.Utilities.Utility.ParseDateTime(this.lastConnection),
                ServerIP = this.server_ip_address,
                ClientId = int.Parse(this.client_id),
                ClientName = this.client_name,
                ClientStatus = string.IsNullOrEmpty(this.client_status) ? (int?)null : int.Parse(this.client_status)
            };
        }
    }

    public class ServerCustomer
    {
        public string client_id { get; set; }
        public string client_name { get; set; }
        public string client_status { get; set; }
        public List<CustServerUnit> Units { get; set; }
    }
}