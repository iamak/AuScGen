using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
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
    public class WashersTunnelGeneralTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.WasherGroupsTableGrid.SelectedRows("TunnelWasher1")[0].GetButtonControls()[4].DeskTopMouseClick();
        }

        //[TestCategory(TestType.functional, "TC01_Verify_AddTunnel")]
        //[TestCategory(TestType.regression, "TC01_Verify_AddTunnel")]
        //[Test, Description("Test Case 39169:RG Verify Add Tunnel Functionality")]
        //public void TC01_Verify_AddTunnel()
        //{
        //    try
        //    {
        //        if (Page.WashersTunnelGeneralPage.WashersTableGrid.Rows.Count > 0)
        //        {
        //            Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
        //            DialogHandler.OkButton.Click();
        //        }
        //    }
        //    catch { }
        //    Page.WashersTunnelGeneralPage.AddingWahser("TestWasher", "1", "20");
        //    if (null != Page.WashersTunnelGeneralPage.divMessage)
        //    {
        //        if (!Page.WashersTunnelGeneralPage.divMessage.BaseElement.InnerText
        //            .Equals(@"Tunnel Details Saved successfully."))
        //        {
        //            Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //            Assert.Fail("Incorrect error message is displayed");
        //        }
        //    }
        //    else
        //    {
        //        Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //        Assert.Fail("Error message is not displayed");
        //    }

        //}


        //[TestCategory(TestType.functional, "TC02_Verify_UpdateTunnel")]
        //[TestCategory(TestType.regression, "TC02_Verify_UpdateTunnel")]
        //[Test, Description("Test Case 40643:RG Verify Update Washer functionality")]
        //public void TC02_Verify_UpdateTunnel()
        //{
        //    Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
        //    Thread.Sleep(2000);
        //    Page.WashersTunnelGeneralPage.UpdatingWasher("TestWasher", "1", "20");
        //    if (null != Page.WashersTunnelGeneralPage.divMessage)
        //    {
        //        if (!Page.WashersTunnelGeneralPage.divMessage.BaseElement.InnerText
        //            .Equals(@"Tunnel Details Updated successfully."))
        //        {
        //            Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //            Assert.Fail("Incorrect error message is displayed");
        //        }
        //    }
        //    else
        //    {
        //        Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //        Assert.Fail("Error message is not displayed");
        //    }

        //}

        //[TestCategory(TestType.functional, "TC03_Verify_UpdateTunnel_DuplicateName")]
        //[TestCategory(TestType.regression, "TC03_Verify_UpdateTunnel_DuplicateName")]
        //[Test, Description("Test Case 40644:RG Verify Update Washer functionality - Already existing Plant Washer # and Name")]
        //public void TC03_Verify_UpdateTunnel_DuplicateName()
        //{
        //    Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
        //    Thread.Sleep(2000);
        //    Page.WashersTunnelGeneralPage.UpdatingWasher("TestWasher", "1", "20");
        //    if (null != Page.WashersTunnelGeneralPage.divMessage)
        //    {
        //        if (!Page.WashersTunnelGeneralPage.divMessage.BaseElement.InnerText
        //            .Equals(@""))
        //        {
        //            Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //            Assert.Fail("Incorrect error message is displayed");
        //        }
        //    }
        //    else
        //    {
        //        Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //        Assert.Fail("Error message is not displayed");
        //    }

        //}

        //[TestCategory(TestType.functional, "TC04_Verify_AddTunnel_DuplicateName")]
        //[TestCategory(TestType.regression, "TC04_Verify_AddTunnel_DuplicateName")]
        //[Test, Description("Test Case 40646:RG Verify Add Tunnel Functionality-Name and Plant Washer# Already exists")]
        //public void TC04_Verify_AddTunnel_DuplicateName()
        //{
        //    try
        //    {
        //        if (Page.WashersTunnelGeneralPage.WashersTableGrid.Rows.Count > 0)
        //        {
        //            Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
        //            DialogHandler.OkButton.Click();
        //        }
        //    }
        //    catch { }
        //    Page.WashersTunnelGeneralPage.AddingWahser("TestWasher", "1", "20");
        //    if (null != Page.WashersTunnelGeneralPage.divMessage)
        //    {
        //        if (!Page.WashersTunnelGeneralPage.divMessage.BaseElement.InnerText
        //            .Equals(@""))
        //        {
        //            Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //            Assert.Fail("Incorrect error message is displayed");
        //        }
        //    }
        //    else
        //    {
        //        Page.WashersTunnelGeneralPage.BackToWasher.Click();
        //        Assert.Fail("Error message is not displayed");
        //    }

        //}


        //[TestCategory(TestType.functional, "TC05_Verify_AddWasher_Cancel")]
        //[TestCategory(TestType.regression, "TC05_Verify_AddWasher_Cancel")]
        //[Test, Description("Test Case 42847:RG Verify Cancel functionality")]
        //public void TC05_Verify_AddWasher_Cancel()
        //{
        //    try
        //    {
        //        if (Page.WashersTunnelGeneralPage.WashersTableGrid.Rows.Count > 0)
        //        {
        //            Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
        //            DialogHandler.OkButton.Click();
        //        }
        //    }
        //    catch { }
        //    Page.WashersTunnelGeneralPage.CancellingAddWahser("TestWasher", "1", "20");
        //}

        //[TestCategory(TestType.functional, "TC06_Verify_DeleteWasher")]
        //[TestCategory(TestType.regression, "TC06_Verify_DeleteWasher")]
        //[Test, Description("Test Case 48355:RG Verify the Delete Washer functionality")]
        //public void TC06_Verify_DeleteWasher()
        //{
        //    try
        //    {
        //        if (Page.WashersTunnelGeneralPage.WashersTableGrid.Rows.Count > 0)
        //        {
        //            Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
        //            Thread.Sleep(2000);
        //            DialogHandler.CancelButton.Click();
        //            Thread.Sleep(2000);
        //            Page.WashersTunnelGeneralPage.WashersTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
        //            Thread.Sleep(2000);
        //            DialogHandler.OkButton.Click();
        //            Thread.Sleep(2000);
        //            if (null != Page.WashersTunnelGeneralPage.divMessage)
        //            {
        //                if (!Page.WashersTunnelGeneralPage.divMessage.BaseElement.InnerText
        //                    .Equals(@""))
        //                {
        //                    Assert.Fail("Incorrect error message is displayed");
        //                }
        //            }
        //            else
        //            {
        //                Assert.Fail("Error message is not displayed");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        Assert.Fail("No Washer Record Exist - Grid Empty");
        //    }
        //}

        //[TestCategory(TestType.functional, "TC07_Localization_WasherTunnelGeneral")]
        //[TestCategory(TestType.regression, "TC07_Localization_WasherTunnelGeneral")]
        //[Test, Description("Test Case 42843:RG: Verify the Localization criteria")]
        //public void TC07_Localization_WasherTunnelGeneral()
        //{
        //    Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
        //    Page.PlantSetupPage.GeneralTab.Click();
        //    Page.SensorTabPage.Language.Focus();
        //    Page.SensorTabPage.Language.SelectByText("Deutsch", true);
        //    Page.SensorTabPage.Language.ScrollToVisible();
        //    Page.SensorTabPage.GeneralTabSave.Click();
        //    Telerik.ActiveBrowser.RefreshDomTree();
        //    Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
        //    Thread.Sleep(2000);
        //    Page.WasherGroupFormulasPage.WasherGroupsTableGrid.SelectedRows("TunnelWasher1")[0].GetButtonControls()[4].DeskTopMouseClick();
        //    Thread.Sleep(2000);
        //    if (Page.WashersTunnelGeneralPage.AddNewWasher.BaseElement.InnerText != "Voeg Washer")
        //    {
        //        PostLocalizationStorageTanks();
        //        Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - Voeg Washer", Page.WashersTunnelGeneralPage.AddNewWasher.BaseElement.InnerText));
        //    }
        //    PostLocalizationStorageTanks();
        //}

        //private void PostLocalizationStorageTanks()
        //{
        //    Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
        //    Page.PlantSetupPage.GeneralTab.Click();
        //    Page.SensorTabPage.Language.Focus();
        //    Page.SensorTabPage.Language.SelectByText("English US", true);
        //    Page.SensorTabPage.Language.ScrollToVisible();
        //    Page.SensorTabPage.GeneralTabSave.Click();
        //    Telerik.ActiveBrowser.RefreshDomTree();
        //    Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
        //    Thread.Sleep(2000);
        //    Page.WasherGroupFormulasPage.WasherGroupsTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
        //    Thread.Sleep(2000);
        //    if (Page.WashersTunnelGeneralPage.AddNewWasher.BaseElement.InnerText != "Add Washer")
        //    {
        //        Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - Voeg Washer", Page.WashersTunnelGeneralPage.AddNewWasher.BaseElement.InnerText));
        //    }
        //}

        //[TestCategory(TestType.functional, "TC08_Verify_WasherTunnel_UserRole7")]
        //[TestCategory(TestType.regression, "TC08_Verify_WasherTunnel_UserRole7")]
        //[Test, Description("Test case 42846: RG Verify access to Add/Update Washer Page depending on UserRoleId 7 and above")]
        //public void TC08_Verify_WasherTunnel_UserRole7()
        //{
        //    Page.PlantSetupPage.TopMainMenu.LogOut();
        //    Thread.Sleep(2000);
        //    Page.LoginPage.VerifyLogin("AutoTestTMAdvance", "test");
        //    Thread.Sleep(2000);
        //    try
        //    {
        //        Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPageAvailable();
        //    }
        //    catch
        //    {
        //        Page.PlantSetupPage.TopMainMenu.LogOut();
        //        Assert.Fail("Washer Group menu not displayed for User Role - 7");
        //    }
        //    Page.PlantSetupPage.TopMainMenu.LogOut();
        //}


        //[TestCategory(TestType.functional, "TC09_Verify_WasherTunnel_UserRole6")]
        //[TestCategory(TestType.regression, "TC09_Verify_WasherTunnel_UserRole6")]
        //[Test, Description("Test Case 42845:RG Verify access to Washer Add/Edit Page depending on UserRoleId - 6")]
        //public void TC09_Verify_WasherTunnel_UserRole6()
        //{
        //    Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
        //    Thread.Sleep(2000);
        //    Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
        //    Thread.Sleep(2000);
        //    if (Page.WasherGroupPage.WasherGroupTableGrid.Rows[1].GetButtonControls().Count > 1)
        //    {
        //        Page.PlantSetupPage.TopMainMenu.LogOut();
        //        Assert.Fail("Update/Edit/Delete buttons enabled for User role 6 and expected readonly / viewonly access for WasherGroup setup");
        //    }
        //    Page.PlantSetupPage.TopMainMenu.LogOut();
        //}


        //[TestCategory(TestType.functional, "TC10_Verify_WasherTunnel_UserRole4")]
        //[TestCategory(TestType.regression, "TC10_Verify_WasherTunnel_UserRole4")]
        //[Test, Description("Test Case 42844:RG: Verify access to Washer Group Page depending on UserRoleId - 1 to 5")]
        //public void TC10_Verify_WasherTunnel_UserRole4()
        //{
        //    Page.LoginPage.VerifyLogin("AutoTestPM", "test");
        //    Thread.Sleep(2000);
        //    try
        //    {
        //        if (Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPageAvailable())
        //        {
        //            Page.PlantSetupPage.TopMainMenu.LogOut();
        //            Assert.Fail("WasherGroup Menu visible for User Role - 4 i. e Plant Manager");
        //        }
        //    }
        //    catch
        //    {
        //        Page.PlantSetupPage.TopMainMenu.LogOut();
        //        Assert.True(true, "WasherGroup Menu not visible for User Role - 4 i. e Plant Manager");
        //    }
        //}


    }
}
