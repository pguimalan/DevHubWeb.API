using DevHubWeb.Libraries.ObjectAttirbutes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DevHubWeb.Libraries.Extensions
{
    public static class EnumExtension
    {
        public static string StringValue(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs =
                fi.GetCustomAttributes(typeof(StringValueAttribute),
                                        false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            else
            {
                throw new InvalidOperationException("The StringValue can just be invoked on String enumerations having the '[StringValue(...)]' attribute.");
            }

            return output;
        }

        public static string HtmlClass(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            HtmlClassAttribute[] attrs =
                fi.GetCustomAttributes(typeof(HtmlClassAttribute),
                                        false) as HtmlClassAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            else
            {
                throw new InvalidOperationException("The HtmlClass can just be invoked on String enumerations having the '[HtmlClass(...)]' attribute.");
            }

            return output;
        }
    }
}
