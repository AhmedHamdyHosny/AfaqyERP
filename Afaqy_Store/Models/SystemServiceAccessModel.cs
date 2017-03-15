using Classes.Utilities;
using Models;
using Security.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Security.Models
{
    public class SystemServiceAccessModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSystemServiceAccess/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public SystemServiceAccessModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SystemServiceAccessViewModel : ServiceAccessView
    {
        public class CustSystemService
        {
            public int ServiceId { get; set; }
            public string ServiceTag { get; set; }
            public string ServiceName_en { get; set; }
            public string ServiceName_ar { get; set; }
            public string ServiceDescription_en { get; set; }
            public string ServiceDescription_ar { get; set; }
            public bool Service_IsBlock { get; set; }
            public CustSystemAccess[] SystemAccess { get; set; }

            public CustSystemService()
            {

            }
            public CustSystemService(Service service)
            {
                this.ServiceId = service.ServiceId;
                this.ServiceTag = service.ServiceTag;
                this.ServiceName_en = service.ServiceName_en;
                this.ServiceName_ar = service.ServiceName_ar;
                this.ServiceDescription_en = service.ServiceDescription_en;
                this.ServiceDescription_ar = service.ServiceDescription_ar;
                this.Service_IsBlock = service.IsBlock;
            }

            public CustSystemService(int serviceId, List<SystemServiceAccessViewModel> serviceAccess)
            {
                var service = serviceAccess.Where(x => x.ServiceId == serviceId)
                                .Select(x => new CustSystemService()
                                {
                                    ServiceId = x.ServiceId,
                                    ServiceTag = x.ServiceTag,
                                    ServiceName_en = x.ServiceName_en,
                                    ServiceName_ar = x.ServiceName_ar,
                                    ServiceDescription_en = x.ServiceDescription_en,
                                    ServiceDescription_ar = x.ServiceDescription_ar,
                                    Service_IsBlock = x.Service_IsBlock
                                }).GroupBy(x => new { x.ServiceId }).Select(x => x.FirstOrDefault()).SingleOrDefault();

                this.ServiceId = service.ServiceId;
                this.ServiceTag = service.ServiceTag;
                this.ServiceName_en = service.ServiceName_en;
                this.ServiceName_ar = service.ServiceName_ar;
                this.ServiceDescription_en = service.ServiceDescription_en;
                this.ServiceDescription_ar = service.ServiceDescription_ar;
                this.Service_IsBlock = service.Service_IsBlock;
                this.SystemAccess = serviceAccess.Where(x => x.ServiceId == serviceId)
                                .Select(x => new CustSystemAccess()
                                {
                                    ServiceAccessId = x.ServiceAccessId,
                                    AccessTypeId = x.AccessTypeId,
                                    AccessTypeName_en = x.AccessTypeName_en,
                                    AccessTypeName_ar = x.AccessTypeName_ar,
                                    ControlTag = x.ControlTag,
                                    AccessType_IsBlock = x.AccessType_IsBlock,
                                    ServiceAccess_IsBlock = x.ServiceAccess_IsBlock
                                }).Distinct().ToArray();
            }

        }

        public class CustSystemAccess
        {
            public int ServiceAccessId { get; set; }
            public int AccessTypeId { get; set; }
            public string AccessTypeName_en { get; set; }
            public string AccessTypeName_ar { get; set; }
            public string ControlTag { get; set; }
            public bool AccessType_IsBlock { get; set; }
            public bool ServiceAccess_IsBlock { get; set; }
            public bool Checked { get; set; }

            
        }
    }

    [Bind(Include = "ServiceAccessId,ServiceId,Checked")]
    public class SystemServiceAccessCreateBindModel 
    {

    }

   
}