using ArtOfTest.WebAii.Controls.HtmlControls;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.FunctionalTest
{
    public class ManualInputUtilityTests : TestBase
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
            Precondition();
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputUtilityTabPage.UtilityTab.Click();
            
        }

        ///// <summary>
        ///// Tests the fixture tear down.
        ///// </summary>
        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        /// <summary>
        /// Test case 31940: RG: Verify Whether User able to see Utility meters in Utility page
        /// </summary>
        [TestCategory(TestType.functional, "TC01_VerifyManualEntry")]
        [Test]
        public void TC01_VerifyManualEntry()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "withoutmanualentry");
            data.Add("UtilityType", "Oil");
            data.Add("UtilityLocation", "Washer-Extractor1");
            data.Add("MachineCompartment", "Washer 1");
            //data.Add("Parent", "m");
            data.Add("Calibration", "1");
            data.Add("UOM", "pound_per_hour");
            data.Add("Controller", "MyControl");
            data.Add("MaxRollOverPoint", "120000");

            AddMeter(data,false);

            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputUtilityTabPage.UtilityTab.Click();

            if(Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("withoutmanualentry").Count > 0)
            {
                Assert.Fail("Meter without allow manual entry is displayed in manual entry page");
            }
        }

        /// <summary>
        /// Test case 31945: RG: Verify Utility meters displayed in the grid
        /// Test case 33582: RG: Verify whether user able to delete the current recording from the popup
        /// Test case 33592: RG: Verify whether User able to SAVE the New recording
        /// Test case 33594: RG: Verify whether saved details are displayed in the grid
        /// Test case 33596: RG: Verify whether UOM's are displayed properly
        /// Test case 33597: RG: verify whether deleted current record shows effect on the grid
        /// </summary>
        [TestCategory(TestType.functional, "TC02_AddAndDeleteRecord")]
        [Test]
        public void TC02_AddAndDeleteRecord()
        {
            List<EcolabDataGridItems> rows = Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("allowmanualentry");
            List<HtmlControl> controls = rows.FirstOrDefault().GetButtonControls();
            controls.LastOrDefault().DeskTopMouseClick();
            Page.ManualInputUtilityTabPage.NewUsage.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("12");
            //Page.ManualInputUtilityTabPage.NewValue.DeskTopMouseClick();
            //KeyBoardSimulator.SetNumeric("13");

            if (null != Page.ManualInputUtilityTabPage.UOM)
            {
                string message = Page.ManualInputUtilityTabPage.UOM.BaseElement.InnerText;
                if (!message.Equals(@"pound_per_hour"))
                {
                    Assert.Fail("Incorrect UOM displayed, Expected:{0} , Actual:{1}", "pound_per_hour", message);
                }
            }
            else
            {
                Assert.Fail("UOM is not displayed while editing utility through manual input");
            }

            Page.ManualInputUtilityTabPage.SaveManualInput.Focus();
            
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Thread.Sleep(2000);
            if (null != Page.ManualInputUtilityTabPage.SuccessMessage)
            {
                string message = Page.ManualInputUtilityTabPage.SuccessMessage.BaseElement.InnerText;
                if (!message.Equals(@"Saved successfully"))
                {
                    Assert.Fail("Incorrect Message, Expected:{0} , Actual:{1}", "Saved successfully", message);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            List<EcolabDataGridItems> gridValues = Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("12");

            if (gridValues.Count < 0)
            {
                Assert.Fail("On editing utility through manual input , value is not saved");
            }
            else
            {
                EcolabDataGridItems editedGrid = gridValues.FirstOrDefault();

                IReadOnlyCollection<string> columnvlaues = editedGrid.GetColumnValues();
                if(!columnvlaues.Contains("12 "))
                {
                    Assert.Fail("Cell values are not saved after editing utility through manual input");
                }
                
                //if(!columnvlaues.Contains("13"))
                //{
                //    Assert.Fail("Cell values are not saved after editing utility through manual input");
                //}

            }

            rows = Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("allowmanualentry");
            controls = rows.FirstOrDefault().GetButtonControls();
            controls.LastOrDefault().DeskTopMouseClick();
            Page.ManualInputUtilityTabPage.LastRecordDeleteButton.DeskTopMouseClick();
            if (null != Page.ManualInputUtilityTabPage.PopupMessage)
            {
                string message = Page.ManualInputUtilityTabPage.PopupMessage.BaseElement.InnerText;
                if (!message.Equals(@"Deleted successfully"))
                {
                    Assert.Fail("Incorrect Message, Expected:{0} , Actual:{1}", "Deleted successfully", message);
                }
            }
            else
            {
                Assert.Fail("Delete message is not displayed");
            }

            Page.ManualInputUtilityTabPage.CancelManualInput.DeskTopMouseClick();

            gridValues = Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("12");

            if (gridValues.Count > 0)
            {
                Assert.Fail("On deleting utility through manual input , value is not saved");
            }
        }

        [TestCategory(TestType.functional, "TC03_UnAllowMeter")]
        [Test]
        public void TC03_UnAllowMeter()
        {
            NavigateToMetersPage();
            Page.MetersTabPage.MetersTabGrid.SelectedRows("allowmanualentry").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.MetersTabPage.ManualEntryEdit.DeskTopMouseClick();
            Page.MetersTabPage.ManualEntryEdit.Check(false, true);
            Page.MetersTabPage.EditMeterSaveButton.DeskTopMouseClick();
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputUtilityTabPage.UtilityTab.Click();
            if(Page.ManualInputUtilityTabPage.UtilityTabGrid.SelectedRows("allowmanualentry").Count > 0)
            {
                Assert.Fail("After editing meter to un-allow meter for manual input, the meter is still visible in manual input tab");
            }

        }

        private void NavigateToMetersPage()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(2000);
            Page.PlantSetupPage.MeterTab.Click();
            KeyBoardSimulator.KeyPress(Keys.Tab);
            Thread.Sleep(2000);
        } 
        
        private void AddMeter(Dictionary<string, string> testdata, bool isAllowManualEntry)
        {
            NavigateToMetersPage();
            Page.MetersTabPage.AddMeterButton.Click();

            KeyBoardSimulator.KeyPress(Keys.Tab);
            Thread.Sleep(2000);

            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();
            Telerik.Desktop.KeyBoard.TypeText(testdata["MeterName"]);
            KeyBoardSimulator.SetText(testdata["MeterName"]);

            Page.MetersTabPage.UtilityType.SelectByText(testdata["UtilityType"], Timeout);

            Page.MetersTabPage.UtilityLocation.SelectByText(testdata["UtilityLocation"], Timeout);

            Page.MetersTabPage.MachineCompartment.SelectByText(testdata["MachineCompartment"], Timeout);

            //Page.MetersTabPage.Parent.SelectByText(testdata["Parent"], Timeout);

            Page.MetersTabPage.Calibration.Focus();
            Page.MetersTabPage.Calibration.ExtendedMouseClick();
            KeyBoardSimulator.SetText(testdata["Calibration"]);

            Page.MetersTabPage.UOM.SelectByText(testdata["UOM"], Timeout);  

            Page.MetersTabPage.Controller.SelectByText(testdata["Controller"], Timeout);

            Page.MetersTabPage.AllowManualEntry.Check(isAllowManualEntry, true);
            Page.MetersTabPage.MaxRollOverPoint.Focus();
            Page.MetersTabPage.MaxRollOverPoint.ExtendedMouseClick();
            KeyBoardSimulator.SetText(testdata["MaxRollOverPoint"]);

            
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);


        }

        private void Precondition()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "allowmanualentry");
            data.Add("UtilityType", "Gas");
            data.Add("UtilityLocation", "Washer-Extractor1");
            data.Add("MachineCompartment", "Washer 1");
            //data.Add("Parent", "m");
            data.Add("Calibration", "1");
            data.Add("UOM", "pound_per_hour");
            data.Add("Controller", "MyControl");
            data.Add("MaxRollOverPoint", "120000");

            AddMeter(data, true);
        }        
    }
}
