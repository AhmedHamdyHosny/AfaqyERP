using Afaqy_Store.DataLayer;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{
    public class WarehouseInfoModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiWarehouseInfo/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public WarehouseInfoModel() : base(ApiUrl, ApiRoute)
        {
        }

        public TModel GetById(string warehouseCode)
        {
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "Warehouse_wa_code",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = warehouseCode,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            List<TModel> result = this.Get(requestBody);
            if(result != null && result.Count > 0)
            {
                return result.SingleOrDefault();
            }
            else
            {
                return null;
            }

        }
    }
}