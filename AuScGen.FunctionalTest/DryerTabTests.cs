using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using NUnit.Framework;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Threading;
using System.Data;


namespace Ecolab.FunctionalTest
{
    public class DryerTabTests : TestBase
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

            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CleanSideTab.Click();
            //Page.DryerTabPage.NoButton.Click();
            Page.DryerTabPage.DryerTab.Click();
            
        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        //[TestFixtureTearDown]
        //public void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    //base.TestFixtureTearDown();
        //}

        /// <summary>
        /// Test case 27564: Verify whether the "Dryers" menu is displaying in the menu bar
        /// </summary>
        [TestCategory(TestType.functional, "TC01_VerifyDryerMenu")]
        [TestCategory(TestType.regression, "TC01_VerifyDryerMenu")]
        [Test]
        public void TC01_VerifyDryerMenu()
        {
            
            if (null != Page.DryerTabPage.DryerTab.BaseElement.InnerText)
            {
                if (!Page.DryerTabPage.DryerTab.BaseElement.InnerText
                    .Equals(@"Dryer"))
                {
                    Assert.Fail("Incorrect menu name displayed");
                }
            }
            else
            {
                Assert.Fail("Dryer menu is not displayed");
            }

            Page.DryerTabPage.DryerTab.Click();
        }

