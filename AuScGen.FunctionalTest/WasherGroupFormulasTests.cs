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
    public class WasherGroupFormulasTests : TestBase
    {

        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
            //Page.WasherGroupFormulasPage.AddWasherGroup.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.WasherGroupsTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
            //Page.WasherGroupFormulasPage.AddingWasherGroup("4345", "ABCD");
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
        }

        [TestCategory(TestType.functional, "TC01_VerifyDefaultValues_WasherGroupFormaulas")]
        [TestCategory(TestType.regression, "TC01_VerifyDefaultValues_WasherGroupFormaulas")]
        [Test, Description("Test Case 32967:RG Verify default values in Washer Group Formulas Tab")]
        public void TC01_VerifyDefaultValues_WasherGroupFormaulas()
        {
            Page.WasherGroupFormulasPage.AddFormula.Click();
            Thread.Sleep(2000);
            if(Page.WasherGroupFormulasPage.Number.Text != "0")
            {
                Assert.Fail("Default value of Number is not 0, displayed value is " + (Page.WasherGroupFormulasPage.Number.Text));
            }
            if (Page.WasherGroupFormulasPage.NominalLoad.Text != "0")
            {
                Assert.Fail("Default value of NominalLoad is not 0, displayed value is " + (Page.WasherGroupFormulasPage.NominalLoad.Text));
            }
            if (Page.WasherGroupFormulasPage.LoadsPerMonth.Text != "0")
            {
                Assert.Fail("Default value of LoadsPerMonth is not 0, displayed value is " + (Page.WasherGroupFormulasPage.LoadsPerMonth.Text));
            }
            if (Page.WasherGroupFormulasPage.ExtraTime.Text != "0")
            {
                Assert.Fail("Default value of ExtraTime is not 0, displayed value is " + (Page.WasherGroupFormulasPage.ExtraTime.Text));
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
        }

        [TestCategory(TestType.functional, "TC02_VerifyAddingFormula")]
        [TestCategory(TestType.regression, "TC02_VerifyAddingFormula")]
        [Test, Description("Test Case 32968:RG Verify Save functionality in Washer Group Formulas Tab")]
        public void TC02_VerifyAddingFormula()
        {
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.AddFormula.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.AddingFormula("123", "25", "0", "0");
            Thread.Sleep(3000);
            if (null != Page.WasherGroupFormulasPage.divMessageCreate)
            {
                if (!Page.WasherGroupFormulasPage.divMessageCreate.BaseElement.InnerText
                    .Equals(@"Formula created successfully"))
                {
                    Page.WasherGroupFormulasPage.FormulaTab.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.WasherGroupFormulasPage.FormulaTab.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Page.WasherGroupFormulasPage.AddingFormula("123", "25", "0", "0");
            Thread.Sleep(3000);
            if (null != Page.WasherGroupFormulasPage.divMessageCreate)
            {
                if (!Page.WasherGroupFormulasPage.divMessageCreate.BaseElement.InnerText
                    .Equals(@"Formula Number already exists."))
                {
                    Page.WasherGroupFormulasPage.FormulaTab.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.WasherGroupFormulasPage.FormulaTab.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
        }

        [TestCategory(TestType.functional, "TC03_VerifyUpdatingFormula")]
        [TestCategory(TestType.regression, "TC03_VerifyUpdatingFormula")]
        [Test, Description("Test Case 32971:RG Verify Update functionality in Washer Group Formulas Tab")]
        public void TC03_VerifyUpdatingFormula()
        {
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulasTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.UpdatingFormula("26","0","0");
            if (null != Page.WasherGroupFormulasPage.divMessage)
            {
                if (!Page.WasherGroupFormulasPage.divMessageCreate.BaseElement.InnerText
                    .Equals(@"Formula updated Successfully"))
                {
                    Page.WasherGroupFormulasPage.FormulaTab.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.WasherGroupFormulasPage.FormulaTab.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
        }

        [TestCategory(TestType.functional, "TC04_InLineEditingFormula")]
        [TestCategory(TestType.regression, "TC04_InLineEditingFormula")]
        [Test, Description("Test Case 32970:RG Verify Inline Edit functionality in Washer Group Formulas Tab")]
        public void TC04_InLineEditingFormula()
        {
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulasTableGrid.Rows[1].GetButtonControls()[2].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.InLineEditingFormula("27", "0", "0");
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulasTableGrid.Rows[1].GetButtonControls()[0].DeskTopMouseClick();
            Thread.Sleep(2000);
            if (null != Page.WasherGroupFormulasPage.divMessage)
            {
                if (!Page.WasherGroupFormulasPage.divMessage.BaseElement.InnerText
                    .Equals(@"Formula updated Successfully"))
                {
                    Page.WasherGroupFormulasPage.FormulaTab.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.WasherGroupFormulasPage.FormulaTab.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
        }

        [TestCategory(TestType.functional, "TC05_DeleteFormula")]
        [TestCategory(TestType.regression, "TC05_DeleteFormula")]
        [Test, Description("Test Case 32972:RG Verify Delete functionality in Washer Group Formulas Tab")]
        public void TC05_DeleteFormula()
        {
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulasTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.CancelButton.Click();
            Page.WasherGroupFormulasPage.FormulasTableGrid.Rows[1].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.OkButton.Click();
            if (null != Page.WasherGroupFormulasPage.divMessage)
            {
                if (!Page.WasherGroupFormulasPage.divMessage.BaseElement.InnerText
                    .Equals(@"Formula Deleted Successfully"))
                {
                    Page.WasherGroupFormulasPage.FormulaTab.Click();
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Page.WasherGroupFormulasPage.FormulaTab.Click();
                Assert.Fail("Error message is not displayed");
            }
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.BacktoWasherGroups.Click();
        }

        [TestCategory(TestType.functional, "TC06_VerifyFormulasLocalization")]
        [TestCategory(TestType.regression, "TC06_VerifyFormulasLocalization")]
        [Test, Description("Test Case 34216:RG Verify the Localization criteria")]
        public void TC06_VerifyFormulasLocalization()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
            //Page.WasherGroupFormulasPage.AddWasherGroup.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.WasherGroupsTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            if (Page.WasherGroupFormulasPage.FormulaTab.BaseElement.InnerText != "formules")
            {
                PostLocalizationStorageTanks();
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - formules", Page.WasherGroupFormulasPage.FormulaTab.BaseElement.InnerText));
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
            Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();
            //Page.WasherGroupFormulasPage.AddWasherGroup.Click();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.WasherGroupsTableGrid.Rows[1].GetButtonControls()[4].DeskTopMouseClick();
            Thread.Sleep(2000);
            Page.WasherGroupFormulasPage.FormulaTab.Click();
            Thread.Sleep(2000);
            if (Page.WasherGroupFormulasPage.FormulaTab.BaseElement.InnerText != "Formulas")
            {
                Assert.Fail(string.Format("Incorrert Tab name displayed in  - {0} when localization changed to Deutsch, Expected - formules", Page.WasherGroupFormulasPage.FormulaTab.BaseElement.InnerText));
            }
        }
    }
}
