using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Clio.Demo.Core.Lib.Extension
{
	public static class DataRowEx
	{
		public static T MapTo<T>(this DataRow row) where T : class, new()
		{
			T entity = new T();
			DataTable table = row.Table;
			PropertyInfo[] props = typeof(T).GetProperties();

			foreach (DataColumn column in table.Columns)
			{
				PropertyInfo prop = null;

				if (!row.IsNull(column))
				{
					if (null != (prop = props.FirstOrDefault(p => string.Equals(p.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase))))
					{
						prop.SetValue(entity, Convert.ChangeType(row[column], prop.PropertyType));
					}
				}
			}
			return entity;
		}
	}
}
