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
    public class BatchDataWashRunTests : TestBase
    {
        private string xmlPath;
        public BatchDataWashRunTests(): base("BatchDataWashRun.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "BatchDataWashRun.xml");
        }

        [Test, Description("TC01_VerifyBatchdata_Washrun")]
        public void TC01_VerifyBatchdata_Washrun()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyBatchdata_Washrun");
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

        [Test, Description("TC02_VerifyBacthDataWasherinj")]
        public void TC02_VerifyBacthDataWasherinj()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyBacthDataWasherinj");
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

        [Test, Description("TC03_VerifyBacthDataWasherchem")]
        public void TC03_VerifyBacthDataWasherchem()
        {
            CompareData data = new CompareData(xmlPath, "TC03_VerifyBacthDataWasherchem");
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
