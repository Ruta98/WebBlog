using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SharedLib
{
    public class Parameter
    {
        public string ColumnName { get; set; }
        public object Value { get; set; }
    //    public SqliteType Type { get; set; }
        public Parameter(string parColumnName, object parValue)
        {
            ColumnName = parColumnName;
            Value = parValue;
        }
    }


    public class SQLite
    {
        SQLiteConnection connection = null;
        public SQLite(String varConectionString)
        {
           
            connection = new SQLiteConnection(@"Data Source="+ varConectionString);
            //SQLitePCL.raw.SetProvider(new SQLitePCL.ISQLite3Provider() ); //SQLite3Provider_e_sqlite3()

            connection.Open();
        }

        void Close()
        {

        }
        /// <summary>
        /// Выполняет переданный запрос в виде строки.
        /// </summary>
        /// <param name="query">Строка запроса</param>
        /// <param name="parameters">Коллекция параметров</param>
        /// <returns>Таблица с данными</returns>
        public DataTable Execute(string query, Parameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            var command = connection.CreateCommand();
            command.CommandText = query;
            if (parameters != null)
                foreach (var iparam in parameters)
                    command.Parameters.AddWithValue(iparam.ColumnName, iparam.Value);

            using (var reader = command.ExecuteReader())
                dt.Load(reader);
                return dt;
        }

        public void ExecuteNonQuery(string query, Parameter[] parameters = null)
        {
            using (var transaction = connection.BeginTransaction())
            {

                DataTable dt = new DataTable();
                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Transaction = transaction;
                if (parameters != null)
                    foreach (var iparam in parameters)
                        command.Parameters.AddWithValue(iparam.ColumnName, iparam.Value);
                command.ExecuteNonQuery();
                transaction.Commit();
            }
        }
        public object ExecuteScalar(string query, Parameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            var command = connection.CreateCommand();
            command.CommandText = query;
            if (parameters != null)
                foreach (var iparam in parameters)
                    command.Parameters.AddWithValue(iparam.ColumnName, iparam.Value);
            return command.ExecuteScalar();

        }

    }
}
