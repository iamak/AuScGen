using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
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
    public class AlarmDataTests : TestBase
    {
        private string xmlPath;
        public AlarmDataTests()
            : base("AlarmData.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "AlarmData.xml");
        }
        [Test]
        public void TC01_VerifyAlarmData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyAlarmData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source Table data not matching with Target Table.");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
    }
}
