// ***********************************************************************
// <copyright file="DataAccess.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>DataAccess class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Framework;
using Framework;

namespace AuScGen.CommonUtilityPlugin
{
    /// <summary>
    /// DataCategory
    /// </summary>
    public enum DataCategory
    {
        /// <summary>
        /// The SQLDB
        /// </summary>
        SQLDB,
        /// <summary>
        /// The ms excel
        /// </summary>
        MSExcel,
        /// <summary>
        /// The Visual FoxPro
        /// </summary>
        VSFoxPro
    }

    /// <summary>
    /// DataAccess
    /// </summary>
    [Export(typeof(IPlugin))]
    public class DataAccess : IPlugin
    {
        /// <summary>
        /// The connection
        /// </summary>
        private DbConnection connection;

        /// <summary>
        /// The command
        /// </summary>
        private DbCommand command;

        /// <summary>
        /// The adapter
        /// </summary>
        private DbDataAdapter adapter;

        /// <summary>
        /// Gets or sets the data category.
        /// </summary>
        /// <value>
        /// The data category.
        /// </value>
        public DataCategory DataCategory { get; set; }

        /// <summary>
        /// Gets or sets the conection string.
        /// </summary>
        /// <value>
        /// The conection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the query string.
        /// </summary>
        /// <value>
        /// The query string.
        /// </value>
        public string QueryValue { get; set; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>connection</returns>
        public T GetConnection<T>() where T : DbConnection
        {
            CreateConection();
            return (T)connection;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>command</returns>
        public T GetCommand<T>() where T : DbCommand
        {
            CreateCommand();
            return (T)command;
        }

        /// <summary>
        /// Gets the adapter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>adapter</returns>
        public T GetAdapter<T>() where T : DbDataAdapter
        {
            return (T)adapter;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <exception cref="DataBaseException">Exception occured while fetching the data.</exception>
        public DataSet GetData()
        {
            DataSet dataSet = new DataSet();
			dataSet.Locale = CultureInfo.CurrentCulture;
            try
            {
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        CreateDataAdapter();
                        GetAdapter<SqlDataAdapter>().SelectCommand = GetCommand<SqlCommand>();
                        GetAdapter<SqlDataAdapter>().Fill(dataSet);
                        break;

                    case DataCategory.MSExcel:
                        CreateDataAdapter();
                        GetAdapter<OleDbDataAdapter>().SelectCommand = GetCommand<OleDbCommand>();
                        GetAdapter<OleDbDataAdapter>().Fill(dataSet);
                        break;

                    case DataCategory.VSFoxPro:
                        CreateDataAdapter();
                        GetAdapter<OleDbDataAdapter>().SelectCommand = GetCommand<OleDbCommand>();
                        GetAdapter<OleDbDataAdapter>().Fill(dataSet);
                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while fetching the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
            return dataSet;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>data</returns>
        public IList<T> GetData<T>()
        {
            List<T> data = null;
            CreateDataAdapter();
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    using (connection)
                    {
                        SqlDataReader reader = GetCommand<SqlCommand>().ExecuteReader();
                        data = CreateList<T>(reader);
                        return data;
                    }
                    break;

                case DataCategory.MSExcel:
                    using (connection)
                    {
                        OleDbDataReader reader = GetCommand<OleDbCommand>().ExecuteReader();
                        data = CreateList<T>(reader);
                        return data;

                    }
                    break;

            }

            return data;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <exception cref="DataBaseException">Exception occured while updating the data.</exception>
        public void ExecuteCommand()
        {
            try
            {
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        CreateDataAdapter();
                        GetCommand<SqlCommand>().ExecuteNonQuery();
                        //GetAdapter<SqlDataAdapter>().Fill(aDataSet);
                        break;

                    case DataCategory.MSExcel:
                        CreateDataAdapter();
                        GetCommand<OleDbCommand>().ExecuteNonQuery();
                        //GetAdapter<OleDbDataAdapter>().Fill(aDataSet);
                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while updating the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DataBaseException">Exception occured while updating the data.</exception>
        public void ExecuteCommand(Action<DbCommand> command)
        {
            try
            {
                CreateDataAdapter();
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        using (connection)
                        {
                            SqlCommand cmd = GetCommand<SqlCommand>();
                            command(cmd);
                            cmd.ExecuteNonQuery();
                        }

                        break;

                    case DataCategory.MSExcel:
                        using (connection)
                        {
                            OleDbCommand cmd = GetCommand<OleDbCommand>();
                            command(cmd);
                            cmd.ExecuteNonQuery();
                        }

                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while updating the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>data</returns>
        public IList<T> GetData<T>(Action<DbCommand> command)
        {
            List<T> data = null;
            CreateDataAdapter();
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    using (connection)
                    {
                        SqlCommand cmd = GetCommand<SqlCommand>();
                        command(cmd);
                        SqlDataReader reader = cmd.ExecuteReader();
                        data = CreateList<T>(reader);
                        return data;
                    }
                    break;

                case DataCategory.MSExcel:
                    using (connection)
                    {
                        OleDbCommand cmd = GetCommand<OleDbCommand>();
                        command(cmd);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        data = CreateList<T>(reader);
                        return data;
                    }
                    break;

            }

            return data;
        }

        /// <summary>
        /// Inserts the data.
        /// </summary>
        /// <returns>true or false</returns>
        /// <exception cref="DataBaseException">Exception occured while Inserting the data.</exception>
        public bool InsertData()
        {
            try
            {
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        CreateDataAdapter();
                        GetCommand<SqlCommand>().ExecuteNonQuery();
                        //command = new SqlCommand(QueryString, GetConnection<SqlConnection>());
                        //command.ExecuteNonQuery();
                        break;

                    case DataCategory.MSExcel:
                        CreateDataAdapter();
                        GetCommand<OleDbCommand>().ExecuteNonQuery();
                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while Inserting the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
            return true;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <returns>DataTable</returns>
        /// <exception cref="DataBaseException">Exception occured while fetching the data.</exception>
        public DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.CurrentCulture;
			try
            {
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        CreateDataAdapter();
                        GetAdapter<SqlDataAdapter>().SelectCommand = GetCommand<SqlCommand>();
                        GetAdapter<SqlDataAdapter>().Fill(dataTable);
                        break;

                    case DataCategory.MSExcel:
                        CreateDataAdapter();
                        GetAdapter<OleDbDataAdapter>().SelectCommand = GetCommand<OleDbCommand>();
                        GetAdapter<OleDbDataAdapter>().Fill(dataTable);
                        break;

                    case DataCategory.VSFoxPro:
                        CreateDataAdapter();
                        GetAdapter<OleDbDataAdapter>().SelectCommand = GetCommand<OleDbCommand>();
                        GetAdapter<OleDbDataAdapter>().Fill(dataTable);
                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while fetching the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
            return dataTable;
        }
        /// <summary>
        /// Update data in DB
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="DataBaseException">Exception occured while updating the data.</exception>
        public void UpdateData(string query)
        {
            QueryValue = query;

            try
            {
                switch (DataCategory)
                {
                    case DataCategory.SQLDB:
                        CreateDataAdapter();
                        GetCommand<SqlCommand>().ExecuteNonQuery();
                        //GetAdapter<SqlDataAdapter>().Fill(aDataSet);
                        break;

                    case DataCategory.MSExcel:
                        CreateDataAdapter();
                        GetCommand<OleDbCommand>().ExecuteNonQuery();
                        //GetAdapter<OleDbDataAdapter>().Fill(aDataSet);
                        break;
                }
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while updating the data.", e);
            }
            finally
            {
                connection.Close();
                connection = null;
                command = null;
                adapter = null;
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="query">The qurery string.</param>
        /// <returns>DataSet</returns>
        public DataSet GetData(string query)
        {
            QueryValue = query;
            return GetData();
        }

        /// <summary>
        /// iNSERTS the data.
        /// </summary>
        /// <param name="query">The qurery string.</param>
        /// <returns>true or false</returns>
        public bool InsertData(string query)
        {
            QueryValue = query;
            return InsertData();
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="query">The qurery string.</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string query)
        {
            QueryValue = query;
            return GetDataTable();
        }

        /// <summary>
        /// Datas the rows.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns>Rows</returns>
        public DataRowCollection DataRows(string queryString)
        {
            return GetData(queryString).Tables[0].Rows;
        }

        /// <summary>
        /// Gets the data record.
        /// </summary>
        /// <returns>DataRecord</returns>
        public IEnumerable<IDataRecord> GetDataRecord()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:

                    CreateDataAdapter();
                    SqlDataReader Reader;

                    Reader = GetCommand<SqlCommand>().ExecuteReader();
                    {
                        while (Reader.Read())
                        {
                            yield return Reader;
                        }
                    }
                    break;
                case DataCategory.MSExcel:
                    CreateDataAdapter();
                    using (OleDbDataReader reader = GetCommand<OleDbCommand>().ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader;
                        }
                    }
                    break;

                case DataCategory.VSFoxPro:
                    CreateDataAdapter();
                    using (OleDbDataReader reader = GetCommand<OleDbCommand>().ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader;
                        }
                    }
                    break;
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection = null;
            }

            if (adapter != null)
            {
                adapter = null;
            }
        }

        /// <summary>
        /// Creates the conection.
        /// </summary>
        private void CreateConection()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    connection = new SqlConnection(ConnectionString);
                    connection.Open();
                    break;

                case DataCategory.MSExcel:
                    connection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                    connection.Open();
                    break;

                case DataCategory.VSFoxPro:
                    connection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                    connection.Open();
                    break;
            }
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        private void CreateDataAdapter()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    adapter = new SqlDataAdapter();
                    break;

                case DataCategory.MSExcel:
                    adapter = new OleDbDataAdapter();
                    break;

                case DataCategory.VSFoxPro:
                    adapter = new OleDbDataAdapter();
                    break;
            }
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        private void CreateCommand()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    command = new SqlCommand(QueryValue, GetConnection<SqlConnection>());
                    break;

                case DataCategory.MSExcel:
                    command = new OleDbCommand(QueryValue, GetConnection<OleDbConnection>());
                    break;

                case DataCategory.VSFoxPro:
                    command = new OleDbCommand(QueryValue, GetConnection<OleDbConnection>());
                    break;

            }
        }

        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>list</returns>
        private static List<T> CreateList<T>(DbDataReader reader)
        {
            List<T> list = new List<T>();
            Func<object[], T> converter = GetConverter<T>((IDataRecord)reader, 0);
            while (reader.Read())
                list.Add(converter(GetValues((IDataRecord)reader)));
            return list;
        }

        /// <summary>
        /// Gets the converter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns>Object</returns>
        private static Func<object[], T> GetConverter<T>(IDataRecord record, int index)
        {
            Type t = typeof(T);
            if (1 == record.FieldCount - index && record.GetFieldType(index) == t)
                return (Func<object[], T>)(o => (T)o[index]);
            //if (typeof(object[]) == t)
            //{
            //    if (index == 0)
            //        return (Func<object[], T>)(o => (T)o);
            //    return (Func<object[], T>)(o =>
            //    {
            //        object[] objArray = new object[o.Length - index];
            //        Array.Copy((Array)o, index, (Array)objArray, 0, objArray.Length);
            //        return (T)objArray;
            //    });
            //}
            ConstructorInfo ci = GetConstructor(t, record, index);
            if (index == 0)
                return (Func<object[], T>)(o => (T)ci.Invoke(o));
            return (Func<object[], T>)(o =>
            {
                object[] parameters = new object[o.Length - index];
                Array.Copy((Array)o, index, (Array)parameters, 0, parameters.Length);
                return (T)ci.Invoke(parameters);
            });
        }

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns>constructorInfo</returns>
        /// <exception cref="System.Data.DataException"></exception>
        private static ConstructorInfo GetConstructor(Type t, IDataRecord record, int index)
        {
            ConstructorInfo constructorInfo = Array.Find<ConstructorInfo>(t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), (Predicate<ConstructorInfo>)(c =>
            {
                ParameterInfo[] parameters = c.GetParameters();
                if (parameters.Length != record.FieldCount - index)
                    return false;
                for (int index1 = 0; index1 < parameters.Length; ++index1)
                {
                    Type enumType = parameters[index1].ParameterType;
                    Type fieldType = record.GetFieldType(index1 + index);
                    Type[] genericArguments;
                    if (enumType.IsGenericType && typeof(Nullable<>) == enumType.GetGenericTypeDefinition() && ((genericArguments = enumType.GetGenericArguments()) != null && genericArguments.Length > 0))
                        enumType = genericArguments[0];
                    if (!enumType.IsAssignableFrom(fieldType) && (!enumType.IsEnum || !Enum.GetUnderlyingType(enumType).IsAssignableFrom(fieldType)))
                        return false;
                }
                return true;
            }));
            if ((ConstructorInfo)null != constructorInfo)
                return constructorInfo;
            throw new DataException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Unable to find constructor for type {0}. Available constructors do not match source. Source contains fields of type ({1}).", new object[2]
            {
                (object) t.Name,
                (object) string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<Type, string>((IEnumerable<Type>) GetTypes(record), (Func<Type, string>) (ty => ty.Name))))
            }));
        }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>typeArray</returns>
        private static Type[] GetTypes(IDataRecord record)
        {
            Type[] typeArray = new Type[record.FieldCount];
            for (int i = 0; i < record.FieldCount; ++i)
                typeArray[i] = record.GetFieldType(i);
            return typeArray;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>values</returns>
        private static object[] GetValues(IDataRecord record)
        {
            object[] values = new object[record.FieldCount];
            record.GetValues(values);
            for (int index = 0; index < values.Length; ++index)
            {
                if (DBNull.Value == values[index])
                    values[index] = (object)null;
            }
            return values;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return "DataAccess Plugin";
            }
            set
            {
                Description = value;
            }
        }

    }
}
