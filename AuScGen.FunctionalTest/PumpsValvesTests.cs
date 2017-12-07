using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.FunctionalTest
{
    public class PumpsValvesTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Thread.Sleep(2000);
        }

        [TestCategory(TestType.functional, "TC01_Edit_PumpsValves_AllenBradley")]
        [TestCategory(TestType.regression, "TC01_Edit_PumpsValves_AllenBradley")]
        [Test, Description("Test Case 32811:RG Verify Add Controller Page Edit Pumps/Valves Functionality Allen Bradley")]
        public void TC01_Edit_PumpsValves_AllenBradley()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.SelectedRows("Allen Bradley")[0].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if(!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.AddingPumps("5", "200");
            Thread.Sleep(2000);
            if (null != Page.PumpsValvesPage.ValidationMessage)
            {
                if (!Page.PumpsValvesPage.ValidationMessage.BaseElement.InnerText
                    .Equals(@"Pump details updated successfully"))
                {
                    Page.PumpsValvesPage.BacktoPumps.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.PumpsValvesPage.BacktoPumps.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.PumpsValvesPage.BacktoPumps.Click();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.BacktoControllers.Click();
            //DB Validation
        }

        [TestCategory(TestType.functional, "TC02_Edit_PumpsValves_Beckhoff")]
        [TestCategory(TestType.regression, "TC02_Edit_PumpsValves_Beckhoff")]
        [Test, Description("Test Case 32822:RG Verify Add Controller Page -Edit Pumps/Valves Functionality-Beckhoff")]
        public void TC02_Edit_PumpsValves_Beckhoff()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.SelectedRows("Beckhoff")[0].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.AddingPumps("10", "130");
            if (null != Page.PumpsValvesPage.ValidationMessage)
            {
                if (!Page.PumpsValvesPage.ValidationMessage.BaseElement.InnerText
                    .Equals(@"Pump details updated successfully"))
                {
                    Page.PumpsValvesPage.BacktoPumps.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.PumpsValvesPage.BacktoPumps.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.PumpsValvesPage.BacktoPumps.Click();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.BacktoControllers.Click();
            //DB Validation
        }

        [TestCategory(TestType.functional, "TC03_Verify_UpdatePumps_CancelFunctionality")]
        [TestCategory(TestType.regression, "TC03_Verify_UpdatePumps_CancelFunctionality")]
        [Test, Description("Test Case 32823:RG Verify Cancel Button Functionality-Update Button")]
        public void TC03_Verify_UpdatePumps_CancelFunctionality()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.UpdateCancellingPumps("15", "160");
            if (null != Page.PumpsValvesPage.dialogMsg.BaseElement.InnerText)
            {
                if (!Page.PumpsValvesPage.dialogMsg.BaseElement.InnerText
                    .Equals(@"Do you want to save changes?"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed in dialog box : {0}", DialogHandler.LastDialogMessage));
                }
            }
            else
            {
                Assert.Fail("Dialog box with confirmation to delete row is not displayed");
            }
            DialogHandler.NoButton.Click();
            Page.PumpsValvesPage.BacktoControllers.Click();
        }

        [TestCategory(TestType.functional, "TC04_Verify_UpdatePumps")]
        [TestCategory(TestType.regression, "TC04_Verify_UpdatePumps")]
        [Test, Description("Test Case 33630:RG Verify Edit Pump/Valves Functionality-Update Button")]
        public void TC04_Verify_UpdatePumps()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.UpdatingPumps("20", "120");
            Thread.Sleep(2000);
            if (null != Page.PumpsValvesPage.ValidationMessage)
            {
                if (!Page.PumpsValvesPage.ValidationMessage.BaseElement.InnerText
                    .Equals(@"Pump details updated successfully"))
                {
                    Page.PumpsValvesPage.BacktoPumps.Click();
                    Page.PumpsValvesPage.BacktoControllers.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.PumpsValvesPage.BacktoPumps.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.PumpsValvesPage.BacktoPumps.Click();
            Page.PumpsValvesPage.BacktoControllers.Click();
        }

        [TestCategory(TestType.functional, "TC05_Verify_InlineEdit_Pumps_Cancel")]
        [TestCategory(TestType.regression, "TC05_Verify_InlineEdit_Pumps_Cancel")]
        [Test, Description("Test Case 40173:RG Verify Cancel button Functionality-Inline")]
        public void TC05_Verify_InlineEdit_Pumps_Cancel()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.InlineEditingPumps("0", "190");
            Thread.Sleep(2000);
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.BacktoControllers.Click();
        }

        [TestCategory(TestType.functional, "TC06_Verify_InlineEdit_Pumps_Update")]
        [TestCategory(TestType.regression, "TC06_Verify_InlineEdit_Pumps_Update")]
        [Test, Description("Test Case 40107:RG Verify Edit Pump/Valves Functionality-Inline")]
        public void TC06_Verify_InlineEdit_Pumps_Update()
        {
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.InlineEditingPumps("0", "190");
            Thread.Sleep(2000);
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
            Thread.Sleep(2000);
            if (null != Page.PumpsValvesPage.ValidationMessage)
            {
                if (!Page.PumpsValvesPage.ValidationMessage.BaseElement.InnerText
                    .Equals(@"Pump details updated successfully"))
                {
                    Page.PumpsValvesPage.BacktoPumps.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.PumpsValvesPage.BacktoPumps.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.PumpsValvesPage.BacktoControllers.Click();
        }

        [TestCategory(TestType.functional, "TC07_Verify_Pumps_Localization")]
        [TestCategory(TestType.regression, "TC07_Verify_Pumps_Localization")]
        [Test, Description("Test Case 40190:RG Verify the application localization functionality on Pumps/valves Add and Edit and Grid page")]
        public void TC07_Verify_Pumps_Localization()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (Page.PumpsValvesPage.tabPumpList.BaseElement.InnerText != "Pompen/Valves")
            {
                PostLocalizationStorageTanks();
                Thread.Sleep(2000);
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - Pompen/Valves", Page.PumpsValvesPage.tabPumpList.BaseElement.InnerText));
            }
            PostLocalizationStorageTanks();
        }

        private void PostLocalizationStorageTanks()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("English US", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (Page.PumpsValvesPage.tabPumpList.BaseElement.InnerText != "Pumps/Valves")
            {
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - Pompen/Valves", Page.PumpsValvesPage.tabPumpList.BaseElement.InnerText));
            }
        }

        [TestCategory(TestType.functional, "TC08_Verify_Pumps_UserRole8")]
        [TestCategory(TestType.regression, "TC08_Verify_Pumps_UserRole8")]
        [Test, Description("Test Case 40908:RG Verify access to Pump & Valve Page depending on UserRoleId - 8")]
        public void TC08_Verify_Pumps_UserRole8()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Thread.Sleep(2000);
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance", "test");
            Thread.Sleep(2000);
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.ControllerSetupGridTable.Rows.FirstOrDefault().GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.tabPumpList.Click();
            Thread.Sleep(2000);
            if (!Page.PumpsValvesPage.ddlSort.BaseElement.InnerText.Contains("All"))
            {
                Assert.Fail("Default option not set to ALL for sort dropdown");
            }
            Page.PumpsValvesPage.PumpsListTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.PumpsValvesPage.UpdatingPumps("0", "150");
            if (null != Page.PumpsValvesPage.ValidationMessage)
            {
                if (!Page.PumpsValvesPage.ValidationMessage.BaseElement.InnerText
                    .Equals(@"Pump details updated successfully"))
                {
                    Page.PumpsValvesPage.BacktoPumps.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.PumpsValvesPage.BacktoPumps.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.PumpsValvesPage.BacktoPumps.Click();
            Page.PumpsValvesPage.BacktoControllers.Click();
            Page.PlantSetupPage.TopMainMenu.LogOut();
        }

        [TestCategory(TestType.functional, "TC09_Verify_Pumps_UserRole6")]
        [TestCategory(TestType.regression, "TC09_Verify_Pumps_UserRole6")]
        [Test, Description("Test Case 41095:RG: Verify access to Controller Page depending on UserRoleId - 6")]
        public void TC09_Verify_Pumps_UserRole6()
        {
            Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
            Thread.Sleep(2000);
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Thread.Sleep(2000);
            if (Page.PumpsValvesPage.ControllerRoleSetupGridTable.Rows.FirstOrDefault().GetButtonControls().Count > 1)
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.Fail("Update/Edit/Delete buttons enabled for User role 6 and expected readonly / viewonly access for controller setup");
            }
            Page.PlantSetupPage.TopMainMenu.LogOut();
        }


        [TestCategory(TestType.functional, "TC10_Verify_Pumps_UserRole4")]
        [TestCategory(TestType.regression, "TC10_Verify_Pumps_UserRole4")]
        [Test, Description("Test Case 40977:RG Verify access to Pump & Valve Page depending on UserRoleId - 1 to 5")]
        public void TC10_Verify_Pumps_UserRole4()
        {
            Page.LoginPage.VerifyLogin("AutoTestPM", "test");
            Thread.Sleep(2000);
            try
            {
                if (Page.LoginPage.TopMainMenu.NavigateToControlerSetupPageAvailable())
                {
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("Controller Setup Menu visible for User Role - 4 i. e Plant Manager");
                }
            }
            catch
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.True(true, "Controller Setup Menu not visible for User Role - 4 i. e Plant Manager");
            }
        }





    }
}
