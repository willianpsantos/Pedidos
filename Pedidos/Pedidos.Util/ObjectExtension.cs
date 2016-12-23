using System;
using System.Reflection;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Pedidos.Util
{
    public static class ObjectExtension
    {
        public static object Copy(this object source)
        {
            var type = source.GetType();
            var destiny = type.Assembly.CreateInstance(type.FullName);
            var properties = type.GetProperties();

            foreach (var item in properties)
            {
                var value = item.GetValue(source);
                item.SetValue(destiny, value);
            }

            return destiny;
        }

        public static void Copy(this object source, object destiny, bool verifyType = true)
        {
            var type = source.GetType();
            var destinyType = destiny.GetType();

            if (verifyType && !type.Equals(destinyType))
            {
                throw new ArgumentException("O Tipo do objeto de destino é diferente do tipo do objeto fonte.");
            }

            var properties = type.GetProperties();

            foreach (var item in properties)
            {
                var value = item.GetValue(source);
                item.SetValue(destiny, value);
            }
        }

        public static TType Parse<TType>(this object source)
        {
            Type type = typeof(TType);
            Type underlyingType = Nullable.GetUnderlyingType(type);
            ParameterModifier modifiers = new ParameterModifier(1);
            MethodInfo parseMethod = underlyingType.GetMethod("Parse", new Type[] { typeof(string) }, new ParameterModifier[] { modifiers });

            if (parseMethod == null)
                parseMethod = underlyingType.GetMethod("Parse", new Type[] { typeof(object) }, new ParameterModifier[] { modifiers });

            return source != null ? (TType)parseMethod.Invoke(null, new object[] { source.ToString() }) : default(TType);
        }

        public static object Parse(this object source, Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            ParameterModifier modifiers = new ParameterModifier(1);
            MethodInfo parseMethod = underlyingType.GetMethod("Parse", new Type[] { typeof(string) }, new ParameterModifier[] { modifiers });

            if (parseMethod == null)
                parseMethod = underlyingType.GetMethod("Parse", new Type[] { typeof(object) }, new ParameterModifier[] { modifiers });

            return source != null ? parseMethod.Invoke(null, new object[] { source.ToString() }) : null;
        }

        public static string QueryString(this object source)
        {
            var properties = from p in source.GetType().GetProperties()
                             where p.GetValue(source, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(source, null).ToString());

            return string.Join("&", properties.ToArray());
        }

        public static Type GetPropertyType(this object source, string propertyName)
        {
            var property = source.GetType().GetProperty(propertyName);

            if (property == null)
                return null;

            return property.PropertyType;
        }

        public static Type GetPropertyType(this Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);

            if (property == null)
                return null;

            return property.PropertyType;
        }

        public static bool HasKey(this NameValueCollection collection, string key)
        {
            var keys = collection.Keys;
            var contains = false;

            foreach (string item in keys)
            {
                if (item == key)
                {
                    contains = true;
                }
            }

            return contains;
        }
    }
}
