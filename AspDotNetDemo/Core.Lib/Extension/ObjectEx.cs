using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class ObjectEx
	{
        public static T Clone<T>(this T toClone)
		{
			T result;
            var json = JsonSerializer.Serialize<T>(toClone);
            result = JsonSerializer.Deserialize<T>(json);
            return result;
        }

		public static void Inject<T>(this T source, out object target) where T : class
		{
			target = source ?? throw new Exception($"Cannot inject null for {typeof(T).Name}. Check injection middleware"); 			
		}

        public static void Inject<T>(this T source, out T target) where T : class
        {
            target = source ?? throw new Exception($"Cannot inject null for {typeof(T).Name}. Check injection middleware");
        }

        public static void SetPropertyByName(this object obj, string name, object value)
        {
            PropertyInfo prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (null == prop || !prop.CanWrite)
            {
                return;
            }
            prop.SetValue(obj, value, null);
        }

        public static object GetPropertyValue(this object obj, string name)
        {
            if (obj is null) return null;

            PropertyInfo prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            return prop is null ? null : prop.GetValue(obj);
        }

        public static string Stamp(this object sender)
		{
			switch (sender)
			{
				case string str : return str;
				case Type   type: return type.DisplayName();
			}
			return $"{sender.GetType().Name}.{sender.GetHashCode()}";
		}

		public static string TypeName(this object sender)
		{
			if(null == sender) return "null";
			return sender.GetType().DisplayName();
		}
		public static string Location(this object sender)
		{
			if (null == sender) return "null";
			return sender.GetType().Location();
		}
		public static string AssemblyInfo(this object sender)
		{
			if (null == sender) return "null";
			return sender.GetType().AssemblyInfo();
		}
		public static string AssemblyVersion(this object sender)
		{
			if (null == sender) return "null";
			return sender.GetType().Assembly.Version();
		}
    }
}
