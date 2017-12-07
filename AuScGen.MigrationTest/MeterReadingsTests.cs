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
    public class MeterReadingsTests: TestBase
    {
        private string xmlPath;
        public MeterReadingsTests()
            : base("MeterReadings.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "MeterReadings.xml");
        }

        [Test, Description("TC01_VerifyMeterReadingsData")]
        public void TC01_VerifyMeterReadingsData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyMeterReadingsData");
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