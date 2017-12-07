using ArtOfTest.WebAii.Controls.HtmlControls;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ecolab.FunctionalTest
{
    public class ManualInputLabourTests : TestBase
    {
        string msgSaveButton = string.Empty;
        /// <summary>
        /// Tests the fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputLabourTabPage.LabourTab.Click();
        }
        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }
        [TestCategory(TestType.regression, "TC01_VerifyLabourCostCalculatedAutomatically")]
        [Test, Description("Test case 34044: RG: Verify Labor cost is calculated automatilcally once user provides data to all fields in Labor tab")]
        public void TC01_VerifyLabourCostCalculatedAutomatically()
        {
            Thread.Sleep(1000);
            Page.ManualInputLabourTabPage.DdlLocation.SelectByIndex(1);
            Page.ManualInputLabourTabPage.DdlLabourTypes.SelectByIndex(1);
            Thread.Sleep(1000);
            if (Page.ManualInputLabourTabPage.TxtLabourCost.Text == null)
            {
                Assert.Fail("Labour cost not calculated automatically.Expected-Labour cost to displayed");
            }
            else
            {
                Assert.Pass("Labour cost calculated automatically");
            }
        }

        [TestCategory(TestType.functional, "TC02_VerifyLabourCostSaveFunctionality")]
        [Test, Description("Test case 34045: RG: Verify Save functionality in Labor tab")]
        public void TC02_VerifyLabourCostSaveFunctionality()
        {
            Thread.Sleep(1000);
            Page.ManualInputLabourTabPage.AddLabourDetails("Tunnel Washer1", "Normal", "5");
            Thread.Sleep(1000);
            if (null != Page.ManualInputLabourTabPage.SuccessMessage)
            {
                if (Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText == "")
                {
                    msgSaveButton = "Labour details not created.";
                }
                else
                {
                    msgSaveButton = Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText;
                }
                if (!Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText.Equals(@"Labour Data Updated SuccessFully"))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Labour Data Updated SuccessFully" + " but Actual:" + msgSaveButton);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "select id, Locationid, ManHourTypeId AllocatedManHours " +
              " from TCD.ManualLabor where LocationId = 2 and ManHourTypeid = 1  and AllocatedManHours = 5  order by id desc";
            DataSet ds = DBValidation.GetData(strCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Labour records not found in DB.Expected- Allocated Man Hours = 5 with LocationId = 1");
            }
            else
            {
                Assert.Fail("Labour records not found in DB.Expected- Allocated Man Hours = 5 with LocationId = 1");
            }
            //Validating in History Table for the above added newly record....
            string strTblHistory = "Select * from TCD.ManualLaborHistory where LocationId = 2 " +
                                "and ManHourTypeId=1 and AllocatedManHours = 5 order by operationTimestamp desc";
            DataSet ds1 = DBValidation.GetData(strTblHistory);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Labour records not found in TCD.ManualLaborHistory Table.Expected - data for columns with Allocated Man Hours = 5 with LocationId = 2");
            }
            else
            {
                Assert.Fail("Labour records not found in TCD.ManualLaborHistory Table.Expected - data for columns withAllocated Man Hours = 5 with LocationId = 2");
            }
            //Update the Labour records
            try
            {
                string strUpdate = "Update TCD.ManualLabor Set AllocatedManHours = 15 where id = " + ds.Tables[0].Rows[0]["id"].ToString();
                DBValidation.UpdateData(strUpdate);
            }
            catch
            {
                Assert.Fail("Failed to update Manual Labor Allocated Man HOurs");
            }

        }

        [TestCategory(TestType.regression, "TC03_VerifyAccessforUserRoleTwo")]
        [Test, Description("Test case 40732: RG: Verify the access for User Role 2 on Manual Input Labor for")]
        public void TC03_VerifyAccessforUserRoleTwo()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestOperator", "test");
            List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
            if (lstMenuList.Contains("Manual Inputs"))
            {
                Assert.Fail("Login with user roleid = 2 found Labour tab. Expected - Manual Input Labor link should not be available for User Role 2.");
            }
            else
            {
                Assert.Pass("Labour tab not visible for user roleid = 2.");
            }
        }

        [TestCategory(TestType.regression, "TC04_VerifyAccessforUserRoleFive")]
        [Test, Description("Test case 40754: RG: Verify the access for User Role 5 on Manual Input Labor for")]
        public void TC04_VerifyAccessforUserRoleFive()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestBDM", "test");
            List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
            Thread.Sleep(2000);
            if (lstMenuList.Contains("Manual Inputs"))
            {
                Thread.Sleep(2000);
                Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
                Page.ManualInputLabourTabPage.LabourTab.Click();
                if (null == Page.ManualInputLabourTabPage.LabourTabGrid)
                {
                    Assert.Fail("Login with Level-5 user. Expected - Labour details in gridview.");
                }
                Thread.Sleep(2000);
                Page.ManualInputLabourTabPage.VerifyUserRoleCreateOption();
                if (Page.ManualInputLabourTabPage.BtnSave.IsEnabled == true)
                {
                    Assert.Fail("Login with Level - 5 user.Expected - Manual Input Labor link should available for User Role 5.");
                }
            }
            else
            {
                Assert.Fail("Login with level-5 user. " +
                               "Expected - Manual Input Labor link should available for User Role 5.");
            }
           
        }
        [TestCategory(TestType.regression, "TC05_VerifyAccessRolefor346789")]
        [Test, Description("Test case 40758: RG: Verify the access for User Role 3/4/6/7/8/9 on Manual Input Labor for")]
        public void TC05_VerifyAccessRolefor346789()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestPE", "test");
            userAccess.Add("AutoTestPM", "test");
            userAccess.Add("AutoTestTMBasic", "test");
            userAccess.Add("AutoTestTMAdvance", "test");
            userAccess.Add("AutoTestEng", "test");
            userAccess.Add("AutoTestAdmin", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                if (lstMenuList.Contains("Manual Inputs"))
                {
                    Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
                    Page.ManualInputLabourTabPage.LabourTab.Click();
                    if (Page.ManualInputLabourTabPage.LabourTab.IsEnabled == true)
                    {
                        Assert.Fail("Login with user id " + pair.Key + " Expected - User should navigated to Manual Input Labor page once clicks on the Manual Input labor link.");
                    }
                    Page.ManualInputLabourTabPage.VerifyUserRoleCreateOption();
                    if (Page.ManualInputLabourTabPage.BtnSave.IsEnabled == false)
                    {
                        Assert.Fail("Login with user id " + pair.Key + " Expected- User should be able to create a new record once navigated to ManualInput Laborpage. But Actual user unable to create the records.");
                    }
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                }
                else
                {
                    Assert.Fail("Login with user id " + pair.Key + " Expected - Manual Input Labor link should be available.Actual - Manual Input Labor link is not available.");
                }
            }
        }

        [TestCategory(TestType.regression, "TC06_VerifyAuditTableForInsertAndUpdate")]
        //[Test, Description("Test case 40772: RG: Verify the Audit Table for Insert and Update")]
        public void TC06_VerifyAuditTableForInsertAndUpdate()
        {
            Thread.Sleep(2000);
            Page.ManualInputLabourTabPage.AddLabourDetails("Tunnel Washer1", "Normal", "5");
            Thread.Sleep(1000);
            if (Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText == "")
            {
                msgSaveButton = "Labour details not created.";
            }
            else
            {
                msgSaveButton = Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText;
            }
            if (null != Page.ManualInputLabourTabPage.SuccessMessage)
            {
                if (!Page.ManualInputLabourTabPage.SuccessMessage.BaseElement.InnerText.Equals(@"Labour Data Updated SuccessFully"))
                {
                    Assert.Fail("Expected: Labour Data Updated SuccessFully" + " but Actual:" + msgSaveButton);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "select id, Locationid, ManHourTypeId AllocatedManHours " +
              " from TCD.ManualLabor where LocationId = 2 and ManHourTypeid = 1  and AllocatedManHours = 5  order by id desc";
            DataSet ds = DBValidation.GetData(strCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Labour records not found in DB.Expected- Allocated Man Hours = 5 with LocationId = 1");
            }
            else
            {
                Assert.Fail("Labour records not found in DB.Expected- Allocated Man Hours = 5 with LocationId = 1");
            }
            //Validating in History Table for the above added newly record....
            string strTblHistory = "Select * from TCD.ManualLaborHistory where LocationId = 2 " +
                                "and ManHourTypeId=1 and AllocatedManHours = 5 order by operationTimestamp desc";
            DataSet ds1 = DBValidation.GetData(strTblHistory);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Labour records not found in TCD.ManualLaborHistory Table.Expected - data for columns with Allocated Man Hours = 5 with LocationId = 2");
            }
            else
            {
                Assert.Fail("Labour records not found in TCD.ManualLaborHistory Table.Expected - data for columns withAllocated Man Hours = 5 with LocationId = 2");
            }
            //Update the Labour records
            try
            {
                string strUpdate = "Update TCD.ManualLabor Set AllocatedManHours = 15 where id = "+ ds.Tables[0].Rows[0]["id"].ToString();
                DBValidation.UpdateData(strUpdate);
            }
            catch
            {
                Assert.Fail("Failed to update Manual Labor Allocated Man HOurs");
            }
        }

        [TestCategory(TestType.regression, "TC07_VerifyLocalization")]
        [Test, Description("Test case 34209: RG: Verify the Localization criteria")]
        public void TC07_VerifyLocalization()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.RedFlagTabPage.LanguagePreferred.Focus();
            Page.RedFlagTabPage.LanguagePreferred.SelectByText("Deutsch", true);
            Page.RedFlagTabPage.LanguagePreferred.ScrollToVisible();
            Page.RedFlagTabPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            string text = Page.ManualInputLabourTabPage.LabourTab.ChildNodes[0].Content;
            if (Page.ManualInputLabourTabPage.LabourTab.ChildNodes[0].Content == "Labour")
            {
                Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - in Deutsch language, but Actual content found is -"
                                + Page.ManualInputLabourTabPage.LabourTab.ChildNodes[0].Content);
            }
        }
        
        [TestCategory(TestType.regression, "TC08_PostLocalization")]
        [Test, Description("Test case 34209: RG: Post Localization ")]
        public void TC08_PostLocalization()
        {
            //Post Condition to revert back the localization
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
        }
    }
}
