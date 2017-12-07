// ***********************************************************************
// <copyright file="ExcelReader.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ExcelReader class</summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using log4net;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;

namespace AuScGen.CommonUtilityPlugin
{
	/// <summary>
	/// Class ExcelReader.
	/// </summary>
	/// <seealso cref="Framework.IPlugin" />
	[Export(typeof(IPlugin))]
	public class ExcelReader : IPlugin
	{
		/// <summary>
		/// The logger
		/// </summary>
		private log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		/// <summary>
		/// The xssfworkbook
		/// </summary>
		static XSSFWorkbook xssfworkbook;
		/// <summary>
		/// The test data set
		/// </summary>
		static DataSet testDataSet;
		/// <summary>
		/// Loads the test data.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="sheetName">Name of the sheet.</param>
		/// <param name="testCaseId">The test case identifier.</param>
		/// <param name="columnNames">The column names.</param>
		/// <returns></returns>
		/// <exception cref="Framework.ResourceException"></exception>
		/// Need to make a singleton object for this everytime it is loading once the excel is readed then
		/// we have to reuse it somewhere....
		public object[] LoadTestData(string fileName, string sheetName, string testCaseId, string[] columnNames)
		{
			log4net.Config.XmlConfigurator.Configure();
			log4net.ThreadContext.Properties["myContext"] = "Logging from ExcelReader class";
			object[] objectArray = null;
			try
			{
				testDataSet = new DataSet();
				testDataSet.Locale = CultureInfo.CurrentCulture;
				InitializeWorkbook(fileName);
				XlsxToTableData(testCaseId, sheetName, columnNames);
				objectArray = DisplayData(testCaseId, testDataSet.Tables[0]);
			}
			catch (NullReferenceException e)
			{
				string errorMessage = string.Concat("You Have Either Specified  wrong Sheet name", "or the specified sheet name does not have data for the specified columns");
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				throw new ResourceException(methodName, errorMessage, e);
			}
			return objectArray;
		}

		/// <summary>
		/// Initializes the workbook.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <exception cref="Framework.ResourceException"></exception>
		private void InitializeWorkbook(string path)
		{
			using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				try
				{
					xssfworkbook = new XSSFWorkbook(file);
				}
				catch (NullReferenceException e)
				{
					string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
					throw new ResourceException(methodName, e);
				}
			}
		}

		/// <summary>
		/// XLSXs to table data.
		/// </summary>
		/// <param name="testCaseId">The test case identifier.</param>
		/// <param name="sheetName">Name of the sheet.</param>
		/// <param name="columnNames">The column names.</param>
		/// <exception cref="Framework.ResourceException"></exception>
		/// <exception cref="System.Exception">You Have Either Specified  wrong Sheet name
		/// + or the specified sheet name does not have data for the specified columns</exception>
		private void XlsxToTableData(string testCaseId, string sheetName, string[] columnNames)
		{
			ArrayList list = new ArrayList();
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.CurrentCulture;
			int sheetCount = xssfworkbook.NumberOfSheets;
			Logger.Info(string.Concat("Total Sheets found in the workbook are : [", sheetCount, "]"));
			ISheet sheet = null;
			//Get all sheets and based on passed sheet name get the sheet id
			for (int i = 0; i < sheetCount; i++)
			{
				if (xssfworkbook.GetSheetName(i).Equals(sheetName))
				{
					sheet = xssfworkbook.GetSheetAt(i);
					Logger.Info(string.Concat("User had passed Sheetname: [", sheetName, "]"));
					Logger.Info(string.Concat("Fetching the data for sheet : [", sheetName + "]"));
					break;
				}
			}
			//Get the column Header
			// sheet = xssfworkbook.GetSheetAt(0);
			Logger.Debug("Fetching the Test Data header information..");
			IRow headerRow = sheet.GetRow(0);
			if (null == headerRow)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				throw new ResourceException(methodName);
			}
			IEnumerator rows = sheet.GetRowEnumerator();
			//Get the column and row count
			int columnCount = headerRow.LastCellNum;
			int rowCount = sheet.LastRowNum;
			Logger.Info(string.Concat("Total Column count is : [", rowCount + "]"));
			Logger.Info(string.Concat("Total Row count is : [", columnCount + "]"));
			//Add the row data table
			for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
			{
				for (int requiredColumn = 0; requiredColumn < columnNames.Length; requiredColumn++)
				{
					if (headerRow.GetCell(columnIndex).ToString().Equals(columnNames[requiredColumn]))
					{
						list.Add(columnIndex);
						dataTable.Columns.Add(headerRow.GetCell(columnIndex).ToString());
					}
				}
			}

