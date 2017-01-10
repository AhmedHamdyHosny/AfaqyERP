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

        internal void Get_Create_Modify_User(int createUserId, int? modifyUserId, ref UserViewModel createUser, ref UserViewModel modifyUser)
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
            var users = this.Get(requestBody);

            createUser = users.Cast<UserViewModel>().SingleOrDefault(x => x.UserId == createUserId);
            modifyUser = users.Cast<UserViewModel>().SingleOrDefault(x => x.UserId == modifyUserId);

        }
    }


    public class UserViewModel : User
    {
        private string _fullName_en;
        public string FullName_En
        {
            get
            {
                if(string.IsNullOrEmpty(_fullName_en))
                {
                    _fullName_en = GetUserFullName(this,Languages.en);
                }
                return _fullName_en;
            }
            set
            {
                _fullName_en = value;
            }
        }


        private string _fullName_ar;
        public string FullName_Ar
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName_ar))
                {
                    _fullName_ar = GetUserFullName(this,Languages.ar);
                }
                return _fullName_ar;
            }
            set
            {
                _fullName_ar = value;
            }
        }


        public static string GetUserFullName(User user, Languages language = Languages.en)
        {
            string firstName = null;
            string lastName = null;

            switch (language)
            {
                case Languages.en:
                    if(!string.IsNullOrEmpty(user.FirstName_en))
                    {
                        firstName = user.FirstName_en;
                    }
                    if(!string.IsNullOrEmpty(user.LastName_en))
                    {
                        lastName = user.LastName_en;
                    }
                    break;
                case Languages.ar:
                    if (!string.IsNullOrEmpty(user.FirstName_ar))
                    {
                        firstName = user.FirstName_ar;
                    }
                    if (!string.IsNullOrEmpty(user.LastName_ar))
                    {
                        lastName = user.LastName_ar;
                    }
                    break;
                default:
                    if (!string.IsNullOrEmpty(user.FirstName_en))
                    {
                        firstName = user.FirstName_en;
                    }
                    if (!string.IsNullOrEmpty(user.LastName_en))
                    {
                        lastName = user.LastName_en;
                    }
                    break;
            }
            var fullName = "";
            if(user != null)
            {
                fullName = string.IsNullOrWhiteSpace( firstName + " " ) ? lastName : firstName + " "  + lastName; 
            }
            return fullName;
        }
    }
}