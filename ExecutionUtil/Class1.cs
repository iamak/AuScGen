using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecutionUtil
{
    public class Utility
    {
        #region Variables
        public int rowNumber { get; set; }
        public string colName { get; set; }
        public string colValue { get; set; }
        public static Dictionary<string, SchemaValues> destinationDBDictionary = new Dictionary<string, SchemaValues>();
        public static List<string> DataTypeList = new List<string>();
        public static List<string> DataTypeLengthList = new List<string>();
        public static DataTable table;
        public static string destinationCountValue;
        public static string sourceCountValue;
        public static int dataTypeIndex = 0;
        public static List<string> lstDataType = new List<string>();
        public static int dataTypeLengthIndex = 0;
        public static List<string> lstDataTypeLength = new List<string>();
        public static List<string> lstTable = new List<string>();
        public static List<string> dataList = new List<string>();
        public static List<string> lstDataTypeWithoutLength = new List<string>();
        static List<Utility> dataCol = new List<Utility>();
        public static List<string> failedCountTables = new List<string>();
        #endregion

        #region Methods
        public static DataTable ExcelToDataTable(string fileName)
        {
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;
            if (stream.Name.ToLower().Contains("xlsx"))
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            //.xls

            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            DataTableCollection table = result.Tables;
            DataTable resultTable = table["Sheet1"];
            return resultTable;
        }

        public static void PopulateDataIncollection(string fileName)
        {
            table = ExcelToDataTable(fileName);
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Utility p1 = new Utility()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    dataCol.Add(p1);
                }
            }

        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                string data = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).Select(y => y.colValue).SingleOrDefault();
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<string> ReadDataList(string columnName)
        {
            try
            {
                var data = dataCol.Where(x => x.colName == columnName).Select(y => y.colValue).ToList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int CountRows(string columnName)
        {
            var count = dataCol.Where(x => x.colName == columnName).Count();
            return count;
        }

        public static void CreateHtml(string filePath)
        {
            StringBuilder sb = GetHtmlHeader();
            sb = Parser(sb, filePath);
            SaveHtml(sb);
        }

        private static StringBuilder GetHtmlHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Html >");

            sb.Append("<body onload = 'failcase()'  background=\"grey.png\" background-size='cover'>");
            // CreateChartScript(sb, 1.2, 2.2);

            sb.Append("<div id = 'pieData'></div>");

            sb.Append("<table><tr><td width='1300px'>");
            sb.Append("<img src='logo.png' align='middle' style='width:25%'>");
            sb.Append("</td><td>");
            sb.Append("<canvas align ='left' id=\"chart-area\" height=\"200\" width=\"400\"></canvas>");
            sb.Append("</td>");
            //sb.Append(string.Format("<td align='right' style='font-size:25;color:darkgreen;font-weight:bold;'>Pass: {0}%</td>", (1.0 / 2.0) * 100));
            sb.Append("<td align='right' id = 'passPercent' style='font-size:25;color:darkgreen;font-weight:bold;'></td>");
            sb.Append("</tr><tr><td></td><td align='right'>");
            sb.Append("<img align ='right' src='./img/Legends.png' />");
            sb.Append("</td></tr></table>");
            sb.Append("<div id = 'failResult'>");
            sb.Append("</div>");
            sb.Append("<br></br>");
            return sb;
        }

        public static void SaveHtml(StringBuilder sb)
        {
            string fileName = string.Format(@"{0}\{1}.html", Directory.GetCurrentDirectory(), "Reports");
            FileStream f = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            using (StreamWriter s = new StreamWriter(f))
                s.WriteLine(sb.ToString());
            if (File.Exists("Reports"))
            {
                Console.WriteLine("Report Generated");
            }
        }

        public static StringBuilder Parser(StringBuilder sb, string filePath)
        {

            string summaryTable = "<table border='1' style='width:100%'><tr><td align='center' colspan='100%' style='color: black;font-size:30;font-weight:Bold' bgcolor='sky blue'><b>Summary<b></td></tr>";
            //sb.Append("<table><tr><tr><tr><tr><tr><tr><tr><tr><tr><tr><td>");
            string[] lstfiles = Directory.GetFiles(filePath);
            List<string> disticntSchemaTable = new List<string>();

            if (lstfiles.Count() == 0)
            {
                MessageBox.Show("No Files found in the directory");
                Application.Exit();
            }
            foreach (var file in lstfiles)
            {
                dataTypeIndex = 0;
                lstDataType = new List<string>();
                dataTypeLengthIndex = 0;
                lstDataTypeLength = new List<string>();
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (file.ToLower().Contains("xlsx"))
                {
                    PopulateDataIncollection(string.Format(@"{0}\{1}.xlsx", filePath, fileName));
                }
                else
                {
                    PopulateDataIncollection(string.Format(@"{0}\{1}.xls", filePath, fileName));
                }
                SQLConnectionDestination();
                SQLConnectionSource();
                if (sourceCountValue == destinationCountValue)
                {
                    sb.Append("<table border='1' style='width:100%'><tr><td align='center' colspan='100%' style='color: black;font-size:20;font-weight:Bold' bgcolor='sky blue'>TOTAL COUNT VALIDATION</td></tr><tr><td align='center' style='color:green'>" + ReadData(24, "Value") + " (Expected :" + sourceCountValue + ". Actual :" + destinationCountValue + ") " + "</td></tr>");

                    sb.Append("</table>");
                }
                else
                {
                    sb.Append("<table border='1' style='width:100%'><tr><td align='center' colspan='100%' style='color: black;font-size:20;font-weight:Bold' bgcolor='sky blue'>TOTAL COUNT VALIDATION</td></tr><tr><td align='center' style='color:red'>" + ReadData(24, "Value") + " (Expected :" + sourceCountValue + ". Actual :" + destinationCountValue + ") " + "</td></tr>");
                    sb.Append("</table>");
                    failedCountTables.Add(ReadData(24, "Value"));

                }
                sb.Append("<table border='1' style='width:100%' id ='" + ReadData(24, "Value") + "'>");
                sb.Append("<tr>");
                sb.Append("<td align='center'  style='font-size:25px' id = '" + ReadData(24, "Value") + "' colspan='100%' bgcolor='sky blue'><b>" + ReadData(24, "Value") + "</b></td>");

                sb.Append("</tr>");
                sb.Append("<tr >");
                sb.Append("<th align ='center' bgcolor='sky blue' width='20px'><b>Column Names Validation</b></th>");
                string desExpectedColumns = ReadData(26, "Value");
                string[] expectedColumns = desExpectedColumns.Split(':');
                int inc = 0;
                List<string> lstDatatypeWithLength = new List<string>();
                for (int j = 0; j < expectedColumns.Count(); j++)
                {
                    var columnName = expectedColumns[j].Split('|');

                    lstDatatypeWithLength.Add(columnName[0]);
                    inc = 1;
                }
                lstDatatypeWithLength.RemoveAt(0);
                for (int k = 0; k < lstDatatypeWithLength.Count; k++)
                {
                    if (lstDatatypeWithLength[k].Contains("char"))
                    {
                        var dataType = lstDatatypeWithLength[k].Split('(');
                        lstDataTypeWithoutLength.Add(dataType[0]);
                        var dataLength = dataType[1].Substring(0, dataType[1].Length - 1);
                        dataList.Add(dataLength);
                    }
                    else
                    {
                        lstDataTypeWithoutLength.Add(lstDatatypeWithLength[k]);
                        dataList.Add("");
                    }
                }
                ColumnValidations(sb);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th align ='center'  bgcolor='sky blue' width='20px' ><b>Data type validation</b></th>");
                DataTypeValidations(sb);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th align ='center'  bgcolor='sky blue' width='20px'><b>Data Type length validation</b></th>");
                DataTypeLengthValidations(sb);
                disticntSchemaTable = lstTable.Distinct().ToList();
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br></br><br></br><br></br><br></br><br></br><br></br>");
                dataCol.Clear();
                destinationDBDictionary.Clear();
                //lstTable.Clear();
                lstDataTypeWithoutLength.Clear();
                dataList.Clear();
            }
            
            var removeItems = failedCountTables.Concat(disticntSchemaTable);
            var distinctSchemaCountTable = removeItems.Distinct().ToList();
            summaryTable = summaryTable + "<tr><td align ='center' colspan='100%' style='color: Blue;font-size:20;font-weight:Bold'> Total No of Tables Executed : " + lstfiles.Count() + "</td></tr><tr><td align='center'  colspan='100%' style='color: Green;font-size:20;font-weight:Bold'> No of Tables Passed : " + (lstfiles.Count() - distinctSchemaCountTable.Count) + " </td></tr><tr><td align ='center' colspan='100%' style='color: Red;font-size:20;font-weight:Bold'> No of Tables Failed : " + distinctSchemaCountTable.Count + " </td> </tr>";
            summaryTable = summaryTable + "<tr><td align ='center' width ='20%'height='50%' bgcolor='sky blue' width='auto'><b>Failed Schema Tables</b></td>";
            foreach (var failedTable in disticntSchemaTable)
            {
                summaryTable = summaryTable + "<td align ='center' ><a href = #" + failedTable + " >" + failedTable + " </a> </td> ";
            }
            if (disticntSchemaTable.Count == 0)
            {
                summaryTable = summaryTable + "<td align ='center' colspan='100%'><b> N/A </b></td> ";
            }
            summaryTable = summaryTable + "</tr>";
            summaryTable = summaryTable + "<tr><td align ='center' width ='20%'height='50%' bgcolor='sky blue' width='auto' ><b>Failed Count Tables</b></td>";
            foreach (var failedTable in failedCountTables)
            {
                summaryTable = summaryTable + "<td align ='center' ><a href = #" + failedTable + " >" + failedTable + " </a> </td> ";
            }
            if (failedCountTables.Count == 0)
            {
                summaryTable = summaryTable + "<td align ='center' colspan='100%'><b> N/A </b></td> ";
            }
            summaryTable = summaryTable + "</tr></table><br></br><br></br><br></br><br></br><br></br>";

          //  var pieChartData = "<table><tr><td width='1300px'><img src='logo.png' align='middle' style='width:25%'></td><td><canvas align ='left' id=\"chart-area\" height=\"200\" width=\"400\"></canvas></td>" + string.Format("<td align='right' style='font-size:25;color:darkgreen;font-weight:bold;'>Pass: {0}%</td>", ((lstfiles.Count() - distinctSchemaCountTable.Count) / lstfiles.Count()) * 100) + "</tr><tr><td></td><td align='right'><img align ='right' src='./img/Legends.png' /></td></tr></table>";

            sb.Append("</body>");
            sb.Append("<head>");
            CreateChartScript(sb, distinctSchemaCountTable.Count, lstfiles.Count() - distinctSchemaCountTable.Count);
            sb.Append("<script src=\"./js/Chart.js\"></script>");
        
            sb.Append("<script>");
            sb.Append("function failcase() { document.getElementById('passPercent').innerHTML = \" Pass: " + Math.Round((Convert.ToDecimal(lstfiles.Count() - distinctSchemaCountTable.Count) / Convert.ToDecimal(lstfiles.Count())) * 100, 2) + " %\"  ; document.getElementById('failResult').innerHTML = \"" + summaryTable + "\";  var ctx = document.getElementById(\"chart-area\").getContext(\"2d\");  window.myPie = new Chart(ctx).Pie(pieData);  } ");
            sb.Append("</script>");
            sb.Append("</head>");
            sb.Append("</html>");
            return sb;
        }

        public static bool ReadDataCoulmn(string vals)
        {
            var dataCount = destinationDBDictionary.Where(x => x.Key.Equals(vals)).Count();
            if (dataCount == 1)
            {
                var dataType = destinationDBDictionary.Where(x => x.Key.Equals(vals)).Select(x => x.Value.DataType).SingleOrDefault();
                lstDataType.Add(dataType + dataTypeIndex.ToString("00"));

                var datatypeLength = destinationDBDictionary.Where(x => x.Key.Equals(vals)).Select(x => x.Value.DataTypeLength).FirstOrDefault();
                if (datatypeLength == "")
                {
                    datatypeLength = "Null" + datatypeLength;
                }
                lstDataTypeLength.Add(datatypeLength + dataTypeLengthIndex.ToString("00"));

                dataTypeIndex++;
                dataTypeLengthIndex++;
                return true;
            }
            else
            {
                dataTypeIndex++;
                dataTypeLengthIndex++;
                return false;
            }

        }

        public static void SQLConnectionDestination()
        {
            SqlCommand destinationSchemaCommand = new SqlCommand();
            SqlCommand destinationCountCommand = new SqlCommand();
            destinationSchemaCommand.CommandTimeout = 6000;
            destinationCountCommand.CommandTimeout = 6000;
            SchemaValues schema = null;
            destinationSchemaCommand.Connection = new SqlConnection(@"Data Source=" + ReadData(9, "Value") + ";" + "" + "Initial Catalog=" + ReadData(10, "Value") + "; Trusted_Connection=Yes");
            destinationCountCommand.Connection = new SqlConnection(@"Data Source=" + ReadData(9, "Value") + ";" + "" + "Initial Catalog=" + ReadData(10, "Value") + "; Trusted_Connection=Yes");
            destinationSchemaCommand.CommandText = "select distinct COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH from " + ReadData(10, "Value") + ".INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + ReadData(24, "Value") + "'";
            destinationCountCommand.CommandText = ReadData(14, "Value");
            destinationSchemaCommand.Connection.Open();
            destinationCountCommand.Connection.Open();
            SqlDataReader destinationSchema = destinationSchemaCommand.ExecuteReader();
            SqlDataReader destinationCount = destinationCountCommand.ExecuteReader();
            while (destinationCount.Read())
            {
                destinationCountValue = destinationCount[0].ToString();
            }

            while (destinationSchema.Read())
            {
                schema = new SchemaValues();
                schema.DataType = destinationSchema["DATA_TYPE"].ToString();
                schema.DataTypeLength = destinationSchema["CHARACTER_MAXIMUM_LENGTH"].ToString();
                destinationDBDictionary.Add(destinationSchema["COLUMN_NAME"].ToString(), schema);
            }
            destinationSchemaCommand.Connection.Close();
            destinationCountCommand.Connection.Close();

        }

        public static void SQLConnectionSource()
        {
            SqlCommand sourceCommand = new SqlCommand();
            sourceCommand.CommandTimeout = 6000;
            sourceCommand.Connection = new SqlConnection(@"Data Source=" + ReadData(9, "Value") + ";" + "" + "Initial Catalog=" + ReadData(10, "Value") + "; Trusted_Connection=Yes");
            sourceCommand.CommandText = ReadData(13, "Value");
            sourceCommand.Connection.Open();
            SqlDataReader sourceCount = sourceCommand.ExecuteReader();
            SchemaValues schema = null;
            while (sourceCount.Read())
            {
                sourceCountValue = sourceCount[0].ToString();
            }
            sourceCommand.Connection.Close();
        }

        public static void ColumnValidations(StringBuilder sb)
        {
            string desValues = ReadData(25, "Value");
            string[] destinationColumns = desValues.Split(',');
            List<string> listColumnName = new List<string>();
            for (int i = 0; i < destinationColumns.Count(); i++)
            {
                var columnNames = destinationColumns[i];
                listColumnName.Add(columnNames.Trim());
            }
            Console.WriteLine("Column names validation\n");
            for (int x = 0; x < listColumnName.Count; x++)
            {

                if (ReadDataCoulmn(listColumnName[x]))
                {
                    sb.Append("<td align ='center' style = 'color:Green' width='100px' >" + listColumnName[x] + "</td>");
                }
                else
                {
                    lstTable.Add(ReadData(24, "Value"));
                    sb.Append("<td align ='center' width='100px' style = 'color:Red' >" + listColumnName[x] + " Column Not found" + "</td>");
                }
            }
        }

        private static void CreateChartScript(StringBuilder sb, double failResultsCount, double passResultsCount)
        {
            sb.AppendLine("<script>");
            sb.AppendLine("var pieData = [");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("value: {0},", failResultsCount.ToString()));
            sb.AppendLine("color:\"#FF0000\",");
            sb.AppendLine("highlight: \"#FF5A5E\",");
            sb.AppendLine("label: \"FAIL\"");
            sb.AppendLine("},");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("value: {0},", passResultsCount));
            sb.AppendLine("color: \"#2CF808\",");
            sb.AppendLine("highlight: \"#34E555\",");
            sb.AppendLine("label: \"PASS\"");
            sb.AppendLine("},");
            sb.AppendLine("];");
            sb.AppendLine("</script>");
        }

        public static void DataTypeValidations(StringBuilder sb)
        {
            List<string> splitIndexDatatype = new List<string>();
            foreach (var split in lstDataType)
            {
                var splitString = split.Substring(split.Length - 2);
                splitIndexDatatype.Add(splitString.ToString());
            }
            Console.WriteLine("\nData type validation\n");

            for (int l = 0; l < lstDataTypeWithoutLength.Count; l++)
            {
                bool fflag = true;
                for (int z = 0; z < splitIndexDatatype.Count; z++)
                {
                    if (l == Int32.Parse(splitIndexDatatype[z]))
                    {
                        if (lstDataTypeWithoutLength[Int32.Parse(splitIndexDatatype[z])].ToLower() == lstDataType[z].Substring(0, lstDataType[z].Length - 2).ToLower())
                        {
                            sb.Append("<td align ='center' style = 'color:Green' width='100px'>" + lstDataTypeWithoutLength[Int32.Parse(splitIndexDatatype[z])].ToLower() + "</td>");

                        }
                        else
                        {
                            lstTable.Add(ReadData(24, "Value"));
                            sb.Append("<td align ='center' width='100px' style = 'color:Red' >" + "Actual : " + lstDataType[z].Substring(0, lstDataType[z].Length - 2) + "    Expected : " + lstDataTypeWithoutLength[Int32.Parse(splitIndexDatatype[z])].ToLower() + "</td>");
                        }
                        fflag = false;
                    }
                }
                if (fflag)
                {
                    lstTable.Add(ReadData(24, "Value"));
                    sb.Append("<td align ='center' width='100px' style = 'color:Red' >" + "N/A" + "</td>");
                }
            }
        }

        public static void DataTypeLengthValidations(StringBuilder sb)
        {
            List<string> splitIndexDatatypeLength = new List<string>();
            foreach (var split in lstDataTypeLength)
            {
                var splitStringOne = split.Substring(split.Length - 2);
                splitIndexDatatypeLength.Add(splitStringOne.ToString());
            }
            for (int l = 0; l < dataList.Count; l++)
            {
                bool fflag = true;
                for (int z = 0; z < splitIndexDatatypeLength.Count; z++)
                {
                    if (l == Int32.Parse(splitIndexDatatypeLength[z]))
                    {
                        if (dataList[Int32.Parse(splitIndexDatatypeLength[z])] == "")
                        {

                            dataList[Int32.Parse(splitIndexDatatypeLength[z])] += dataList[Int32.Parse(splitIndexDatatypeLength[z])] + "Null";
                        }

                        if (dataList[Int32.Parse(splitIndexDatatypeLength[z])] == lstDataTypeLength[z].Substring(0, lstDataTypeLength[z].Length - 2))
                        {

                            if (dataList[Int32.Parse(splitIndexDatatypeLength[z])] == "Null")
                            {
                                sb.Append("<td align ='center' style = 'color:Green' width='100px'>Null</td>");
                            }
                            else
                            {
                                sb.Append("<td align ='center' style = 'color:Green' >" + dataList[Int32.Parse(splitIndexDatatypeLength[z])] + "</td>");
                            }

                        }
                        else
                        {
                            lstTable.Add(ReadData(24, "Value"));
                            sb.Append("<td align ='center' width='100px' style = 'color:Red' >" + "Actual : " + lstDataTypeLength[z].Substring(0, lstDataTypeLength[z].Length - 2) + "   Expected : " + dataList[Int32.Parse(splitIndexDatatypeLength[z])] + "</td>");
                        }

                        fflag = false;
                    }
                }
                if (fflag)
                {
                    sb.Append("<td align ='center' width='100px' style = 'color:Red' >" + "N/A" + "</td>");
                }

            }
        }
        #endregion
    }
}
