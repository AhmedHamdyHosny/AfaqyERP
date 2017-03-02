using Afaqy_Store.DataLayer;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Afaqy_Store.Models
{
    public class ServerUnitModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiServerUnit/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ServerUnitModel() : base(ApiUrl, ApiRoute)
        {
        }
        
        private static string url = null;
        private static MyHttpRequestMessage request = null;
        private static List<CustServerUnit> GetAllUnitsFromServerIn()
        {
            //afaqy.in
            //http://afaqy.in/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json
            //sfac.afaqy.in
            //http://sfac.afaqy.in/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json
            //afaqy.xyz 
            //http://afaqy.xyz/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json

            System.Threading.Tasks.Task<AfaqyTacker> task = null;
            List<CustServerUnit> result = new List<CustServerUnit>();
            //afaqy.in
            string url = SiteConfig.AfaqyIn.AfaqyIn_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            var data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = GetServerIP(x.server_ip_address); return x; }).ToList();
            result.AddRange(data);

            //sfac.afaqy.in
            url = SiteConfig.AfaqyIn.SFAC_AfaqyIn_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.Sfac_AfaqyIn; return x; }).ToList();
            result.AddRange(data);

            //afaqy.xyz 
            url = SiteConfig.AfaqyIn.AfaqyXYZ_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.AfaqyXYZ; return x; }).ToList();
            result.AddRange(data);

            return result;
        }
        private static string GetServerIP(string server_ip_address)
        {
            string serverIp = null;
            switch(server_ip_address)
            {
                case "node1.afaqy.in":
                    serverIp = Classes.Common.DBEnums.SystemServerIp.AfaqyIn_Old;
                    break;
                case "node2.afaqy.in":
                    serverIp = Classes.Common.DBEnums.SystemServerIp.AfaqyIn_New;
                    break;
                default:
                    break;
            }

            return serverIp;
        }
        private static List<CustServerUnit> GetAllUnitsFromServerNet()
        {
            //afaqy.net
            //101
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=101
            //20
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=20
            //104
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=104
            //119
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=119
            //29
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=29 (Closed)
            //99
            //http://a.afaqy.net/api/?token=57684b31615e9a63dfc2622496f37a38&server=99 (Closed)

            System.Threading.Tasks.Task<AfaqyTacker> task = null;
            List<CustServerUnit> result = new List<CustServerUnit>();

            //101
            string url = SiteConfig.AfaqyNet.Net_101_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            var data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.A_AfaqyNet_101; return x; }).ToList();
            result.AddRange(data);

            //20
            url = SiteConfig.AfaqyNet.Net_20_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.AfaqyNet_20; return x; }).ToList();
            result.AddRange(data);

            //104
            url = SiteConfig.AfaqyNet.Net_104_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.Waste_AfaqyNet_104; return x; }).ToList();
            result.AddRange(data);

            //119
            url = SiteConfig.AfaqyNet.Net_119_Url;
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            task = request.Execute<AfaqyTacker>();
            task.Wait();
            //set server ip address
            data = task.Result.AfaqyTackers;
            data = data.Select(x => { x.server_ip_address = Classes.Common.DBEnums.SystemServerIp.Tatweer_AfaqyNet_119; return x; }).ToList();
            result.AddRange(data);

            //request = new MyHttpRequestMessage(url, HttpMethod.Get);
            //request.RequestMessage.SetConfiguration(new System.Web.Http.HttpConfiguration() {  });
            //request.RequestMessage.Properties.Add(System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new System.Web.Http.HttpConfiguration().Formatters.Add(new System.Net.Http.Formatting.MediaTypeFormatter() )
            //var task2 = request.ExcecuteAsString();
            //result = Newtonsoft.Json.JsonConvert.DeserializeObject<AfaqyTacker>(task2.Result).AfaqyTackers;

            return result;
        }
        private static List<T> GetAllUnitsFromServerInfo<T>()
        {
            //get session id
            //login to get session id
            string userName = SiteConfig.AfaqyInfo.UserName;
            string password = SiteConfig.AfaqyInfo.Password;
            url = SiteConfig.AfaqyInfo.Url;
            url = url + "/ajax.html?svc=core/login&params={\"user\":" + userName + ",\"password\":" + password + "}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var accountInfoTask = request.Execute<Afaqy_Info_Me.AfaqyAccount>();
            accountInfoTask.Wait();
            string sessionId = accountInfoTask.Result.ssid;
            //get all accounts 
            url = SiteConfig.AfaqyInfo.Url;
            url += "/ajax.html?ssid="+sessionId+"&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_resource\",\"propName\":\"rel_is_account,sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\"},\"force\":1,\"flags\":5,\"from\":0,\"to\":0xffffffff}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var accountsInfoTask = request.Execute<AfaqyInfo.AfaqySearchItem>();
            accountsInfoTask.Wait();
            var accounts = accountsInfoTask.Result.items.Select(x=> new Afaqy_Info_Me.AfaqySearchItem.Account().ToAccount(x)).ToList();

            //get units
            url = SiteConfig.AfaqyInfo.Url;
            url = url + "/ajax.html?ssid=" + sessionId + "&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_unit\",\"propName\":\"sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\"},\"force\":0,\"flags\":1301,\"from\":0,\"to\":0xffffffff}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var unitsInfoTask = request.Execute<AfaqyInfo.AfaqySearchItem>();
            unitsInfoTask.Wait();
            var data = unitsInfoTask.Result.items;
            dynamic result = Activator.CreateInstance(typeof(List<T>));
            string serverIp = Classes.Common.DBEnums.SystemServerIp.AfaqyInfo;
            if (typeof(T) == typeof(CustServerUnit))
            {
                result = ((IEnumerable<T>)data.Select(x => x.ToUnit(accounts))).ToList<T>();
                result = ((IEnumerable<CustServerUnit>)result).Select(x => { x.server_ip_address = serverIp; return x; }).ToList();
            }
            else
            {
                result = ((IEnumerable<T>)data.Select(x => x.ToServerUnit(accounts))).ToList<T>();
                result = ((IEnumerable<ServerUnit>)result).Select(x => { x.ServerIP = serverIp; return x; }).ToList();
            }
            return result;
        }
        private static List<T> GetAllUnitsFromServerMe<T>()
        {
            //get session id
            url = SiteConfig.AfaqyMe.Url;
            string token = SiteConfig.AfaqyMe.Token;
            url = url + "/wialon/ajax.html?svc=token/login&params={\"token\":\"" + token + "\",\"operateAs\":\"\"}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var accountMeTask = request.Execute<AfaqyMe.AfaqyAccount>();
            accountMeTask.Wait();
            var sessionId = accountMeTask.Result.eid;
            //get all accounts 
            url = SiteConfig.AfaqyMe.Url;
            url += "/wialon/ajax.html?sid=" + sessionId + "&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_resource\",\"propName\":\"rel_is_account,sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\"},\"force\":1,\"flags\":5,\"from\":0,\"to\":0xffffffff}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var accountsMeTask = request.Execute<AfaqyMe.AfaqySearchItem>();
            accountsMeTask.Wait();
            var accounts = accountsMeTask.Result.items.Select(x => new Afaqy_Info_Me.AfaqySearchItem.Account().ToAccount(x)).ToList();
            //get units
            url = SiteConfig.AfaqyMe.Url;
            url = url + "/wialon/ajax.html?sid=" + sessionId + "&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_unit\",\"propName\":\"sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\",\"propType\":\"property\"},\"force\":0,\"flags\":1293,\"from\":0,\"to\":0xffffffff,\"or_logic\":false}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var unitsMeTask = request.Execute<AfaqyMe.AfaqySearchItem>();
            unitsMeTask.Wait();
            var itms = unitsMeTask.Result.items;
            dynamic result = Activator.CreateInstance(typeof(List<T>));
            string serverIp = Classes.Common.DBEnums.SystemServerIp.AfaqyMe;

            if (typeof(T) == typeof(CustServerUnit))
            {
                result = ((IEnumerable<T>)unitsMeTask.Result.items.Select(x => x.ToUnit(accounts))).ToList<T>();
                result = ((IEnumerable<CustServerUnit>)result).Select(x => { x.server_ip_address = serverIp; return x; }).ToList();
            }
            else
            {
                result = ((IEnumerable<T>)unitsMeTask.Result.items.Select(x => x.ToServerUnit(accounts))).ToList<T>();
                result = ((IEnumerable<ServerUnit>)result).Select(x => { x.ServerIP = serverIp; return x; }).ToList();
            }
            return result;
        }
        internal static bool SynchronizeServerUnits()
        {
            try
            {
                List<CustServerUnit> result = new List<CustServerUnit>();
                List<ServerUnit> items = new List<ServerUnit>();

                //clear all data from db
                if(new ServerUnitModel<ServerUnit>().ClearData())
                {
                    //afaqy.in
                    result = GetAllUnitsFromServerIn();
                    items = result.Select(x => x.ToServerUnit()).ToList();
                    //insert result 
                    new ServerUnitModel<ServerUnit>().Import(items.ToArray());

                    //afaqy.net
                    result = GetAllUnitsFromServerNet();
                    items = result.Select(x => x.ToServerUnit()).ToList();
                    //insert result
                    new ServerUnitModel<ServerUnit>().Import(items.ToArray());

                    //afaqy.info
                    items = GetAllUnitsFromServerInfo<ServerUnit>();
                    //insert result
                    new ServerUnitModel<ServerUnit>().Import(items.ToArray());

                    //afaqy.me
                    items = GetAllUnitsFromServerMe<ServerUnit>();
                    //insert result
                    new ServerUnitModel<ServerUnit>().Import(items.ToArray());

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                
            }

            return false;

        }

        public bool ClearData()
        {
            string url = ApiServerUrl + ControllerRoute + "clear";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var task = request.Execute<String>();
            task.Wait();
            if (task.Result.ToString().Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ServerUnitViewModel : ServerUnit
    {
        public string Block
        {
            get
            {
                return this.IsDeleted != null && (bool)this.IsDeleted ? Resources.Resource.True : Resources.Resource.False;
            }
        }

        public string LastConnection_Format
        {
            get
            {

                return this.LastConnection != null ? ((DateTime)this.LastConnection).ToString(Classes.Common.Constant.DateTimeFormat) : "";
            }
        }
    }

    public class ServerUnitIndexViewModel : ServerUnitViewModel
    {

    }
    public class ServerUnitDetailsViewModel : ServerUnitViewModel
    {

    }
}