        /// <summary>
        /// Test case 27579: RG: Verify the "Add Dryer Group" button functionality
        /// </summary>
        [TestCategory(TestType.functional, "TC04_VerifyAddDryerGroup")]
        [TestCategory(TestType.regression, "TC04_VerifyAddDryerGroup")]
        [Test]
        public void TC04_VerifyAddDryerGroup()
        {
            if (null != Page.DryerTabPage.AddDryerGroup)
            {
                if (!Page.DryerTabPage.AddDryerGroup.BaseElement.InnerText
                    .Equals(@"Add Dryer Group"))
                {
                    Assert.Fail("Incorrect Add Dryer Group button name is displayed");
                }
                else
                {
                    Page.DryerTabPage.AddDryerGroup.Click();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            if (null != Page.DryerTabPage.DryerGroupName)
            {
                if (!Page.DryerTabPage.DryerGroupName.IsEnabled)
                {
                    Assert.Fail("Dryer GroupName textbox not enabled");
                }
                else
                {
                    Page.DryerTabPage.DryerGroupName.SetText("DD");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            if (null != Page.DryerTabPage.SaveDryerGroup)
            {
                if (!Page.DryerTabPage.SaveDryerGroup.IsEnabled)
                {
                    Assert.Fail("Save DryerGroup button not enabled");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            if (null != Page.DryerTabPage.CancelDryerGroup)
            {
                if (!Page.DryerTabPage.CancelDryerGroup.IsEnabled)
                {
                    Assert.Fail("Cancel DryerGroup button not enabled");
                }
                else
                {
                    Page.DryerTabPage.CancelDryerGroup.Click();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        /// <summary>
        /// Test case 27580: RG: Verify the "Save" button functionality on "Add Dryer Group" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC05_VerifyDryerGroupSaveFunctionality")]
        [TestCategory(TestType.regression, "TC05_VerifyDryerGroupSaveFunctionality")]
        [Test]
        public void TC05_VerifyDryerGroupSaveFunctionality()
        {
            Page.DryerTabPage.AddingDryerGroup("Dryer Group");
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "Dryer Group" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'Dryer Group'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "Dryer Group" + " DryerGroup added successfully in DB");
            }
            else
            {
                Assert.Fail("Dryer Group" + " record not saved/ created in DB");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "Dryer Group" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "Dryer Group" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1 And [GroupTypeId] = " + outType;
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'Dryer Group'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount >= 1)
            {
                Assert.True(true, "Dryer Group" + " DryerGroup added successfully in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("Dryer Group" + " record not saved/ created Audit Tables in DB");
            }
        }

        /// <summary>
        /// Test case 27581: RG: Verify the "Cancel" button functionality on "Add Dryer Group" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC06_VerifyDryerGroupCancelFunctionality")]
        [TestCategory(TestType.regression, "TC06_VerifyDryerGroupCancelFunctionality")]
        [Test]
        public void TC06_VerifyDryerGroupCancelFunctionality()
        {
            Page.DryerTabPage.CancelingDryerGroup("CancelDryerGroup");
            Assert.True( Page.DryerTabPage.AddDryerGroup.IsEnabled);
            if (Page.DryerTabPage.IsContainsDryerGroup("CancelDryerGroup") == true)
            {
                Assert.Fail("DryerGroup updated on clicking on Cancel button in Add Dryergroup popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "CancelDryerGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'CancelDryerGroup'");
            int count = foundRows.Length;
            if (count < 1)
            {
                Assert.True(true, "CancelDryerGroup" + " DryerGroup not added in DB");
            }
            else
            {
                Assert.Fail("CancelDryerGroup" + " record saved/ created in DB on clicking on Cancel button in popup");
            }
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "CancelDryerGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1";
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'CancelDryerGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount < 1)
            {
                Assert.True(true, "CancelDryerGroup" + " DryerGroup not added in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("CancelDryerGroup" + " record created in Audit Tables in DB");
            }
        }

        /// <summary>
        /// Test case 27587: RG: Verify the "Edit Dryer Group" functionality
        /// </summary>
        [TestCategory(TestType.functional, "TC08_EditDryerGroup")]
        [TestCategory(TestType.regression, "TC08_EditDryerGroup")]
        [Test]
        public void TC08_EditDryerGroup()
        {
            Page.DryerTabPage.GetAddDryerGroupActionItems("Dryer Group")[0].Click();
            Page.DryerTabPage.EditDryerGroup("EditDryerGroup01");
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
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

        /// <summary>
        /// Test case 27593: RG: Verify the "Save" button functionality on "Edit Dryer Group" popup
        /// </summary>
        [TestCategory(TestType.functional, "TC09_VerifyEditDryerGroupSaveFunctionality")]
        [TestCategory(TestType.regression, "TC09_VerifyEditDryerGroupSaveFunctionality")]
        [Test]
        public void TC09_VerifyEditDryerGroupSaveFunctionality()
        {
            Page.DryerTabPage.GetAddDryerGroupActionItems("EditDryerGroup01")[0].Click();
            Page.DryerTabPage.EditDryerGroup("EditDryerGroup");
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
                    .Equals(@"Record updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "EditDryerGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'EditDryerGroup'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "EditDryerGroup" + " DryerGroup updated successfully in DB");
            }
            else
            {
                Assert.Fail("EditDryerGroup" + " record not saved/ updated in DB");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "EditDryerGroup" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "EditDryerGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 2 And [GroupTypeId] = " + outType;
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'EditDryerGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount >= 1)
            {
                Assert.True(true, "EditDryerGroup" + " DryerGroup updated successfully in DB");
            }
            else
            {
                Assert.Fail("EditDryerGroup" + " record not saved/ updated in Audit Tables in DB");
            }
        }

        /// <summary>
        /// Test case 27595: RG: Verify the "Cancel" button functionality on "Edit Dryer Group" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC10_EditDryerGroupCancelFunctionality")]
        [TestCategory(TestType.regression, "TC10_EditDryerGroupCancelFunctionality")]
        [Test]
        public void TC10_EditDryerGroupCancelFunctionality()
        {
            Page.DryerTabPage.GetAddDryerGroupActionItems("EditDryerGroup")[0].Click();
            Page.DryerTabPage.CancelingEditDryerGroup("CancelEditDryerGroup");
            Assert.True(Page.DryerTabPage.AddDryerGroup.IsEnabled);
            if (Page.DryerTabPage.IsContainsDryerGroup("CancelEditDryerGroup") == true)
            {
                Assert.Fail("DryerGroup updated on clicking on Cancel button in Edit Dryergroup popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "CancelEditDryerGroup" + "'" + "And [Is_Deleted] = 0";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'CancelEditDryerGroup'");
            int count = foundRows.Length;
            if (count < 1)
            {
                Assert.True(true, "CancelEditDryerGroup" + " DryerGroup not added in DB");
            }
            else
            {
                Assert.Fail("CancelEditDryerGroup" + " record saved/ created in DB on clicking on Cancel button in popup");
            }
            string strAduit = "Select * from [TCD].[GroupTypeHistory] where [GroupDescription] = '" + "CancelEditDryerGroup" + "'" + "And [Is_Deleted] = 0 And [OPERATIONID] = 1";
            DataRow[] AuditRows = DBValidation.GetData(strAduit).Tables[0].Select("GroupDescription = " + "'CancelEditDryerGroup'");
            int nAuditcount = AuditRows.Length;
            if (nAuditcount < 1)
            {
                Assert.True(true, "CancelEditDryerGroup" + " DryerGroup not added in Audit Tables in DB");
            }
            else
            {
                Assert.Fail("CancelEditDryerGroup" + " record created in Audit Tables in DB");
            }
        }

        /// <summary>
        /// Test case 27583: RG: Verify the "Remove" functionality in Dryer Group grid
        /// </summary>
        [TestCategory(TestType.functional, "TC11_VerifyRemoveDryerGroup")]
        [TestCategory(TestType.regression, "TC11_VerifyRemoveDryerGroup")]
        [Test]
        public void TC11_VerifyRemoveDryerGroup()
        {
            Page.DryerTabPage.GetAddDryerGroupActionItems("EditDryerGroup")[1].Click();
            DialogHandler.NoButton.Click();
            if (Page.DryerTabPage.IsContainsDryerGroup("EditDryerGroup") == false)
            {
                Assert.Fail("DryerGroup deleted on clicking on Cancel button in delete confirmation popup");
            }
            Thread.Sleep(2000);
            Page.DryerTabPage.GetAddDryerGroupActionItems("EditDryerGroup")[1].Click();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.DryerTabPage.IsContainsDryerGroup("EditDryerGroup") == true)
            {
                Assert.Fail("DryerGroup not deleted on clicking on Ok button in delete confirmation popup");
            }
            string strCommand = "Select * from [TCD].[GroupType] where [GroupDescription] = '" + "EditDryerGroup" + "'" + "And [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("GroupDescription = " + "'EditDryerGroup'");
            int count = foundRows.Length;
            if (count >=1)
            {
                Assert.True(true, "EditDryerGroup" + " DryerGroup deleted in DB");
            }
            else
            {
                Assert.Fail("EditDryerGroup" + " record not deleted in DB on clicking on Ok button in popup");
            }
        }

        /// <summary>
        /// Test case 27608: RG: Verify the Add Dryer functionality
        /// </summary>
        [TestCategory(TestType.functional, "TC12_AddDryerGroup")]
        [TestCategory(TestType.regression, "TC12_AddDryerGroup")]
        [Test]
        public void TC12_AddDryerGroup()
        {
            Page.DryerTabPage.AddingDryerGroup("AutoTestDryerGroup");
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.DryerTabPage.AddingDryerGroup("AutoTestDryerGroup");
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.DryerErrorMessage)
            {
                if (!Page.DryerTabPage.DryerErrorMessage.BaseElement.InnerText
                    .Equals(@"Name already exists"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
                else
                {
                    Page.DryerTabPage.CancelDryerGroup.TypeEnterKey();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        /// <summary>
        /// Test case 27649: RG: Verify the "Save" button functionality on "Add New Dryer" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC13_VerifyAddNewDryerSaveFunctionality")]
        [TestCategory(TestType.regression, "TC13_VerifyAddNewDryerSaveFunctionality")]
        [Test]
        public void TC13_VerifyAddNewDryerSaveFunctionality()
        {
            Page.DryerTabPage.GetAddDryerButton("AutoTestDryerGroup").Click();
            //Page.DryerTabPage.AddingDryer("DR1", "Type 1", "1");         
            Page.DryerTabPage.AddingDryer("101", "DR1", "1");         
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
                    .Equals(@"Record added successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.DryerTabPage.GetAddDryerButton("AutoTestDryerGroup").Click();
            //Page.DryerTabPage.AddingDryer("DR1", "Type 1", "1");
            Page.DryerTabPage.AddingDryer("101", "DR1", "1");
            if (null != Page.DryerTabPage.DryerErrorMessage)
            {
                if (!Page.DryerTabPage.DryerErrorMessage.BaseElement.InnerText
                    .Equals(@"Number & name already exists. Please use a different number & name"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
                else
                {
                    Page.DryerTabPage.CancelDryer.TypeEnterKey();
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[Dryers] where [Description] = '" + "DR1" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Description = " + "'DR1'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "DR1" + " Dryer added successfully in DB");
            }
            else
            {
                Assert.Fail("DR1" + " record not saved/ created Audit Tables in DB");
            }
            string strGroupType = "Select [GroupTypeId] from [TCD].[GroupType] where [GroupDescription] = '" + "AutoTestDryerGroup" + "'" + "And [Is_Deleted] = 0";
            string outType = DBValidation.GetData(strGroupType).Tables[0].Rows[0]["GroupTypeId"].ToString();
            string strGroupIdCommand = "Select [DryerGroupId]  from [TCD].[Dryers] where [Description] = '" + "DR1" + "'" + "And [Is_Deleted] = 0";
            string GroupId = DBValidation.GetData(strGroupIdCommand).Tables[0].Rows[0]["DryerGroupId"].ToString();
            if(!outType.Equals(GroupId))
            {
                Assert.Fail("DR1" + " DryerGroupId and GroupTypeId of AutoTestDryerGroup didnt match");
            }
            //TO-DO-Audit Table Validation 
        }

        /// <summary>
        /// Test case 27650: RG: Verify the "Cancel" button functionality on "Add Dryer" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC14_VerifyAddNewDryerCancelFunctionality")]
        [TestCategory(TestType.regression, "TC14_VerifyAddNewDryerCancelFunctionality")]
        [Test]
        public void TC14_VerifyAddNewDryerCancelFunctionality()
        {
            Page.DryerTabPage.GetAddDryerButton("AutoTestDryerGroup").Click();
            //Page.DryerTabPage.CancellingDryer("DR2", "Type 1", "1");
            Page.DryerTabPage.CancellingDryer("101", "DR2", "1");
            Assert.True(Page.DryerTabPage.AddDryerGroup.IsEnabled);
            if (Page.DryerTabPage.IsContainsDryer("AutoTestDryerGroup", "DR2") == true)
            {
                Assert.Fail("Dryer created on clicking Cancel button in Add Dryer popup");
            }
            string strCommand = "Select * from [TCD].[Dryers] where [Description] = '" + "DR2" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Description = " + "'DR2'");
            int ncount = FoundRows.Length;
            if (ncount < 1)
            {
                Assert.True(true, "DR2" + " Dryer not in DB");
            }
            else
            {
                Assert.Fail("DR2" + " Dryer saved/ created Audit Tables in DB");
            }
            //TO-DO-Audit Table Validation 
        }

        /// <summary>
        /// Test case 27653: RG: Verify the "Save" button functionality on "Edit Dryer" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC14_VerifyAddNewDryerCancelFunctionality")]
        [TestCategory(TestType.regression, "TC14_VerifyAddNewDryerCancelFunctionality")]
        [Test]
        public void TC16_VerifyEditDryerSaveFunctionality()
        {
            Page.DryerTabPage.GetDryerButtonControls("AutoTestDryerGroup", "DR1")[4].Click();
            //Page.DryerTabPage.EditDryer("DR3", "Type 2", "2");
            Page.DryerTabPage.EditDryer("102", "DR3", "2");
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
                    .Equals(@"Record updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[Dryers] where [Description] = '" + "DR3" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Description = " + "'DR3'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "DR3" + " Dryer updated successfully in DB");
            }
            else
            {
                Assert.Fail("DR3" + " record not saved/ updated in DB");
            }
            //TO-DO-Audit Table Validation 
        }

        /// <summary>
        /// Test case 27654: RG: Verify the "Cancel" button functionality on "Edit Dryer" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC17_VerifyEditDryerCancelFunctionality")]
        [TestCategory(TestType.regression, "TC17_VerifyEditDryerCancelFunctionality")]
        [Test]
        public void TC17_VerifyEditDryerCancelFunctionality()
        {
            Page.DryerTabPage.GetDryerButtonControls("AutoTestDryerGroup", "DR3")[4].Click();
            //Page.DryerTabPage.CancelingEditDryer("DR4", "Type 3", "4");
            Page.DryerTabPage.CancelingEditDryer("102", "DR4", "4");
            Assert.True(Page.DryerTabPage.AddDryerGroup.IsEnabled);
            if (Page.DryerTabPage.IsContainsDryer("AutoTestDryerGroup", "DR4") == true)
            {
                Assert.Fail("Dryer updated on clicking on Cancel button in edit popup");
            }
            string strCommand = "Select * from [TCD].[Dryers] where [Description] = '" + "DR4" + "'" + "And [Is_Deleted] = 0";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Description = " + "'DR4'");
            int ncount = FoundRows.Length;
            if (ncount < 1)
            {
                Assert.True(true, "DR4" + " Dryer not updated successfully in DB");
            }
            else
            {
                Assert.Fail("DR4" + " record saved/ updated in DB on clicking cancel button");
            }
            //TO-DO-Audit Table Validation 
        }

        /// <summary>
        /// Test case 27651: RG: Verify the "Remove" functionality in Dryers grid
        /// </summary>
        [TestCategory(TestType.functional, "TC18_VerifyRemoveDryer")]
        [TestCategory(TestType.regression, "TC18_VerifyRemoveDryer")]
        [Test]
        public void TC18_VerifyRemoveDryer()
        {
            Page.DryerTabPage.GetDryerButtonControls("AutoTestDryerGroup", "DR3")[3].Click();
            DialogHandler.NoButton.Click();
            if (Page.DryerTabPage.IsContainsDryer("AutoTestDryerGroup", "DR3") != true)
            {
                Assert.Fail("Dryer deleted on clicking on Cancel button in delete confirmation popup");
            }
            Thread.Sleep(3000);
            Page.DryerTabPage.GetDryerButtonControls("AutoTestDryerGroup", "DR3")[3].Click();
            DialogHandler.YesButton.Click();
            Thread.Sleep(3000);
            if (null != Page.DryerTabPage.AddDryerMessage)
            {
                if (!Page.DryerTabPage.AddDryerMessage.BaseElement.InnerText
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
            if (Page.DryerTabPage.IsContainsDryer("AutoTestDryerGroup", "DR3") == true)
            {
                Assert.Fail("Dryer not deleted on clicking on Ok button in delete confirmation popup");
            }
            Thread.Sleep(3000);
            Page.DryerTabPage.GetAddDryerGroupActionItems("AutoTestDryerGroup")[1].Click();
            DialogHandler.YesButton.Click();
            Thread.Sleep(3000);
            if (Page.DryerTabPage.IsContainsDryerGroup("AutoTestDryerGroup") == true)
            {
                Assert.Fail("DryerGroup not deleted on clicking on OK button in delete confirmation popup");
            }
            string strCommand = "Select * from [TCD].[Dryers] where [Description] = '" + "DR3" + "'" + "And [Is_Deleted] = 1";
            DataRow[] FoundRows = DBValidation.GetData(strCommand).Tables[0].Select("Description = " + "'DR3'");
            int ncount = FoundRows.Length;
            if (ncount >= 1)
            {
                Assert.True(true, "DR3" + " Dryer deleted successfully in DB");
            }
            else
            {
                Assert.Fail("DR3" + " record not deleted in DB");
            }

        }

        /// <summary>
        /// Test case 28457: RG: Verify Localization on Dryers Page
        /// </summary>
        [TestCategory(TestType.functional, "TC19_LocalizationDryers")]
        [TestCategory(TestType.regression, "TC19_LocalizationDryers")]
        [Test]
        public void TC19_LocalizationDryers()
        {
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.PlantSetupPage.CleanSideTab.Click();
            Page.DryerTabPage.DryerTab.Click();
            if (Page.DryerTabPage.DryerTab.BaseElement.InnerText != "Droger")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed in Dryer Tab - {0} when localization changed to Deutsch, Expected - Droger", Page.DryerTabPage.DryerTab.BaseElement.InnerText));
            }
        }

        [TestCategory(TestType.functional,"TC20_PostLocalizationDryers")]
        [TestCategory(TestType.regression, "TC20_PostLocalizationDryers")]
        [Test]
        public void TC20_PostLocalizationDryers()
        {
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("English US", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.PlantSetupPage.CleanSideTab.Click();
            Page.DryerTabPage.DryerTab.Click();
            if (Page.DryerTabPage.DryerTab.BaseElement.InnerText != "Dryer")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed in Dryer Tab - {0} when localization changed to English, Expected - Dryer", Page.DryerTabPage.DryerTab.BaseElement.InnerText));
            }
        }

    }
}



