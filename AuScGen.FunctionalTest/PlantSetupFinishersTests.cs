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

namespace Ecolab.FunctionalTest
{
    public class PlantSetupFinishersTests : TestBase
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
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.CleanSideTab.Click();
            Thread.Sleep(2000);
            Page.FinishersTabPage.FinishersTab.Click();

        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        [TestCategory(TestType.functional, "TC03_VerifyAddFinisherGroup_SaveFunctionality")]
        [TestCategory(TestType.regression, "TC03_VerifyAddFinisherGroup_SaveFunctionality")]
        [Test]
        public void TC01_VerifyAddFinisherGroup_SaveFunctionality()
        {
            Page.FinishersTabPage.AddingFinisherGroup("Finisher Group");
            Thread.Sleep(3000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.FinishersTabPage.AddingFinisherGroup("Finisher Group");
            Thread.Sleep(2000);
            if (null != Page.FinishersTabPage.FinishersDuplicateMessage)
            {
                if (!Page.FinishersTabPage.FinishersDuplicateMessage.BaseElement.InnerText
                    .Equals(@"Name already exists"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
                else
                {
                    Page.FinishersTabPage.CancelFinisherGroup.TypeEnterKey();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "Finisher Group" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "Finisher Group" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1 And [GroupTypeId] = " + outType;
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'Finisher Group'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount >= 1)
            {
                Assert.True(true, "Finisher Group" + " DryerGroup added successfully in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("Finisher Group" + " record not saved/ created Audit Tables in DB");
            }
        }

        [TestCategory(TestType.functional, "TC04_VerifyCancelFinisherGroup")]
        [TestCategory(TestType.regression, "TC04_VerifyCancelFinisherGroup")]
        [Test]
        public void TC02_VerifyCancelFinisherGroup()
        {
            Page.FinishersTabPage.CancelingFinisherGroup("CancelFinisherGroup");
            Assert.True(Page.FinishersTabPage.AddFinisherGroup.IsEnabled);
            if (Page.FinishersTabPage.IsContainsFinisherGroup("CancelFinisherGroup") == true)
            {
                Assert.Fail("FinisherGroup added on clicking on Cancel button in Add Dryergroup popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "CancelFinisherGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'CancelFinisherGroup'");
            int count = foundRows.Length;
            if (count < 1)
            {
                Assert.True(true, "CancelFinisherGroup" + " DryerGroup not added in DB");
            }
            else
            {
                Assert.Fail("CancelFinisherGroup" + " record saved/ created in DB on clicking on Cancel button in popup");
            }
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "CancelFinisherGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1";
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'CancelFinisherGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount < 1)
            {
                Assert.True(true, "CancelFinisherGroup" + " DryerGroup not added in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("CancelFinisherGroup" + " record created in Audit Tables in DB");
            }
        }

        [TestCategory(TestType.functional, "TC05_EditFinisherGroup")]
        [TestCategory(TestType.regression, "TC05_EditFinisherGroup")]
        [Test]
        public void TC03_EditFinisherGroup()
        {
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("Finisher Group")[0].Click();
            Page.FinishersTabPage.EditingFinisherGroup("EditFinisherGroup01");
            Thread.Sleep(3000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            //DB Validation GroupType
            //DB Validation GrouptypeHistory
        }

        [TestCategory(TestType.functional, "TC07_VerifyEditFinisherGroupSaveFunctionality")]
        [TestCategory(TestType.regression, "TC07_VerifyEditFinisherGroupSaveFunctionality")]
        [Test]
        public void TC04_VerifyEditFinisherGroupSaveFunctionality()
        {
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("EditFinisherGroup01")[0].Click();
            Page.FinishersTabPage.EditingFinisherGroup("EditFinisherGroup");
            Thread.Sleep(3000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(3000);
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "EditFinisherGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'EditFinisherGroup'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "EditFinisherGroup" + " FinisherGroup updated successfully in DB");
            }
            else
            {
                Assert.Fail("EditFinisherGroup" + " record not saved/ updated in DB");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "EditFinisherGroup" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "EditFinisherGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 2 And [GroupTypeId] = " + outType;
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'EditFinisherGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount >= 1)
            {
                Assert.True(true, "EditFinisherGroup" + " FinisherGroup updated successfully in DB");
            }
            else
            {
                Assert.Fail("EditFinisherGroup" + " record not saved/ updated in Audit Tables in DB");
            }
        }

        [TestCategory(TestType.functional, "TC08_EditFinisherGroupCancelFunctionality")]
        [TestCategory(TestType.regression, "TC08_EditFinisherGroupCancelFunctionality")]
        [Test]
        public void TC05_EditFinisherGroupCancelFunctionality()
        {
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("EditFinisherGroup")[0].Click();
            Page.FinishersTabPage.EditCancellingFinisherGroup("CancelEditFinisherGroup");
            Assert.True(Page.FinishersTabPage.FinishersMessage.IsEnabled);
            if (Page.FinishersTabPage.IsContainsFinisherGroup("CancelEditFinisherGroup") == true)
            {
                Assert.Fail("FinisherGroup updated on clicking on Cancel button in Edit Dryergroup popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "CancelEditFinisherGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'CancelEditFinisherGroup'");
            int count = foundRows.Length;
            if (count < 1)
            {
                Assert.True(true, "CancelEditFinisherGroup" + " DryerGroup not added in DB");
            }
            else
            {
                Assert.Fail("CancelEditFinisherGroup" + " record saved/ created in DB on clicking on Cancel button in popup");
            }
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "CancelEditFinisherGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1";
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'CancelEditFinisherGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount < 1)
            {
                Assert.True(true, "CancelEditFinisherGroup" + " DryerGroup not added in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("CancelEditFinisherGroup" + " record created in Audit Tables in DB");
            }
        }

        [TestCategory(TestType.functional, "TC09_RemoveFinisherGroup")]
        [TestCategory(TestType.regression, "TC09_RemoveFinisherGroup")]
        [Test]
        public void TC06_RemoveFinisherGroup()
        {
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("EditFinisherGroup")[1].Click();
            Thread.Sleep(2000);
            DialogHandler.NoButton.Click();
            if (Page.FinishersTabPage.IsContainsFinisherGroup("EditFinisherGroup") == false)
            {
                Assert.Fail("FinisherGroup deleted on clicking on Cancel button in delete confirmation popup");
            }
            Thread.Sleep(2000);
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("EditFinisherGroup")[1].Click();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.FinishersTabPage.IsContainsFinisherGroup("EditFinisherGroup") == true)
            {
                Assert.Fail("FinisherGroup not deleted on clicking on Ok button in delete confirmation popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "EditFinisherGroup" + "'" + "And [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'EditFinisherGroup'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "EditFinisherGroup" + " DryerGroup deleted in DB");
            }
            else
            {
                Assert.Fail("EditFinisherGroup" + " record not deleted in DB on clicking on Ok button in popup");
            }
        }

        [TestCategory(TestType.functional, "TC10_AddFinisherGroup")]
        [TestCategory(TestType.regression, "TC10_AddFinisherGroup")]
        [Test]
        public void TC07_AddFinisherGroup()
        {
            Page.FinishersTabPage.AddingFinisherGroup("AutoFinisherGroup");
            Thread.Sleep(3000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.FinishersTabPage.AddingFinisherGroup("AutoFinisherGroup");
            Thread.Sleep(2000);
            if (null != Page.FinishersTabPage.FinishersDuplicateMessage)
            {
                if (!Page.FinishersTabPage.FinishersDuplicateMessage.BaseElement.InnerText.Equals(@"Name already exists"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
                else
                {
                    Page.FinishersTabPage.CancelFinisherGroup.TypeEnterKey();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        [TestCategory(TestType.functional, "TC11_VerifyAddNewFinisherSaveFunctionality")]
        [TestCategory(TestType.regression, "TC11_VerifyAddNewFinisherSaveFunctionality")]
        [Test]
        public void TC08_VerifyAddNewFinisherSaveFunctionality()
        {
            Page.FinishersTabPage.GetAddFinisherButton("AutoFinisherGroup").Click();
            Thread.Sleep(2000);
            //Page.FinishersTabPage.AddingFinisher("FR1", "FinisherType 1");
            Page.FinishersTabPage.AddingFinisher("101","FR1");
            Thread.Sleep(2000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.FinishersTabPage.GetAddFinisherButton("AutoFinisherGroup").Click();
            Thread.Sleep(2000);
            //Page.FinishersTabPage.AddingFinisher("FR1", "FinisherType 1");
            Page.FinishersTabPage.AddingFinisher("101", "FR1");
            Thread.Sleep(2000);
            if (null != Page.FinishersTabPage.FinishersDuplicateMessage)
            {
                if (!Page.FinishersTabPage.FinishersDuplicateMessage.BaseElement.InnerText
                    .Equals(@"Number already exists. Please use a different number"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
                else
                {
                    Page.FinishersTabPage.CancelFinisher.TypeEnterKey();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(3000);
            string strCommand = "Select * from [TCD].[Finnishers] where [Name] = '" + "FR1" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'FR1'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "FR1" + " Finisher added successfully in DB");
            }
            else
            {
                Assert.Fail("FR1" + " record not saved/ created Audit Tables in DB");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "AutoFinisherGroup" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strGroupIdCommand = "Select [FinnisherGroupId]  from [TCD].[Finnishers] where [Name] = '" + "FR1" + "'" + "And [Is_Deleted] = 0";
            string GroupId = DBValidation.GetData(strGroupIdCommand).Tables[0].Rows[0]["FinnisherGroupId"].ToString();
            if (!outType.Equals(GroupId))
            {
                Assert.Fail("FR1" + " FinisherGroupId and GroupTypeId of AutoFinisherGroup didnt match");
            }
            //TO-DO-Audit Table Validation 
        }

        [TestCategory(TestType.functional, "TC12_VerifyAddNewFinisherCancelFunctionality")]
        [TestCategory(TestType.regression, "TC12_VerifyAddNewFinisherCancelFunctionality")]
        [Test]
        public void TC09_VerifyAddNewFinisherCancelFunctionality()
        {
            Page.FinishersTabPage.GetAddFinisherButton("AutoFinisherGroup").Click();
            //Page.FinishersTabPage.CancellingFinisher("FR2", "FinisherType 1");
            Page.FinishersTabPage.CancellingFinisher("101", "FR2");
            Assert.True(Page.FinishersTabPage.AddFinisherGroup.IsEnabled);
            if (Page.FinishersTabPage.IsContainsFinisher("AutoFinisherGroup", "FR2") == true)
            {
                Assert.Fail("Finisher created on clicking Cancel button in Add Dryer popup");
            }
            string strCommand = "Select * from [TCD].[Finnishers] where [Name] = '" + "FR2" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'FR2'");
            int ncount = FoundRows.Length;
            if (ncount < 1)
            {
                Assert.True(true, "FR2" + " Finisher not in DB");
            }
            else
            {
                Assert.Fail("FR2" + " Finisher saved/ created Audit Tables in DB");
            }
            //TO-DO-Audit Table Validation 
        }

        [TestCategory(TestType.functional, "TC13_VerifyEditFinisherSaveFunctionality")]
        [TestCategory(TestType.regression, "TC13_VerifyEditFinisherSaveFunctionality")]
        [Test]
        public void TC10_VerifyEditFinisherSaveFunctionality()
        {
            Page.FinishersTabPage.GetFinisherButtonControls("AutoFinisherGroup", "FR1")[4].Click();
            //Page.FinishersTabPage.EditingFinisher("FR3", "FinisherType 2");
            Page.FinishersTabPage.EditingFinisher("102", "FR3");
            Thread.Sleep(2000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText.Equals(@"Record updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(2000);
            string strCommand = "Select * from [TCD].[Finnishers] where [Name] = '" + "FR3" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'FR3'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "FR3" + " Finisher updated successfully in DB");
            }
            else
            {
                Assert.Fail("FR3" + " record not saved/ updated in DB");
            }
            //TO-DO-Audit Table Validation 
        }

        [TestCategory(TestType.functional, "TC14_VerifyEditFinisherCancelFunctionality")]
        [TestCategory(TestType.regression, "TC14_VerifyEditFinisherCancelFunctionality")]
        [Test]
        public void TC11_VerifyEditFinisherCancelFunctionality()
        {
            Page.FinishersTabPage.GetFinisherButtonControls("AutoFinisherGroup", "FR3")[4].Click();
            //Page.FinishersTabPage.CancelingEditFinisher("FR4", "FinisherType 3");
            Page.FinishersTabPage.CancelingEditFinisher("102", "FR4");
            Thread.Sleep(2000);
            Assert.True(Page.FinishersTabPage.AddFinisherGroup.IsEnabled);
            if (Page.FinishersTabPage.IsContainsFinisher("AutoFinisherGroup", "FR4") == true)
            {
                Assert.Fail("Finisher updated on clicking on Cancel button in edit popup");
            }
            string strCommand = "Select * from [TCD].[Finnishers] where [Name] = '" + "FR4" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'FR4'");
            int ncount = FoundRows.Length;
            if (ncount < 1)
            {
                Assert.True(true, "FR4" + " Finisher not updated successfully in DB");
            }
            else
            {
                Assert.Fail("FR4" + " record saved/ updated in DB on clicking cancel button");
            }
            //TO-DO-Audit Table Validation 
        }

        [TestCategory(TestType.functional, "TC15_VerifyRemoveFinisher")]
        [TestCategory(TestType.regression, "TC15_VerifyRemoveFinisher")]
        [Test]
        public void TC12_VerifyRemoveFinisher()
        {
            Page.FinishersTabPage.GetFinisherButtonControls("AutoFinisherGroup", "FR3")[3].Click();
            Thread.Sleep(2000);
            DialogHandler.NoButton.Click();
            if (Page.FinishersTabPage.IsContainsFinisher("AutoFinisherGroup", "FR3") != true)
            {
                Assert.Fail("Finisher deleted on clicking on Cancel button in delete confirmation popup");
            }
            Thread.Sleep(3000);
            Page.FinishersTabPage.GetFinisherButtonControls("AutoFinisherGroup", "FR3")[3].Click();
            DialogHandler.YesButton.Click();
            Thread.Sleep(3000);
            if (null != Page.FinishersTabPage.FinishersMessage)
            {
                if (!Page.FinishersTabPage.FinishersMessage.BaseElement.InnerText
                    .Equals(@"Record deleted successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(3000);
            if (Page.FinishersTabPage.IsContainsFinisher("AutoFinisherGroup", "FR3") == true)
            {
                Assert.Fail("Finisher not deleted on clicking on Ok button in delete confirmation popup");
            }
            Thread.Sleep(3000);
            Page.FinishersTabPage.GetAddFinisherGroupActionItems("AutoFinisherGroup")[1].Click();
            DialogHandler.YesButton.Click();
            Thread.Sleep(3000);
            if (Page.FinishersTabPage.IsContainsFinisherGroup("AutoFinisherGroup") == true)
            {
                Assert.Fail("FinisherGroup not deleted on clicking on OK button in delete confirmation popup");
            }
            string strCommand = "Select * from [TCD].[Finnishers] where [Name] = '" + "FR3" + "'" + "And [Is_Deleted] = 1";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'FR3'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "FR3" + " Finisher deleted successfully in DB");
            }
            else
            {
                Assert.Fail("FR3" + " record not deleted in DB");
            }

        }

        [TestCategory(TestType.functional, "TC16_LocalizationFinishers")]
        [TestCategory(TestType.regression, "TC16_LocalizationFinishers")]
        [Test]
        public void TC13_LocalizationFinishers()
        {
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.PlantSetupPage.CleanSideTab.Click();
            Page.FinishersTabPage.FinishersTab.Click();
            if (Page.FinishersTabPage.AddFinisherGroup.BaseElement.InnerText != "Voeg Finisher froup")
            {
                PostLocalizationDryers();
                Assert.Fail(string.Format("Incorrect Label is displayed in Finisher Tab - {0} when localization changed to Deutsch, Expected - Voeg Finisher froup", Page.FinishersTabPage.AddFinisherGroup.BaseElement.InnerText));
            }
            PostLocalizationDryers();
        }

        [TestCategory(TestType.functional, "TC01_Verify_UserRoles_LevelFour")]
        [TestCategory(TestType.regression, "TC01_Verify_UserRoles_LevelFour")]
        [Test]
        public void TC14_Verify_UserRoles_LevelFour()
        {
            //User Less than 6 - 4
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestPM", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.CleanSideTab.Click();
            try
            {
                if (Page.FinishersTabPage.FinishersTab.IsEnabled)
                {
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("FinishersTab enabled for user Level 4 - Expected it should not allow");
                }
            }
            catch
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.True(true, "FinishersTab not enabled for user Level 4 - Expected it should not allow");
            }

        }

        [TestCategory(TestType.functional, "TC02_Verify_UserRoles_LevelBasic")]
        [TestCategory(TestType.regression, "TC02_Verify_UserRoles_LevelBasic")]
        [Test]
        public void TC15_Verify_UserRoles_LevelBasic()
        {
            //User Less than 6 - 4
            Page.LoginPage.VerifyLogin("AutoTestTMBasic", "test");
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.CleanSideTab.Click();
            Thread.Sleep(2000);
            Page.FinishersTabPage.FinishersTab.Click();
            try
            {
                if (Page.FinishersTabPage.AddFinisherGroup.IsEnabled)
                {
                    Page.PlantSetupPage.TopMainMenu.LogOut();
                    Assert.Fail("AddFinisherGroup button enabled for user Level 6 - Expected it should not allow..View only permissions");
                }
            }
            catch
            {
                Page.PlantSetupPage.TopMainMenu.LogOut();
                Assert.True(true, "AddFinisherGroup button not enabled for user Level 6 - Expected it should not allow..View only permissions");
            }
        }

        private void PostLocalizationDryers()
        {
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("English US", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.PlantSetupPage.CleanSideTab.Click();
            Page.FinishersTabPage.FinishersTab.Click();
            if (Page.FinishersTabPage.AddFinisherGroup.BaseElement.InnerText != "Finishers")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed in Finisher Tab - {0} when localization changed to English, Expected - Add Finisher Group", Page.FinishersTabPage.AddFinisherGroup.BaseElement.InnerText));
            }
        }

    }
}
