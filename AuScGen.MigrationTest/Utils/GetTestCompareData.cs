using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;
using Framework;
using System.Collections.ObjectModel;

namespace Ecolab.MigrationTest
{
    public class CompareData
    {
        private string testParameterPath;
        private string testName;
        public TestParameters TestParams
        {
            get
            {
                return new TestParameters(testParameterPath,testName);
            }
        }
               
        public CompareData(string testParamsPath, string testcaseName)
        {
            testParameterPath = testParamsPath;
            testName = testcaseName;
        }
        public IEnumerable<DataRow> SourceMatchingDataRow { get; set; }
        public IEnumerable<DataRow> SourceMissMatchDataRow { get; set; }
        public IEnumerable<DataRow> TargetMatchingDataRow { get; set; }
        public IEnumerable<DataRow> TargetMissMatchDataRow { get; set; }
        public DataTable setSourceData { get; set; }
        public DataTable setTargetData { get; set; }
        public DataTable dtEmpty { get; set; }

        private bool WhereAction(DataRow table1, DataRow table2,
                                 ReadOnlyCollection<string> SourceFields, ReadOnlyCollection<string> TargetFields)
        {
            for (int i = 0; i < SourceFields.Count; i++)
            {
                string test1 = Convert.ToString(table1[SourceFields[i].Trim()]).Trim();
                string test2 = Convert.ToString(table2[TargetFields[i].Trim()]).Trim();
                //12-01-2014-----Considering one scenario - where from source null values are converting to zero in target table....
                //the below var and if condition is to check for null values and convert it into zero (work on progress)
                var setSourceField = table1[SourceFields[i].Trim()].GetType();
                if (setSourceField == null)
                {
                    //setSourceField = Convert.ChangeType("0", System.DBNull);
                }
                if (Convert.ToString(table1[SourceFields[i].Trim()]).Trim() !=
                        Convert.ToString(table2[TargetFields[i].Trim()]).Trim())
                {
                    return false;
                }
            }
            return true;
        }

        private bool WhereAction(DataRow table1, DataRow table2)
        {
            return Convert.ToString(table1["size"]).Trim() ==
                   Convert.ToString(table2["size"]).Trim();
        }

        public int CountZero
        {
            get
            {
                return 0;
            }
        }
      
        public int SourceTableActualRecordsCount
        {
            get
            {
                return DBValidationChemWatch.GetDataTable(TestParams.GetSourceQuery).Rows.Count;
            }
        }
       
        public int TargetTableActualRecordsCount
        {
            get
            {
                return DBValidationConduit.GetDataTable(TestParams.GetTargetQuery).Rows.Count;
            }
        }
  
        public int SourceTableMatchingRecordsCount
        {
            get
            {
                SourceMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                         Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table1).ToList().Distinct();
                if (SourceMatchingDataRow.Count() > 0)
                {
                    return SourceMatchingDataRow.Count();
                }
                return CountZero;
            }
        }
 
