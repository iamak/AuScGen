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
    class WasherTagsTests: TestBase
    {
        private string xmlPath;
        public WasherTagsTests()
            : base("WasherTags.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "WasherTags.xml");
        }

        [Test, Description("TC01_VerifyWasherTagsAlelnBardleyData")]
        public void TC01_VerifyWasherTagsAlelnBardleyData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyWasherTagsAlelnBardleyData");
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
        [Test, Description("TC02_VerifyWasherTgasBeckoff")]
        public void TC02_VerifyWasherTgasBeckoff()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyWasherTgasBeckoff");
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