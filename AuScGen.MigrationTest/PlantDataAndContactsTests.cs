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
   public class PlantDataAndContactsTests: TestBase
    {
        private string xmlPath;
        public PlantDataAndContactsTests()
            : base("PlantDataAndContacts.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "PlantDataAndContacts.xml");
        }

        [Test, Description("TC01_VerifyPlantDetails")]
        public void TC01_VerifyPlantDetails()
        {
            CompareData data = new CompareData(xmlPath, "TC01_VerifyPlantDetails");
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

        [Test, Description("TC02_VerifyPlantCustomerAddressDetails")]
        public void TC02_VerifyPlantCustomerAddressDetails()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyPlantCustomerAddressDetails");
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

        [Test, Description("TC03_VerifyPlantContactsDetailsforColumnContact")]
        public void TC03_VerifyPlantContactsDetailsforColumnContact()
        {
            CompareData data = new CompareData(xmlPath, "TC03_VerifyPlantContactsDetailsforColumnContact");
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
        [Test, Description("TC04_VerifyPlantContactDetailsTSNREP")]
        public void TC04_VerifyPlantContactDetailsTSNREP()
        {
            CompareData data = new CompareData(xmlPath, "TC04_VerifyPlantContactDetailsTSNREP");
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