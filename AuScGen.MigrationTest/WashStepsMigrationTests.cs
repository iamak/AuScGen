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
   public class WashStepsMigrationTests: TestBase
    {
        private string xmlPath;
        public WashStepsMigrationTests()
            : base("WashStepsMigration.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "WashStepsMigration.xml");
        }

        [Test, Description("TC01_VerifyWashStepsConventional")]
        public void TC01_VerifyWashStepsConventional()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyWashStepsConventional");
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
        [Test, Description("TC02_VerifyWashstepsTunnel")]
        public void TC02_VerifyWashstepsTunnel()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyWashstepsTunnel");
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