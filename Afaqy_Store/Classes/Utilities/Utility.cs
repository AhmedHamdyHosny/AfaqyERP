using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using static Classes.Common.Enums;

namespace Classes.Utilities
{
    public class Utility
    {
        public static object GetPropertyValue(object entity, string propertyName)
        {
            Type type = entity.GetType();

            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(entity);
            }
            return null;
        }

        public static object GetFieldValue(object entity, string fieldName)
        {
            Type type = entity.GetType();
            FieldInfo fieldInfo = type.GetFields().Where(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(entity);
            }
            return null;
        }

        public static void SetPropertyValue(ref object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static void SetPropertyValue<T>(ref T obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //  Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }

        public static void CopyObject<T>(object sourceObject, ref T destObject, List<PropertyInfo> Ex)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            List<PropertyInfo> lst = sourceType.GetProperties().ToList();
            lst = sourceType.GetProperties().Where(x => !Ex.Any(y => y.Name == x.Name)).ToList();
            //  Loop through the source properties
            foreach (PropertyInfo p in lst)
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }

        public static TEntity CopyEntity<TEntity>(TEntity source) where TEntity : class, new()
        {

            // Get properties from EF that are read/write and not marked witht he NotMappedAttribute
            var sourceProperties = typeof(TEntity)
                                    .GetProperties()
                                    .Where(p => p.CanRead && p.CanWrite &&
                                                p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true).Length == 0);
            var notVirtualProperties = sourceProperties.Where(p => !p.GetGetMethod().IsVirtual);
            var newObj = new TEntity();

            foreach (var property in notVirtualProperties)
            {

                // Copy value
                property.SetValue(newObj, property.GetValue(source, null), null);

            }

            return newObj;

        }

        public static object ChangeType(object value, Type conversionType)
        {
            var targetType = conversionType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            return Convert.ChangeType(value, targetType);
        }
    }

    public class AlertMessage
    {

        public AlertMessageType MessageType { get; set; }
        private string _MessageContent;
        public string MessageContent
        {
            get
            {
                if(_MessageContent == null)
                {
                    _MessageContent = GetAlertMessage();
                }

                return _MessageContent;
            }
            set
            {
                _MessageContent = value;
            }
        }
        public int? TransactionCount { get; set; }
        public Transactions Transaction { get; set; }


        internal string GetAlertMessage()
        {
            var message = "";
            switch (this.Transaction)
            {
                case Transactions.Create:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertAddInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Edit:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertEditInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Delete:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertDeleteInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Import:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertImportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                case Transactions.Export:
                    switch (this.MessageType)
                    {
                        case AlertMessageType.Success:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportSuccessMessage;
                            break;
                        case AlertMessageType.Error:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportErrorMessage;
                            break;
                        case AlertMessageType.Warning:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportWarningMessage;
                            break;
                        case AlertMessageType.info:
                            message = this.TransactionCount + " " + Resources.Resource.AlertExportInfoMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return message;
        }
    }
}