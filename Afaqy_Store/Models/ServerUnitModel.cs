using Classes.Utilities;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Afaqy_Store.Models
{
    public class ServerUnitModel
    {
        private static string url = null;
        private static MyHttpRequestMessage request = null;

        internal static List<ServerUnit> GetAllUnits(GenericDataFormat options = null)
        {
            List<ServerUnit> result = new List<ServerUnit>();

            //afaqy.in
            result.AddRange(GetAllUnitsFromServerIn());

            //afaqy.net
            result.AddRange(GetAllUnitsFromServerNet());

            //afaqy.info
            result.AddRange(GetAllUnitsFromServerInfo());

            //afaqy.me
            result.AddRange(GetAllUnitsFromServerMe());

            return result;
        }
        private static List<ServerUnit> GetAllUnitsFromServerIn()
        {
            //afaqy.in
            //http://afaqy.in/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json
            //sfac.afaqy.in
            //http://sfac.afaqy.in/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json
            //afaqy.xyz 
            //http://afaqy.xyz/func/fn_accounting_system.php?token=011d75d043b8a01088c58dbf7b1865f9&cmd=loadData&format=json

            System.Threading.Tasks.Task<AfaqyTacker> task = null;
            List<ServerUnit> result = new List<ServerUnit>();
            foreach (string url in SiteConfig.AfaqyIn.Url.Split(','))
            {
                request = new MyHttpRequestMessage(url, HttpMethod.Get);
                task = request.Execute<AfaqyTacker>();
                task.Wait();
                result.AddRange(task.Result.AfaqyTackers);
            }

            return result;
        }
        private static List<ServerUnit> GetAllUnitsFromServerNet()
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
            List<ServerUnit> result = new List<ServerUnit>();
            foreach (string url in SiteConfig.AfaqyNet.Url.Split(','))
            {
                request = new MyHttpRequestMessage(url, HttpMethod.Get);
                //request.RequestMessage.SetConfiguration(new System.Web.Http.HttpConfiguration() {  });
                //request.RequestMessage.Properties.Add(System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new System.Web.Http.HttpConfiguration().Formatters.Add(new System.Net.Http.Formatting.MediaTypeFormatter() )
                //var task2 = request.ExcecuteAsString();
                task = request.Execute<AfaqyTacker>();
                task.Wait();
                //result = Newtonsoft.Json.JsonConvert.DeserializeObject<AfaqyTacker>(task2.Result).AfaqyTackers;
                result.AddRange(task.Result.AfaqyTackers);
            }

            return result;
        }
        private static List<ServerUnit> GetAllUnitsFromServerInfo()
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
            //get units
            url = SiteConfig.AfaqyInfo.Url;
            url = url + "/ajax.html?ssid=" + sessionId + "&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_unit\",\"propName\":\"sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\"},\"force\":0,\"flags\":1285,\"from\":0,\"to\":0xffffffff}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var unitsInfoTask = request.Execute<AfaqyInfo.AfaqySearchUnit>();
            unitsInfoTask.Wait();
            var result = unitsInfoTask.Result.items.Select(x => x.ToUnit()).ToList();
            return result;
        }
        private static List<ServerUnit> GetAllUnitsFromServerMe()
        {
            //get session id
            url = SiteConfig.AfaqyMe.Url;
            string token = SiteConfig.AfaqyMe.Token;
            url = url + "/wialon/ajax.html?svc=token/login&params={\"token\":\"" + token + "\",\"operateAs\":\"\"}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var accountMeTask = request.Execute<AfaqyMe.AfaqyAccount>();
            accountMeTask.Wait();
            var sessionId = accountMeTask.Result.eid;
            //get units
            url = SiteConfig.AfaqyMe.Url;
            url = url + "/wialon/ajax.html?sid=" + sessionId + "&svc=core/search_items&params={\"spec\":{\"itemsType\":\"avl_unit\",\"propName\":\"sys_name\",\"propValueMask\":\"*\",\"sortType\":\"sys_name\",\"propType\":\"property\"},\"force\":0,\"flags\":0x00402001,\"from\":0,\"to\":0xffffffff,\"or_logic\":false}";
            request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var unitsMeTask = request.Execute<AfaqyMe.AfaqySearchUnit>();
            unitsMeTask.Wait();
            var result = unitsMeTask.Result.items.Select(x => x.ToUnit()).ToList();
            string serverIp = Classes.Common.DBEnums.SystemServerIp.AfaqyMe;
            result = result.Select(x => { x.server_ip_address = serverIp; return x; }).ToList();
            return result;
        }
    }
}