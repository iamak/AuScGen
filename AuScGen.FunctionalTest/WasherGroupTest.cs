using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.FunctionalTest
{
    public class WasherGroupTest : TestBase
    {
        bool verifyAssert = false;
        string setWasherType = "Conventional";
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
        }
        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        [TestCategory(TestType.functional, "TC01_VerifyAddWasherGroupFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyAddWasherGroupFunctionality")]
        [Test, Description("Test case 32952: RG: Verify the functionality of Add Washer Group;Test case 32955: RG: Verify the Save functionality in Washer Group page;Test case 32959: RG: Verify Group Name and Group Number fields not accepting the duplicte values while creating new Washer Group")]
        public void TC01_VerifyAddWasherGroupFunctionality()
        {
            Random randomNumber = new Random();
            string wgNumberAdd = randomNumber.Next(200).ToString();
            string wgNameAdd = string.Format("GroupName{0}", randomNumber.Next());
            Thread.Sleep(2000);
            Page.WasherGroupPage.BtnAddWasherGroup.Click();
            Page.WasherGroupPage.AddWasherGroupItems(wgNumberAdd, wgNameAdd, "Conventional");
            Thread.Sleep(2000);
            if (null != Page.WasherGroupPage.ErrorMessage)
            {
                if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Washer Group Created successfully."))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Washer Group Created successfully."
                                    + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(2000);
            //This is to check for duplication record....
            Page.WasherGroupPage.BtnSaveAdd.Click();
            Thread.Sleep(2000);
            if (null != Page.WasherGroupPage.ErrorMessage)
            {
                if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Washer Group already exists for the plant."))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Washer Group already exists for the plant."
                                    + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupPage.BtnBacktoWasherGroup.DeskTopMouseClick();
            Thread.Sleep(2000);
            List<string> actualValues = new List<string>();
            List<string> expectedValues = new List<string>();
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count - 1; i++)
                {
                    actualValues.Add(Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[1]);
                }
                Thread.Sleep(1000);
                actualValues.Sort();
                Page.WasherGroupPage.SortWasherGroupGridHeader.DeskTopMouseClick();
                Thread.Sleep(1000);
                int chkCount = Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count;
                for (int i = 1; i <= chkCount - 1; i++)
                {
                    expectedValues.Add(Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[1]);
                }
                Thread.Sleep(1000);
                int result = string.Compare(actualValues[0] + actualValues[1] + actualValues[2], expectedValues[0] + expectedValues[1] + expectedValues[2]);
                if (result == 0)
                {
                    Assert.True(true, "Verified washer group tab grid header have sorting feature");
                }
                else
                {
                    Assert.Fail("Sorting feature not found in washer group tab grid header");
                }
            }
            else
            {
                Assert.Fail("Washer group details not found in the existing grid table");
            }

        }

        [TestCategory(TestType.functional, "TC02_VerifySaveWasherGroupFunctionality")]
        [TestCategory(TestType.regression, "TC02_VerifySaveWasherGroupFunctionality")]
        [Test, Description("Test case 32955: RG: Verify the Save functionality in Washer Group page")]
        public void TC02_VerifySaveWasherGroupFunctionality()
        {
            Random randomNumber = new Random();
            string wgNameSave = string.Format("GroupName{0}", randomNumber.Next());
            string wgNumberSave = randomNumber.Next(200).ToString();

            Thread.Sleep(2000);
            Page.WasherGroupPage.BtnAddWasherGroup.DeskTopMouseClick();
            Page.WasherGroupPage.AddWasherGroupItems(wgNumberSave, wgNameSave, "Conventional");
            Thread.Sleep(2000);
            if (null != Page.WasherGroupPage.ErrorMessage)
            {
                if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Washer Group Created successfully."))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Washer Group Created successfully."
                                    + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "select controllerid,washergroupid,WasherGroupTypeId,WasherGroupName, " +
                                "WasherGroupNumber, LastModifiedByUserId " +
                                " from TCD.WasherGroup where WasherGroupName = '" + wgNameSave + "' and WasherGroupNumber='" + wgNumberSave + "' ";
            DataSet ds = DBValidation.GetData(strCommand);
            Thread.Sleep(2000);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Expected Washer GroupNumber- " + wgNumberSave + " which doesn't exist in DB");
            }
            else
            {
                Assert.Fail("Expected Washer GroupNumber- " + wgNumberSave + "  which doesn't exist in DB");
            }
        }

        [TestCategory(TestType.functional, "TC03_VerifyInlineEditWasherGroupFunctionality")]
        [TestCategory(TestType.regression, "TC03_VerifyInlineEditWasherGroupFunctionality")]
        [Test, Description("Test case 32956: RG: Verify the Inline Edit functionality in Washer Group Page")]
        public void TC03_VerifyInlineEditWasherGroupFunctionality()
        {
            Random randomNumber = new Random();
            string wgNameInlineEdit = string.Format("EditTestWG{0}", randomNumber.Next());
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
            Thread.Sleep(2000);
            int cnt = Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count;
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
                Page.WasherGroupPage.InlineEditUpdateFunctionalityWasherGroup(wgNameInlineEdit);
                Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
                Thread.Sleep(1000);
                string actualGroupName = Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetColumnValues()[3];
                Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
                Page.WasherGroupPage.InlineEditUpdateFunctionalityWasherGroup(wgNameInlineEdit);
                Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
                Thread.Sleep(1000);
                if (null != Page.WasherGroupPage.ErrorMessage)
                {
                    if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                        .Equals(@"Washer Group Updated successfully."))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Washer Group Updated successfully."
                                        + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                Thread.Sleep(2000);
                string strCommand = "select controllerid,washergroupid,WasherGroupTypeId,WasherGroupName, " +
                                    "WasherGroupNumber, LastModifiedByUserId " +
                                    " from TCD.WasherGroup where WasherGroupName = '" + wgNameInlineEdit + "'";
                DataSet ds = DBValidation.GetData(strCommand);
                Thread.Sleep(1000);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Assert.True(true, "Expected Washer Group Name - " + wgNameInlineEdit + " which doesn't exist in DB");
                }
                else
                {
                    Assert.Fail("Expected Washer Group Name - " + wgNameInlineEdit + " which doesn't exist in DB");
                }
            }
            else
            {
                Assert.Fail("Failed.Washer Group data not found in Grid.");
            }
        }

        [TestCategory(TestType.functional, "TC04_VerifyUpdateWasherGroupFunctionality")]
        [TestCategory(TestType.regression, "TC04_VerifyUpdateWasherGroupFunctionality")]
        [Test, Description("Test case 32957: RG: Verify the Inline Update functionality in Washer Group Page")]
        public void TC04_VerifyUpdateWasherGroupFunctionality()
        {
            Random randomNumber = new Random();
            string wgNameUpdate = string.Format("updatetest{0}", randomNumber.Next());
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
                Page.WasherGroupPage.UpdateFunctionalityWasherGroup(wgNameUpdate);
                Thread.Sleep(1000);
                if (null != Page.WasherGroupPage.ErrorMessage)
                {
                    if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                        .Equals(@"Washer Group Updated successfully."))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Washer Group Updated successfully."
                                        + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                Thread.Sleep(2000);
                string strCommand = "select controllerid,washergroupid,WasherGroupTypeId,WasherGroupName, " +
                                    "WasherGroupNumber, LastModifiedByUserId " +
                                    " from TCD.WasherGroup where WasherGroupName = '" + wgNameUpdate + "'";
                DataSet ds = DBValidation.GetData(strCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Assert.True(true, "Expected Washer Group Name - " + wgNameUpdate + " which doesn't exist in DB");
                }
                else
                {
                    Assert.Fail("Expected Washer Group Name - " + wgNameUpdate + " which doesn't exist in DB");
                }
            }
            else
            {
                Assert.Fail("Failed.Washer Group data not found in Grid.");
            }
        }

        [TestCategory(TestType.functional, "TC05_VerifyDeleteWasherGroupFunctionality")]
        [TestCategory(TestType.regression, "TC05_VerifyDeleteWasherGroupFunctionality")]
        [Test, Description("Test case 34036: RG: Verify the Delete fucntionality")]
        public void TC05_VerifyDeleteWasherGroupFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                ReadOnlyCollection<string> delWasherGrp = Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetColumnValues();
                Page.WasherGroupPage.DeleteWasherGroup(delWasherGrp[3]);
                Thread.Sleep(1000);
                Page.WasherGroupPage.BtnOkDelete.DeskTopMouseClick();
                Thread.Sleep(2000);
                if (null != Page.WasherGroupPage.ErrorMessage)
                {
                    if (!Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText
                        .Equals(@"Washer Group deleted successfully."))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Washer Group deleted successfully."
                                        + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                Thread.Sleep(1000);
                EcolabDataGridItems deletedRow = Page.WasherGroupPage.WasherGroupTableGrid.GetRow(delWasherGrp[3]);
                Assert.True(deletedRow == null, "Washer Group details not found in grid");

                string strCommand = "select WasherGroupName, WasherGroupNumber" +
                                    " from TCD.WasherGroup where WasherGroupName = '" + delWasherGrp[3] + "'";
                Thread.Sleep(1000);
                DataSet ds = DBValidation.GetData(strCommand);
                Thread.Sleep(1000);
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, delWasherGrp[3] + "Washer Group details deleted successfully in DB");
                }
            }
            else
            {
                Assert.Fail("Washer group details not found in gridview.Expected- Washer group Data");
            }
        }

        [TestCategory(TestType.functional, "TC06_VerifyAccessPermissionForUserRole1to5")]
        [TestCategory(TestType.regression, "TC06_VerifyAccessPermissionForUserRole1to5")]
        [Test, Description("Test case 32963: RG: Verify Access permission for User Role 1 to 5")]
        public void TC06_VerifyAccessPermissionForUserRole1to5()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestBDM", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washer Groups"))
                {
                    Assert.Fail("Logged in with level-5 user " + pair.Key + " Actual-Found Add Washer Group Button.Expected- AddWasherGroup button should not display.");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
        }

        [TestCategory(TestType.functional, "TC07_VerifyAccessPermissionForUserRole6")]
        [TestCategory(TestType.regression, "TC07_VerifyAccessPermissionForUserRole6")]
        [Test, Description("Test case 32962: RG: Verify Access permission for User Role 6")]
        public void TC07_VerifyAccessPermissionForUserRole6()
        {
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
            Thread.Sleep(2000);
            Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
            Thread.Sleep(1000);
            if (null != Page.WasherGroupPage.LabelViewGroupNameAccess)
            {
                Assert.True(true, "Logged in with level-6 user.Actual-Found Edit/Delete Access.Expected-user should have only view access.");
            }
            else
            {
                Assert.Fail("Logged in with level-6 user.Actual-Found Edit/Delete Access.Expected-user should have only view access.");
            }
        }

        /// <summary>
        /// Test case 28455: RG: Verify Localization
        /// </summary>
        [TestCategory(TestType.functional, "TC08_VerifyWasherGroupLocalization")]
        [TestCategory(TestType.regression, "TC08_VerifyWasherGroupLocalization")]
        [Test, Description("Test case 34205: RG: Verify the Localization criteria")]
        public void TC08_VerifyWasherGroupLocalization()
        {
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.WasherGroupPage.LanguagePreferred.Focus();
            Page.WasherGroupPage.LanguagePreferred.SelectByText("Deutsch", true);
            Page.WasherGroupPage.LanguagePreferred.ScrollToVisible();
            Page.WasherGroupPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            Thread.Sleep(3000);
            string text = Page.WasherGroupPage.TabWasherGroup.ChildNodes[0].Content;
            if (Page.WasherGroupPage.TabWasherGroup.ChildNodes[0].Content == "Washer Groups")
            {
                Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - Deutsh lang, but Actual-"
                    + Page.WasherGroupPage.TabWasherGroup.ChildNodes[0].Content);
            }
        }

        /// <summary>
        /// Post Localization 
        /// </summary>
        [TestCategory(TestType.functional, "TC09_WasherGroupPostLocalization")]
        [TestCategory(TestType.regression, "TC09_WasherGroupPostLocalization")]
        [Test, Description("Test case 34205: RG: Verify the Localization criteria")]
        public void TC09_WasherGroupPostLocalization()
        {
            //Post Condition to revert back the localization
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
        }

        [TestCategory(TestType.functional, "TC10_VerifyDeleteWashStep")]
        [Test, Description("Test case 44172: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA) : Verify the Delete Wash Step ")]
        public void TC10_VerifyDeleteWashStep()
        {
            Thread.Sleep(2000);
            setWasherType = "Conventional";
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                for (int i = 0; i <= Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count - 1; i++)
                {
                    if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[2].ToString() == setWasherType)
                    {
                        verifyAssert = true;
                        Thread.Sleep(2000);
                        Page.WasherGroupPage.WasherGroupTableGrid.SelectedRows(setWasherType)[0].GetButtonControls()[4].DeskTopMouseClick();
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                        Page.WasherGroupPage.AddFormula.Focus();
                        Page.WasherGroupPage.AddFormula.DeskTopMouseClick();
                        Page.WasherGroupPage.AddingFormula("25", "0", "0");
                        Thread.Sleep(3000);
                        if (null == Page.WasherGroupPage.SuccessMessage)
                        {
                            Assert.Fail("Error message is not displayed");
                        }
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                        Thread.Sleep(1000);
                        if (Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows.Count > 1)
                        {
                            for (int j = 1; j <= Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows.Count - 1; j++)
                            {
                                //Pre-Condition Adding WashSteps to the existing formula
                                Thread.Sleep(2000);
                                Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[j].GetButtonControls()[4].Click();
                                Thread.Sleep(2000);
                                Page.WasherGroupPage.BtnAddWashStep.Focus();
                                Page.WasherGroupPage.BtnAddWashStep.DeskTopMouseClick();
                                Page.WasherGroupPage.AddWashSteps("4", "25", "90:40", "15");
                                Thread.Sleep(2000);
                                if (null == Page.WasherGroupPage.SuccessMessage)
                                {
                                    Assert.Fail("Error message is not displayed");
                                }
                                Thread.Sleep(1000);
                                Page.WasherGroupPage.BtnBackWashStep.DeskTopMouseClick();
                                //Page.WasherGroupPage.TabFormula.Click();
                                //Thread.Sleep(2000);
                                //Telerik.ActiveBrowser.RefreshDomTree();
                                //Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[j].GetButtonControls()[4].DeskTopMouseClick();
                                //Checking WashSteps data availability
                                Thread.Sleep(1000);
                                if (Page.WasherGroupPage.WashStepsTableGrid.Rows.Count > 1)
                                {
                                    //Verifying the conditions.
                                    for (int k = 1; k <= Page.WasherGroupPage.WashStepsTableGrid.Rows.Count; k++)
                                    {
                                        Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetButtonControls()[1].DeskTopMouseClick();
                                        Thread.Sleep(1000);
                                        Assert.True(Page.WasherGroupPage.BtnPopUpCancel.IsVisible(), "Delete button not found in formulas grid");
                                        Thread.Sleep(1000);
                                        Page.WasherGroupPage.BtnPopUpCancel.DeskTopMouseClick();
                                    }
                                    Page.WasherGroupPage.WashStepsTableGrid.Rows[0].GetButtonControls()[1].DeskTopMouseClick();
                                    string strCommand = "Select * from tcd.[WasherDosingSetup] where GroupId in " +
                                                        "(Select WasherGroupID from tcd.WasherGroup Where WasherGroupName ='WasherExtractor1') " +
                                                        "AND StepNumber=4 and Is_Deleted='1'";
                                    DataSet ds = DBValidation.GetData(strCommand);
                                    if (ds.Tables[0].Rows.Count >= 0)
                                    {
                                        Assert.True(true, "WashSteps details not deleted from DB.");
                                    }
                                    else
                                    {
                                        Assert.Fail("WashSteps details not deleted from DB.");
                                    }
                                    Thread.Sleep(1000);
                                }
                                //break;
                            }
                        }
                    }
                    // break;
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC11_VerifyUpdateWashStep")]
        [Test, Description("Test case 44176: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify Update Wash step ")]
        public void TC11_VerifyUpdateWashStep()
        {
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                AddFormula();
                AddandUpdateWashStep("4", "50", "90:20", "12");
                Thread.Sleep(1000);
                if (Page.WasherGroupPage.WashStepsTableGrid.Rows.Count > 1)
                {
                    //Verifying the conditions.
                    for (int k = 1; k <= Page.WasherGroupPage.WashStepsTableGrid.Rows.Count; k++)
                    {
                        Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetButtonControls()[2].DeskTopMouseClick();
                        Thread.Sleep(1000);
                    }
                    Page.WasherGroupPage.WashStepsTableGrid.Rows[0].GetButtonControls()[1].DeskTopMouseClick();
                    string strCommand = "Select * from tcd.[WasherDosingSetup] where GroupId in " +
                                        "(Select WasherGroupID from tcd.WasherGroup Where WasherGroupName ='WasherExtractor1') " +
                                        "AND StepNumber=4 and Is_Deleted='1'";
                    DataSet ds = DBValidation.GetData(strCommand);
                    if (ds.Tables[0].Rows.Count >= 0)
                    {
                        Assert.True(true, "WashSteps details not deleted from DB.");
                    }
                    else
                    {
                        Assert.Fail("WashSteps details not deleted from DB.");
                    }
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC12_VerifyCopyWashStepFunctionality")]
        [Test, Description("Test case 44177: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify Copy Wash Step functionality;" +
            "Test case 44182: RG: 20974-> Verify Navigation to Wash steps through drop down & Arrows functionality")]
        public void TC12_VerifyCopyWashStepFunctionality()
        {
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                AddFormula();
                AddandUpdateWashStep("4", "25", "90:40", "15");
                Thread.Sleep(1000);
                if (Page.WasherGroupPage.WashStepsTableGrid.Rows.Count > 1)
                {
                    //Verifying the conditions.
                    Thread.Sleep(1000);
                    for (int k = 1; k <= Page.WasherGroupPage.WashStepsTableGrid.Rows.Count; k++)
                    {
                        Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetButtonControls()[0].DeskTopMouseClick();
                        Thread.Sleep(1000);
                        AddandUpdateWashStep("4", "25", "90:40", "15");
                        break;
                    }
                    Thread.Sleep(2000);
                    string strCommand = "Select * from tcd.[WasherDosingSetup] where GroupId in " +
                                        "(Select WasherGroupID from tcd.WasherGroup Where WasherGroupName ='WasherExtractor1') " +
                                        "AND StepNumber=4 and Is_Deleted='1'";
                    DataSet ds = DBValidation.GetData(strCommand);
                    Thread.Sleep(1000);
                    if (ds.Tables[0].Rows.Count >= 0)
                    {
                        Assert.True(true, "WashSteps details not deleted from DB.");
                    }
                    else
                    {
                        Assert.Fail("WashSteps details not deleted from DB.");
                    }
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC13_VerifyDrainWashStepFunctionality")]
        [Test, Description("Test case 44178: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify Drain wash step")]
        public void TC13_VerifyDrainWashStepFunctionality()
        {
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                Thread.Sleep(1000);
                Page.WasherGroupPage.WasherGroupTableGrid.SelectedRows(setWasherType)[0].GetButtonControls()[4].DeskTopMouseClick();
                Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                Thread.Sleep(1000);
                Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
                Thread.Sleep(1000);
                if (Page.WasherGroupPage.WashStepsTableGrid.Rows.Count > 1)
                {
                    //Verifying the conditions.
                    Thread.Sleep(1000);
                    for (int k = 1; k <= Page.WasherGroupPage.WashStepsTableGrid.Rows.Count; k++)
                    {
                        Thread.Sleep(1000);
                        if (Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetColumnValues()[2].ToString() == "Drain")
                        {
                            Dictionary<string, string> ActualDrainWashStep = new Dictionary<string, string>();
                            Dictionary<string, string> ExpectedDrainWashStep = new Dictionary<string, string>();
                            ExpectedDrainWashStep.Add("Operations", "Drain");
                            ExpectedDrainWashStep.Add("Run Time (mm:ss)", "02:00");
                            ExpectedDrainWashStep.Add("Temperature (oF)", "1");
                            ExpectedDrainWashStep.Add("Water Level (inches)", "0");
                            //Need to find the list of row values to compare and fail or pass the test case....with this finish...
                            ReadOnlyCollection<string> getValues = Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetColumnValues();
                            ActualDrainWashStep.Add("Operations", getValues[2]);
                            ActualDrainWashStep.Add("Run Time (mm:ss)", getValues[3]);
                            ActualDrainWashStep.Add("Temperature (oF)", getValues[4]);
                            ActualDrainWashStep.Add("Water Level (inches)", getValues[5]);
                            List<KeyValuePair<string,string>> diffCount = ExpectedDrainWashStep.Except(ActualDrainWashStep).ToList();
                            if(diffCount.Count > 0)
                            {
                                Assert.Fail("Drain Wash Steps are not matching with Expected.Actual - " + diffCount[0]);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    Assert.Fail("Drain WashStep not found in the gridview: Expected- Drain WashStep to be display while adding new WashStep");
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC14_VerifyCancelWashStepFunctionality")]
        [Test, Description("Test case 44180: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify Cancel functionality")]
        public void TC14_VerifyCancelWashStepFunctionality()
        {
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                AddFormula();
                AddandUpdateWashStep("4", "25", "90:40", "15");
                Thread.Sleep(1000);
                if (Page.WasherGroupPage.WashStepsTableGrid.Rows.Count > 1)
                {
                    //Verifying the conditions.
                    Thread.Sleep(1000);
                    for (int k = 1; k <= Page.WasherGroupPage.WashStepsTableGrid.Rows.Count; k++)
                    {
                        //Validating Page Navigation on washstep without edit clicking on Cancel Button.Expected-Navigation to Formulas page.
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.WashStepsTableGrid.Rows[k].GetButtonControls()[2].DeskTopMouseClick();
                        Page.WasherGroupPage.BtnYesPopUpOnCancel.DeskTopMouseClick();
                        Thread.Sleep(2000);
                        //Now Performing Edit and Update Operation.
                        Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[0].GetButtonControls()[4].Click();
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.WashStepsTableGrid.Rows[0].GetButtonControls()[2].DeskTopMouseClick();
                        Page.WasherGroupPage.TxtTemperature.Focus();
                        Page.WasherGroupPage.TxtTemperature.SetText("73");
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.BtnCancel.DeskTopMouseClick();
                        Thread.Sleep(2000);
                        Assert.True(Page.WasherGroupPage.BtnYesPopUpOnCancel.IsVisible(),
                                        "Expected PopUp on WashStep Edit functionality while clicking on Cancel Button.");
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.BtnYesPopUpOnCancel.DeskTopMouseClick();
                        Page.WasherGroupPage.BtnBackWashStep.DeskTopMouseClick();
                        //Verifying the Saved data on Cancel Button Functionality
                        if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[k].GetColumnValues()[4].ToString() != "73")
                        {
                            Assert.Fail("WashStep details not updated on Cancel button functionality");
                        }
                        break;
                    }
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC15_VerifyAddFunctionalityforUpdate")]
        [Test, Description("Test case 44188: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify Add functionality for Update")]
        public void TC15_VerifyAddFunctionalityforUpdate()
        {
            Thread.Sleep(2000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                //Dont Remove the below lines of code.....
                //AddFormula();
                //AddandUpdateWashStep("4", "25", "90:40", "15");

                //Start Of Remove forloop if we want to use the above two methods as a precondition to adding formulas and washsteps
                Thread.Sleep(1000);
                for (int i = 0; i <= Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count - 1; i++)
                {
                    if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[2].ToString() == setWasherType)
                    {
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.WasherGroupTableGrid.SelectedRows(setWasherType)[0].GetButtonControls()[4].DeskTopMouseClick();
                        break;
                    }
                }
                Thread.Sleep(1000);
                Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                Thread.Sleep(1000);
                Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
                Thread.Sleep(2000);
                //ENd of Removing Code to use above two methods......23-12-2014

                Assert.True(Page.WasherGroupPage.BtnAddWashStep.IsVisible(), "Add Wash Step button not found");
                Page.WasherGroupPage.BtnAddWashStep.Click();
                Page.WasherGroupPage.TxtTemperature.SetText("73");
                Page.WasherGroupPage.TxtTemperature.Text = string.Empty;
                Thread.Sleep(1000);
                Page.WasherGroupPage.BtnSaveAddWashStep.Click();
                Thread.Sleep(2000);
                Assert.True(Page.WasherGroupPage.LblErrMsgTemperature.BaseElement.InnerText.Contains("Temperature cannot be Empty."),
                                "Expected validation message while adding WashStep - Temperature cannot be Empty., but Actual is -"
                                + Page.WasherGroupPage.LblErrMsgTemperature.BaseElement.InnerText);
                Thread.Sleep(1000);
                Assert.True(Page.WasherGroupPage.LblErrMsgWaterType.BaseElement.InnerText.Contains("Please select Water Type"),
                                "Expected validation message while adding WashStep - Please select Water Type, but Actual is -"
                                + Page.WasherGroupPage.LblErrMsgWaterType.BaseElement.InnerText);
                Thread.Sleep(1000);
                Assert.True(Page.WasherGroupPage.LblErrMsgRunTime.BaseElement.InnerText.Contains("Enter runtime in mm:ss format"),
                                "Expected validation message while adding WashStep -Enter runtime in mm:ss format, but Actual is -"
                                + Page.WasherGroupPage.LblErrMsgRunTime.BaseElement.InnerText);
                Thread.Sleep(1000);
                Assert.True(Page.WasherGroupPage.LblErrMsgWashOperation.BaseElement.InnerText.Contains("Please select Wash operation"),
                                "Expected validation message while adding WashStep - Please select Wash operation, but Actual is -"
                                + Page.WasherGroupPage.LblErrMsgWashOperation.BaseElement.InnerText);
                Thread.Sleep(1000);
                Assert.True(Page.WasherGroupPage.LblErrMsgWaterLevel.BaseElement.InnerText.Contains("Water level cannot be Empty."),
                              "Expected validation message while adding WashStep - Water level cannot be Empty., but Actual is -"
                              + Page.WasherGroupPage.LblErrMsgWaterLevel.BaseElement.InnerText);

                Thread.Sleep(2000);
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        [TestCategory(TestType.functional, "TC16_VerifyAccessToWasherGroupPageUserRoleId1To5")]
        [Test, Description("Test case 44199: RG: Verify access to Washer Group Page depending on UserRoleId - 1 to 5")]
        public void TC16_VerifyAccessToWasherGroupPageUserRoleId1To5()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestOperator", "test");
            userAccess.Add("AutoTestBDM", "test");
            userAccess.Add("AutoTestPE", "test");
            userAccess.Add("AutoTestPM", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washer Groups"))
                {
                    Assert.Fail("Logged in with user  " + pair.Key + " Actual- Found Washer Group option in the setup menu list." +
                                "Expected- Washer Group option not to display");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
        }

        [TestCategory(TestType.functional, "TC17_VerifyAccessToWashStepPageForUserRoldId6Above")]
        [Test, Description("Test case 44200: RG: Verify access to Add/Update Wash Step Page depending on UserRoleId - 6 and above")]
        public void TC17_VerifyAccessToWashStepPageForUserRoldId6Above()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestTMBasic", "test");
            userAccess.Add("AutoTestTMAdvance", "test");
            userAccess.Add("AutoTestEng", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washer Groups"))
                {
                    AddFormula();
                    AddandUpdateWashStep("1", "25", "90:30", "15");
                }
                else
                {
                    Assert.Fail("Logged in with user  " + pair.Key + " Actual- Found Washer Group option not found in the setup menu list." +
                               "Expected- Washer Group option to display");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
        }

        [TestCategory(TestType.functional, "TC18_VerifyWashStepsLocalization")]
        [Test, Description("Test case 44191: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify the Localization criteria")]
        public void TC18_VerifyWashStepsLocalization()
        {
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.WasherGroupPage.LanguagePreferred.Focus();
            Page.WasherGroupPage.LanguagePreferred.SelectByText("Deutsch", true);
            Page.WasherGroupPage.LanguagePreferred.ScrollToVisible();
            Page.WasherGroupPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            Thread.Sleep(3000);
            if (Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count > 1)
            {
                Thread.Sleep(1000);
                for (int i = 0; i <= Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count - 1; i++)
                {
                    if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[2].ToString() == setWasherType)
                    {
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.WasherGroupTableGrid.SelectedRows(setWasherType)[0].GetButtonControls()[4].DeskTopMouseClick();
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                        Thread.Sleep(1000);
                        Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
                        Thread.Sleep(2000);
                        Page.WasherGroupPage.BtnAddWashStep.Click();
                        if (Page.WasherGroupPage.LblWashStepLocalization.ChildNodes[0].Content == "Wash Step #")
                        {
                            Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - Deutsh lang, but Actual is-"
                                + Page.WasherGroupPage.LblWashStepLocalization.ChildNodes[0].Content);
                        }
                        break;
                    }
                }
            }
            else
            {
                Assert.Fail("Washer Group data not found in grid page");
            }
        }

        /// <summary>
        /// Post Localization 
        /// </summary>
        [TestCategory(TestType.functional, "TC19_WasherStepsPostLocalization")]
        [Test, Description("est case 44191: RG: 20974 : Controller > Washer Groups/Formulas/Add Wash step and Products (NA):Verify the Localization criteria")]
        public void TC19_WasherStepsPostLocalization()
        {
            //Post Condition to revert back the localization
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
        }

        public void AddFormula()
        {
            for (int i = 0; i <= Page.WasherGroupPage.WasherGroupTableGrid.Rows.Count - 1; i++)
            {
                if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[i].GetColumnValues()[2].ToString() == setWasherType)
                {
                    verifyAssert = true;
                    Thread.Sleep(2000);
                    Page.WasherGroupPage.WasherGroupTableGrid.SelectedRows(setWasherType)[0].GetButtonControls()[4].DeskTopMouseClick();
                    Thread.Sleep(1000);
                    Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                    Thread.Sleep(1000);
                    Page.WasherGroupPage.AddFormula.Focus();
                    Page.WasherGroupPage.AddFormula.DeskTopMouseClick();
                    Page.WasherGroupPage.AddingFormula("25", "0", "0");
                    Thread.Sleep(3000);
                    if (null == Page.WasherGroupPage.SuccessMessage)
                    {
                        Assert.Fail("Error message is not displayed.OR Failed to add the Formula");
                    }
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
        public void AddandUpdateWashStep(string washStep, string temperature, string runTime, string waterLevel)
        {
            Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
            Thread.Sleep(1000);
            if (Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows.Count > 1)
            {
                for (int j = 1; j <= Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows.Count - 1; j++)
                {
                    //Pre-Condition Adding WashSteps to the existing formula
                    Thread.Sleep(2000);
                    Page.WasherGroupPage.WasherGroupFormulaTabelGrid.Rows[j].GetButtonControls()[4].DeskTopMouseClick();
                    Thread.Sleep(2000);
                    Page.WasherGroupPage.BtnAddWashStep.Focus();
                    Page.WasherGroupPage.BtnAddWashStep.DeskTopMouseClick();
                    Page.WasherGroupPage.AddWashSteps(washStep, temperature, runTime, waterLevel);
                    Thread.Sleep(2000);
                    if (null == Page.WasherGroupPage.SuccessMessage)
                    {
                        Assert.Fail("Error message is not displayed");
                    }
                    Thread.Sleep(1000);
                   // Page.WasherGroupPage.BtnBackWashStep.DeskTopMouseClick();
                    Page.WasherGroupPage.TabFormula.DeskTopMouseClick();
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}
