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
using Framework;

namespace EDMC.DataAccess
{
    /// <summary>
    ///  Class DataCategory
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
        MSExcel
    }

    /// <summary>
    /// Class DataAccess.
    /// </summary>
    /// <seealso cref="Framework.IPlugin" />
    [Export(typeof(IPlugin))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DataAccess : IPlugin
    {
        #region Private Properties

        /// <summary>
        /// The connection
        /// </summary>
        private DbConnection Connection;

        /// <summary>
        /// The command
        /// </summary>
        private DbCommand Command;

        /// <summary>
        /// The adapter
        /// </summary>
        private DbDataAdapter Adapter;

        #endregion Private Properties

        #region Public Properties

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
        public string QueryString { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>Connection</returns>
        public T GetConnection<T>() where T : DbConnection
        {
            CreateConection();
            return (T)Connection;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>Command</returns>
        public T GetCommand<T>() where T : DbCommand
        {
            CreateCommand();
            return (T)Command;
        }

        /// <summary>
        /// Gets the adapter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>Adapter</returns>
        public T GetAdapter<T>() where T : DbDataAdapter
        {
            return (T)Adapter;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetData()
        {
            DataSet dataSet = new DataSet();

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
            }

            Connection.Close();
            Connection = null;
            Command = null;
            Adapter = null;

            return dataSet;
        }

        /// <summary>
        /// Gets the data record.
        /// </summary>
        /// <returns>Reader</returns>
        public IEnumerable<IDataRecord> GetDataRecord()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    CreateDataAdapter();

                    using (SqlDataReader Reader = GetCommand<SqlCommand>().ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            yield return Reader;
                        }
                    }
                    break;

                case DataCategory.MSExcel:
                    CreateDataAdapter();
                    using (OleDbDataReader Reader = GetCommand<OleDbCommand>().ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            yield return Reader;
                        }
                    }
                    break;
            }

            Connection.Close();
            Connection = null;
            Command = null;
            Adapter = null;
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
                    using (Connection)
                    {
                        SqlDataReader reader = GetCommand<SqlCommand>().ExecuteReader();
                        data = DataMapper.CreateList<T>(reader);
                        return data;
                    }
                    break;

                case DataCategory.MSExcel:
                    using (Connection)
                    {
                        OleDbDataReader reader = GetCommand<OleDbCommand>().ExecuteReader();
                        data = DataMapper.CreateList<T>(reader);
                        return data;
                    }
                    break;
            }

            return data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T">DbCommand</typeparam>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>data</returns>
        public IList<T> GetData<T, TCommand>(Action<TCommand> command) where TCommand : DbCommand
        {
            List<T> data = null;
            CreateDataAdapter();
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    using (Connection)
                    {
                        TCommand cmd = GetCommand<TCommand>();
                        command(cmd);
                        SqlDataReader reader = (SqlDataReader)cmd.ExecuteReader();
                        data = DataMapper.CreateList<T>(reader);
                        return data;
                    }
                    break;

                case DataCategory.MSExcel:
                    using (Connection)
                    {
                        TCommand cmd = GetCommand<TCommand>();
                        command(cmd);
                        OleDbDataReader reader = (OleDbDataReader)cmd.ExecuteReader();
                        data = DataMapper.CreateList<T>(reader);
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
                Connection.Close();
                Connection = null;
                Command = null;
                Adapter = null;
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <exception cref="DataBaseException">Exception occured while updating the data.</exception>
        public void ExecuteCommand<TCommand>(Action<TCommand> command) where TCommand : DbCommand
        {
            try
            {
                CreateDataAdapter();

                TCommand cmd = GetCommand<TCommand>();
                command(cmd);
                cmd.ExecuteNonQuery();
                //switch (DataCategory)
                //{
                //    case DataCategory.SQLDB:
                //        using (Connection)
                //        {
                //            TCommand cmd = GetCommand<TCommand>();
                //            command(cmd);
                //            cmd.ExecuteNonQuery();
                //        }

                //        break;

                //    case DataCategory.MSExcel:
                //        using (Connection)
                //        {
                //            OleDbCommand cmd = GetCommand<OleDbCommand>();
                //            command(cmd);
                //            cmd.ExecuteNonQuery();
                //        }

                //        break;
                //}
            }
            catch (DataException e)
            {
                throw new DataBaseException("Exception occured while updating the data.", e);
            }
            finally
            {
                Connection.Close();
                Connection = null;
                command = null;
                Adapter = null;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the conection.
        /// </summary>
        private void CreateConection()
        {
            switch (DataCategory)
            {
                case DataCategory.SQLDB:
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();
                    break;

                case DataCategory.MSExcel:
                    Connection = new System.Data.OleDb.OleDbConnection(ConnectionString);
                    Connection.Open();
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
                    Adapter = new SqlDataAdapter();
                    break;

                case DataCategory.MSExcel:
                    Adapter = new OleDbDataAdapter();
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
                    Command = new SqlCommand(QueryString, GetConnection<SqlConnection>());
                    break;

                case DataCategory.MSExcel:
                    Command = new OleDbCommand(QueryString, GetConnection<OleDbConnection>());
                    break;
            }
        }

        #endregion Private Methods

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}