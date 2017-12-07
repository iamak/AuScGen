using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecolab.MigrationTest
{
    public class GetTestReport
    {
        public void GenerateMigrationTestReport(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head></head><body>");
            sb.Append("<table width='1000px'  align='center' style='border:1px solid #95bff7; text-align:center' cellspacing=0px>");

            ////DataBase and Table names....
            sb.Append("<tr style='background-color:#736F6E;height:35px'>");
            sb.Append("<td align='center' colspan='5' style='border:1px solid #95bff7; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#ffffff ;font-family:Arial'>Source Database Migration Testing Report Summary</td>");
            sb.Append("</tr>");

            sb.Append(GenerateReportSummaryNew(dataEntity));
            sb.Append(GenerateSourcePassResults(dataEntity));

            sb.Append("</table>");
            sb.Append("</body></html>");
            FileStream fs = new FileStream(".\\TestResults\\" + dataEntity.TestParams.TestName + ".html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sb.ToString());
            sw.Close();
            sb.Clear();
        }

        public void GenerateMigrationTestFullReport(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head></head><body>");
            sb.Append("<table width='1000px'  align='center' style='border:1px solid #95bff7; text-align:center' cellspacing=0px>");

            //DataBase and Table names....
            sb.Append("<tr style='background-color:#736F6E;height:35px'>");
            sb.Append("<td align='center' colspan='4' style='border:1px solid #95bff7; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#ffffff ;font-family:Arial'> Migration Report Summary and Results</td>");
            sb.Append("</tr>");

            sb.Append(GenerateReportSummary(dataEntity));
            sb.Append(GenerateSourceTargetTableData(dataEntity));
            sb.Append(GenerateMatchingData(dataEntity));
            sb.Append(GenerateMissMatchingData(dataEntity));

            sb.Append("</table>");
            sb.Append("</body></html>");
            FileStream fs = new FileStream(".\\TestResults\\" + dataEntity.TestParams.TestName + ".html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sb.ToString());
            sw.Close();
            sb.Clear();
        }

        public StringBuilder GenerateReportSummary(CompareData dataEntity)
        {
            // Migration report summary
            StringBuilder sb = new StringBuilder();
            //-------- Table Name printing here need to pass......DBName and Table Name
            sb.Append("<tr style='background-color:#B6B6B4;height:35px'>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'> " + dataEntity.TestParams.SourceDBName + " DB - " + dataEntity.TestParams.SourceTableName + " table summary </td>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'>" + dataEntity.TestParams.TargetDBName + " DB - " + dataEntity.TestParams.TargetTableName + " table summary </td>");
            sb.Append("</tr>");

            // Migration report summary
            sb.Append("<tr style='background-color:#CCFFFF;height:35px'>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'> " + dataEntity.TestParams.SourceTableName + " table total records </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.SourceTableActualRecordsCount + "</td>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'> " + dataEntity.TestParams.TargetTableName + " table total records </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.TargetTableActualRecordsCount + "</td></tr>");

            sb.Append("<tr style='background-color:#CCFFFF;height:35px'>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'>  " + dataEntity.TestParams.SourceTableName + " table total matching records </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.SourceTableMatchingRecordsCount + "</td>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'> " + dataEntity.TestParams.TargetTableName + "table total matching records </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.TargetTableMatchingRecordsCount + "</td></tr>");

            sb.Append("<tr style='background-color:#CCFFFF;height:35px'>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'>  " + dataEntity.TestParams.SourceTableName + " table total Miss-matching rows </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.SourceTableMissMatchRecordsCount + "</td>");
            sb.Append("<td align='left' style='border:1px solid #95bff7; font-size:16px; width:350px;'>" + dataEntity.TestParams.TargetTableName + " table total Miss-matching rows </td>");
            sb.Append("<td align='center' style='border:1px solid #95bff7; font-size:16px; width:150px;'>" + dataEntity.TargetTableMissMatchRecordsCount + "</td></tr>");
            return sb;
        }

        public StringBuilder GenerateSourceTargetTableData(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            //-------- Table Name printing here need to pass......DBName and Table Name
            sb.Append("<tr style='background-color:#B6B6B4;height:35px'>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'>" + dataEntity.TestParams.SourceTableName + " Table Results </td>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'>" + dataEntity.TestParams.TargetTableName + " Table Results </td>");
            sb.Append("</tr>");

            //Actual data binding....
            sb.Append("<tr>");
            //Actual data Source table - columns and rows are printing......
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            sb.Append("<tr style='background-color:#B6B6B4;'>");
            foreach (DataColumn column in dataEntity.SourceTableActualData.Columns)
            {
                sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; font-weight:bold; width:120px; color:#000000 ;font-family:Arial;'>"
                    + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
            }
            sb.Append("</tr>");
            foreach (DataRow row in dataEntity.SourceTableActualData.Rows) // Loop over the rows.
            {
                sb.Append("<tr style='background-color:#CCFFFF;'>");
                foreach (var item in row.ItemArray) // Loop over the items.
                {
                    sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>"
                        + item + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            //--- Actual Data Target table- columns and rows are printing......
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            sb.Append("<tr style='background-color:#B6B6B4;'>");
            foreach (DataColumn column in dataEntity.TargetTableActualData.Columns)
            {
                sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px;font-weight:bold; width:120px;color:#000000 ;font-family:Arial;'>"
                    + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
            }
            sb.Append("</tr>");
            foreach (DataRow row in dataEntity.TargetTableActualData.Rows) // Loop over the rows.
            {
                sb.Append("<tr style='background-color:#CCFFFF;'>");
                foreach (var item in row.ItemArray) // Loop over the items.
                {
                    sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>"
                        + item + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            return sb;
        }

        public StringBuilder GenerateMatchingData(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            //Matching Data from both source and destination tables.....
            sb.Append("<tr style='background-color:#736F6E; height:35px'>");
            sb.Append("<td align='center' colspan='4' style='border:1px solid #95bff7; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#ffffff;font-family:Arial'>Matching Results of Source and Target Tables.</td>");
            sb.Append("</tr>");

            //-------- Table Name printing here need to pass......Table Name
            sb.Append("<tr style='background-color:#B6B6B4;height:35px'>");
            sb.Append("<td align='center'colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000;font-family:Arial'>" + dataEntity.TestParams.SourceTableName + " Table Matching Results </td>");
            sb.Append("<td align='center'colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000;font-family:Arial'>" + dataEntity.TestParams.TargetTableName + " Table Matching Results </td>");
            sb.Append("</tr>");

            //--- Matching data from source table
            sb.Append("<tr>");
            //Actual data Source table - columns and rows are printing......
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            if (dataEntity.SourceTableMatchingRecords != null)
            {
                if (dataEntity.SourceTableMatchingRecords.Rows.Count > 0)
                {
                    sb.Append("<tr style='background-color:#B6B6B4;'>");
                    foreach (DataColumn column in dataEntity.SourceTableMatchingRecords.Columns)
                    {
                        sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px; font-weight:bold; color:#000000;font-family:Arial;'>"
                            + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dataEntity.SourceTableMatchingRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C; text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>"
                                + item + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
            }
            else
            {
                sb.Append("<tr style='background-color:#254117;text-align:center;'>");
                sb.Append("Data not found from source table</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            //--- Matching data from target table
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            if (dataEntity.TargetTableMatchingRecords != null)
            {
                if (dataEntity.TargetTableMatchingRecords.Rows.Count > 0)
                {
                    sb.Append("<tr style='background-color:#B6B6B4;'>");
                    foreach (DataColumn column in dataEntity.TargetTableMatchingRecords.Columns)
                    {
                        sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;font-weight:bold; color:#000000 ;font-family:Arial;'>"
                            + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
                    }
                    sb.Append("</tr>");

                    foreach (DataRow row in dataEntity.TargetTableMatchingRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>"
                                + item + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
            }
            else
            {
                sb.Append("<tr style='background-color:#254117;text-align:center;'>");
                sb.Append("Data not found from target table</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            return sb;
        }

        public StringBuilder GenerateMissMatchingData(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            // Miss-Matching Data from Source and Target Table....
            sb.Append("<tr style='background-color:#736F6E;height:35px'>");
            sb.Append("<td align='center' colspan='4' style='border:1px solid #95bff7; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#ffffff;font-family:Arial'>Miss-Matching Results of Source and Target Tables.</td>");
            sb.Append("</tr>");

            //-------- Table Name printing here need to pass......Table Name
            sb.Append("<tr style='background-color:#B6B6B4;height:35px'>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'>" + dataEntity.TestParams.SourceTableName + "Table Miss-Matching Results </td>");
            sb.Append("<td align='center' colspan='2' style='border:1px solid #95bff7; text-align:center; font-size:16px; width:120px;");
            sb.Append("font-weight:bold; color:#000000 ;font-family:Arial'>" + dataEntity.TestParams.TargetTableName + " Table Miss-Matching Results </td>");
            sb.Append("</tr>");

            //------- Miss-Matching Data from Source Table....
            sb.Append("<tr>");
            //Actual data Source table - columns and rows are printing......
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            DataTable dtcheck = dataEntity.SourceTableMissMatchRecords;
            if (dataEntity.SourceTableMissMatchRecords != null)
            {
                if (dataEntity.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    sb.Append("<tr style='background-color:#B6B6B4;'>");
                    foreach (DataColumn column in dataEntity.SourceTableMissMatchRecords.Columns)
                    {
                        sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;font-weight:bold; color:#000000 ;font-family:Arial;'>"
                            + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dataEntity.SourceTableMissMatchRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>" + item + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
            }
            else
            {
                sb.Append("<tr style='background-color:#254117;text-align:center;'>");
                sb.Append("Data not found from source table</tr>");
            }

            sb.Append("</table>");
            sb.Append("</td>");
            //--- Matching data from target table
            sb.Append("<td valign='top' colspan='2'>");
            sb.Append("<table>");
            if (dataEntity.TargetTableMissMatchRecords != null)
            {
                if (dataEntity.TargetTableMissMatchRecords.Rows.Count > 0)
                {
                    sb.Append("<tr style='background-color:#B6B6B4;'>");
                    foreach (DataColumn column in dataEntity.TargetTableMissMatchRecords.Columns)
                    {
                        sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;font-weight:bold; color:#000000 ;font-family:Arial;'>"
                            + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dataEntity.TargetTableMissMatchRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>" + item + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
            }
            else
            {
                sb.Append("<tr style='background-color:#254117;text-align:center;'>");
                sb.Append("Data not found from source table</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            return sb;
        }

        public StringBuilder GenerateReportSummaryNew(CompareData dataEntity)
        {
            // Migration report summary
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr style='background-color:#D1D0CE;height:35px'>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'> Database Name </td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'> Table Name </td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'> Total Records </td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:150px;'>Pass Records</td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'>Failed Records</td></tr>");
            
            sb.Append("<tr style='background-color:#CCFFFF;height:35px'>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'>  " + dataEntity.TestParams.SourceDBName + "</td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'>  " + dataEntity.TestParams.SourceTableName + "</td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:350px;'>  " + dataEntity.SourceTableActualRecordsCount + "</td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:150px;'>" + dataEntity.SourceTableMatchingRecordsCount + "</td>");
            sb.Append("<td align='center' style='border:1px solid #736F6E; font-size:16px; width:150px;'>" + dataEntity.SourceTableMissMatchRecordsCount + "</td></tr>");
            return sb;
        }

        public StringBuilder GenerateSourcePassResults(CompareData dataEntity)
        {
            StringBuilder sb = new StringBuilder();
            //Matching Data from both source and destination tables.....
            sb.Append("<tr style='background-color:#736F6E; height:35px'>");
            sb.Append("<td align='center' colspan='5' style='border:1px solid #95bff7; font-size:16px; width:1000px");
            sb.Append("font-weight:bold; color:#ffffff;font-family:Arial'>" + dataEntity.TestParams.SourceTableName + " Table Compared Results </td>");
            sb.Append("</tr>");
            //--- Matching data from source table
            sb.Append("<tr>");
            sb.Append("<td valign='top' colspan='5'>");
            sb.Append("<table style='width:1000px'>");
            if (dataEntity.SourceTableMatchingRecords != null)
            {
                if (dataEntity.SourceTableMatchingRecords.Rows.Count > 0)
                {
                    sb.Append("<tr style='background-color:#D1D0CE;'>");
                       sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;" +
                                "color:#000000;font-family:Arial;'>S.NO</td>");
                    foreach (DataColumn column in dataEntity.SourceTableMatchingRecords.Columns)
                    {
                        sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;"+
                                    "color:#000000;font-family:Arial;'>"+ column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
                    }

                    sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;" +"color:#000000;font-family:Arial;'>STATUS</td>");
                    sb.Append("</tr>");
                    int i = 1;
                    foreach (DataRow row in dataEntity.SourceTableMatchingRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");
                        sb.Append("<td style='border:1px solid #837E7C; text-align:center; font-size:16px; width:120px;color:#000000;font-family:Arial;'>"
                             + i + "</td>");
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C; text-align:center; font-size:16px; width:120px;color:#000000;font-family:Arial;'>"
                                + item + "</td>");
                           
                        }
                        sb.Append("<td bgcolor='#00FF00'; style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;" +
                                    "color:#000000;font-family:Arial;'>PASS</td>");
                        sb.Append("</tr>");
                        i++;
                    }
                    //Binding Miss-Matching Results from Source Table...
                    sb.Append(GenerateSourceFailedResults(dataEntity,  i));
                }
            }
            else
            {
                //Need to Bind Source Actual data... if Matching data not found then we have to bind Actual data with status == Failed.
                //sb.Append("<tr style='background-color:#254117;text-align:left;'>");
                //sb.Append("Source data not matching with Target table</tr>");
                sb.Append(GenerateActualSourceResults(dataEntity, 1));
              
            }
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            return sb;
        }

        public StringBuilder GenerateActualSourceResults(CompareData dataEntity, int sNO)
        {
            StringBuilder sb = new StringBuilder();
            //Actual data binding....
            sb.Append("<tr>");
            //Actual data Source table - columns and rows are printing......
            sb.Append("<td valign='top' colspan='5'>");
            sb.Append("<table style='width:1000px'>");
            sb.Append("<tr style='background-color:#B6B6B4;'>");
            sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;" +
                              "color:#000000;font-family:Arial;'>S.NO</td>");
            foreach (DataColumn column in dataEntity.SourceTableActualData.Columns)
            {
                sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; font-weight:bold; width:120px; color:#000000 ;font-family:Arial;'>"
                    + column.ColumnName + "(DataType =" + column.DataType.Name + ")" + "</td>");
            }

            sb.Append("<td style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px;font-weight:bold;" + "color:#000000;font-family:Arial;'>STATUS</td>");
            sb.Append("</tr>");
            foreach (DataRow row in dataEntity.SourceTableActualData.Rows) // Loop over the rows.
            {
                sb.Append("<tr style='background-color:#CCFFFF;'>");

                sb.Append("<td style='border:1px solid #837E7C; text-align:center; font-size:16px; width:120px;color:#000000;font-family:Arial;'>"
                     + sNO + "</td>");

                foreach (var item in row.ItemArray) // Loop over the items.
                {
                    sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px;color:#000000 ;font-family:Arial;'>"
                        + item + "</td>");
                }
                sb.Append("<td bgcolor='#FF0000'; style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px; font-weight:bold;" +
                             "color:#ffffff;font-family:Arial;'>FAILED</td>");
                sb.Append("</tr>");
                sNO++;
            }
            sb.Append("</table>");
            sb.Append("</td></tr>");
            return sb;
        }

        public StringBuilder GenerateSourceFailedResults(CompareData dataEntity, int sNO)
        {
            StringBuilder sb = new StringBuilder();
            if (dataEntity.SourceTableMissMatchRecords != null)
            {
                if (dataEntity.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    foreach (DataRow row in dataEntity.SourceTableMissMatchRecords.Rows) // Loop over the rows.
                    {
                        sb.Append("<tr style='background-color:#CCFFFF;'>");

                        sb.Append("<td style='border:1px solid #837E7C; text-align:center; font-size:16px; width:120px;color:#000000;font-family:Arial;'>"
                            + sNO + "</td>");
                      
                        foreach (var item in row.ItemArray) // Loop over the items.
                        {
                            sb.Append("<td style='border:1px solid #837E7C;text-align:center; font-size:16px; width:120px; color:#000000;font-family:Arial;'>" 
                                + item + "</td>");
                        }

                        sb.Append("<td bgcolor='#FF0000'; style='border:1px solid #837E7C;text-align:center;font-size:16px; width:120px; font-weight:bold;" +
                                  "color:#ffffff;font-family:Arial;'>FAILED</td>");
                        sb.Append("</tr>");
                        sNO++;
                    }
                }
            }
            return sb;
        }
    }
}
