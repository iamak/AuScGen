using ThatcherTech.CommonUtilityPlugin;
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
    public class WashersMigrationTests: TestBase
    {
        private string xmlPath;
        public WashersMigrationTests()
            : base("WashersMigration.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "WashersMigration.xml");
        }
        [Test, Description("TC01_WashersMigrationData")]
        public void TC01_WashersMigrationData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_WashersMigrationData");
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