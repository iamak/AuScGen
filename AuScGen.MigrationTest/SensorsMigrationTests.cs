using NUnit.Framework;
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
    public class SensorsMigrationTests : TestBase
    {
        private string xmlPath;
        public SensorsMigrationTests()
            : base("SensorsMigration.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "SensorsMigration.xml");
        }

        [Test, Description("TC01_VerifySensorMigration")]
        public void TC01_VerifySensorMigration()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifySensorMigration");
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
        [Test, Description("TC02_VerifySensorReadings")]
        public void TC02_VerifySensorReadings()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifySensorReadings");
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