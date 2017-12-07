using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using Ecolab.Pages.CommonControls;
using Ecolab.TelerikPlugin;
using NUnit.Core;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupGeneralTests : TestBase
    {
        private static string[] generalTabSaveDetails = { "English US", "Britain (Pound)", "Metrics Default", "Disabled", "2", "D:\\test", "Images\\Telerik.png"};
        private static string[] generalTabPreferedLanguage = { "nynorsk", "Britain (Pound)", "Metrics Default", "Disabled", "2", "D:\\test", "Images\\Telerik.png" };
        private static string[] generalTabCancelDetails = { "nynorsk", "Britain (Pound)", "Metrics Default", "Disabled", "2", "D:\\test", "Images\\Kendo.png" };
        private static string[] generalTabOkDetails = { "nynorsk", "Britain (Pound)", "Metrics Default", "Disabled", "2", "D:\\test", "Images\\Telerik.png" };

        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
        }

        [TestCategory(TestType.bvt, "TC01_VerifyGeneralTabSaveFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyGeneralTabSaveFunctionality")]
        [Test, Description("TC 18691 : Plant Setup -&gt; Plant Details Page:Verify SAVE button functionality")]
        public void TC01_VerifyGeneralTabSaveFunctionality()
        {
            ArrayList expectedlist = new ArrayList();
            for (int i = 0; i < generalTabSaveDetails.Length - 1; i++)
            {
                expectedlist.Add(generalTabSaveDetails[i]);
            }
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            //Set the values,save and then return back the saved values
            ArrayList actualList = Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            Thread.Sleep(2000);
            Assert.AreEqual(expectedlist, actualList, "Failed to save the preferences!");
            Thread.Sleep(2000);
            Assert.IsTrue(Page.GeneralTabPage.IsPreferencesSaved(), "Failed to save the preferences!");
        }

        [TestCategory(TestType.bvt, "TC02_VerifyGeneralTabPreferredLanguage")]
        [TestCategory(TestType.regression, "TC02_VerifyGeneralTabPreferredLanguage")]
        [TestCategory(TestType.functional, "TC02_VerifyGeneralTabPreferredLanguage")]
        [Test, Description("Test case 19306: RG:8503: Plant Setup -&gt; Plant Details Page:Verify preferred Language field")]
        public void TC02_VerifyGeneralTabPreferredLanguage()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabPreferedLanguage);
            //Verify that the language preferences are saved
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            try
            {
                Assert.IsTrue(Page.GeneralTabPage.IsLanguageSet(), "Language is not set!");
            }
            catch
            {
                Assert.IsFalse(true, "Preference language is not set!");
            }
            finally
            {
                //Reset back the preferences
                Page.GeneralTabPage.ResetUserPreferences(generalTabPreferedLanguage);
            }
            ArrayList expectedList = GelColumnValues("Name", "select * from TCD.LanguageMaster");
            ArrayList actualList = Page.GeneralTabPage.GetLanguageOptions();
            ArrayList sortedList = new ArrayList();
            foreach (string listItem in actualList)
            {
                sortedList.Add(listItem);
            }

            //Verify that the selected language is saved in database
            ArrayList exList = GelColumnValues("Languageid","select Languageid from TCD.plant where EcoLabAccountNumber = 1");
            string languageid = exList[0].ToString();
            ArrayList languageNames = GelColumnValues("Name","select name from TCD.LanguageMaster where languageid ='" + languageid + "'");
            string expectedLanguage = languageNames[0].ToString();
            Assert.AreEqual(generalTabPreferedLanguage[0], expectedLanguage, "Failed to update the currency in Database!");
            foreach (string actItem in actualList)
            {
                if (!expectedList.Contains("nynorsk"))
                {
                    string s1 = actItem;
                    Assert.IsFalse(true, "Failed!.Either the sorting of Language in not as expected or The options does not match with that option from database!");
                }
            }
            Page.GeneralTabPage.ResetUserPreferences(generalTabPreferedLanguage);
            //Check if the list items are sorted...Sort list and comapre with original list
            sortedList.Sort();
            Assert.AreEqual(actualList, sortedList, "Failed!Preference Language items are not in sorting order!");
        }

        [TestCategory(TestType.bvt, "TC03_VerifyGenTabCancelButtonFunctionality")]
        [TestCategory(TestType.regression, "TC03_VerifyGenTabCancelButtonFunctionality")]
        [Test, Description("Test case 18784: Plant Setup -&gt; Plant Details Page:Verify Cancel button functionality")]
        public void TC03_VerifyGenTabCancelButtonFunctionality()
        {
            ArrayList expectedlist = new ArrayList();
            for (int i = 3; i < generalTabCancelDetails.Length - 1; i++)
            {
                expectedlist.Add(generalTabCancelDetails[i]);
            }
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.GeneralTabPage.LanguagePreferred.Focus();
            Page.GeneralTabPage.LanguagePreferred.MouseClick();
            Page.GeneralTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.GeneralTabPage.btnSave.Click();
            //Click on Confirmation Cancel button
            Page.GeneralTabPage.ClickonCancelPreferencesButton(generalTabCancelDetails);
            Page.GeneralTabPage.clickOnSaveNoButton.Click();
            Page.GeneralTabPage.HomeTab.Click();
            Thread.Sleep(1000);
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            ArrayList actualList = Page.GeneralTabPage.GetSelectedValues();
            Assert.AreNotEqual(expectedlist[0], actualList[0], "User Preference Cancel Functionality failed!");
        }

        [TestCategory(TestType.bvt, "TC04_VerifyGenTabOkButtonFunctionality")]
        [TestCategory(TestType.regression, "TC04_VerifyGenTabOkButtonFunctionality")]
        [Test, Description("Test case 18784: Plant Setup -&gt; Plant Details Page:Verify Ok button functionality")]
        public void TC04_VerifyGenTabOkButtonFunctionality()
        {
            ArrayList expectedlist = new ArrayList();
            for (int i = 3; i < generalTabOkDetails.Length - 1; i++)
            {
                expectedlist.Add(generalTabOkDetails[i]);
            }
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.GeneralTabPage.LanguagePreferred.Focus();
            Page.GeneralTabPage.LanguagePreferred.MouseClick();
            Page.GeneralTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.GeneralTabPage.btnSave.Click();
            //Click on Confirmation Cancel button
            Page.GeneralTabPage.ClickonOkPreferencesButton(generalTabOkDetails);
            Page.GeneralTabPage.HomeTab.Click();
            Thread.Sleep(1000);
            //Go back to plantsetup>> General tab
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            ArrayList actualList = Page.GeneralTabPage.GetSelectedValues();
            Assert.AreEqual(expectedlist[0], actualList[0], "User Preference Cancel Functionality failed!");
        }

        [TestCategory(TestType.bvt, "TC05_VerifyPlantSetupPageNavigation")]
        [TestCategory(TestType.regression, "TC05_VerifyPlantSetupPageNavigation")]
        [Test, Description("Test case 18815: RG:8503: Plant Setup -&gt; Plant Details Page:VerifyNavigation")]
        public void TC05_VerifyPlantSetupPageNavigation()
        {
            ArrayList expectedlist = new ArrayList();
            for (int i = 0; i < generalTabCancelDetails.Length - 1; i++)
            {
                expectedlist.Add(generalTabCancelDetails[i]);
                if (i == 0)
                {
                    expectedlist.Remove(generalTabCancelDetails[0]);
                    expectedlist.Add("English US");
                }
            }
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            //Set user preferences
            Page.GeneralTabPage.LanguagePreferred.Focus();
            Page.GeneralTabPage.LanguagePreferred.MouseClick();
            Page.GeneralTabPage.LanguagePreferred.SelectByText("Deutsch", true);
            //Click on any tab to check the data is saved or not on navigation.
            Page.PlantSetupPage.ContactsTab.Click();
            Thread.Sleep(3000);
            Page.GeneralTabPage.DialogPopupClickSaveData();
            ArrayList actualList = Page.GeneralTabPage.GetSelectedValues();
            Assert.AreNotEqual(expectedlist[0], actualList[0], "User Preference Save Functionality failed!");
            //Revert back the selected language
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.GeneralTabPage.LanguagePreferred.Focus();
            Page.GeneralTabPage.LanguagePreferred.MouseClick();
            Page.GeneralTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.GeneralTabPage.btnSave.Click();
        }

        [TestCategory(TestType.bvt, "TC06_VerifyGeneralTabPreferredCurrency")]
        [TestCategory(TestType.regression, "TC06_VerifyGeneralTabPreferredCurrency")]
        [TestCategory(TestType.functional, "TC06_VerifyGeneralTabPreferredCurrency")]
        [Test, Description("Test case 19281: RG:8503: Plant Setup -&gt; Plant Details Page:Verify preferred currency field")]
        public void TC06_VerifyGeneralTabPreferredCurrency()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            ArrayList expectedList = GelColumnValues("CurrencyName", "select CurrencyName from TCD.CurrencyMaster order by CurrencyName ASC");
            Assert.AreEqual(expectedList, Page.GeneralTabPage.GetCurrencyoptions(),
                "Failed!.Either the sorting of Currency in not as expected or The options does not match with that option from database!");
            ArrayList exList = GelColumnValues("currencyCode", "select * from TCD.plant where EcoLabAccountNumber=1 and currencyCode"+
                                    "= (select currencyCode from TCD.CurrencyMaster WHERE CurrencyName = '" + generalTabSaveDetails[1] + "')");
            string currencyCode = exList[0].ToString();
            ArrayList currencyNames = GelColumnValues("CurrencyName", "select CurrencyName from TCD.CurrencyMaster where currencyCode = '" + currencyCode + "'");
            string expectedCurrency = currencyNames[0].ToString();
            Assert.AreEqual(generalTabSaveDetails[1], expectedCurrency, "Failed to update the currency in Database!");
        }

        [TestCategory(TestType.bvt, "TC07_VerifyGeneralTabExportPath")]
        [TestCategory(TestType.regression, "TC07_VerifyGeneralTabExportPath")]
        [Test, Description("Test case 19382: RG:8503: Plant Setup -&gt; Plant Details Page:Verify Database Export Path")]
        public void TC07_VerifyGeneralTabExportPath()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            ArrayList expectedList = GelColumnValues("Exportpath", "select Exportpath from TCD.plant where ecolabaccountnumber=1");
            String actualPath = expectedList[0].ToString();
            Assert.AreEqual(generalTabSaveDetails[5], actualPath);
        }

        [TestCategory(TestType.functional, "TC08_VerifyDataArchive")]
        [Test, Description("Test case TC08_VerifyDataArchive.")]
        public void TC08_VerifyDataArchive()
        {
            //Verify the default value is selected
            ArrayList getOptions = Page.GeneralTabPage.GetSelectedValues();
            string actualOption = getOptions[4].ToString();
            Assert.AreEqual(generalTabSaveDetails[4], actualOption);
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            ArrayList expectedList = GelColumnValues("DataLiveTime", "select DataLiveTime from TCD.plant where ecolabaccountnumber=1");
            String actualTime = expectedList[0].ToString();
            Assert.AreEqual(generalTabSaveDetails[4], actualTime);
        }
       
        [TestCategory(TestType.functional, "TC09_VerifyFlatFee")]
        [Test, Description("Test case 19413: Verify Flat fee")]
        public void TC09_VerifyFlatFee()
        {
            //Verify the default value is selected
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            ArrayList expectedFeeList = GelColumnValues("Rate", "select Rate from TCD.plant where ecolabaccountnumber=1");
            string actualFeeList = expectedFeeList[0].ToString();
            string status = "";
            if (actualFeeList == "0")
            {
                status = "Disabled";
            }
            else if (actualFeeList == "1")
            {
                status = "Enabled";
            }
            Assert.AreEqual(generalTabSaveDetails[3], status);
        }

        [TestCategory(TestType.functional, "TC10_VerifyUnitId")]
        [Test, Description("Test case 19729: Verify UOM field")]
        public void TC10_VerifyUnitId()
        {
            //Verify the default value is selected
            Page.GeneralTabPage.SetAndSaveUserPreferenceDetails(generalTabSaveDetails);
            ArrayList expectedUnit = GelColumnValues("UnitSystem", "select UnitSystem from TCD.dimensionalunitsystems where unitsystemid=2");
            string actUnit = expectedUnit[0].ToString();
            Assert.AreEqual(generalTabSaveDetails[2], actUnit);
        }

        private ArrayList GelColumnValues(string columnName, string strCommand)
        {
            DataView dv = new DataView(DBValidation.GetData(strCommand).Tables[0]);
            DataTable dt = null;
            try
            {
                dt = new DataTable();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    dt = dv.ToTable(true, columnName);
                }
                catch (Exception e)
                {
                    throw new TCDFrameworkException(e);
                }
            }
            ArrayList rowList = null;
            try
            {
                rowList = new ArrayList();
            }
            catch (Exception e)
            {
                throw new TCDFrameworkException(e);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowList.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            return rowList;
        }

        private void GetInsertedValuefromDatabase(string parameter)
        {
            DataView dv = new DataView(DBValidation.GetData(parameter).Tables[0]);
            DataTable dt = new DataTable();

            dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                dt = dv.ToTable(true, "CurrencyName");
            }
            ArrayList rowList = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowList.Add(dt.Rows[i].ItemArray[0].ToString());
            }
        }
    }
}
