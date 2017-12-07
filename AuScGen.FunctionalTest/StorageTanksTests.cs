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
    public class StorageTanksTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToStorageTanksPage();
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.StorageTanksTab.Click();
        }

        [TestCategory(TestType.functional, "TC01_Verify_AddStorageTank")]
        [TestCategory(TestType.regression, "TC01_Verify_AddStorageTank")]
        [Test, Description("Test Case 32438:RG Verify the functionality of the Add Tank;Test Case 40946:RG Verify the Audit Table for Insert")]
        public void TC01_Verify_AddStorageTank()
        {
            Page.StorageTanksTabPage.AddingStorageTank("AddAutoStorageTank","0", "0","0","0");
            Thread.Sleep(3000);
            if (null != Page.StorageTanksTabPage.Errordiv)
            {
                if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                    .Equals(@"Storage tanks created successfully"))
                {
                    Page.StorageTanksTabPage.BackToStorageTanks.Click();
                    Assert.Fail("Incorrect error message is displayed Expected - Storage tanks created successfully, Actual -  " + Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.StorageTanksTabPage.BackToStorageTanks.Click();
            Page.StorageTanksTabPage.AddingStorageTank("AddAutoStorageTank", "0", "0", "0", "0");
            Thread.Sleep(3000);
            if (null != Page.StorageTanksTabPage.Errordiv)
            {
                if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                    .Equals(@"Name already exists.Please use a different name"))
                {
                    Page.StorageTanksTabPage.BackToStorageTanks.Click();
                    Assert.Fail("Incorrect error message is displayed Expected - " + "Name already exists.Please use a different name" + "Actual - " + Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText);
                }
                else
                {
                    Page.StorageTanksTabPage.BackToStorageTanks.Click();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            //Storage Tank Insert DB validation
            string strCommand = "Select * from [TCD].[TankSetup] where [TankName] = '" + "AddAutoStorageTank" + "' and [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("TankName = " + "'AddAutoStorageTank'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "AddAutoStorageTank" + " - storage tank added successfully in DB");
            }
            else
            {
                Assert.Fail("AddAutoStorageTank" + " - record not saved/ created in DB");
            }

            //Audit Table validation
            string strTankId = "Select [TankId] from [TCD].[TankSetup] where [TankName] = '" + "AddAutoStorageTank" + "' and [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strTankId).Tables[0].Rows[0]["TankId"].ToString();
            string strAuditcmd = "Select * from [TCD].[TankSetupHistory] where [TankName] = '" + "AddAutoStorageTank" + "' and [Is_Deleted] = 0 And [OPERATIONID] = 1  And [TankId] = " + outType;
            DataRow[] AuditfoundRows = DBValidation.GetData(strAuditcmd).Tables[0].Select("TankName = " + "'AddAutoStorageTank'");
            int auditCount = AuditfoundRows.Length;
            if (auditCount >= 1)
            {
                Assert.True(true, "AddAutoStorageTank" + " - storage tank added successfully in DB");
            }
            else
            {
                Assert.Fail("AddAutoStorageTank" + " - record not saved/ created in DB");
            }
        }

        [TestCategory(TestType.functional, "TC02_Verify_UpdatingStorageTank")]
        [TestCategory(TestType.regression, "TC02_Verify_UpdatingStorageTank")]
        [Test, Description("Test Case 32441:RG Verify Update functionality in StorageTank Page;Test Case 40947:RG Verify the Audit Table for Update")]
        public void TC02_Verify_UpdatingStorageTank()
        {
            Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("AddAutoStorageTank")[0].GetButtonControls()[4].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.UpdatingStorageTank("UpdateAutoStorageTank", "1", "1");
            Thread.Sleep(3000);
            if (null != Page.StorageTanksTabPage.Errordiv)
            {
                if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                    .Equals(@"Storage tanks updated successfully"))
                {
                    Page.StorageTanksTabPage.BackToStorageTanks.Click();
                    Assert.Fail("Incorrect error message is displayed Expected - Storage tanks updated successfully, Actual -  " + Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.StorageTanksTabPage.BackToStorageTanks.Click();

            //Storage Tank Insert DB validation
            string strCommand = "Select * from [TCD].[TankSetup] where [TankName] = '" + "UpdateAutoStorageTank" + "' and [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("TankName = " + "'UpdateAutoStorageTank'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "UpdateAutoStorageTank" + " - storage tank added successfully in DB");
            }
            else
            {
                Assert.Fail("UpdateAutoStorageTank" + " - record not saved/ created in DB");
            }

            //Audit Table validation
            string strTankId = "Select [TankId] from [TCD].[TankSetup] where [TankName] = '" + "UpdateAutoStorageTank" + "' and [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strTankId).Tables[0].Rows[0]["TankId"].ToString();
            string strAuditcmd = "Select * from [TCD].[TankSetupHistory] where [TankName] = '" + "UpdateAutoStorageTank" + "' and [Is_Deleted] = 0 And [OPERATIONID] = 2  And [TankId] = " + outType;
            DataRow[] AuditfoundRows = DBValidation.GetData(strAuditcmd).Tables[0].Select("TankName = " + "'UpdateAutoStorageTank'");
            int auditCount = AuditfoundRows.Length;
            if (auditCount >= 1)
            {
                Assert.True(true, "UpdateAutoStorageTank" + " - storage tank added successfully in DB");
            }
            else
            {
                Assert.Fail("UpdateAutoStorageTank" + " - record not saved/ created in DB");
            }
        }


        [TestCategory(TestType.functional, "TC03_Verify_InLineEditingStorageTank")]
        [TestCategory(TestType.regression, "TC03_Verify_InLineEditingStorageTank")]
        [Test, Description("Test Case 32439:RG Verify the InLine Edit functionality in StorageTank Page")]
        public void TC03_Verify_InLineEditingStorageTank()
        {
            //Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("UpdateAutoStorageTank")[0].GetButtonControls()[2].DeskTopMouseClick();
            Page.StorageTanksTabPage.StorageTanksTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.InLineEditingStorageTank("InLineAutoStorageTank", "0", "0", "0", "0");
            Thread.Sleep(2000);
            //Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("UpdateAutoStorageTank")[0].GetButtonControls()[1].DeskTopMouseClick();
            Page.StorageTanksTabPage.StorageTanksTableGrid.Rows[1].GetButtonControls()[1].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.StorageTanksTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
            //Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("UpdateAutoStorageTank")[0].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.InLineEditingStorageTank("InLineAutoStorageTank","0", "0", "0", "0");
            Thread.Sleep(2000);
            Page.StorageTanksTabPage.StorageTanksTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
            //Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("UpdateAutoStorageTank")[0].GetButtonControls()[0].DeskTopMouseClick();
            Thread.Sleep(2000);
            if (null != Page.StorageTanksTabPage.Errordiv)
            {
                if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                    .Equals(@"Storage tanks updated successfully"))
                {
                    Page.StorageTanksTabPage.BackToStorageTanks.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            //DB Validation Pending...
        }


        [TestCategory(TestType.functional, "TC04_Verify_DefaultValuesStorageTank")]
        [TestCategory(TestType.regression, "TC04_Verify_DefaultValuesStorageTank")]
        [Test, Description("Test Case 32442:RG Verify the default values in Add Tank Page")]
        public void TC04_Verify_DefaultValuesStorageTank()
        {
            Page.StorageTanksTabPage.AddStorageTanks.Click();
            Thread.Sleep(2000);
            if (Page.StorageTanksTabPage.EmptyLevel.Text != "0")
            {
                Assert.Fail("Default value of Empty Level is not 0, displayed value is " + Page.StorageTanksTabPage.EmptyLevel.Text);
            }
            if (Page.StorageTanksTabPage.LevelDeviation.Text != "0")
            {
                Assert.Fail("Default value of LevelDeviation in not 0, displayed value is " + Page.StorageTanksTabPage.LevelDeviation.Text);
            }
            if (Page.StorageTanksTabPage.LowLevel.Text != "0")
            {
                Assert.Fail("Default value of LowLevel in not 0, displayed value is " + Page.StorageTanksTabPage.LowLevel.Text);
            }
            if (Page.StorageTanksTabPage.Size.Text != "0")
            {
                Assert.Fail("Default value of Size in not 0, displayed value is " + Page.StorageTanksTabPage.Size.Text);
            }

            if (Page.StorageTanksTabPage.CallibrationLevel.Text != "0")
            {
                Assert.Fail("Default value of CallibrationLevel in not 0, displayed value is " + Page.StorageTanksTabPage.CallibrationLevel.Text);
            }

            if (Page.StorageTanksTabPage.InputType.Text != "0-20mA")
            {
                Assert.Fail("Default value of InputType in not 0-20mA, displayed value is " + Page.StorageTanksTabPage.InputType.Text);
            }
            Page.StorageTanksTabPage.BackToStorageTanks.Click();
            //DB Validation Pending...
        }


        [TestCategory(TestType.functional, "TC05_Verify_DeletingStorageTank")]
        [TestCategory(TestType.regression, "TC05_Verify_DeletingStorageTank")]
        [Test, Description("Test Case 32440:RG Verify the Delete functionality inStorageTank Page; Test Case 40948:RG Verify the Audit Table for Delete")]
        public void TC05_Verify_DeletingStorageTank()
        {
            string strTest = Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("InLineAutoStorageTank")[0].GetColumnValues()[1];
            Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("InLineAutoStorageTank")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.CancelButton.Click();
            Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("InLineAutoStorageTank")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            if (null != Page.StorageTanksTabPage.dialogMsg.BaseElement.InnerText)
            {
                if (!Page.StorageTanksTabPage.dialogMsg.BaseElement.InnerText
                    .Equals(@"Are you sure you want to delete this Storage Tanks?"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed in dialog box : {0}", DialogHandler.LastDialogMessage));
                }
            }
            else
            {
                Assert.Fail("Dialog box with confirmation to delete row is not displayed");
            }
            DialogHandler.OkButton.Click();
            Thread.Sleep(2000);
            if (null != Page.StorageTanksTabPage.Errordiv)
            {
                if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                    .Equals(@"Storage tanks deleted successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed Expected - Storage tanks deleted successfully, Actual - " + Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            //Storage Tank Insert DB validation
            string strCommand = "Select * from [TCD].[TankSetup] where [TankName] = '" + strTest + "' and [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("TankName = " + "'" + strTest + "'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strTest + " - storage tank deleted successfully in DB");
            }
            else
            {
                Assert.Fail(strTest + " - record not delted in DB");
            }

            //Audit Table validation
            string strTankId = "Select [TankId] from [TCD].[TankSetup] where [TankName] = '" + strTest + "' and [Is_Deleted] = 1";
            string outType = DBValidation.GetData(strTankId).Tables[0].Rows[0]["TankId"].ToString();
            string strAuditcmd = "Select * from [TCD].[TankSetupHistory] where [TankName] = '" + strTest + "' and [Is_Deleted] = 1 And [OPERATIONID] = 3  And [TankId] = " + outType;
            DataRow[] AuditfoundRows = DBValidation.GetData(strAuditcmd).Tables[0].Select("TankName = " + "'" + strTest + "'");
            int auditCount = AuditfoundRows.Length;
            if (auditCount >= 1)
            {
                Assert.True(true, strTest + " - storage tank added successfully in DB");
            }
            else
            {
                Assert.Fail(strTest + " - record not saved/ created in DB");
            }


            Page.StorageTanksTabPage.StorageTanksTableGrid.SelectedRows("UpdateAutoStorageTank")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.OkButton.Click();
        }


        [TestCategory(TestType.functional, "TC06_Verify_StorageTanks_Localization")]
        [TestCategory(TestType.regression, "TC06_Verify_StorageTanks_Localization")]
        [Test, Description("Test Case 34202:RG Verify the Localization criteria")]
        public void TC06_Verify_StorageTanks_Localization()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.LoginPage.TopMainMenu.NavigateToStorageTanksPage();
            Page.StorageTanksTabPage.StorageTanksTab.Click();
            if (Page.StorageTanksTabPage.StorageTanksTab.BaseElement.InnerText != "opslagtanks")
            {
                PostLocalizationStorageTanks();
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - opslagtanks", Page.StorageTanksTabPage.StorageTanksTab.BaseElement.InnerText));
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
            Page.LoginPage.TopMainMenu.NavigateToStorageTanksPage();
            Page.StorageTanksTabPage.StorageTanksTab.Click();
            if (Page.StorageTanksTabPage.StorageTanksTab.BaseElement.InnerText != "Storage Tanks")
            {
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - opslagtanks", Page.StorageTanksTabPage.StorageTanksTab.BaseElement.InnerText));
            }
        }

        [TestCategory(TestType.functional, "TC07_Verify_StorageTank_UserRole8")]
        [TestCategory(TestType.regression, "TC07_Verify_StorageTank_UserRole8")]
        [Test, Description("Test Case 32446:RG Verify the Access to Storage Tank page for 8-Eco Lab Engineer")]
        public void TC07_Verify_StorageTank_UserRole8()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Thread.Sleep(2000);
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance", "test");
            Thread.Sleep(2000);
            try
            {
                Page.LoginPage.TopMainMenu.NavigateToStorageTanksPage();
                Thread.Sleep(2000);
                Page.StorageTanksTabPage.StorageTanksTab.Click();
                Thread.Sleep(2000);
                if (!Page.StorageTanksTabPage.AddStorageTanks.IsEnabled)
                {
                    Assert.Fail("Write access to storage tank not ther for User Role - 8 i. e Engineer");
                }
                Page.StorageTanksTabPage.AddingStorageTank("UserRoleEightAutoStorageTank", "0", "0", "0", "0");
                Thread.Sleep(3000);
                if (null != Page.StorageTanksTabPage.Errordiv)
                {
                    if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                        .Equals(@"Storage tanks created successfully"))
                    {
                        Assert.Fail("Incorrect error message is displayed");
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
                Page.StorageTanksTabPage.BackToStorageTanks.Click();
                Page.StorageTanksTabPage.AddingStorageTank("UserRoleEightAutoStorageTank", "0", "0", "0", "0");
                Thread.Sleep(2000);
                if (null != Page.StorageTanksTabPage.Errordiv)
                {
                    if (!Page.StorageTanksTabPage.Errordiv.BaseElement.InnerText
                        .Equals(@"Storage Tank already exists."))
                    {
                        Page.StorageTanksTabPage.BackToStorageTanks.Click();
                        Assert.Fail("Incorrect error message is displayed");
                    }
                    else
                    {
                        Page.StorageTanksTabPage.BackToStorageTanks.Click();
                    }
                }
                else
                {
                    Assert.Fail("Error message is not displayed");
                }
            }
            catch
            {
                Assert.Fail("NavigateToStorageTanksPage failed unable to find Storage Tanks menu item in Setup Menu - AutoTestTMAdvance - Level 8");
            }
        }


        [TestCategory(TestType.functional, "TC08_Verify_StorageTank_UserRole7")]
        [TestCategory(TestType.regression, "TC08_Verify_StorageTank_UserRole7")]
        [Test, Description("Test Case 32447:RG Verify the Access to Storage Tank page for 7-Eco Lab Engineer")]
        public void TC08_Verify_StorageTank_UserRole7()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Thread.Sleep(2000);
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance", "test");
            Thread.Sleep(2000);
            try
            {
                if (!Page.LoginPage.TopMainMenu.NavigateToStorageTanksPageAvailable())
                {
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("Storage Tanks Menu visible for User Role - 6 i. e Engineer");
                }
            }
            catch
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.True(true, "Storage Tanks Menu not visible for User Role - 6 i. e Engineer");
            }  
        }


        [TestCategory(TestType.functional, "TC09_Verify_StorageTank_UserRole6")]
        [TestCategory(TestType.regression, "TC09_Verify_StorageTank_UserRole6")]
        [Test, Description("Test Case 32448:RG Verify the Access to Storage Tank page for 1 to 6 Eco Lab Engineer")]
        public void TC09_Verify_StorageTank_UserRole6()
        {
            Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
            Thread.Sleep(2000);
            try
            {
                if (Page.LoginPage.TopMainMenu.NavigateToStorageTanksPageAvailable())
                {
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("Storage Tanks Menu visible for User Role - 6 i. e Engineer");
                }
            }
            catch
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.True(true, "Storage Tanks Menu not visible for User Role - 6 i. e Engineer");
            }
        }







    }
}
