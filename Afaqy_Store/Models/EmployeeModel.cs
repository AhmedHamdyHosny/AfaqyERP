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
    public class EmployeeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiEmployee/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public EmployeeModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class EmployeeViewModel : EmployeeView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    public class EmployeeIndexViewModel : EmployeeView
    {

    }
    public class EmployeeDetailsViewModel : EmployeeViewModel
    {

    }
    [Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate")]
    public class EmployeeCreateBindModel : Employee
    {

    }
    [Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate,IsBlock,CreateUserId,CreateDate")]
    public class EmployeeEditBindModel : Employee
    {
    }
    public class EmployeeEditModel
    {
        public Employee EditItem { get; set; }
        public IEnumerable<CustomSelectListItem> Branch { get; set; }
        public IEnumerable<CustomSelectListItem> Country { get; set; }
        public IEnumerable<CustomSelectListItem> Department { get; set; }
        
    }
}