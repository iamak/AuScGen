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
    public class GroupFormulasTests: TestBase
    {
        private string xmlPath;
        public GroupFormulasTests()
            : base("GroupFormulas.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "GroupFormulas.xml");
        }

        [Test, Description("TC01_VerifyGroupFormulasConventional")]
        public void TC01_VerifyGroupFormulasConventional()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyGroupFormulasConventional");
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

        [Test, Description("TC02_GroupFormulasTunnel")]
        public void TC02_GroupFormulasTunnel()
        {
            CompareData data = new CompareData(xmlPath, "TC02_GroupFormulasTunnel");
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