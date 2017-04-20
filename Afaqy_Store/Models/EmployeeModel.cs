using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classes.Common;

namespace Afaqy_Store.Models
{
    public class RpauxEmployeeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiRpauxEmployee/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public RpauxEmployeeModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class RpauxEmployeeViewModel : RpauxEmployeeView
    {
        public string Block
        {
            get
            {
                return this.aux_blocked == 1 ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    //public class RpauxEmployeeIndexViewModel : EmployeeView
    //{

    //}
    //public class RpauxEmployeeDetailsViewModel : RpauxEmployeeViewModel
    //{

    //}
    //[Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate")]
    //public class RpauxEmployeeCreateBindModel : Employee
    //{

    //}
    //[Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate,IsBlock,CreateUserId,CreateDate")]
    //public class RpauxEmployeeEditBindModel : Employee
    //{
    //}
    //public class RpauxEmployeeEditModel
    //{
    //    public Employee EditItem { get; set; }
    //    public IEnumerable<CustomSelectListItem> Branch { get; set; }
    //    public IEnumerable<CustomSelectListItem> Country { get; set; }
    //    public IEnumerable<CustomSelectListItem> Department { get; set; }

    //}

    public class EmployeeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiEmployee/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public EmployeeModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal void GetEmployeeUserWithManager(int empId, ref int? empUserId, ref int? managerUserId)
        {
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "EmployeeId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = empId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "Employee2" } }; //Employee2 (Manager Reference)
            List<Employee> emps = new EmployeeModel<Employee>().Get(requestBody);
            if (emps != null && emps.Count > 0)
            {
                Employee emp = emps.SingleOrDefault();
                empUserId = emp.UserId;

                if (emp.Employee2 != null && emp.Employee2.UserId != null)
                {
                    managerUserId = emp.Employee2.UserId;
                }
            }
        }

        internal List<Employee> GetEmployee(bool withManager = false, int? jobTitleId = null)
        {
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            
            if (jobTitleId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JobTitleId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = jobTitleId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            if (withManager)
            {
                requestBody.Includes = new GenericDataFormat.IncludeItems() { References = "Employee2" };  //Employee2 (Manager Reference)
            }

            List<Employee> emps = new EmployeeModel<Employee>().Get(requestBody);
            return emps;
        }

        internal List<Employee> GetBranchEmployee(string branchId, bool withManager = false, int? jobTitleId = null)
        {
            List<GenericDataFormat.FilterItems> filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "Branch_br_code",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = branchId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            if(jobTitleId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JobTitleId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = jobTitleId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            GenericDataFormat requestBody = new GenericDataFormat() { Filters = filters };
            if(withManager)
            {
                requestBody.Includes = new GenericDataFormat.IncludeItems() { References = "Employee2" };  //Employee2 (Manager Reference)
            }

            List<Employee> emps = new EmployeeModel<Employee>().Get(requestBody);
            return emps;
        }
    }

    public class EmployeeViewModel : EmployeeView
    {
        public string Block
        {
            get
            {
                return this.IsBlock == true ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
    //public class EmployeeIndexViewModel : EmployeeView
    //{

    //}
    //public class EmployeeDetailsViewModel : EmployeeViewModel
    //{

    //}
    //[Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate")]
    //public class EmployeeCreateBindModel : Employee
    //{

    //}
    //[Bind(Include = "EmployeeId,FirstName_en,FirstName_ar,LastName_en,LastName_ar,PhoneNumber,MobileNumber,ResidencyNumber,CountryId,Salary,DepartmentId,ManagerId,BranchId,JoinDate,JoinDate,IsBlock,CreateUserId,CreateDate")]
    //public class EmployeeEditBindModel : Employee
    //{
    //}
    //public class EmployeeEditModel
    //{
    //    public Employee EditItem { get; set; }
    //    public IEnumerable<CustomSelectListItem> Branch { get; set; }
    //    public IEnumerable<CustomSelectListItem> Country { get; set; }
    //    public IEnumerable<CustomSelectListItem> Department { get; set; }
    //}


}