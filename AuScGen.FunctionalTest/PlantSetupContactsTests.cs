using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
using Ecolab.TelerikPlugin;
using NUnit.Core;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupContactsTests : TestBase
    {
        bool verifyAssert = false;
        private static string[] addContactsData = new string[] { "Mr", "Dirk", "Brabanter", "Administrator", "Dirk@ecolab.com", "123456123", "123456789", "123456789" };
        private static string[] updateContactsData = new string[] { "MS", "Darshini", "Vatsala", "Administrator", "Darsh@ecolab.com", "123456123", "123456123", "123456123" };
        private static string[] cancelContactsData = new string[] { "Mr", "Dirk", "Brabanter", "Administrator", "Dirk@ecolab.com", "789456123", "789456123", "789456123" };

        /// <summary>
        /// Tests the fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }

        [TestCategory(TestType.bvt, "TC01_VerifyAddContactFunctionality")]
        [TestCategory(TestType.functional, "TC01_VerifyAddContactFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyAddContactFunctionality")]
        [Test, Description("Test case 18693: RG 18041 Plant Setup Contacts Page Verify Add Contact Functionality")]
        public void TC01_VerifyAddContactFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Assert.True(Page.ContactsTabPage.AddContactButton.IsVisible(), "AddContacts button not found");
            Page.ContactsTabPage.AddContactButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.ContactsTabPage.AddContactsDetails(addContactsData);
            Thread.Sleep(2000);
            if (null != Page.ContactsTabPage.ErrorMsg)
            {
                if (!Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText.Equals(@"Contact added Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Contact added Successfully" + " but Actual:" + Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Thread.Sleep(2000);
            if (Page.ContactsTabPage.ContactsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ContactsTabPage.ContactsTabGrid.Rows.Count - 1; i++)
                {
                    if (Page.ContactsTabPage.ContactsTabGrid.Rows[i].GetColumnValues()[3].ToString() == addContactsData[5])
                    {
                        verifyAssert = true;
                        Assert.True(true, "Contacts details not found in the existing grid page");
                        string strCommand = "Select * from TCD.PlantContact where ContactEMail='" + addContactsData[4].Trim() + "' and IS_Deleted =" + '0';
                        DataSet ds = DBValidation.GetData(strCommand);
                        if (ds.Tables[0].Rows.Count >= 0)
                        {
                            Assert.True(true, addContactsData[1] + " " + addContactsData[2] + " Contact details added successfully in DB");
                        }
                        else
                        {
                            Assert.Fail(addContactsData[1] + " " + addContactsData[2] +" User contact details not found in DB");
                        }
                        break;
                    }
                }
                if (verifyAssert == false)
                {
                    Assert.Fail("Contacts details not found in the existing grid page");
                }
            }
            else
            {
                Assert.Fail("Contacts details grid is empty.Expected data in contacts grid");
            }

        }
      
        [TestCategory(TestType.bvt, "TC02_VerifyCancelContactFunctionality")]
        [TestCategory(TestType.regression, "TC02_VerifyCancelContactFunctionality")]
        [Test, Description("Test case 20432: RG 18041 Plant Setup Contacts Page Verify Cancel button functionality while adding new contact")]
        public void TC02_VerifyCancelContactFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Page.ContactsTabPage.AddContactButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.ContactsTabPage.CancelAddContactDetails(cancelContactsData);
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            if (Page.ContactsTabPage.ContactsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ContactsTabPage.ContactsTabGrid.Rows.Count - 1; i++)
                {
                    if (Page.ContactsTabPage.ContactsTabGrid.Rows[i].GetColumnValues()[3].ToString() == cancelContactsData[5])
                    {
                        Assert.Fail("Contact added on cancel button click event.");
                    }
                }
            }
            else
            {
                Assert.Fail("Contacts details not found in the existing grid table");
            }
        }

        [TestCategory(TestType.bvt, "TC03_VerifyUpdateContactFunctionality")]
        [TestCategory(TestType.regression, "TC03_VerifyUpdateContactFunctionality")]
        [Test, Description("Test case 20434: RG 18041 Plant Setup Contacts Page Verify Edit Conatct functionality")]
        public void TC03_VerifyUpdateContactFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Thread.Sleep(2000);
            if (Page.ContactsTabPage.ContactsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ContactsTabPage.ContactsTabGrid.Rows.Count-1; i++)
                {
                    if (Page.ContactsTabPage.ContactsTabGrid.Rows[i].GetColumnValues()[3].ToString() == updateContactsData[5])
                    {
                        verifyAssert = true;
                        Page.ContactsTabPage.ContactsTabGrid.SelectedRows(updateContactsData[5].ToLower())[0].GetButtonControls()[2].Click();
                        Page.ContactsTabPage.UpdateContactDetails(updateContactsData);
                        if (null != Page.ContactsTabPage.ErrorMsg)
                        {
                            if (!Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText
                                .Equals(@"Contact updated Successfully"))
                            {
                                Assert.Fail("Incorrect error message is displayed,Expected: Contact updated Successfully"
                                                + " but Actual:" + Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText);
                            }
                        }
                        else
                        {
                            Assert.Fail("Error message is not displayed");
                        }
                        string strCommand = "Select * from TCD.PlantContact where ContactEMail='" + updateContactsData[4].Trim() + "' and IS_Deleted =" + '0';
                        DataSet ds = DBValidation.GetData(strCommand);
                        if (ds.Tables[0].Rows.Count >= 0)
                        {
                            Assert.True(true, updateContactsData[1] + " " + updateContactsData[2] + " Contact details updated successfully in DB");
                        }
                        else
                        {
                            Assert.Fail(updateContactsData[1] + " " + updateContactsData[2] + " User Contact details not updated in DB");
                        }
                        break;
                    }
                }
                if (verifyAssert == false)
                {
                    Assert.Fail("Contacts details not found in the existing grid table");
                }
            }
            else
            {
                Assert.Fail("Contacts details not found in the existing grid table");
            }
        }
      
        [TestCategory(TestType.bvt, "TC04_VerifyInlineEditContactFunctionality")]
        [TestCategory(TestType.regression, "TC04_VerifyInlineEditContactFunctionality")]
        [Test, Description("Test case 25443: RG 18041 Plant Setup Contacts Page Verify Update button while performing inline editing")]
        public void TC04_VerifyInlineEditContactFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Thread.Sleep(2000);
            if (Page.ContactsTabPage.ContactsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ContactsTabPage.ContactsTabGrid.Rows.Count - 1; i++)
                {
                    if (Page.ContactsTabPage.ContactsTabGrid.Rows[i].GetColumnValues()[3].ToString() == addContactsData[5])
                    {
                        verifyAssert = true;
                        EcolabDataGridItems myRow = Page.ContactsTabPage.ContactsTabGrid.SelectedRows(addContactsData[5])[0];
                        myRow.GetButtonControls()[0].Click();
                        Page.ContactsTabPage.EditUpdateInlineContactDetails(addContactsData);
                        myRow.GetButtonControls()[0].Click();
                        if (null != Page.ContactsTabPage.ErrorMsg)
                        {
                            if (!Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText
                                .Equals(@"Contact updated Successfully"))
                            {
                                Assert.Fail("Incorrect error message is displayed,Expected: Contact updated Successfully"
                                                + " but Actual:" + Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText);
                            }
                        }
                        else
                        {
                            Assert.Fail("Error message is not displayed");
                        }
                        break;
                    }
                }
                if (verifyAssert == false)
                {
                    Assert.Fail("Contacts details not found in the existing grid table");
                }
            }
            else
            {
                Assert.Fail("Contacts Grid is Empty");
            }
        }
      
        [TestCategory(TestType.bvt, "TC05_VerifyDeleteContactFunctionality")]
        [TestCategory(TestType.regression, "TC05_VerifyDeleteContactFunctionality")]
        [Test, Description("Test case 20433: RG 18041 Plant Setup Contacts Page Verify Delete Contact functionality")]
        public void TC05_VerifyDeleteContactFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.ContactsTab.Click();
            Thread.Sleep(2000);
            if (Page.ContactsTabPage.ContactsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ContactsTabPage.ContactsTabGrid.Rows.Count - 1; i++)
                {
                    if (Page.ContactsTabPage.ContactsTabGrid.Rows[i].GetColumnValues()[3].ToString() == addContactsData[5])
                    {
                        verifyAssert = true;
                        Page.ContactsTabPage.DeleteContactsDetails(addContactsData[5]);
                        if (null != Page.ContactsTabPage.ErrorMsg)
                        {
                            if (!Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText
                                .Equals(@"Contact deleted Successfully"))
                            {
                                Assert.Fail("Incorrect error message is displayed,Expected: Contact deleted Successfully"
                                                + " but Actual:" + Page.ContactsTabPage.ErrorMsg.BaseElement.InnerText);
                            }
                        }
                        else
                        {
                            Assert.Fail("Error message is not displayed");
                        }
                        Thread.Sleep(2000);
                        EcolabDataGridItems deletedRow = Page.ContactsTabPage.ContactsTabGrid.GetRow(addContactsData[5].Trim());
                        Assert.True(deletedRow == null, "Failed to delete the contact");
                        string strCommand = "Select * from TCD.PlantContact where ContactOfficePhone='" + addContactsData[5].Trim() + "' and IS_Deleted =" + '1';
                        DataSet ds = DBValidation.GetData(strCommand);
                        if (ds.Tables[0].Rows.Count >= 0)
                        {
                            Assert.True(true, addContactsData[1] + " " + addContactsData[2]  +" Contact details updated successfully in DB");
                        }
                        else
                        {
                            Assert.Fail(addContactsData[1] + " " + addContactsData[2] + " User Contact details not updated in DB");
                        }
                        break;
                    }
                }
                if (verifyAssert == false)
                {
                    Assert.Fail("Contacts details not found in the existing grid table");
                }
            }
            else
            {
                Assert.Fail("Contacts details not found in the existing grid table");
            }
        }
    }
}