			//Skip reading the Header data
			//bool skipReadingHeaderRow = rows.MoveNext();
			while (rows.MoveNext())
			{
				IRow row = (XSSFRow)rows.Current;
				DataRow dataRow = dataTable.NewRow();
				foreach (int i in list)
				{
					ICell cell = row.GetCell(i);
					if (cell != null && row.GetCell(0).ToString().Equals(testCaseId))
					{
						dataRow[i] = cell.ToString();
					}
				}
				dataTable.Rows.Add(dataRow);
			}

			xssfworkbook = null;
			sheet = null;
			testDataSet.Tables.Add(dataTable);
		}

		/// <summary>
		/// Displays the data.
		/// </summary>
		/// <param name="testCaseId">The test case identifier.</param>
		/// <param name="table">The table.</param>
		/// <returns></returns>
		private object[] DisplayData(string testCaseId, DataTable table)
		{
			ArrayList list;
			ArrayList superList = new ArrayList();
			ArrayList superArray = new ArrayList();
			foreach (DataRow row in table.Rows)
			{
				list = new ArrayList();
				if (row[0].ToString().Equals(testCaseId))
				{
					for (int count = 0; count < table.Columns.Count; count++)
					{
						//if the test case id does not matches with the current TCId
						//Do not add to list

						list.Add(row[count].ToString());
					}
				}
				//If the list is not empty do not add the row
				if (list.Count > 0)
				{
					superList.Add(list);
				}
			}
			foreach (ArrayList colList in superList)
			{
				object[] myArr = (object[])colList.ToArray(typeof(string));
				superArray.Add(myArr);
			}
			object[] finalArray = (object[])superArray.ToArray(typeof(object));
			return finalArray;
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
				return "Excel Reader Plugin";
			}
			set
			{
				Description = value;
			}
		}

		/// <summary>
		/// Gets the data set.
		/// </summary>
		/// <param name="command">The string command.</param>
		/// <returns></returns>
		/// Remote the database related Method
		public DataSet GetData(string command)
		{
			using (SqlConnection con = new SqlConnection(string.Empty))
			{
				SqlDataAdapter da = new SqlDataAdapter();
				DataSet ds = new DataSet();
				ds.Locale = CultureInfo.CurrentCulture;
				con.Open();
				da = new SqlDataAdapter(command, con);
				da.Fill(ds);
				con.Close();
				con.Dispose();
				return ds;
			}
		}

		/// <summary>
		/// Finds the row exists.
		/// </summary>
		/// <param name="columnValue">The column value.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="strCommand">The string command.</param>
		/// <returns></returns>
		//public bool FindRowExists(string columnName, string columnValue, string strCommand)
		//{
		//    DataView dv = new DataView(GetData(strCommand).Tables[0]);
		//    DataTable dt = new DataTable();
		//    dv.RowFilter = columnName + " = '" + columnValue + "'";
		//    dt = dv.ToTable();
		//    for (int i = 0; i <= dt.Rows.Count; i++)
		//    {

		//    }
		//    if (dt.Rows.Count > 0)
		//    {
		//        return true;
		//    }
		//    else
		//    {
		//        return false;
		//    }
		//}
	}
}