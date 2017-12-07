using Ecolab.CommonUtilityPlugin;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;


namespace Ecolab.MigrationTest
{
    public class MetersMigrationTests: TestBase
    {
        private string xmlPath;
        public MetersMigrationTests()
            : base("MetersMigration.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "MetersMigration.xml");
        }

        [Test, Description("TC01_VerifyMetersData")]
        public void TC01_VerifyMetersData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyMetersData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table.");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("TC02_VerifyMetersData_Water")]
        public void TC02_VerifyMetersData_Water()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyMetersData_Water");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table.");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("TC03_VerifyMetersData_Electric")]
        public void TC03_VerifyMetersData_Electric()
        {
            CompareData data = new CompareData(xmlPath, "TC03_VerifyMetersData_Electric");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table.");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
    }
}