        public int TargetTableMatchingRecordsCount
        {
            get
            {
                TargetMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                         Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table2).ToList().Distinct();

                if (TargetMatchingDataRow.Count() > 0)
                {
                    return TargetMatchingDataRow.Count();
                }
                return CountZero;
            }
        }

        public int SourceTableMissMatchRecordsCount
        {
            get
            {
                setSourceData = SourceTableActualData;
                SourceMatchingDataRow = (from table1 in setSourceData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                         Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table1).ToList().Distinct();
                if (SourceMatchingDataRow.Count() > 0)
                {
                    SourceMissMatchDataRow = (from table1 in setSourceData.AsEnumerable()
                                              where !SourceMatchingDataRow.Contains(table1)
                                              select table1).ToList().Distinct();
                    if (SourceMissMatchDataRow.Count() > 0)
                    {
                        return SourceMissMatchDataRow.Count();
                    }
                }
                return CountZero;
            }
        }

        public int TargetTableMissMatchRecordsCount
        {
            get
            {
                setTargetData = TargetTableActualData;
                TargetMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in setTargetData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                        Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table2).ToList().Distinct();

                if (TargetMatchingDataRow.Count() > 0)
                {
                    TargetMissMatchDataRow = (from table2 in setTargetData.AsEnumerable()
                                              where !TargetMatchingDataRow.Contains(table2)
                                              select table2).ToList();
                    if (TargetMissMatchDataRow.Count() > 0)
                    {
                        return TargetMissMatchDataRow.Count();
                       
                    }
                }
                return CountZero;
            }
        }

        public DataTable SourceTableActualData
        {
            get
            {
                return DBValidationChemWatch.GetDataTable(TestParams.GetSourceQuery);
            }
        }
 
        public DataTable TargetTableActualData
        {
            get
            {
                return DBValidationConduit.GetDataTable(TestParams.GetTargetQuery);
            }
        }
 
        public DataTable SourceTableMatchingRecords
        {
            get
            {
                SourceMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                        Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table1).ToList().Distinct();
                if (SourceMatchingDataRow.Count() > 0)
                {
                    return SourceMatchingDataRow.CopyToDataTable();
                }
                return dtEmpty;
            }
        }
     
        public DataTable SourceTableMissMatchRecords
        {
            get
            {
                setSourceData = SourceTableActualData;
                SourceMatchingDataRow = (from table1 in setSourceData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                         Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table1).ToList().Distinct();
                if (SourceMatchingDataRow.Count() > 0)
                {
                    SourceMissMatchDataRow = (from table1 in setSourceData.AsEnumerable()
                                              where !SourceMatchingDataRow.Contains(table1)
                                              select table1).ToList().Distinct();
                    if (SourceMissMatchDataRow.Count() > 0)
                    {
                        return SourceMissMatchDataRow.CopyToDataTable();
                    }
                    else
                    {
                        return dtEmpty;
                    }
                }
                return dtEmpty;
            }
        }
  
        public DataTable TargetTableMatchingRecords
        {
            get
            {
                TargetMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in TargetTableActualData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                         Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table2).ToList().Distinct();

                if (TargetMatchingDataRow.Count() > 0)
                {
                    return TargetMatchingDataRow.CopyToDataTable();
                }
                return dtEmpty;
            }
        }
  
        public DataTable TargetTableMissMatchRecords
        {
            get
            {
                setTargetData = TargetTableActualData;
                TargetMatchingDataRow = (from table1 in SourceTableActualData.AsEnumerable()
                                         join table2 in setTargetData.AsEnumerable() on
                                         Convert.ToString(table1[TestParams.SourceTableUniqueId]).Trim() equals
                                        Convert.ToString(table2[TestParams.TargetTableUniqueId]).Trim()
                                         where (WhereAction(table1, table2, TestParams.SourceFieldCollection,
                                                                TestParams.TargetFieldCollection))
                                         select table2).ToList().Distinct();

                if (TargetMatchingDataRow.Count() > 0)
                {
                    TargetMissMatchDataRow = (from table2 in setTargetData.AsEnumerable()
                                              where !TargetMatchingDataRow.Contains(table2)
                                              select table2).ToList();
                    if (TargetMissMatchDataRow.Count() > 0)
                    {
                        return TargetMissMatchDataRow.CopyToDataTable();
                    }
                    else
                    {
                        return dtEmpty;
                    }
                }
                return dtEmpty;
            }
        }

        private static ContainerAccess container = new ContainerAccess();
        private CommonUtilityPlugin.DataAccess dataAccessChemWatch;
        private CommonUtilityPlugin.DataAccess DBValidationChemWatch
        {
            get
            {
                dataAccessChemWatch = CreatePlugin<CommonUtilityPlugin.DataAccess>();
                dataAccessChemWatch.ConectionString = Config.MigrationTestSettings.Default.ChemWatchDBConnection;
                dataAccessChemWatch.DataCategory = CommonUtilityPlugin.DataCategory.VSFoxPro;
                return dataAccessChemWatch;
            }
        }

        private CommonUtilityPlugin.DataAccess dataAccessConduit;
        private CommonUtilityPlugin.DataAccess DBValidationConduit
        {
            get
            {
                dataAccessConduit = CreatePlugin<CommonUtilityPlugin.DataAccess>();
                dataAccessConduit.ConectionString = Config.MigrationTestSettings.Default.ConduitDBConnection;
                dataAccessConduit.DataCategory = CommonUtilityPlugin.DataCategory.SQLDB;
                return dataAccessConduit;
            }
        }
        private static T CreatePlugin<T>() where T : IContainerPlugin
        {
            return container.GetPlugin<T>();
        }
    }
}
