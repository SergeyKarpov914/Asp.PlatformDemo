using Clio.Demo.Abstraction.Interface;
using Clio.Demo.Core.Lib.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class SqlEx
    {
        public const string SqlNull = "null";

        private static Dictionary<Type, IEnumerable<string>> _columnsMap = new Dictionary<Type, IEnumerable<string>>();
        private static Dictionary<Type, string> _tableMap = new Dictionary<Type, string>();

        public static string SelectQuery(this Type type, string clause = null)
        {
            IEnumerable<string> columns = mapEntity(type);

            return $"SELECT {string.Join(",", columns.Select(x => $"[{x}]"))} FROM {_tableMap[type]} {clause}";
        }

        public static string SelectQuery(this IEntity entity, string clause = null)
        {
            return entity.GetType().SelectQuery(clause);
        }

        public static string InsertQuery(this IEntity entity)
        {
            IEnumerable<string> columns = mapEntity(entity);

            return $"INSERT INTO {_tableMap[entity.GetType()]} VALUES ({getPropValues(entity, columns)})";
        }

        public static string UpdateQuery(this IEntity entity)
        {
            IEnumerable<string> columns = mapEntity(entity);

            return $"UPDATE {_tableMap[entity.GetType()]} SET ({getColumnAssignements(entity, columns)})";
        }

        public static string DeleteQuery(this IEntity entity)
        {
            return $"DELETE {mapTable(entity)} WHERE Id = {entity.Id}";
        }

        public static string ToInClause<T>(this IEnumerable<T> values, string column)
        { 
            return values == null || column == null ? null : $"WHERE {column} IN ({string.Join(",", values.Select(x => $"{sqlFormat(x)}"))})";
        }
        
        private static string getPropValues(IEntity entity, IEnumerable<string> columns = null)
        {
            List<string> values = new List<string>();

            if (null == columns)
            {
                columns = mapEntity(entity);
            }
            PropertyInfo[] props = entity.GetType().GetProperties();
            object value = null;

            foreach (PropertyInfo prop in props)
            {
                if (null != columns.FirstOrDefault(x => x.EqualsNoCase(prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name)))
                {
                    values.Add(null != (value = prop.GetValue(entity)) ? sqlFormat(value) : SqlNull);
                }
            }
            return string.Join(",", values);
        }

        private static string getColumnAssignements(IEntity entity, IEnumerable<string> columns)
        {
            PropertyInfo[] props = entity.GetType().GetProperties();
            List<string> assignPairs = new List<string>();

            foreach (PropertyInfo prop in props)
            {
                object value = null;
                string column = null;

                if (null != (column = columns.FirstOrDefault(x => x.EqualsNoCase(prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name))))
                {
                    assignPairs.Add($"{column}={(null != (value = prop.GetValue(entity)) ? sqlFormat(value) : SqlNull)}");
                }
            }
            return string.Join(",", assignPairs);
        }

        private static IEnumerable<string> mapEntity(IEntity entity)
        {
            return mapEntity(entity.GetType());
        }

        private static IEnumerable<string> mapEntity(Type type)
        {
            mapTable(type);

            if (!_columnsMap.TryGetValue(type, out IEnumerable<string> columns))
            {
                PropertyInfo[] props = type.GetProperties();
                HashSet<string> set = new HashSet<string>();

                foreach (PropertyInfo prop in props)
                {
                    if (null != prop.GetCustomAttribute<JsonIgnoreAttribute>())
                    {
                        continue;
                    }
                    set.Add(prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name);
                }
                _columnsMap.Add(type, columns = set);
            }
            return columns;
        }

        private static string mapTable(IEntity entity)
        {
            return mapTable(entity.GetType());
        }

        private static string mapTable(Type type)
        {
            if (!_tableMap.TryGetValue(type, out string table))
            {
                _tableMap.Add(type, (table = type.GetCustomAttribute<TableAttribute>()?.Name) ?? type.Name);
            }
            return table;
        }

        private static string sqlFormat(object value)
        {
            if (null == value) return "NULL";

            switch (value)
            {
                case string str:
                    return $"'{str}'";
                case DateTime date:
                    return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return value.ToString();
        }
    }
}
