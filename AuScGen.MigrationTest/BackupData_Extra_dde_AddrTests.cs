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
    public class BackupData_Extra_dde_AddrTests : TestBase
    {
        private string xmlPath;
        public BackupData_Extra_dde_AddrTests()
            : base("BackupData_extra_dde_addr.xml")
        {
            xmlPath = string.Concat(TestParamsPath, "BackupData_extra_dde_addr.xml");
        }
        [Test, Description("Test case TC01_Verifyextra_dde_addrdata")]
        public void TC01_Verifyextra_dde_addrdata()
        {
            CompareData data = new CompareData(xmlPath, "TC01_Verifyextra_dde_addrdata");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC02_VerifyExtra_dde_data")]
        public void TC02_VerifyExtra_dde_data()
        {
            CompareData data = new CompareData(xmlPath, "TC02_VerifyExtra_dde_data");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC03_VerifySrcttCheckData")]
        public void TC03_VerifySrcttCheckData()
        {
            CompareData data = new CompareData(xmlPath, "TC03_VerifySrcttCheckData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC04_VerifySrheaderData")]
        public void TC04_VerifySrheaderData()
        {
            CompareData data = new CompareData(xmlPath, "TC04_VerifySrheaderData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC05_VerifySrheatrecData")]
        public void TC05_VerifySrheatrecData()
        {
            CompareData data = new CompareData(xmlPath, "TC05_VerifySrheatrecData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC06_VerifySrironersData")]
        public void TC06_VerifySrironersData()
        {
            CompareData data = new CompareData(xmlPath, "TC06_VerifySrironersData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC07_VerifySrlfsChkdetData")]
        public void TC07_VerifySrlfsChkdetData()
        {
            CompareData data = new CompareData(xmlPath, "TC07_VerifySrlfsChkdetData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC08_VerifySrlfsChkhdData")]
        public void TC08_VerifySrlfsChkhdData()
        {
            CompareData data = new CompareData(xmlPath, "TC08_VerifySrlfsChkhdData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC09_VerifySrlfspartsData")]
        public void TC09_VerifySrlfspartsData()
        {
            CompareData data = new CompareData(xmlPath, "TC09_VerifySrlfspartsData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC10_VerifySrsteamData")]
        public void TC10_VerifySrsteamData()
        {
            CompareData data = new CompareData(xmlPath, "TC10_VerifySrsteamData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC11_VerifySrtitrtdetData")]
        public void TC11_VerifySrtitrtdetData()
        {
            CompareData data = new CompareData(xmlPath, "TC11_VerifySrtitrtdetData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC12_VerifySrtitrthdData")]
        public void TC12_VerifySrtitrthdData()
        {
            CompareData data = new CompareData(xmlPath, "TC12_VerifySrtitrthdData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
        [Test, Description("Test case TC13_VerifySrttcheckData")]
        public void TC13_VerifySrttcheckData()
        {
            CompareData data = new CompareData(xmlPath, "TC13_VerifySrttcheckData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC14_VerifySrttformData")]
        public void TC14_VerifySrttformData()
        {
            CompareData data = new CompareData(xmlPath, "TC14_VerifySrttformData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC15_VerifySrttstepsData")]
        public void TC15_VerifySrttstepsData()
        {
            CompareData data = new CompareData(xmlPath, "TC15_VerifySrttstepsData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC16_VerifySrtunchkdesData")]
        public void TC16_VerifySrtunchkdesData()
        {
            CompareData data = new CompareData(xmlPath, "TC16_VerifySrtunchkdesData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC17_VerifySrtunchkdes_iniData")]
        public void TC17_VerifySrtunchkdes_iniData()
        {
            CompareData data = new CompareData(xmlPath, "TC17_VerifySrtunchkdes_iniData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC18_VerifySrtunchkdetData")]
        public void TC18_VerifySrtunchkdetData()
        {
            CompareData data = new CompareData(xmlPath, "TC18_VerifySrtunchkdetData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC19_VerifySrtunchkhdData")]
        public void TC19_VerifySrtunchkhdData()
        {
            CompareData data = new CompareData(xmlPath, "TC19_VerifySrtunchkhdData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC20_VerifySrwaterData")]
        public void TC20_VerifySrwaterData()
        {
            CompareData data = new CompareData(xmlPath, "TC20_VerifySrwaterData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC21_VerifySrwfwashersData")]
        public void TC21_VerifySrwfwashersData()
        {
            CompareData data = new CompareData(xmlPath, "TC21_VerifySrwfwashersData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC22_VerifyEtech_settingsData")]
        public void TC22_VerifyEtech_settingsData()
        {
            CompareData data = new CompareData(xmlPath, "TC22_VerifyEtech_settingsData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }

        [Test, Description("Test case TC23_VerifyDailyactualsData")]
        public void TC23_VerifyDailyactualsData()
        {
            CompareData data = new CompareData(xmlPath, "TC23_VerifyDailyactualsData");
            TestDBReport.GenerateMigrationTestReport(data);
            if (data.SourceTableMissMatchRecords != null)
            {
                if (data.SourceTableMissMatchRecords.Rows.Count > 0)
                {
                    Assert.Fail("Source table data not matching with Target table data");
                }
            }
            else
            {
                Assert.Pass("Source and Target table records matching.");
            }
        }
    }
}

