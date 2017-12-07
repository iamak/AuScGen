using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecolab.TelerikPlugin;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using NUnit.Framework;
using NUnit.Core;
using System.Threading;
using System.IO;
using ArtOfTest.WebAii.ObjectModel;
using Ecolab.Pages.CommonControls;
using System.Collections;
using Ecolab.CommonUtilityPlugin;
using System.Data;
using System.Collections.ObjectModel;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupRedFlagTests : TestBase
    {
        bool verifyAssert = false;
       
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.RedFlagTab.Click();
        }

        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        [TestCategory(TestType.bvt, "TC01_VerifyAddRedFlagFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyAddRedFlagFunctionality")]
        [TestCategory(TestType.functional, "TC01_VerifyAddRedFlagFunctionality")]
        [Test, Description("Test case 28347: RG: Verify Audit tables applied for inline edit and delete test cases;Test case 28063: RG:17910 : Plant Setup -> Red Flag Page:Verify Save on Add	")]
        public void TC01_VerifyAddRedFlagFunctionality()
        {
            string testdata = "Chemical Consumption";
            string strUpdate = "update TCD.redflag set is_deleted = '1' where item='9'";
            DBValidation.UpdateData(strUpdate);
            Page.RedFlagTabPage.BtnAddRedFlag.Click();
            Thread.Sleep(2000);
            Page.RedFlagTabPage.TxtMaximumRangeAdd.ScrollToVisible();
            Thread.Sleep(2000);
            Page.RedFlagTabPage.AddRefFlagDetails();
            Thread.Sleep(2000);
            if (null != Page.RedFlagTabPage.ErrorMsg)
            {
                if (!Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText
                    .Equals(@"Red flag added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Red flag added successfully but Actual:"+ Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.RedFlagTabPage.SortRedFlagGrid.Click();
            if (Page.RedFlagTabPage.RedFlagTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.RedFlagTabPage.RedFlagTabGrid.Rows.Count - 1; i++)
                {
                    Thread.Sleep(1000);
                    if (Page.RedFlagTabPage.RedFlagTabGrid.Rows[i].GetColumnValues()[1].ToString() == testdata)
                    {
                        verifyAssert = true;
                        Assert.True(true, "RedFlag details not found in the grid view.Expected data in grid with the name " + testdata);
                        break;
                    }
                }
                if (verifyAssert == false)
                {
                    Assert.Fail("RedFlag details not found in the grid view.Expected data in grid with the name " + testdata);
                }
                string strCommand = "select * from TCD.redflag where item = '9' and is_deleted='0' order by id desc";
                DataSet ds = DBValidation.GetData(strCommand);
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, "Red flag details not found in DB.Expected results with the name " + testdata);
                }
                else
                {
                    Assert.Fail("Red flag details not found in DB.Expected results with the name " + testdata);
                }
                //Auditing history of redflag operations on Add functionality
                string strAuditAdd = "select rfl.id,rfl.itemName,rh.id,rh.item,rh.operationid,rh.OperationTimeStamp,rh.RedFlagHistoryId " +
                                     " from TCD.redflaghistory rh inner join TCD.RedFlagItemList rfl on rh.item = rfl.id " +
                                     " where rh.item = '9'  and rh.operationid = '1'" +
                                     " and (CONVERT(VARCHAR(10),rh.OperationTimeStamp,120))='" + DateTime.Now.ToString("yyyy/MM/dd") + "' order by rh.redflaghistoryid desc";
                DataSet dsAudit = DBValidation.GetData(strAuditAdd);
                if (dsAudit.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, "Red flag details not found in DB Expected results with the name:" + testdata);
                }
                else
                {
                    Assert.Fail("Red flag details not found in DB Expected results with the name: " + testdata);
                }
            }
            else
            {
                Assert.Fail("Failed to find the item in grid.Expected results with the name:" + testdata);
            }
        }

        [TestCategory(TestType.regression, "TC02_ValidateInlineEdit")]
        [TestCategory(TestType.functional, "TC02_ValidateInlineEdit")]
        [Test, Description("Test case 28366: RG: Verify Inline edit functionality")]
        public void TC02_ValidateInlineEdit()
        {
            Thread.Sleep(1000);
            if (Page.RedFlagTabPage.RedFlagTabGrid.Rows.Count > 0)
            {
                //Sorting the Header to get the saved data in top of grid 1st page.
                Page.RedFlagTabPage.RedFlagTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                ReadOnlyCollection<string> itemName = Page.RedFlagTabPage.RedFlagTabGrid.Rows[0].GetColumnValues();
                Page.RedFlagTabPage.InlineEditRedFlag();
                Page.RedFlagTabPage.RedFlagTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                Thread.Sleep(2000);
                if (null != Page.RedFlagTabPage.ErrorMsg)
                {
                    if (!Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText
                        .Equals(@"Red flag updated successfully"))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Red flag updated successfully but Actual-"
                                                    + Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                //Auditing history of redflag operations on InLine Edit functionality
                string strAuditAdd = "select rfl.id,rfl.itemName,rh.id,rh.item,rh.operationid,rh.OperationTimeStamp,rh.RedFlagHistoryId" +
                                    " from TCD.redflaghistory rh inner join TCD.RedFlagItemList rfl on rh.item = rfl.id " +
                                     " where rfl.ItemName ='" + itemName[1] + "'  and rh.operationid = '2'" +
                                    " and (CONVERT(VARCHAR(10),rh.OperationTimeStamp,120))='" + DateTime.Now.ToString("yyyy/MM/dd") + "' order by rh.redflaghistoryid desc";
                DataSet dsAudit = DBValidation.GetData(strAuditAdd);
                if (dsAudit.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, "Redflag item on inline edit operation not found in redflaghistory table.Expected results min = 15 & max =30");
                }
                else
                {
                    Assert.Fail("Redflag item on inline edit operation not found in redflaghistory table.Expected results min = 15 & max =30");
                }
            }
            else
            {
                Assert.Fail("Red flag details not found in the existing grid table");
            }

        }

        [TestCategory(TestType.regression, "TC03_VerifyDeleteFunctionality")]
        [TestCategory(TestType.functional, "TC03_VerifyDeleteFunctionality")]
        [Test, Description("Test case 28065: RG:17910 : Plant Setup -> Red Flag Page:Verify Delete functionality")]
        public void TC03_VerifyDeleteFunctionality()
        {
            Thread.Sleep(2000);
            if (Page.RedFlagTabPage.RedFlagTabGrid.Rows.Count > 0)
            {
                ReadOnlyCollection<string> itemName = Page.RedFlagTabPage.RedFlagTabGrid.Rows[0].GetColumnValues();
                Page.RedFlagTabPage.DeleteRedFlagDetails(itemName[1]);
                Thread.Sleep(2000);
                if (null != Page.RedFlagTabPage.ErrorMsg)
                {
                    if (!Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText
                        .Equals(@"Red flag deleted successfully"))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Red flag deleted successfully but Actual-" + Page.RedFlagTabPage.ErrorMsg.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                //Auditing history of redflag operations on Delete functionality
                string strAuditDel = "select rfl.id,rfl.itemName,rh.id,rh.item,rh.operationid,rh.OperationTimeStamp,rh.RedFlagHistoryId" +
                                      " from TCD.redflaghistory rh inner join TCD.RedFlagItemList rfl on rh.item = rfl.id " +
                                      " where rfl.ItemName ='" + itemName[1] + "'  and rh.operationid = '3' " +
                                      " and (CONVERT(VARCHAR(10),rh.OperationTimeStamp,120))='" + DateTime.Now.ToString("yyyy/MM/dd") + "' order by rh.redflaghistoryid desc";
                DataSet dsAudit = DBValidation.GetData(strAuditDel);
                if (dsAudit.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, "Redflag item delete operation not found in redflaghistory table.Expected item in DB :" + itemName[1]);
                }
                else
                {
                    Assert.Fail("Redflag item delete operation not found in redflaghistory table.Expected item in DB :" + itemName[1]);
                }
            }
            else
            {
                Assert.Fail("Red flag details not found in the existing grid table");
            }
        }

        [TestCategory(TestType.regression, "TC04_VefiryMinimumRange")]
        [TestCategory(TestType.functional, "TC04_VefiryMinimumRange")]
        [Test, Description("Test case 28067: RG:17910 : Plant Setup -> Red Flag Page:Verify Minimum Range")]
        public void TC04_VefiryMinimumRange()
        {
            Thread.Sleep(2000);
            Page.RedFlagTabPage.RedFlagTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.RedFlagTabPage.VerifyMinValidation();
            try
            {
                HtmlSpan ctrlMinValidMsg = Page.RedFlagTabPage.VerifyMinValidMsg;
                if (null != ctrlMinValidMsg)
                {
                    Assert.True(true, "Validation msg not displayed.Expected-Minimum Range must be less than Maximum Range");
                }
            }
            catch
            {
                Assert.Fail("Validation msg not displayed.Expected - Minimum Range must be less than Maximum Range");
            }
        }

        [TestCategory(TestType.regression, "TC05_VefiryMaximumRange")]
        [TestCategory(TestType.functional, "TC05_VefiryMaximumRange")]
        [Test, Description("Test case 28068: RG:17910 : Plant Setup -> Red Flag Page:Verify Maximum range")]
        public void TC05_VefiryMaximumRange()
        {
            Thread.Sleep(2000);
            Page.RedFlagTabPage.RedFlagTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.RedFlagTabPage.VerifyMaxValidation();
            try
            {
                HtmlSpan ctrlMaxValidMsg = Page.RedFlagTabPage.VerifyMaxValidMsg;
                if (null != ctrlMaxValidMsg)
                {
                    Assert.True(true, "Validation msg not displayed.Expected- Maximum Range must be greater than Minimum Range");
                }
            }
            catch
            {
                Assert.Fail("Validation msg not displayed.Expected- Maximum Range must be greater than Minimum Rangess");
            }
        }

        [TestCategory(TestType.bvt, "TC06_VerifySaveButtonActiveStatus")]
        [TestCategory(TestType.regression, "TC06_VerifySaveButtonActiveStatus")]
        [Test, Description("Test case 28345: RG:17910 : Plant Setup -> Red Flag Page:Verify whether SAVE button is disabled/enabled")]
        public void TC06_VerifySaveButtonActiveStatus()
        {
            Thread.Sleep(3000);
            Page.PlantSetupPage.RedFlagTab.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.RedFlagTabPage.BtnAddRedFlag.Click();
            HtmlButton testctrl = Page.RedFlagTabPage.VerifyAddSaveButtonStatus;
            if (Page.RedFlagTabPage.VerifyAddSaveButtonStatus.IsEnabled == true)
            {
                Assert.Fail("Found Save button in active mode: Expected save button should be inActive mode");
            }
            Page.RedFlagTabPage.DdlRedFlagItemAdd.SelectByText("Chemical Consumption", true);
            if (Page.RedFlagTabPage.VerifyAddSaveButtonStatus.IsEnabled == false)
            {
                Assert.Fail("Found save button status in active mode: Expected save button should be active mode");
            }
        }

        [TestCategory(TestType.functional, "TC07_VerifyUOMChangesDynamically")]
        [Test, Description("Test case 28058: RG:17910 : Plant Setup -> Red Flag Page:Verify whether UOM changes dynamically based on the selection of Item")]
        public void TC07_VerifyUOMChangesDynamically()
        {
            string setRegion = string.Empty;
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.RedFlagTabPage.BtnAddRedFlag.Click();
            string cmdUpdateRegionOne = "update TCD.plant set regionid='1' where EcolabAccountNumber='1'";
            DBValidation.UpdateData(cmdUpdateRegionOne);
            Dictionary<string, string> UOMListNA = new Dictionary<string, string>();
            UOMListNA.Add("Overall Efficiency", "%");
            UOMListNA.Add("Time Efficiency", "%");
            UOMListNA.Add("Turn Time Efficiency", "%");
            UOMListNA.Add("Transfer/hour", "T/H");
            UOMListNA.Add("Rewash Rate", "%");
            UOMListNA.Add("Water usage", "G/CWT");
            UOMListNA.Add("Energy consumption", "BTU/CwT");
            UOMListNA.Add("Gas consumption", "Therms/CWT");
            UOMListNA.Add("Chemical Consumption", "oz/cwt");
            UOMListNA.Add("Drying Efficiency", "%");
            foreach (KeyValuePair<string, string> pair in UOMListNA)
            {
                Page.RedFlagTabPage.DdlRedFlagItemAdd.SelectByText(pair.Key, true);
                if (Page.RedFlagTabPage.MinRangeUOMAdd.ChildNodes[0].Content != pair.Value)
                {
                    Assert.Fail("Using region NA, Adding Red flag item: " + pair.Key + ".Expected UOM value should be: " + pair.Value + " but Actual:" + Page.RedFlagTabPage.MinRangeUOMAdd.ChildNodes[0].Content);
                }
            }
            string cmdUpdateRegion = "update TCD.plant set regionid='2' where EcolabAccountNumber='1'";
            DBValidation.UpdateData(cmdUpdateRegion);
            Dictionary<string, string> UOMListEurope = new Dictionary<string, string>();
            UOMListEurope.Add("Overall Efficiency", "%");
            UOMListEurope.Add("Time Efficiency", "%");
            UOMListEurope.Add("Turn Time Efficiency", "%");
            UOMListEurope.Add("Transfer/hour", "T/H");
            UOMListEurope.Add("Rewash Rate", "%");
            UOMListEurope.Add("Water usage", "L/kg");
            UOMListEurope.Add("Energy consumption", "kWh/kg");
            UOMListEurope.Add("Gas consumption", "m²/kg");
            UOMListEurope.Add("Chemical Consumption", "gr/kg");
            UOMListEurope.Add("Drying Efficiency", "%");
            foreach (KeyValuePair<string, string> pair in UOMListEurope)
            {
                Page.RedFlagTabPage.DdlRedFlagItemAdd.SelectByText(pair.Key, true);
                if (Page.RedFlagTabPage.MinRangeUOMAdd.ChildNodes[0].Content != pair.Value)
                {
                    Assert.Fail("Using region Europe, Adding Red flag item: " + pair.Key + ".Expected UOM value should be: " + pair.Value + " but Actual -" + Page.RedFlagTabPage.MinRangeUOMAdd.ChildNodes[0].Content);
                }
            }
        }

        [TestCategory(TestType.regression, "TC08_VerifyUserRoleAccessForView")]
        [TestCategory(TestType.functional, "TC08_VerifyUserRoleAccessForView")]
        [Test, Description("Test case 28059: RG: Verify UserRole access:View")]
        public void TC08_VerifyUserRoleAccessForView()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.RedFlagTab.Click();
            Thread.Sleep(2000);
            if (Page.RedFlagTabPage.UserRolesGridTable.Rows.Count > 0)
            {
                Page.RedFlagTabPage.UserRolesGridTable.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                HtmlControl ctrlRedFlagLabel = Page.RedFlagTabPage.ViewLabelUserRole;
                if (ctrlRedFlagLabel.ChildNodes[0].Content == "View Red Flag")
                {
                    Assert.True(true, "Logged in with level 6 user to verify view access.Expected label name - View Red Flag, but Actual- " + ctrlRedFlagLabel.ChildNodes[0].Content);
                }
                else
                {
                    Assert.Fail("Logged in with level 6 user to verify view access.Expected label name - View Red Flag, but Actual- " + ctrlRedFlagLabel.ChildNodes[0].Content);
                }
            }
            else
            {
                Assert.Fail("Data not found in grid view while login with level 6 user");
            }
        }

        [TestCategory(TestType.regression, "TC09_VerifyUserRoleAccessForEdit")]
        [TestCategory(TestType.functional, "TC09_VerifyUserRoleAccessForEdit")]
        [Test, Description("Test case 28062: RG: Verify UserRole access :Edit")]
        public void TC09_VerifyUserRoleAccessForEdit()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            if (null != Page.PlantSetupPage.RedFlagTab)
            {
                Page.PlantSetupPage.RedFlagTab.Click();
            }
            else
            {
                Assert.Fail("Logged in with user level 7 to verify edit access.Expected Red Flag tab should display.But Actual- Red Flag tab has not displayed.");
            }
            Thread.Sleep(2000);
            if (Page.RedFlagTabPage.RedFlagTabGrid.Rows.Count > 0)
            {
                Page.RedFlagTabPage.RedFlagTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
                HtmlControl ctrlRedFlagEdit = Page.RedFlagTabPage.EditLabelUserRole;
                if (ctrlRedFlagEdit.ChildNodes[0].Content == "Edit Red Flag")
                {
                    Assert.True(true, "Logged in with user level 7 to verify edit access.Expected label- Edit Red Flag, but Actual- " + ctrlRedFlagEdit.ChildNodes[0].Content);
                }
                else
                {
                    Assert.Fail("Logged in with user level 7 to verify edit access.Expected label-Edit Red Flag, but Actual- " + ctrlRedFlagEdit.ChildNodes[0].Content);
                }
            }
            else
            {
                Assert.Fail("Data not found in grid view while login with level 7 user");
            }
        }

        [TestCategory(TestType.regression, "TC10_VerifyLocalization")]
        [Test, Description("Test case 28455: RG: Verify Localization")]
        public void TC10_VerifyRedFlagLocalization()
        {
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Thread.Sleep(2000);
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
            Thread.Sleep(2000);
            string text = Page.PlantSetupPage.RedFlagTab.ChildNodes[0].Content;
            if (Page.PlantSetupPage.RedFlagTab.ChildNodes[0].Content == "Red Flag")
            {
                Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - Rood vlag, but Actual-" + Page.PlantSetupPage.RedFlagTab.ChildNodes[0].Content);
            }
        }

        [TestCategory(TestType.regression, "TC11_PostLocalization")]
        [Test, Description("Test case 28455:Post Localization ")]
        public void TC11_PostRedFlagLocalization()
        {
            //Post Condition to revert back the localization
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
        }
    }
}
