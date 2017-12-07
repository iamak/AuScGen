using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.FunctionalTest
{
    public class ManualInputRewashTests : TestBase
    {
        /// <summary>
        /// Tests the fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Thread.Sleep(3000);
            Page.RewashTabPage.RewashTab.Click();
            Thread.Sleep(3000);

        }

        [TestCategory(TestType.functional, "TC01_VerifyNavigationToManualInputPage")]
        [TestCategory(TestType.regression, "TC01_VerifyNavigationToManualInputPage")]
        [Test]
        public void TC01_VerifyNavigationToManualInputPage()
        {
            Page.PlantSetupPage.TopMainMenu.IsNavigateToManualInputPage();
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            if(Page.RewashTabPage.RewashTab.IsEnabled)
            {
                Assert.True(true, "Application navigated to Manual Input Page");
            }
            else
            {
                Assert.Fail("Application didnot navigate to Manual Input Page");
            }
            
        }

        [TestCategory(TestType.functional, "TC02_VerifySubMenuItemsOfManualInputPage")]
        [TestCategory(TestType.regression, "TC02_VerifySubMenuItemsOfManualInputPage")]
        [Test]
        public void TC02_VerifySubMenuItemsOfManualInputPage()
        {
            if (Page.RewashTabPage.UtilityTab.IsEnabled)
            {
                Assert.True(true, "Utility Tab visible in Manual Input Page");
            }
            else
            {
                Assert.Fail("Utility Tab not visible in Manual Input Page");
            }
            if (Page.RewashTabPage.ProductionTab.IsEnabled)
            {
                Assert.True(true, "Production Tab visible in Manual Input Page");
            }
            else
            {
                Assert.Fail("Production Tab not visible in Manual Input Page");
            }
            if (Page.RewashTabPage.RewashTab.IsEnabled)
            {
                Assert.True(true, "Rewash Tab visible in Manual Input Page");
            }
            else
            {
                Assert.Fail("Rewash Tab not visible in Manual Input Page");
            }
            if (Page.RewashTabPage.LabourTab.IsEnabled)
            {
                Assert.True(true, "Labour Tab visible in Manual Input Page");
            }
            else
            {
                Assert.Fail("Labour Tab not visible in Manual Input Page");
            }
        }

        [TestCategory(TestType.functional, "TC03_VerifyNavigationOfSubmenuItemsInManualInputPage")]
        [TestCategory(TestType.regression, "TC03_VerifyNavigationOfSubmenuItemsInManualInputPage")]
        [Test]
        public void TC03_VerifyNavigationOfSubmenuItemsInManualInputPage()
        {
            Page.RewashTabPage.UtilityTab.Click();
            Page.RewashTabPage.ProductionTab.Click();
            if (Page.RewashTabPage.ProductionData.IsEnabled)
            {
                Assert.True(true, "ProductionData Tab visible in Production Tab Page");
            }
            else
            {
                Assert.Fail("ProductionData Tab not visible in Production Tab Page");
            }

            Page.RewashTabPage.RewashTab.Click();
            if (Page.RewashTabPage.WasherGroup.IsEnabled)
            {
                Assert.True(true, "WasherGroup visible in Rewash Tab Page");
            }
            else
            {
                Assert.Fail("WasherGroup not visible in Rewash Tab Page");
            }

            Page.RewashTabPage.LabourTab.Click();
            if (Page.RewashTabPage.Location.IsEnabled)
            {
                Assert.True(true, "Location Tab visible in Labour Tab Page");
            }
            else
            {
                Assert.Fail("Location Tab not visible in Labour Tab Page");
            }
        }

        [TestCategory(TestType.functional, "TC04_VerifyFieldsRewashPage")]
        [TestCategory(TestType.regression, "TC04_VerifyFieldsRewashPage")]
        [Test]
        public void TC04_VerifyFieldsRewashPage()
        {
            Page.RewashTabPage.RewashTab.Click();
            if (Page.RewashTabPage.WasherGroup.IsEnabled)
            {
                Assert.True(true, "WasherGroup drop down visible in Rewash Page");
            }
            else
            {
                Assert.Fail("WasherGroup Tab not visible in Rewash Page");
            }
            if (Page.RewashTabPage.Formula.IsEnabled)
            {
                Assert.True(true, "Formula drop down visible in Rewash Page");
            }
            else
            {
                Assert.Fail("Formula drop down not visible in Rewash Page");
            }
            if (Page.RewashTabPage.RewashReason.IsEnabled)
            {
                Assert.True(true, "RewashReason drop down visible in Rewash Page");
            }
            else
            {
                Assert.Fail("RewashReason drop down not visible in Rewash Page");
            }
        }

        [TestCategory(TestType.functional, "TC05_VerifyValueInWasherGroupField")]
        [TestCategory(TestType.regression, "TC05_VerifyValueInWasherGroupField")]
        [Test]
        public void TC05_VerifyValueInWasherGroupField()
        {
            Page.RewashTabPage.RewashTab.Click();
            string strCommand = "Select [GroupDescription] from [TCD].[GroupType] where GroupMaintype = '" + "WasherGroup" + "' and [Is_Deleted] = '0'";
            DataSet ds = DBValidation.GetData(strCommand);
            List<string> lstDB = new List<string>();
            List<string> lstDrp = new List<string>();
            foreach (DataRow d in ds.Tables[0].Rows)
            {
                lstDB.Add(d["GroupDescription"].ToString());
            }
            ICollection<Element> col = Page.RewashTabPage.WasherGroup.ChildNodes;
            foreach(Element cl in col)
            {
                if (cl.InnerText != "-- Select --")
                lstDrp.Add(cl.InnerText.ToString());
            }
            lstDB.Sort();
            lstDrp.Sort();

            if(lstDB.Count == lstDrp.Count)
            {
                List<string> difference = lstDB.Except(lstDrp).ToList();
            }
            else
            {
                Assert.Fail("Count of DB values and WasherGroupField values didnot match");
            }
        }

        [TestCategory(TestType.functional, "TC06_VerifyValueInFormulasField")]
        [TestCategory(TestType.regression, "TC06_VerifyValueInFormulasField")]
        [Test]
        public void TC06_VerifyValueInFormulasField()
        {
            Page.RewashTabPage.RewashTab.Click();
            string strCommand = "Select [Name] from [TCD].[ProgramMaster] where [Rewash]=1";
            DataSet ds = DBValidation.GetData(strCommand);
            List<string> lstDB = new List<string>();
            List<string> lstDrp = new List<string>();
            foreach (DataRow d in ds.Tables[0].Rows)
            {
                lstDB.Add(d["Name"].ToString());
            }
            ICollection<Element> col = Page.RewashTabPage.Formula.ChildNodes;
            foreach(Element cl in col)
            {
                if(cl.InnerText != "Select")
                lstDrp.Add(cl.InnerText.ToString());
            }
            lstDB.Sort();
            lstDrp.Sort();

            if(lstDB.Count == lstDrp.Count)
            {
                List<string> difference = lstDB.Except(lstDrp).ToList();
            }
            else
            {
                Assert.Fail("Count of DB values and FormulaDropDown values didnot match Expected : " + lstDB.Count + "Actual : " + lstDrp.Count);
            }
        }

        [TestCategory(TestType.functional, "TC07_VerifyValueInRewashReasonField")]
        [TestCategory(TestType.regression, "TC07_VerifyValueInRewashReasonField")]
        [Test]
        public void TC07_VerifyValueInRewashReasonField()
        {
            Page.RewashTabPage.RewashTab.Click();
            bool bWashing = false;
            bool bFinishing = false;
            ICollection<Element> col = Page.RewashTabPage.RewashReason.ChildNodes;
            foreach (Element cl in col)
            {
                if (cl.InnerText == "Washing")
                {
                    bWashing = true;
                }
                if (cl.InnerText == "Finishing")
                {
                    bFinishing = true;
                }
            }
            if (bWashing != true)
            {
                Assert.Fail("Washing value not appearing in RewashReason drop down");
            }
            if(bFinishing != true)
            {
                Assert.Fail("Finishinh value not appearing in RewashReason drop down");
            }
        }

        [TestCategory(TestType.functional, "TC08_VerifyAllowRewash")]
        [TestCategory(TestType.regression, "TC08_VerifyAllowRewash")]
        [Test]
        public void TC08_VerifyAllowRewash()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("English US", true);
            Thread.Sleep(3000);
            if (null != Page.RewashTabPage.GetSwitchNameActive())
            {
                if (Page.RewashTabPage.GetSwitchNameActive() == "Yes")
                {
                    //KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);  
                    Page.RewashTabPage.AllowRewashContainer.DeskTopMouseClick();
                    Thread.Sleep(2000);
                    Page.RewashTabPage.ExportPath.Click();
                }
                Thread.Sleep(2000);
                Page.SensorTabPage.GeneralTabSave.Click();
                Telerik.ActiveBrowser.RefreshDomTree();
                Thread.Sleep(2000);
                Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
                Thread.Sleep(3000);
                try
                {
                    if(Page.RewashTabPage.RewashTab.IsVisible())
                    {
                        Assert.Fail("Washing value not appearing in RewashReason drop down");
                    }
                }
                catch(Exception e)
                {
                    Assert.True(true, "Rewash tab not found when allow rewash switch set to No" + e.Message);
                    Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
                    Thread.Sleep(2000);
                    Page.PlantSetupPage.GeneralTab.Click();
                    Thread.Sleep(2000);
                    KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                    Page.SensorTabPage.Language.Focus();
                    Page.SensorTabPage.Language.SelectByText("Deutsch", true);
                    Page.SensorTabPage.Language.Focus();
                    Page.SensorTabPage.Language.SelectByText("English US", true);
                    Thread.Sleep(2000);
                    //KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                    Page.RewashTabPage.AllowRewashContainer.Focus();
                    Page.RewashTabPage.AllowRewashContainer.DeskTopMouseClick();
                    Thread.Sleep(2000);
                    Page.RewashTabPage.ExportPath.Click();
                    Thread.Sleep(2000);
                    Page.SensorTabPage.GeneralTabSave.Click();
                    Telerik.ActiveBrowser.RefreshDomTree();
                    Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
                    if (Page.RewashTabPage.RewashTab.IsVisible())
                    {
                        Assert.True(true, "Rewash tab found when allow rewash switch set to Yes");
                    }
                }       
            }      
        }

        [TestCategory(TestType.functional, "TC09_VerifyRewashFunctionality")]
        [TestCategory(TestType.regression, "TC09_VerifyRewashFunctionality")]
        [Test]
        public void TC09_VerifyRewashFunctionality()
        {
            Page.RewashTabPage.RewashTab.Click();
            Page.RewashTabPage.CheckFields();
            if (!Page.RewashTabPage.IsLastDateEnabled())
            {
                Assert.True(true, "Last Date field disabled");
            }
            else
            {
                Assert.Fail("Last Date field is not readonly field");
            }

            if (Page.RewashTabPage.IsNewDateEnabled())
            {
                Assert.True(true, "New Date field enabled");
            }
            else
            {
                Assert.Fail("New Date field is readonly field");
            }

            if (Page.RewashTabPage.LastValue.IsEnabled)
            {
                Assert.True(true, "Last Value field enabled");
            }
            else
            {
                Assert.Fail("Last Value field is readonly field");
            }

            if (Page.RewashTabPage.NewValue.IsEnabled)
            {
                Assert.True(true, "New Value field enabled");
            }
            else
            {
                Assert.Fail("New Value field is readonly field");
            }

        }

        [TestCategory(TestType.functional, "TC10_VerifySaveFunctionality")]
        [TestCategory(TestType.regression, "TC10_VerifySaveFunctionality")]
        [Test]
        public void TC10_VerifySaveFunctionality()
        {
            //Page.RewashTabPage.AddingRewash("Tunnel-Washer2", "ecoform1", "Washing", "25", "5");
            Page.RewashTabPage.AddingRewash("Tunnel Washer1", "Washing", "25", "5");
            Thread.Sleep(2000);
             if (null != Page.RewashTabPage.SuccessMsg)
            {
                if (!Page.RewashTabPage.SuccessMsg.BaseElement.InnerText
                    .Equals(@"Saved successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        [TestCategory(TestType.functional, "TC11_UserRolemanagement_Rewash")]
        [TestCategory(TestType.regression, "TC11_UserRolemanagement_Rewash")]
        [Test]
        public void TC11_UserRolemanagement_Rewash()
        {
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestTMAdvance ", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            if(!Page.RewashTabPage.RewashTab.IsEnabled)
            {
                Assert.Fail("Rewsah Tab not found on logging in with user role Level 7");
            }
            Page.PlantSetupPage.TopMainMenu.LogOut();
        }

    }
}
