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
    public class PlantSetupFormulasTests : TestBase
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
            Thread.Sleep(3000);
            Page.FormulasTabPage.FormulasTab.Click();
            Thread.Sleep(3000);

        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        [TestCategory(TestType.functional, "TC01_AddNewFormula")]
        [TestCategory(TestType.regression, "TC01_AddNewFormula")]
        [Test, Description("Test Case 24704:RG Plant Setup -> Formulas Setup Page : Verify Create New Formula Functionality")]
        public void TC01_AddNewFormula()
        {
            Page.FormulasTabPage.AddFormula.Click();
            Page.FormulasTabPage.AddingFormula("AddFormula", "textilecategory1", "Saturation2", "Blend", "Test2", "1", "2");
            Thread.Sleep(2000);
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"New formula added Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + "AddFormula" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'AddFormula'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "AddFormula" + " Formula added successfully in DB");
            }
            else
            {
                Assert.Fail("AddFormula" + " record not saved/ created in DB");
            }
        }

        [TestCategory(TestType.functional, "TC02_EditFormula")]
        [TestCategory(TestType.regression, "TC02_EditFormula")]
        [Test, Description("Test Case 24723:RG Plant Setup -> Formulas Setup Page : Verify the Edit functionality and Cancel the changes")]
        public void TC02_EditFormula()
        {
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            Page.FormulasTabPage.EditingFormulaCancelFunctionality("PA", "textilecategory1", "Saturation2", "Blend", "Test2", "1", "2");
            if(!Page.FormulasTabPage.AddFormula.IsVisible())
            {
                Assert.Fail("Page not navigated back to Formulas page on clicking cancel button in EditFormula Popup");
            }
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            Page.FormulasTabPage.EditingFormula("EditFormula", "textilecategory1", "saturation2", "Blend", "Test2", "1", "2");
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Formula Updated Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + "EditFormula" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'EditFormula'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "EditFormula" + " Formula added successfully in DB");
            }
            else
            {
                Assert.Fail("EditFormula" + " record not saved/ created in DB");
            }
            //DB Validation
        }

        [TestCategory(TestType.functional, "TC03_VerifyInLineEdit")]
        [TestCategory(TestType.regression, "TC03_VerifyInLineEdit")]
        [Test, Description("Test Case 24761:RG Plant Setup Formulas Setup Page Verify the Inline Edit functionality and Update the changes")]
        public void TC03_VerifyInLineEdit()
        {
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Page.FormulasTabPage.InLineEditingFormula("InlineEdit");
            Thread.Sleep(2000);
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Thread.Sleep(2000);
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText.Trim()
                    .Equals(@"Formula Updated Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + " InlineEdit" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "' InlineEdit'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, " InlineEdit" + " Formula updated successfully in DB");
            }
            else
            {
                Assert.Fail(" InlineEdit" + " record not saved/ created in DB");
            }
        }

        [TestCategory(TestType.functional, "TC04_VerifyInLineEditCancel")]
        [TestCategory(TestType.regression, "TC04_VerifyInLineEditCancel")]
        [Test, Description("Test Case 24765:RG Plant Setup Formulas Setup Page : Verify the Inline Edit functionality and Cancel the changes")]
        public void TC04_VerifyInLineEditCancel()
        {
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Page.FormulasTabPage.InLineEditingFormula("InlineEditCancel");
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            Thread.Sleep(2000);
            if (Page.FormulasTabPage.isRecordExist("CA") == true)
            {
                Assert.Fail("CA" + " formula added on clicking cancel after changing the name in inline Editing");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + "InlineEditCancel" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'InlineEditCancel'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.Fail("InlineEditCancel" + " record saved/ created in DB on clicking on edit cancel button");

            }
            else
            {
                Assert.True(true, "InlineEditCancel" + " Formula not updated successfully in DB");
            }
            //DB Validation
        }

        [TestCategory(TestType.functional, "TC05_CopyFormula")]
        [TestCategory(TestType.regression, "TC05_CopyFormula")]
        [Test, Description("Test Case 25022:RG Plant Setup Formulas Setup Page : Verify the Copy functionality")]
        public void TC05_CopyFormula()
        {
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[3].Click();
            Page.FormulasTabPage.CopyingFormula("CopyFormula", "textilecategory2", "Saturation2", "Blend", "Test2", "1", "2");
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Formula Added Successfully from copy"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + "CopyFormula" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'CopyFormula'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, "CopyFormula" + " Formula added successfully in DB");
            }
            else
            {
                Assert.Fail("CopyFormula" + " record not saved/ created in DB");
            }
        }

        [TestCategory(TestType.functional, "TC06_DeleteFormula")]
        [TestCategory(TestType.regression, "TC06_DeleteFormula")]
        [Test, Description("Test Case 24743:RG:Plant Setup -> Formulas Setup Page : Verify Delete button functionality by selecting Ok")]
        public void TC06_DeleteFormula()
        {
            Thread.Sleep(5000);
            DialogHandler.GetMessageAndOKButton();
            string strTest = Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetColumnValues()[1];
            Thread.Sleep(2000);
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            Thread.Sleep(2000);
            if (null != DialogHandler.LastDialogMessage)
            {
                if (!DialogHandler.LastDialogMessage
                    .Equals(@"Are you sure you want to delete this record?"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed in dialog box : {0}", DialogHandler.LastDialogMessage));
                }
            }
            else
            {
                Assert.Fail("Dialog box with confirmation to delete row is not displayed");
            }
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Formula Deleted Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + strTest + "'" + "And [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'" + strTest + "'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strTest + " Formula deleted successfully in DB");
            }
            else
            {
                Assert.Fail(strTest + " record not deleted in DB on clicking on Ok button in popup");
            }

            DialogHandler.GetMessageAndOKButton();
            Thread.Sleep(2000);
            Page.FormulasTabPage.FormulasTableGrid.SelectedRows("InlineEdit")[0].GetButtonControls()[1].Click();
            Thread.Sleep(2000);
            if (null != Page.FormulasTabPage.SuccessMessage)
            {
                if (!Page.FormulasTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Formula Deleted Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            //DB Validation Pending
        }

        [TestCategory(TestType.functional, "TC07_DeleteFormulaCancelFunctionality")]
        [TestCategory(TestType.regression, "TC07_DeleteFormulaCancelFunctionality")]
        [Test, Description("Test Case 24759:RG Plant Setup Formulas Setup Page : Verify Delete button functionality by selecting Cancel")]
        public void TC07_DeleteFormulaCancelFunctionality()
        {
            DialogHandler.ClickonCancelButton();
            string Text = Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetColumnValues()[1];
            Thread.Sleep(2000);
            Page.FormulasTabPage.FormulasTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            Thread.Sleep(1000);
            if(Page.FormulasTabPage.isRecordExist(Text) == false)
            {
                Assert.Fail( Text + " formula deleted on clicking cancel in delete confirmation popup");
            }
            string strCommand = "Select * from [TCD].[ProgramMaster] where Name = '" + Text + "'" + "And [Is_Deleted] = 1";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("Name = " + "'" + Text + "'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.Fail(Text + " record deleted in DB on clicking on Cancel button in popup");              
            }
            else
            {
                Assert.True(true, Text + " Formula not deleted successfully in DB");
            }
            //DB Validation Pending
        }




    }
}
