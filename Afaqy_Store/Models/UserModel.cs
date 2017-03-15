using Afaqy_Store.DataLayer;
using Classes.Utilities;
using Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Classes.Common.Enums;

namespace Afaqy_Store.Models
{
    public class UserModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiUser/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public UserModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal void Get_Create_Modify_User(int createUserId, int? modifyUserId, ref UserViewModel createUser, ref UserViewModel modifyUser )
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (createUserId != 0)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
            }

            if (modifyUserId != null && modifyUserId != 0)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            var users = this.GetView<UserViewModel>(requestBody).PageItems;

            createUser = users.SingleOrDefault(x => x.UserId == createUserId);
            if(modifyUserId != null && modifyUserId != 0 )
            {
                modifyUser = users.SingleOrDefault(x => x.UserId == modifyUserId);
            }
        }

        //internal void Get_Create_User(int createUserId,ref UserViewModel createUser)
        //{
        //    var filters = new List<GenericDataFormat.FilterItems>();
        //    if (createUserId != 0)
        //    {
        //        filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
        //    }
        //    var requestBody = new GenericDataFormat() { Filters = filters };
        //    var users = this.Get(requestBody);
        //    createUser = users.Cast<UserViewModel>().SingleOrDefault(x => x.UserId == createUserId);
        //}
    }


    public class UserViewModel : UserView
    {
        public const string SessionName = "CurrentUser";
        public UserViewModel Login()
        {
            GenericDataFormat requestBody = new GenericDataFormat();
            requestBody.Filters = new List<GenericDataFormat.FilterItems>();
            requestBody.Filters.Add(new GenericDataFormat.FilterItems() { Property = "UserName", Value = this.UserName, Operation = GenericDataFormat.FilterOperations.Equal });
            UserViewModel user = new UserModel<UserViewModel>().GetView<UserViewModel>(requestBody).PageItems.SingleOrDefault();
            if (user != null)
            {
                var uPass = SecurityMethods.Hashing(this.UserName, this.Password);
                if (user.Password == SecurityMethods.Hashing(this.UserName, this.Password))
                {
                    return user;
                }
            }

            return null;
        }

        public void SaveUserToLocalStorage(bool rememberMe)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserName, rememberMe);
            System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserId.ToString(), rememberMe);
            HttpContext.Current.Session[UserViewModel.SessionName] = this;
        }

        public UserViewModel GetUserFromSession()
        {
            UserViewModel cUser = null;
            if (HttpContext.Current.Session[UserViewModel.SessionName] != null)
            {
                cUser = (UserViewModel)HttpContext.Current.Session[UserViewModel.SessionName];
            }

            //for test 
            if(cUser == null)
            {
                var requestBody = new GenericDataFormat();
                requestBody.Filters = new List<GenericDataFormat.FilterItems>();
                requestBody.Filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = 1 });
                cUser = new UserModel<UserViewModel>().GetView<UserViewModel>(requestBody).PageItems.SingleOrDefault();
                cUser.SaveUserToLocalStorage(true);
            }

            return cUser;
               
        }

        public bool RemoveUserSession()
        {
            HttpContext.Current.Session[UserViewModel.SessionName] = null;
            return true;
        }
    }
}