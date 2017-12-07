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
    public class PlantSetupCustomerTests : TestBase
    {
         [TestFixtureSetUp]
         public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Precondition();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");            
        }

        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        /// <summary>
        ///  TC 18822: Customer Setup page:Verify if the User could navigate to Customer Tab
        /// </summary>
        [TestCategory(TestType.regression, "TC01_VerifyCustomerTab")]
        [TestCategory(TestType.functional, "TC01_VerifyCustomerTab")]
        [Test]
        public void TC01_VerifyCustomerTab()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Assert.True(Page.CustomerTabPage.CustomerTabGridTable.IsEnabled, "CustomerTab Tablegrid not found");
        }

        /// <summary>
        /// TC 18823: Chemical: Verify the Customer Page fields displayed are as per the attachment
        /// </summary>
        [TestCategory(TestType.regression, "TC02_Verify_CustomerGridTableHeader")]
        [TestCategory(TestType.functional, "TC02_Verify_CustomerGridTableHeader")]
        [Test]
        public void TC02_Verify_CustomerGridTableHeader()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Assert.True(Page.CustomerTabPage.CustomerTabGridTable.IsEnabled, "CustomerTab Tablegrid not found");
            Assert.True(Page.CustomerTabPage.AreColumnsExist("Number", "Name"), "Customer ID column name not found");
        }

        /// <summary>
        ///  TC 18860: Customer Setup page:Verify Add Customer Functionality
        /// </summary>
        [TestCategory(TestType.regression, "TC03_AddCustomer")]
        [TestCategory(TestType.functional, "TC03_AddCustomer")]
        [Test]
        public void TC03_AddCustomer()
        {
            string strID = System.DateTime.Now.Millisecond.ToString();
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Thread.Sleep(5000);
            Page.CustomerTabPage.AddCustomerButton.Click();
            Page.CustomerTabPage.AddCustomer(strID, "sandiego");
            Assert.True(Page.CustomerTabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Customer added Successfully"), "Success Message not matched");
            
            string strCommand = "Select * from [TCD].[PlantCustomer] Where CustomerId = '" + strID + "' ";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("CustomerId = " + strID);
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strID + " Customer added successfully in DB");
            }
            else
            {
                Assert.Fail(strID + " record not saved/ created in DB");
            }
        }

        /// <summary>
        ///  TC 19047: Customer Setup page:Verify Edit Customer functionality
        /// </summary>
        [TestCategory(TestType.regression, "TC04_UpdateCustomer")]
        [TestCategory(TestType.functional, "TC04_UpdateCustomer")]
        [Test]
        public void TC04_UpdateCustomer()
        {
            string strID = System.DateTime.Now.Millisecond.ToString();
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Thread.Sleep(5000);
            Page.CustomerTabPage.CustomerTabGrid.SelectedRows("sandiego")[0].GetButtonControls()[2].Click();
            Page.CustomerTabPage.UpdateCustomer(strID, "Camarillo");
            Assert.True(Page.CustomerTabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Customer updated Successfully"), "Success Message not matched");

            string strCommand = "Select * from [TCD].[PlantCustomer] Where CustomerId = '" + strID + "' AND CustomerName = '" + "Camarillo" + "'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("CustomerId = " + strID);
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strID + " Customer updated successfully in DB");
            }
            else
            {
                Assert.Fail(strID + " record not updated in DB");
            }
        }

        /// <summary>
        ///  TC 19125: Customer Setup page:Verify Cancel button functionality
        /// </summary>
        [TestCategory(TestType.regression, "TC06_VerifyUpdateCustomerCancelButton")]
        [TestCategory(TestType.functional, "TC06_VerifyUpdateCustomerCancelButton")]
        [Test]
        public void TC06_VerifyUpdateCustomerCancelButton()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Thread.Sleep(5000);
            Page.CustomerTabPage.CustomerTabGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
            ConfirmDialog confirmDialog =
               ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.YES);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            Page.CustomerTabPage.VerifyUpdateCustomerCancelButton("charlotte");
            try
            {
                if (Page.CustomerTabPage.lblConduitPortlet.IsEnabled)
                {
                    Assert.True(true, "screen navigated back to home screen on clicking dirty popup cancel button");
                }
                else
                {
                    Assert.Fail("Pop didn't appear and screen not navigated to home screen on clicking dirty popup cancel button");
                }
            }
            catch(Exception e)
            {
                Assert.Fail("Pop didn't appear and screen not navigated to home screen on clicking dirty popup cancel button"  + e.Message);
            }
        }

        /// <summary>
        ///  TC 19107: Customer Setup page:Verify Delete Customer functionality
        /// </summary>
        [TestCategory(TestType.regression, "TC05_DeleteCustomer")]
        [TestCategory(TestType.functional, "TC05_DeleteCustomer")]
        [Test]
        public void TC05_DeleteCustomer()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.CustomerTab.Click();
            Thread.Sleep(5000);
            string strID = Page.CustomerTabPage.CustomerTabGrid.SelectedRows("camarillo")[0].GetColumnValues()[1].ToString();
            Page.CustomerTabPage.ClickonOkPreferencesButton("camarillo");           
            Assert.True(Page.CustomerTabPage.VerifySuccessMsg.BaseElement.InnerText.Contains("Customer Deleted Successfully"), "Success Message not matched");
            Assert.True(Page.CustomerTabPage.CustomerTabGrid.GetRow("camarillo") == null, "Failed to delete the customer record");

            string strCommand = "Select * from [TCD].[PlantCustomer] Where Is_Deleted = '1'";
            DataRow[] foundRows = DBValidation.GetData(strCommand).Tables[0].Select("CustomerId = " + strID);
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, strID + " Customer deleted successfully in DB");
            }
            else
            {
                Assert.Fail(strID + " record not deleted in DB");
            }
        }

        private void Precondition()
        {
            if ((short?)DBValidation.DataRows("select RegionId from [TCD].[plant] where EcolabAccountNumber = 1")[0].ItemArray[0] == 1)
            {
                DBValidation.UpdateData("update [TCD].[Plant] set RegionId = 2 where EcolabAccountNumber = 1");
            }
        }

    }
}
