using Clio.Demo.Extension;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Clio.Demo.Util
{
    public enum CRUD { Create, ReadAll, Read, Update, Delete }

    public class SqlCol
    {
        public string Data   { get; set; }
        public string Column { get; set; }

        public SqlCol(string col, string data)
        {
            Column = col;
            Data   = data;
        }
    }

    public static class Sql
    {
        public const string SqlNull = "null";

        public static Dictionary<Type, SqlDbType> TypeMap = new Dictionary<Type, SqlDbType>();

        static Sql()
        {
            TypeMap[typeof(string)] = SqlDbType.NVarChar;
            TypeMap[typeof(char[])] = SqlDbType.NVarChar;
            TypeMap[typeof(int)] = SqlDbType.Int;
            TypeMap[typeof(int)] = SqlDbType.Int;
            TypeMap[typeof(short)] = SqlDbType.SmallInt;
            TypeMap[typeof(long)] = SqlDbType.BigInt;
            TypeMap[typeof(decimal)] = SqlDbType.Decimal;
            TypeMap[typeof(double)] = SqlDbType.Float;
            TypeMap[typeof(byte)] = SqlDbType.TinyInt;
            TypeMap[typeof(byte[])] = SqlDbType.VarBinary;
            TypeMap[typeof(bool)] = SqlDbType.Bit;
            TypeMap[typeof(DateTime)] = SqlDbType.DateTime;
        }

        public static string SelectQuery(IEnumerable<string> columns, string table, string clause = null)
        {
            return $"SELECT {string.Join(",", columns.Select(x => $"[{x}]"))} FROM {table} {clause}";
        }

        public static string InsertQuery<T>(T entity, IEnumerable<string> columns, string table)
        {
            return $"INSERT INTO {table} VALUES ({SqlFormatProps(entity, columns)})";
        }

        public static string UpdateQuery<T>(T entity, IEnumerable<string> columns, string table)
        {
            return $"UPDATE {table} SET ({SqlFormatAssignProps(entity, columns)})";
        }

        public static string DeleteQuery(string table, string clause)
        {
            if (clause.IsEmpty()) throw new Exception("Delete query without 'where' clause is not supported");
            
            return $"DELETE {table} WHERE {clause}";
        }

        public static string ExecuteProc<T>(T entity, IEnumerable<string> columns, string proc)
        {
            return $"EXEC {proc} {SqlFormatProps(entity, columns)}";
        }

        public static string SqlFormatProps<T>(T entity, IEnumerable<string> columns)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            List<string> values = new List<string>();

            foreach (string column in columns)
            {
                PropertyInfo prop = null;
                object value = null;

                if (null != (prop = props.FirstOrDefault(x => x.Name.EqualsNoCase(column))))
                {
                    values.Add(null != (value = prop.GetValue(entity)) ? sqlFormat(value) : SqlNull);
                }
                else
                {
                    values.Add(SqlNull);
                }
            }
            return string.Join(",", values);
        }

        public static string SqlFormatAssignProps<T>(T entity, IEnumerable<string> columns)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            List<string> assignPirs = new List<string>();

            foreach (string column in columns)
            {
                PropertyInfo prop = null;
                object value = null;

                if (null != (prop = props.FirstOrDefault(x => x.Name.EqualsNoCase(column))))
                {
                    assignPirs.Add($"{column}={(null != (value = prop.GetValue(entity)) ? sqlFormat(value) : SqlNull)}" );
                }
                else
                {
                    assignPirs.Add($"{column}={SqlNull}");
                }
            }
            return string.Join(",", assignPirs);
        }

        public static string SqlClause(string column, IEnumerable<string> values)
        {
            if (values.IsEmpty())
            {
                return string.Empty;
            }
            if (column.IsEmpty()) throw new ArgumentNullException($"Cannot create clause with empty column");

            return $" WHERE {column} IN ({string.Join(",", values)})";
        }

        public static string SqlClause(string column, object value)
        {
            if (null == column) throw new ArgumentNullException($"Cannot create clause with empty key");
            if (null == value) return $" WHERE {column} IS NULL";

            return $" WHERE {column} = {sqlFormat(value)}";
        }

        public static string SqlClause(string column, string value)
        {
            if (null == column) throw new ArgumentNullException($"Cannot create clause with empty key");
            if (null == value) return $" WHERE {column} IS NULL";

            return $" WHERE {column} = {value}";
        }

        public static string SqlQueryValue(this object value, int precision = 6)
        {
            if (value is null)
            {
                return "NULL";
            }
            if (value is ICollection)
            {
                return "collection";  // ?
            }
            switch (value)
            {
                case int n:
                    return n.ToString();
                case float f:
                    return Math.Round(f, precision).ToString();
                case double ff:
                    return Math.Round(ff, precision).ToString();
                case decimal dec:
                    return Math.Round(dec, precision).ToString();
                case DateTime date:
                    return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return value.ToString();
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

        public static string SqlFormat<D>(D data)
        {
            return sqlFormat((object)data);
        }
        public static string SqlFormat(float number)
        {
            return number.ToString();
        }
        public static string SqlFormat(double number)
        {
            return number.ToString();
        }
        public static string SqlFormat(decimal number)
        {
            return number.ToString();
        }
        public static string SqlFormat(int number)
        {
            return number.ToString();
        }
        public static string SqlFormat(DateTime date)
        {
            return date.ToString("'yyyy-MM-dd HH:mm:ss'");
        }
        public static string SqlFormat(this string text)
        {
            return $"'{text}'";
        }
    }
}
