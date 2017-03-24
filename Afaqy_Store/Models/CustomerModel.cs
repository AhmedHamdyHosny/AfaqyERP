using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class CustomerModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiCustomer/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public CustomerModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class CustomerViewModel : CustomerView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
 
    }

    public class CustomerIndexViewModel : CustomerView
    {
    }

    public class CustomerDetailsViewModel : CustomerViewModel
    {
    }

    [Bind(Include = "CustomerId,DolphinId,CustomerName_en,CustomerName_ar,Email,BranchId,CustomerTypeId,EmployeeId,CustomerContact")]
    public class CustomerCreateBindModel : Customer
    {

    }

    [Bind(Include = "CustomerId,DolphinId,CustomerName_en,CustomerName_ar,Email,BranchId,CustomerTypeId,EmployeeId,CustomerStatusId,IsBlock,CreateUserId,CreateDate,CustomerContact")]
    public class CustomerEditBindModel : Customer
    {
    }
    public class CustomerEditModel
    {
        public Customer EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Branch { get; set; }
    }

    public class CustomerImportModel : Customer
    {
    }
}