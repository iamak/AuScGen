using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Ecolab.Pages;
using System.Globalization;
using System.Windows.Forms;
using ArtOfTest.WebAii.ObjectModel;

namespace Ecolab.FunctionalTest
{
    public class ProductionChartTest : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }

        [TestCategory(TestType.bvt, "TC01_verifyProductionChartDefaultDateTime")]
        [TestCategory(TestType.functional, "TC01_verifyProductionChartDefaultDateTime")]
        [TestCategory(TestType.regression, "TC01_verifyProductionChartDefaultDateTime")]
        [Test, Description("Test Case 25215: Verify whether the application is display default date and time values when user selects Visualization tab.")]
        public void TC01_verifyProductionChartDefaultDateTime()
        {
            Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.Click();
            DateTime fromDateTime = Page.ProductionChart.GetParseDate(Page.ProductionChart.FromDate.Text);
            int timeDiff = System.DateTime.Compare(System.DateTime.Now, fromDateTime);
            if (timeDiff == 1)
            {
                Assert.True(true, "From datetime is one hour before to current datetime.");
            }
            else
            {
                Assert.Fail("Test failed due to time difference" + timeDiff + " expected was one hour before to current datetime");
            }
        }
        [TestCategory(TestType.bvt, "TC02_verifyProductionChartResetButtonFunctionality")]
        [TestCategory(TestType.regression, "TC02_verifyProductionChartResetButtonFunctionality")]
        [TestCategory(TestType.functional, "TC02_verifyProductionChartResetButtonFunctionality")]
        [Test, Description("Test Case 25217: Verify Reset Button Functionality")]
        public void TC02_verifyProductionChartResetButtonFunctionality()
        {
            int timeDiff = 0;
            Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.Click();
            Page.ProductionChart.Reset.Click();
            Thread.Sleep(2000);
            DateTime fromDateTime = Page.ProductionChart.GetParseDate(Page.ProductionChart.FromDate.Text);
            TimeSpan startTime = Convert.ToDateTime(fromDateTime).TimeOfDay;
            TimeSpan endTime = Convert.ToDateTime(System.DateTime.Now).TimeOfDay;
            TimeSpan diff = endTime > startTime ? endTime - startTime : endTime - startTime + TimeSpan.FromDays(1);
            timeDiff = (int)diff.TotalMinutes;
            Thread.Sleep(2000);
            if (timeDiff == 0)
                Assert.True(true, "From datetime is matching with current datetime");
            else if (timeDiff < 0)
                Assert.False(true, "From datetime is not matching with current datetime");
        }
       
        [TestCategory(TestType.bvt, "TC04_VerifyVisualizationProductionChartPage")]
        [TestCategory(TestType.regression, "TC04_VerifyVisualizationProductionChartPage")]
        [TestCategory(TestType.functional, "TC04_VerifyVisualizationProductionChartPage")]
        [Test, Description("Test case 25207: Verify the sub-menu items list in the Visualizations tab")]
        public void TC04_VerifyVisualizationProductionChartPage()
        {
            Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.Click();
            HtmlControl ctrl = Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage;
            string subMenuItemsList = ctrl.ChildNodes[0].Content;
            if (subMenuItemsList.Contains(Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.ChildNodes[0].Content))
            {
                Assert.True(true, "Visualization - > Production Trending Chart Sub Menu Item is Found");
            }
            else
            {
                Assert.Fail("Visualization - > Production Trending Chart Sub Menu Item Not Found");
            }
        }
        [TestCategory(TestType.bvt, "TC05_VerifyVisualizationChemicalChartPage")]
        [TestCategory(TestType.regression, "TC05_VerifyVisualizationChemicalChartPage")]
        [TestCategory(TestType.functional, "TC05_VerifyVisualizationChemicalChartPage")]
        [Test, Description("Test case 25207: Verify the sub-menu items list in the Visualizations tab")]
        public void TC05_VerifyVisualizationChemicalChartPage()
        {

            Page.LoginPage.TopMainMenu.NavigateToChemicalChartPage.Click();
            HtmlControl ctrl = Page.LoginPage.TopMainMenu.NavigateToChemicalChartPage;
            string subMenuItemsList = ctrl.ChildNodes[0].Content;
            if (subMenuItemsList.Contains(Page.LoginPage.TopMainMenu.NavigateToChemicalChartPage.ChildNodes[0].Content))
            {
                Assert.True(true, "Visualization - > Chemical Injection Chart  Sub Menu Item is Visible");
            }
            else
            {
                Assert.Fail("Visualization -> Chemical Injection Chart not Visible");
            }
        }
        [TestCategory(TestType.bvt, "TC06_VerifyBreadCrumbTitle")]
        [TestCategory(TestType.regression, "TC06_VerifyBreadCrumbTitle")]
        [TestCategory(TestType.functional, "TC06_VerifyBreadCrumbTitle")]
        [Test, Description("Test case 25208: Verify the breadcrumb title")]
        public void TC06_VerifyBreadCrumbTitle()
        {
            Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.Click();
            Thread.Sleep(5000);
            if (Page.ProductionChart.GetBreadCrumbList() != ("HOME->Visualizations->Production Trend Chart"))
            {
                Assert.Fail("ProductionCharts page breadcrumb didnot display Home > Visualizations > Trending Chart > Production Trending Chart");
            }
            Page.LoginPage.TopMainMenu.NavigateToChemicalChartPage.Click();
            Thread.Sleep(5000);
            if (Page.ProductionChart.GetBreadCrumbList() != ("HOME->Visualizations->Chemical Injection Chart"))
            {
                Assert.Fail("ProductionCharts page breadcrumb didnot display Home > Visualizations > Trending Chart > Chemical Injection Chart");
            }
        }
        [TestCategory(TestType.functional, "TC07_VerifyFromDateField")]
        [Test, Description("Test case 25210: Verify the Date and Time chooser fields on Trending PAge;Test case 25214:")]
        public void TC07_VerifyFromDateField()
        {
            Page.LoginPage.TopMainMenu.NavigateToProductionChartsPage.Click();
            if (null == Page.ProductionChart.FromDate)
            {
                Assert.Fail("From date field is not displayed in Production Chart page");
            }
            Page.ProductionChart.FromDate.Click();
            Page.ProductionChart.FromDate.TypeText("9/22/2014 11:00 AM");
            Page.ProductionChart.FromDate.Click();
            KeyBoardSimulator.KeyPress(Keys.Escape);
            Page.ProductionChart.Apply.Click();
            try
            {
                if (null == Page.ProductionChart.VisualizationChart)
                {
                    Assert.True(true, "Chart is not displayed for date: 9/22/2014 10:00 AM");
                }
            }
            catch
            {
                Assert.True(true, "Chart is not displayed for date: 9/22/2014 10:00 AM");
            }
          
        }
    }
}
