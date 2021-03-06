﻿using Ecolab.CommonUtilityPlugin;
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
    public class ControllersTests : TestBase
    {
        
        private string xmlPath;
        public ControllersTests()
            : base("Controllers.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "Controllers.xml");
        }

        [Test, Description("TC01_VerifyControllersData")]
        public void TC01_VerifyControllersData()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyControllersData");
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

        [Test, Description("TC02_VerifyControllerSetupdata")]
        public void TC02_VerifyControllerSetupdata()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyControllerSetupdata");
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
