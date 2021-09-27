using Apparare.PropertyEditors.EnumPicker.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Hosting;
using System.Xml.Serialization;

using Umbraco.Web.WebApi;

namespace Apparare.PropertyEditors.EnumPicker.Controllers.API
{
    public class EnumApiController : UmbracoAuthorizedApiController
    {
        [System.Web.Http.HttpGet]
        public IEnumerable<EnumData> GetAll(string a, string t)
        {
            Assembly assembly = GetEnumAssembly(a);
            Type type = GetEnumType(assembly, t);

            List<EnumData> items = new List<EnumData>();

            if (type != null)
            {
                foreach (var item in Enum.GetValues(type))
                {
                    items.Add(new EnumData()
                    {
                        Name = item.ToString(),
                        Value = GetName(item),
                        Description = GetDescription(item)
                    });
                }
            }

            return items;
        }

        private static string GetDescription(object value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return fi.Name;
            }
        }

        private static string GetName(object value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (XmlEnumAttribute[])fi.GetCustomAttributes(typeof(XmlEnumAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Name;
            }
            else
            {
                return fi.Name;
            }
        }

        public static Type GetEnumType(Assembly assembly, string enumName)
        {
            var type = assembly.GetType(enumName);
            if (type.IsEnum)
                return type;

            return null;
        }

        private static Assembly GetEnumAssembly(string assembly)
        {
            if (string.Equals(assembly, "App_Code", StringComparison.InvariantCultureIgnoreCase))
            {
                return Assembly.Load(assembly);
            }

            string assemblyPath = HostingEnvironment.MapPath(string.Concat("~/bin/", assembly));
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
