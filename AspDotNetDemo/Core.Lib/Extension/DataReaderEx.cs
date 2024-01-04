using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Reflection;

namespace Clio.Demo.Extension
{
	public static class DataReaderEx
	{
		public static T MapTo<T>(this SqlDataReader reader) where T : class, new()
		{
			T              entity = new T();
			PropertyInfo[] props  = typeof(T).GetProperties();

			for (int x = 0; x < reader.FieldCount; x++)
			{
				PropertyInfo prop = null;

				if (!reader.IsDBNull(x))
				{
					if(null != (prop = props.FirstOrDefault(p => string.Equals(p.Name, reader.GetName(x), StringComparison.OrdinalIgnoreCase))))
					{
						Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
						prop.SetValue(entity, Convert.ChangeType(reader.GetValue(x), type));
					}
				}
			}
			return entity;
		}
	}
}
