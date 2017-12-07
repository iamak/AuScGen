using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupLabourCostTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }
        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }
        string[] setLaborCost = {"22", "15"};
        [TestCategory(TestType.functional, "TC01_VerifyLaborCostSaveFunctionality")]
        [TestCategory(TestType.regression, "TC01_VerifyLaborCostSaveFunctionality")]
        [Test, Description("Test case 38775: RG: Verify SAVE functionality;Test case 38800: RG: Verify Audit Functionality")]
        public void TC01_VerifyLaborCostSaveFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.LabourCostTabPage.LaborCostTab.Click();
            Thread.Sleep(2000);
            Page.LabourCostTabPage.AddLaborCost(setLaborCost);
            Thread.Sleep(2000);
            if (null != Page.LabourCostTabPage.SuccessMessage)
            {
                if (!Page.LabourCostTabPage.SuccessMessage.BaseElement.InnerText
                    .Equals(@"Saved Successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed,Expected: Saved Successfully"
                                    + " but Actual:" + Page.LabourCostTabPage.SuccessMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Thread.Sleep(2000);
            string strCommand = "select * from TCD.LaborCostHistory where cost= " + setLaborCost[0];
            DataSet ds = DBValidation.GetData(strCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Labor Cost not updated in Audit LaborCostHistory table.Expected Labor Cost -" + setLaborCost[0] + " which doesn't exist in DB");
            }
            else
            {
                Assert.Fail("Expected  Labor Cost - " + setLaborCost[0] + " which doesn't exist in DB");
            }
        }

        [TestCategory(TestType.functional, "TC02_VerifyLaborCostUOMFunctionality")]
        [TestCategory(TestType.regression, "TC02_VerifyLaborCostUOMFunctionality")]
        [Test, Description("Test case 38777: RG: Verify whether UOM's are displayed properly")]
        public void TC02_VerifyLaborCostUOMFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.LabourCostTabPage.LaborCostTab.Click();
            Thread.Sleep(2000);
            for (int i = 1; i <= Page.LabourCostTabPage.LabourCostGridTable.Rows.Count - 1; i++)
            {
                if (Page.LabourCostTabPage.LabourCostGridTable.Rows[i].GetColumnValues()[1].ToString() != "$/hr")
                {
                    Assert.Fail("UOM verification Failed - Expected $/hr but actual is" + Page.LabourCostTabPage.LabourCostGridTable.Rows[i].GetColumnValues()[1].ToString());
                }
            }
        }

        [TestCategory(TestType.regression, "TC03_VerifyLaborCostInShiftPage")]
        [TestCategory(TestType.functional, "TC03_VerifyLaborCostInShiftPage")]
        [Test, Description("Test case 38778: RG: Verify whether saved Laborcost details are displayed properly in shift page,Test case 38786:RG")]
        public void TC03_VerifyLaborCostInShiftPage()
        {
            //Pre-Condition to create a shift to check the Labor cost value which is created in the previous Pre-Condition
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.ShiftTabPage.ShiftTab.Click();
            Thread.Sleep(2000);
            Page.ShiftTabPage.DeleteAllShifts("Monday");
            Thread.Sleep(2000);
            Page.ShiftTabPage.AddShift.Click();
            Thread.Sleep(2000);
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Thread.Sleep(2000);
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Verifying Labour Cost");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("7373");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("05:00 PM");
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("10:30 AM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("11:30 AM");
            Thread.Sleep(2000);
            Page.ShiftTabPage.SaveShiftButton.Focus();
            Page.ShiftTabPage.SaveShiftButton.DeskTopMouseClick();
            Thread.Sleep(2000);
            try
            {
                if (null != Page.ShiftTabPage.ShiftAddMessage)
                {
                    string message = Page.ShiftTabPage.ShiftAddMessage.BaseElement.InnerText;
                    if (!message.Equals(@"Shift created successfully"))
                    {
                        Assert.Fail("Incorrect message is displayed while adding shift , Expected: Shift created successfully ,Actual:{0}", message);
                    }
                }
                else
                {
                    Assert.Fail("Shift added message is not displayed");
                }
            }
            catch
            {
                Assert.Fail("Failed to add the Shift details.Expected - Shifts details to be added.");
            }
            Thread.Sleep(3000);
            Page.ShiftTabPage.AddLabour("Monday", "Verifying Labour Cost").Click();
            string labourType = Page.ShiftTabPage.LabourType.Options[1].Text;
            Page.ShiftTabPage.LabourType.SelectByText(labourType, true);
            string labourLocation = Page.ShiftTabPage.LabourLocation.Options[1].Text;
            Page.ShiftTabPage.LabourLocation.SelectByText(labourLocation, true);
            Page.ShiftTabPage.ManHours.TypeText("12");
            HtmlInputControl avgPrice = Page.ShiftTabPage.AvgPricePerHr;
            if (avgPrice.Value != setLaborCost[0])
            {
                Assert.Fail("Saved Labour cost not displayed in the shifts page.Expected Avg Price " + setLaborCost[0] + "for  Labor type = Normal, in shift page");
            }
            Page.ShiftTabPage.AddLabourSaveButton.TypeEnterKey();
        }

        [TestCategory(TestType.regression, "TC04_VerifyLaborTypeValueInManualInputLaborScreen")]
        [Test, Description("Test case 39064: RG: Verify whether Saved Labor type value details are reflected in ManualInput Labor screen")]
        public void TC04_VerifyLaborTypeValueInManualInputLaborScreen()
        {
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputLabourTabPage.LabourTab.Click();
            Page.ManualInputLabourTabPage.DdlLocation.SelectByIndex(1);
            Page.ManualInputLabourTabPage.DdlLabourTypes.SelectByIndex(1);
            Thread.Sleep(1000);
            if (Page.ManualInputLabourTabPage.TxtLabourCost.Text != setLaborCost[0])
            {
                Assert.Fail("Labour type value not reflected.Expected-Labour cost to displayed - " + setLaborCost[0]);
            }
            else
            {
                Assert.Pass("Labour cost reflected automatically in Manual Input Labor Screen");
            }
        }

        [TestCategory(TestType.functional, "TC05_VerifyLaborCostCancelFunctionality")]
        [TestCategory(TestType.regression, "TC05_VerifyLaborCostCancelFunctionality")]
        [Test, Description("Test case 40154: RG: Verify Cancel button functionality")]
        public void TC05_VerifyLaborCostCancelFunctionality()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.LabourCostTabPage.LaborCostTab.Click();
            Thread.Sleep(2000);
            Page.LabourCostTabPage.CancelLaborCost(setLaborCost);
            Thread.Sleep(3000);
            if(null != Page.LabourCostTabPage.BtnNo)
            {
                Page.LabourCostTabPage.BtnNo.Focus();
                Page.LabourCostTabPage.BtnNo.DeskTopMouseClick();
            }
            else
            {
                Assert.Fail("Confirmation message popup has not displayed on Cancel Functionality.Expected - Confirmation message PopUp.");
            }
        }

        [TestCategory(TestType.functional, "TC05_VerifyLaboursCostLocalization")]
        [TestCategory(TestType.regression, "TC05_VerifyLaboursCostLocalization")]
        [Test, Description("Test case 38799: RG: Verify localisation")]
        public void TC06_VerifyLaboursCostLocalization()
        {
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.LabourCostTabPage.LanguagePreferred.Focus();
            Page.LabourCostTabPage.LanguagePreferred.SelectByText("Deutsch", true);
            Page.LabourCostTabPage.LanguagePreferred.ScrollToVisible();
            Page.LabourCostTabPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            Thread.Sleep(1000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            if (Page.LabourCostTabPage.LaborCostTab.ChildNodes[0].Content == "Labor Cost")
            {
                Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected - Deutsh lang, but Actual-"
                    + Page.LabourCostTabPage.LaborCostTab.ChildNodes[0].Content);
            }
        }

        /// <summary>
        /// Post Localization 
        /// </summary>
        [TestCategory(TestType.functional, "TC08_WashersPostLocalization")]
        [TestCategory(TestType.regression, "TC08_WashersPostLocalization")]
        [Test, Description("Test case 34205: RG: Verify the Localization criteria")]
        public void TC07_LaborCostPostLocalization()
        {
            //Post Condition to revert back the localization
            Thread.Sleep(2000);
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.GeneralTab.Click();
            Page.LabourCostTabPage.LanguagePreferred.Focus();
            Page.LabourCostTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.LabourCostTabPage.LanguagePreferred.ScrollToVisible();
            Thread.Sleep(2000);
            Page.LabourCostTabPage.GeneralTabSaveButton.Click();
        }
    }
}
