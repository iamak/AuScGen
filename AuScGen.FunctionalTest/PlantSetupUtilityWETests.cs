using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using Ecolab.Pages.CommonControls;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupUtilityWETests :TestBase
    {
        /// <summary>
        /// Tests the fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin ", "test");
        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        /// <summary>
        ///  TC 24710: Water & Energy Device Details:Verify SAVE functionality while Adding Water & Energy details
        /// </summary>
        [TestCategory(TestType.regression, "TC01_AddWaterEnergyDevice")]
        [Test]
        public void TC01_AddWaterEnergyDevice()
        {
            DateTime date = DateTime.Now.AddDays(+1);
            string inputDate = date.ToString("MM/dd/yyyy").Replace("-", "/");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.WETabPage.WaterEnergyTab.Click();
            Page.WETabPage.AddWaterEnergyDetailsButton.Click();
            Page.WETabPage.AddWaterEnergyDetailsButton.ScrollToVisible();
            Page.WETabPage.AddWaterEnergyDevice("AddWE", "Device Type1", "DeviceModel1", inputDate, "Create");
            Thread.Sleep(2000);
            Assert.IsTrue(Page.WETabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Device Added Successfully"), "Success Message not matched");
            string strFirstName = Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetColumnValues()[1].ToString();
            Page.WETabPage.AddWaterEnergyDetailsButton.Click();
            Page.WETabPage.AddWaterEnergyDetailsButton.ScrollToVisible();
            Page.WETabPage.VerifyFieldValidationMessage("Device Type1", inputDate, "Create");
            Thread.Sleep(2000);
            Assert.IsTrue(Page.WETabPage.VerifyLastNameFieldValidationMsg.BaseElement.InnerText.Contains("Device Name is required"), "Success Message not matched");
            Page.WETabPage.WaterEnergyCancelButton.Click();
            
            string strCommand = "Select * from [TCD].[WaterAndEnergy]";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("DeviceNumber =" + strFirstName);
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strFirstName + " WE Utility added successfully in DB");
            }
            else
            {
                Assert.Fail(strFirstName + " record not saved/ created in DB");
            }
        }

        /// <summary>
        ///  TC 25001: Water & Energy Device Details:Verify SAVE functionality while editing
        /// </summary>
        [TestCategory(TestType.regression, "TC02_UpdateWaterEnergyDevice")]
        [Test]
        public void TC02_UpdateWaterEnergyDevice()
        {
            DateTime date = DateTime.Now.AddDays(+1);
            string inputDate = date.ToString("MM/dd/yyyy").Replace("-", "/");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(2000);
            Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            Page.WETabPage.VerifyUpdateCancelButton("Updated");
            Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            Page.WETabPage.UpdateWaterEnergyDevice("Updated");
            Thread.Sleep(2000);
            Assert.True(Page.WETabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Device Updated Successfully"), "Success Message not matched");
        }

        /// <summary>
        ///  TC 24720: Water & Energy Device Details:Verify Inline editing of a row
        /// </summary>
        [TestCategory(TestType.regression, "TC03_EditInLineWaterEnergyDevice")]
        [Test]
        public void TC03_EditInLineWaterEnergyDevice()
        {
            DateTime date = DateTime.Now;
            string inputDate = date.ToString("MM/dd/yyyy").Replace("-", "/");
            DateTime nextDate = DateTime.Now.AddDays(+2);
            string inputnextDate = nextDate.ToString("MM/dd/yyyy").Replace("-", "/");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(3000);
            Page.WETabPage.WaterEnergyTabGrid.SelectedRows("Device Type1")[0].GetButtonControls()[0].Click();
            Page.WETabPage.InlineEditing("InLineWE",inputDate);
            Telerik.ActiveBrowser.RefreshDomTree();     
            Page.WETabPage.WaterEnergyTabGrid.SelectedRows("Device Type1")[0].GetButtonControls()[1].Click();
            Thread.Sleep(3000);
            Page.WETabPage.WaterEnergyTabGrid.SelectedRows("Device Type1")[0].GetButtonControls()[0].Click();
            Page.WETabPage.InlineEditing("InLineWE", inputnextDate);
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.WETabPage.WaterEnergyTabGrid.SelectedRows("Device Type1")[0].GetButtonControls()[0].Click();
            Thread.Sleep(2000);
            Assert.True(Page.WETabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Device Updated Successfully"), "Success Message not matched");
        }

        /// <summary>
        ///  TC 24721: Water & Energy Device Details:Verify UPDATE/CANCEL gets displayed only when user modifies any cell in a row
        /// </summary>
        [TestCategory(TestType.regression, "TC04_VerifyInlineEditingButtons")]
        [Test]
        public void TC04_VerifyInlineEditingButtons()
        {
            DateTime nextDate = DateTime.Now.AddDays(+2);
            string inputnextDate = nextDate.ToString("MM/dd/yyyy").Replace("-", "/");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(2000);
            Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Thread.Sleep(2000);
            Page.WETabPage.InlineEditing("InLineWE", inputnextDate);
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(2000);
            Assert.True(Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].IsEnabled, "InLineEdititng Update button not visible");
            Assert.True(Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].IsEnabled, "InLineEdititng Cancel button not visible");
            Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
        }

        /// <summary>
        ///  TC 24717: Water & Energy Device Details:Verify DELETE button/icon
        /// </summary>
        [TestCategory(TestType.regression, "TC05_DeleteWaterEnergyDevice")]
        [Test]
        public void TC05_DeleteWaterEnergyDevice()
        {
            DateTime nextDate = DateTime.Now.AddDays(+2);
            string inputnextDate = nextDate.ToString("MM/dd/yyyy").Replace("-", "/");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(3000);
            string strFirstName = Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetColumnValues()[1].ToString();
            Page.WETabPage.ClickonCancelPreferencesButton();
            Assert.True(Page.WETabPage.WaterEnergyTabGrid.GetRow(strFirstName) != null, "User deleted on clicking cancel in delete confirmation popup");
            Page.WETabPage.ClickonOkPreferencesButton();
            Thread.Sleep(2000);
            Assert.True(Page.WETabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Device Deleted Successfully"), "Success Message not matched");

            string strCommand = "Select * from [TCD].[WaterAndEnergy] Where [DeviceNumber] = '" + strFirstName + "'" + " AND [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("DeviceNumber = " + strFirstName);
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strFirstName + " WE Utility deleted successfully in DB");
            }
            else
            {
                Assert.Fail(strFirstName + " record not deleted");
            }
        }

        /// <summary>
        /// Ts the C06_ user roles_ edit.
        /// </summary>
        [TestCategory(TestType.regression, "TC06_UserRoles_Edit")]
        [Test]
        public void TC06_UserRoles_Edit()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance ", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(2000);
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(2000);
            Page.WETabPage.WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            Page.WETabPage.UpdateWaterEnergyDevice("UserRoleTest");
            Thread.Sleep(2000);
            Assert.True(Page.WETabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Device Updated Successfully"), "Success Message not matched");
        }

        /// <summary>
        /// Ts the C06_ user roles_ edit.
        /// </summary>
        [TestCategory(TestType.regression, "TC07_UserRoles_View")]
        [Test]
        public void TC07_UserRoles_View()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMBasic ", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(2000);
            Page.WETabPage.WaterEnergyTab.Click();
            Thread.Sleep(2000);
            int nActionButtons = Page.WETabPage.WaterEnergyTabGridRoleBased.Rows.FirstOrDefault().GetButtonControls().Count;
            if (nActionButtons > 1)
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.Fail("Logged in as User Role 6, but found edit, update and deleted button in Water & Energy table");
            }
            Thread.Sleep(2000);
            HtmlControl CtrlDeviceType;
            Page.WETabPage.WaterEnergyTabGridRoleBased.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            try
            {
                CtrlDeviceType = Page.WETabPage.DeviceType;
                if (CtrlDeviceType != null)
                {
                    Page.WETabPage.Cancel.Click();
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("Logged in as User Role 6, but found Device Type as Editable Field");
                }
            }
            catch
            {
                Page.WETabPage.Cancel.Click();
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }       
        }
        
    }
}
