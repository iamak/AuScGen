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
    public class WashersTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }
        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        string[] UpdateValues = {"TunnelOne","5","8"};
        [TestCategory(TestType.functional, "TC01_VerifyUpdateWasherFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyUpdateWasherFunctionality")]
        [Test, Description("Test case 38803: RG: Verifyt the Update functionality")]
        public void TC01_VerifyUpdateWasherFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToWashersPage();
            Thread.Sleep(2000);
            for (int i = 0; i <= Page.WashersPage.WashersListGridTable.Rows.Count - 1; i++)
            {
                if (Page.WashersPage.WashersListGridTable.Rows[i].GetColumnValues()[3].ToString() == "Tunnel")
                {
                    Page.WashersPage.WashersListGridTable.SelectedRows("Tunnel")[0].GetButtonControls()[1].DeskTopMouseClick();
                    bool value =  Page.WashersPage.UpdateWasherDetails(UpdateValues);
                    if(value == false)
                    {
                        Assert.Fail("Failed to update the washers data Expected- Data for Models,Controllers and Washers");
                    }
                    break;
                }
            }
            Thread.Sleep(2000);
            if (null != Page.WashersPage.SuccessMessage)
            {
                if (!Page.WashersPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Tunnel Details Updated successfully."))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Tunnel Details Updated successfully."
                                    + " but Actual:" + Page.WashersPage.SuccessMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(2000);
            string strCommand = "Select MachineName from [TCD].[MachineSetup] where MachineName = '" + UpdateValues[0] + "'";
            DataSet ds = DBValidation.GetData(strCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Expected MachineName -" + UpdateValues[0] + " which doesn't exist in DB");
            }
            else
            {
                Assert.Fail("Expected MachineName - " + UpdateValues[0] + " which doesn't exist in DB");
            }
        }

        [TestCategory(TestType.functional, "TC02_VerifyPromptPopUpOnNavigation")]
        [TestCategory(TestType.regression, "TC02_VerifyPromptPopUpOnNavigation")]
        [Test, Description("Test case 38805: RG:Verify the prompt is displayed when user navigates to other tab with out saving record")]
        public void TC02_VerifyPromptPopUpOnNavigation()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToWashersPage();
            Thread.Sleep(2000);
            if (Page.WashersPage.WashersListGridTable.Rows.Count > 0)
            {
                Page.WashersPage.WashersListGridTable.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
                Thread.Sleep(2000);
                Page.WashersPage.TxtName.Focus();
                Page.WashersPage.TxtName.Text = string.Empty;
                Page.WashersPage.TxtName.SetText("TunnelTesting");
                KeyBoardSimulator.KeyPress(Keys.Tab);
                Thread.Sleep(1000);
                Page.WashersPage.BtnBacktoWashersLink.Focus();
                Page.WashersPage.BtnBacktoWashersLink.DeskTopMouseClick();
                Thread.Sleep(2000);
                if (Page.WashersPage.BtnNoPopUp.IsVisible() == true)
                {
                    Page.WashersPage.BtnNoPopUp.DeskTopMouseClick();
                    Assert.True(true, "Expected - Prompt PopUp message on navigation with out saving the washers data");
                }
                else
                {
                    Assert.Fail("Expected - Prompt PopUp message not displayed on navigation with out saving the washers data");
                }
            }
            else
            {
                Assert.Fail("Data not found in the Washers List Page.");
            }
        }

        [TestCategory(TestType.functional, "TC03_VerifyDeleteWasherFunctionality")]
        [TestCategory(TestType.regression, "TC03_VerifyDeleteWasherFunctionality")]
        [Test, Description("Test case 38804: RG:Verify the Delete functionality;Test case 40642: RG: Verify the Delete Washer functionality")]
        public void TC03_VerifyDeleteWasherFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToWashersPage();
            Thread.Sleep(2000);
            if (Page.WashersPage.WashersListGridTable.Rows.Count > 0)
            {
                ReadOnlyCollection<string> delWasher = Page.WashersPage.WashersListGridTable.Rows[1].GetColumnValues();
                Page.WashersPage.DeleteWasherFromGridList(delWasher[2]);
                Page.WashersPage.BtnOkDelete.DeskTopMouseClick();
                Thread.Sleep(2000);
                if (null != Page.WashersPage.SuccessMessage)
                {
                    if (!Page.WashersPage.SuccessMessage.BaseElement.InnerText
                        .Equals(@"Washer deleted successfully."))
                    {
                        Assert.Fail("Incorrect error message is displayed,Expected: Washer deleted successfully."
                                        + " but Actual:" + Page.WasherGroupPage.ErrorMessage.BaseElement.InnerText);
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                Thread.Sleep(1000);
                ReadOnlyCollection<string> delWasherCancel = Page.WashersPage.WashersListGridTable.Rows[1].GetColumnValues();
                Page.WashersPage.DeleteWasherFromGridList(delWasherCancel[1]);
                Page.WashersPage.BtnCancelDelete.DeskTopMouseClick();

                EcolabDataGridItems deletedRow = Page.WashersPage.WashersListGridTable.GetRow(delWasher[2]);
                Assert.True(deletedRow == null, "Washer details not found in grid");
                string strCommand = "Select MachineName from [TCD].[MachineSetup] where isdeleted=0";
                DataSet ds = DBValidation.GetData(strCommand);
                Thread.Sleep(1000);
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, delWasher[2] + "Washer details deleted successfully in DB");
                }
            }
            else
            {
                Assert.Fail("Washer details not found in gridview.Expected- Washer Data");
            }
        }

        [TestCategory(TestType.bvt, "TC04_VerifyAccessToWasherListPageUserRole789")]
        [TestCategory(TestType.regression, "TC04_VerifyAccessToWasherListPageUserRole789")]
        [Test, Description("Test case 38887: RG: Verify the Access to Washer List page for User Role 7/8/9")]
        public void TC04_VerifyAccessToWasherListPageUserRole789()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestEng", "test");
            userAccess.Add("AutoTestTMAdvance", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washers"))
                {
                    Page.PlantSetupPage.TopMainMenu.NavigateToWashersPage();
                    Thread.Sleep(2000);
                    for (int i = 0; i <= Page.WashersPage.WashersListGridTable.Rows.Count - 1; i++)
                    {
                        if (Page.WashersPage.WashersListGridTable.Rows[i].GetColumnValues()[3].ToString() == "Tunnel")
                        {
                            Page.WashersPage.WashersListGridTable.SelectedRows("Tunnel")[0].GetButtonControls()[1].DeskTopMouseClick();
                            Page.WashersPage.UpdateWasherDetails(UpdateValues);
                            break;
                        }
                    }
                }
                else
                {
                    Assert.Fail("Logged in with user " + pair.Key + " Actual- Washers tab not found in setup menu list.Expected- Washers tab should display.");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        [TestCategory(TestType.bvt, "TC05_VerifyAccessToWasherListPageUserRole123456")]
        [TestCategory(TestType.regression, "TC05_VerifyAccessToWasherListPageUserRole123456")]
        [Test, Description("Test case 38888: RG: Verify the Access to Washer List page for User Role 1/2/3/4/5;Test case 43203: RG: Verify the Access for User Role 1 to 5")]
        public void TC05_VerifyAccessToWasherListPageUserRole123456()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestTMBasic", "test");
            userAccess.Add("AutoTestPE", "test");
            userAccess.Add("AutoTestPM", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washers"))
                {
                    Assert.Fail("Logged in with user " + pair.Key + " Actual-Found Washers tab in the setup menu list.Expected- Washers tab should not display.");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        [TestCategory(TestType.bvt, "TC06_VerifyAccessToWasherListPageUserRole6")]
        [TestCategory(TestType.regression, "TC06_VerifyAccessToWasherListPageUserRole6")]
        [Test, Description("Test case 43204: RG: Verify the Access for User Role 6")]
        public void TC06_VerifyAccessToWasherListPageUserRole6()
        {
            TestFixture();
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestTMBasic", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Thread.Sleep(2000);
                List<string> lstMenuList = Page.PlantSetupPage.TopMainMenu.MenuItemsList;
                Thread.Sleep(2000);
                if (Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washers"))
                {
                    //Check for update and Edit options
                }
                else
                {
                    Assert.Fail("Logged in with user " + pair.Key + " Actual-Found Washers tab in the setup menu list.Expected- Washers tab should not display.");
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        /// <summary>
        /// Test case 28455: RG: Verify Localization
        /// </summary>
        [TestCategory(TestType.functional, "TC07_VerifyWashersLocalization")]
        [TestCategory(TestType.regression, "TC07_VerifyWashersLocalization")]
        [Test, Description("Test case 38808: RG:Verify the Localization criteria")]
        public void TC07_VerifyWashersLocalization()
        {
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
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.NavigateToWashersPage();
            Thread.Sleep(2000);
            if (Page.WashersPage.WashersTab.ChildNodes[0].Content == "Washers")
            {
                Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - Deutsh lang, but Actual-"
                    + Page.WashersPage.WashersTab.ChildNodes[0].Content);
            }
        }

        /// <summary>
        /// Post Localization 
        /// </summary>
        [TestCategory(TestType.functional, "TC08_WashersPostLocalization")]
        [TestCategory(TestType.regression, "TC08_WashersPostLocalization")]
        [Test, Description("Test case 34205: RG: Verify the Localization criteria")]
        public void TC08_WashersPostLocalization()
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
