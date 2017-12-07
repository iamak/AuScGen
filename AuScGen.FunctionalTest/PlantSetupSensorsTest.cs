using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
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
    public class PlantSetupSensorsTest : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            PreCondition();
        }

        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        /// <summary>
        /// Preconditions this instance.
        /// </summary>
        public void PreCondition()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(5000);
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(5000);
            while (Page.SensorTabPage.SensorTableGrid.Rows.Count > 0)
            {
                DialogHandler.ClickonOKButton();
                Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("SensorName", "S");
            data.Add("SensorType", "Conductivity");
            data.Add("SensorLocation", "Washer-Extractor1");
            data.Add("Machine", "Compartment1");
            data.Add("OutputType", "0-20mA");
            data.Add("Calibration", "1");
            data.Add("UOM", "Btu_IT_per_Fahrenheit");
            data.Add("Controller", "MyControl");
            AddSensor(data);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(2000);
            if (Page.SensorTabPage.SensorAddedSuccess.BaseElement.InnerText.Contains("Sensor Added Successfully"))
            {
                Assert.True(true, "Sensor added successfully");
            }
            else
            {
                Assert.Fail("Sensor add success messgae not found");
            }
        }

        /// <summary>
        /// Adds the sensor.
        /// </summary>
        /// <param name="testData">The testdata.</param>
        private void AddSensor(Dictionary<string, string> testData)
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(2000);
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(2000);
            Page.SensorTabPage.AddSensorButton.Click();
            Page.SensorTabPage.AddSensorButton.ScrollToVisible();
            Page.SensorTabPage.SensorName.TypeText(testData["SensorName"]);
            Page.SensorTabPage.SensorType.SelectByText(testData["SensorType"], true);
            Page.SensorTabPage.SensorLocation.SelectByText(testData["SensorLocation"], true);
            Thread.Sleep(3000);
            Page.SensorTabPage.MachineName.SelectByText(testData["Machine"], true);
            Page.SensorTabPage.OutputType.SelectByText(testData["OutputType"], true);
            Page.SensorTabPage.Calibration.TypeText(testData["Calibration"]);
            Page.SensorTabPage.UoM.SelectByText(testData["UOM"], true);
            Page.SensorTabPage.SensorController.SelectByText(testData["Controller"], true);
            Thread.Sleep(1000);

        }

        [TestCategory(TestType.functional, "TC01_SensorsCountConnectToSameMachine")]
        [Test, Description("Test case 23629:  RG:18999 : Plant Setup -> Sensors:Verify the maximum Sensors with same utility can be connected to machine/compartment")]
        public void TC01_SensorsCountConnectToSameMachine()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("SensorName", "SA");
            data.Add("SensorType", "Weight");
            data.Add("SensorLocation", "Washer-Extractor1");
            data.Add("Machine", "Compartment1");
            data.Add("OutputType", "0-20mA");
            data.Add("Calibration", "1");
            data.Add("UOM", "carat");
            data.Add("Controller", "MyControl");
            AddSensor(data);
            Assert.True(Page.SensorTabPage.AddSensorAndVerify());
            AddSensor(data);
            Thread.Sleep(5000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(5000);
            if (null != Page.SensorTabPage.ErrorMessage)
            {
                if (!Page.SensorTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Maximum one Sensor with the same type can be connected to one machine/Compartement"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.SensorTabPage.btnCancel.Focus();
            Page.SensorTabPage.btnCancel.Click();
        }

        /// <summary>
        /// Test case 23675:  RG:18999 : Plant Setup -> Sensors:Verify the maximum Temperature sensors
        /// connected to same Washer-Tunnel
        /// </summary>
        [TestCategory(TestType.functional, "TC02_VerifyTemperatureSensorsMaxCount")]
        [Test]
        public void TC02_VerifyTemperatureSensorsMaxCount()
        {
            Dictionary<string, string> data1 = new Dictionary<string, string>();
            data1.Add("SensorName", "S1");
            data1.Add("SensorType", "Temperature");
            data1.Add("SensorLocation", "Tunnel-Washer1");
            data1.Add("Machine", "Compartment1");
            data1.Add("OutputType", "0-20mA");
            data1.Add("Calibration", "1");
            data1.Add("UOM", "Celsius");
            data1.Add("Controller", "MyControl");
            AddSensor(data1);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data2 = new Dictionary<string, string>();
            data2.Add("SensorName", "S2");
            data2.Add("SensorType", "Temperature");
            data2.Add("SensorLocation", "Tunnel-Washer1");
            data2.Add("Machine", "Compartment2");
            data2.Add("OutputType", "0-20mA");
            data2.Add("Calibration", "1");
            data2.Add("UOM", "Celsius");
            data2.Add("Controller", "MyControl");
            AddSensor(data2);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data3 = new Dictionary<string, string>();
            data3.Add("SensorName", "S3");
            data3.Add("SensorType", "Temperature");
            data3.Add("SensorLocation", "Tunnel-Washer1");
            data3.Add("Machine", "Compartment3");
            data3.Add("OutputType", "0-20mA");
            data3.Add("Calibration", "1");
            data3.Add("UOM", "Celsius");
            data3.Add("Controller", "MyControl");
            AddSensor(data3);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data4 = new Dictionary<string, string>();
            data4.Add("SensorName", "S4");
            data4.Add("SensorType", "Temperature");
            data4.Add("SensorLocation", "Tunnel-Washer1");
            data4.Add("Machine", "Compartment4");
            data4.Add("OutputType", "0-20mA");
            data4.Add("Calibration", "1");
            data4.Add("UOM", "Celsius");
            data4.Add("Controller", "MyControl");
            AddSensor(data4);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data5 = new Dictionary<string, string>();
            data5.Add("SensorName", "S5");
            data5.Add("SensorType", "Temperature");
            data5.Add("SensorLocation", "Tunnel-Washer1");
            data5.Add("Machine", "Compartment5");
            data5.Add("OutputType", "0-20mA");
            data5.Add("Calibration", "1");
            data5.Add("UOM", "Celsius");
            data5.Add("Controller", "MyControl");
            AddSensor(data5);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data6 = new Dictionary<string, string>();
            data6.Add("SensorName", "S6");
            data6.Add("SensorType", "Temperature");
            data6.Add("SensorLocation", "Tunnel-Washer1");
            data6.Add("Machine", "Compartment6");
            data6.Add("OutputType", "0-20mA");
            data6.Add("Calibration", "1");
            data6.Add("UOM", "Celsius");
            data6.Add("Controller", "MyControl");
            AddSensor(data6);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data7 = new Dictionary<string, string>();
            data7.Add("SensorName", "S7");
            data7.Add("SensorType", "Temperature");
            data7.Add("SensorLocation", "Tunnel-Washer1");
            data7.Add("Machine", "Compartment7");
            data7.Add("OutputType", "0-20mA");
            data7.Add("Calibration", "1");
            data7.Add("UOM", "Celsius");
            data7.Add("Controller", "MyControl");
            AddSensor(data7);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(5000);
            if (null != Page.SensorTabPage.ErrorMessage)
            {
                if (!Page.SensorTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals("Maximum 6 Temperatures sensor can be connected to the same Washter-Tunnel"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed {0}", Page.SensorTabPage.ErrorMessage.BaseElement.InnerText));
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.SensorTabPage.btnCancel.Focus();
            Page.SensorTabPage.btnCancel.Click();
        }

        /// <summary>
        /// Test case 23677:  RG:18999 : Plant Setup -> Sensors:Verify the maximum pH sensor connected to the same Washer-Tunnel
        /// </summary>
        [TestCategory(TestType.functional, "TC03_VerifypHSensorsMaxCount")]
        [Test]
        public void TC03_VerifypHSensorsMaxCount()
        {
            Dictionary<string, string> data1 = new Dictionary<string, string>();
            data1.Add("SensorName", "S1");
            data1.Add("SensorType", "pH");
            data1.Add("SensorLocation", "Tunnel-Washer1");
            data1.Add("Machine", "Compartment1");
            data1.Add("OutputType", "0-20mA");
            data1.Add("Calibration", "1");
            data1.Add("UOM", "pH");
            data1.Add("Controller", "MyControl");
            AddSensor(data1);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data2 = new Dictionary<string, string>();
            data2.Add("SensorName", "S2");
            data2.Add("SensorType", "pH");
            data2.Add("SensorLocation", "Tunnel-Washer1");
            data2.Add("Machine", "Compartment2");
            data2.Add("OutputType", "0-20mA");
            data2.Add("Calibration", "1");
            data2.Add("UOM", "pH");
            data2.Add("Controller", "MyControl");
            AddSensor(data2);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data7 = new Dictionary<string, string>();
            data7.Add("SensorName", "S7");
            data7.Add("SensorType", "pH");
            data7.Add("SensorLocation", "Tunnel-Washer1");
            data7.Add("Machine", "Compartment3");
            data7.Add("OutputType", "0-20mA");
            data7.Add("Calibration", "1");
            data7.Add("UOM", "pH");
            data7.Add("Controller", "MyControl");
            AddSensor(data7);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Thread.Sleep(5000);
            if (null != Page.SensorTabPage.ErrorMessage)
            {
                if (!Page.SensorTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals("Maximum 2 pH sensors can be connected to the same Washter-Tunnel"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed {0}", Page.SensorTabPage.ErrorMessage.BaseElement.InnerText));
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.SensorTabPage.btnCancel.Focus();
            Page.SensorTabPage.btnCancel.Click();
        }

        /// <summary>
        /// Test case 23683: RG:18999 : Plant Setup -> Sensors:Verify the application functionality when the Sensor is not mapped to a washer  
        /// </summary>
        [TestCategory(TestType.functional, "TC04_VerifySensorNotMappedToWasher")]
        [Test]
        public void TC04_VerifySensorNotMappedToWasher()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("SensorName", "SNMP");
            data.Add("SensorType", "Weight");
            data.Add("SensorLocation", "Washer-Extractor1");
            data.Add("Machine", "Washer 2");
            data.Add("OutputType", "0-20mA");
            data.Add("Calibration", "1");
            data.Add("UOM", "carat");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(2000);
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(2000);
            Page.SensorTabPage.AddSensorButton.Click();
            Page.SensorTabPage.AddSensorButton.ScrollToVisible();
            Page.SensorTabPage.SensorName.TypeText(data["SensorName"]);
            Page.SensorTabPage.SensorType.SelectByText(data["SensorType"], true);
            Page.SensorTabPage.SensorLocation.SelectByText(data["SensorLocation"], true);
            Thread.Sleep(3000);
            Page.SensorTabPage.MachineName.SelectByText(data["Machine"], true);
            Page.SensorTabPage.OutputType.SelectByText(data["OutputType"], true);
            Page.SensorTabPage.Calibration.TypeText(data["Calibration"]);
            Page.SensorTabPage.UoM.SelectByText(data["UOM"], true);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Page.SensorTabPage.SensorTableGrid.SelectedRows("SNMP")[0].GetButtonControls()[2].Click();
            string strControlName = Page.SensorTabPage.EditSensorController.BaseElement.InnerText;
            if (strControlName == "UtilityLogger")
            {
                Assert.True(true, "Sensor mapped to UtilityLogger when not assigned a washer ");
            }
            else
            {
                Assert.Fail(string.Format("Incorrect control mapped to the Sensor {0}", Page.SensorTabPage.EditSensorController.BaseElement.InnerText));
            }
            Page.SensorTabPage.btnCancel.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
        }

        /// <summary>
        /// Test case 23887: RG:18999 : Plant Setup -> Sensors:Verify whether the field name "Machine/Compartment" is changing dynamically
        /// </summary>
        [TestCategory(TestType.functional, "TC05_VerifyAddMachineCompartment")]
        [Test]
        public void TC05_VerifyAddMachineCompartment()
        {

            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.SensorTabPage.SensorTab.Click();
            Page.SensorTabPage.AddSensorButton.Click();
            Page.SensorTabPage.AddSensorButton.ScrollToVisible();
            Page.SensorTabPage.SensorName.TypeText("SS");
            Page.SensorTabPage.SensorType.SelectByText("Temperature", true);
            Page.SensorTabPage.SensorLocation.SelectByText("Washer-Extractor1", true);
            Thread.Sleep(3000);
            if (Page.SensorTabPage.LabelAddMachineCompartment.BaseElement.InnerText != "Machine")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed {0} when selected Washer", Page.SensorTabPage.LabelAddMachineCompartment.BaseElement.InnerText));
            }
            Page.SensorTabPage.SensorLocation.SelectByText("Tunnel-Washer1", true);
            Thread.Sleep(3000);
            if (Page.SensorTabPage.LabelAddMachineCompartment.BaseElement.InnerText != "Compartment")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed {0} when selected Tunnel", Page.SensorTabPage.LabelAddMachineCompartment.BaseElement.InnerText));
            }
            Page.SensorTabPage.btnCancel.MultiTabClickButton();
        }

        /// <summary>
        /// Test case 23924: RG:18999 : Plant Setup -> Sensors:Verify the "Save" button functionality on "Update Sensor" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC06_VerifyEditPopupSaveButton")]
        [Test]
        public void TC06_VerifyEditPopupSaveButton()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(5000);
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(5000);
            string intialSensorNameValue = Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetColumnValues()[2];
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.SensorTabPage.EditSensorName.TypeText("EditPopupTest");
            Page.SensorTabPage.btnEditSave.Click();
           
            if (!Page.SensorTabPage.SensorAddedSuccess.BaseElement.InnerText.Contains("Sensor details updated successfully"))
            {
                Assert.Fail("Sensor update success messgae not found");
            }
            string editedValue = Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().InnerText;
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();

            Page.SensorTabPage.EditSensorName.TypeText(intialSensorNameValue);
            Page.SensorTabPage.btnEditSave.Click();


            if (!Page.SensorTabPage.SensorAddedSuccess.BaseElement.InnerText.Contains("Sensor details updated successfully"))
            {
                Assert.Fail("Sensor update success messgae not found");
            }

            if (null != editedValue)
            {
                if (!editedValue.Contains("EditPopupTest"))
                {
                    Assert.Fail("Edited value not saved, Sensor name value");
                }
            }
            else
            {
                Assert.Fail("Sensor name not displayed");
            }

        }

        /// <summary>
        /// Test case 23932: RG:18999 : Plant Setup -> Sensors:Verify whether the application is displaying prompt message while "Saving/Deleting" the records
        /// </summary>
        [TestCategory(TestType.functional, "TC07_VerifyDeletePromptMessage")]
        [Test]
        public void TC07_VerifyDeletePromptMessage()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.SensorTabPage.SensorTab.Click();
            DialogHandler.GetMessageAndOKButton();
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
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

        }

        /// <summary>
        /// Test case 23951: RG:18999 : Plant Setup -> Sensors:Verify the application localization functionality on Sensors Page
        /// </summary>
        [TestCategory(TestType.functional, "TC08_VerifyLocalizationSensorPage")]
        [Test]
        public void TC08_VerifyLocalizationSensorPage()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
           
            Page.SensorTabPage.SensorTab.Click();
          
            Page.SensorTabPage.AddSensorButton.Click();
            if (Page.SensorTabPage.AddPopupTitle.BaseElement.InnerText != "Voeg Sensor")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed {0} when localization changed to Deutsch", Page.SensorTabPage.AddPopupTitle.BaseElement.InnerText));
            }
            Page.SensorTabPage.btnCancel.MultiTabClickButton();
         
        }

        /// <summary>
        /// Test Case:- 23681: RG:18999: Plant SetUp - Sensors- Verify the maximum conductivity sensors can be connected to the same washer action
        /// </summary>
        [TestCategory(TestType.functional, "TC09_VerifyConductivityMaxSensors")]
        [Test]
        public void TC09_VerifyConductivityMaxSensors()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("SensorName", "se");
            data.Add("SensorType", "Conductivity");
            data.Add("SensorLocation", "Tunnel-Washer1");
            data.Add("Machine", "Compartment1");
            data.Add("OutputType", "0-20mA");
            data.Add("Calibration", "12");
            data.Add("UOM", "Btu_IT_per_Fahrenheit");
            data.Add("Controller", "MyControl");
            AddSensor(data);
            Assert.True(Page.SensorTabPage.AddSensorAndVerify(), "Maximum 1 Conductivity sensor can be connected to the same Washter-Tunnel");
            AddSensor(data);
            Thread.Sleep(2000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(1000);
            if (null != Page.SensorTabPage.ErrorMessage)
            {
                if (!Page.SensorTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Maximum 1 Conductivity sensor can be connected to the same Washter-Tunnel"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.SensorTabPage.btnCancel.Click();
        }

        /// <summary>
        /// Test Case:- 23682: RG:18999: Plant SetUp - Sensors- Verify the maximum conductivity sensors can be connected to the same washer action
        /// </summary>
        [TestCategory(TestType.functional, "TC10_VerifyWeightMaxSensors")]
        [Test]
        public void TC10_VerifyWeightMaxSensors()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("SensorName", "WeightSensor");
            data.Add("SensorType", "Weight");
            data.Add("SensorLocation", "Tunnel-Washer1");
            data.Add("Machine", "Compartment1");
            data.Add("OutputType", "0-20mA");
            data.Add("Calibration", "15");
            data.Add("UOM", "attogram");
            data.Add("Controller", "MyControl");
            AddSensor(data);
            Assert.True(Page.SensorTabPage.AddSensorAndVerify(), "Maximum 1 Weight sensor can be connected to the same Washter-Tunnel");
            AddSensor(data);
            Thread.Sleep(5000);
            Page.SensorTabPage.btnSave.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(5000);
            if (null != Page.SensorTabPage.ErrorMessage)
            {
                if (!Page.SensorTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Maximum 1 Weight sensor can be connected to the same Washter-Tunnel"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.SensorTabPage.btnCancel.Click();
        }

        /// <summary>
        /// Test Case:- 23926 : RG:18999: PlantSetUp:- Sensors:- Verify the Save Button functionality on Edit Sensor PopUp
        ///  Note:- this Test Case we are considering as Inline Editing and Updating the Sensors Data...
        /// </summary>
        [TestCategory(TestType.functional, "TC11_VerifyInLineEditUpdateSensorsData")]
        [Test]
        public void TC11_VerifyInLineEditUpdateSensorsData()
        {

            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(1000);
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Page.SensorTabPage.InLineSensorEdit("SensorUpdated");
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            Thread.Sleep(3000);
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Page.SensorTabPage.InLineSensorEdit("SensorUpdated");
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(2000);
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
            Thread.Sleep(2000);
            if (!Page.SensorTabPage.SensorAddedSuccess.BaseElement.InnerText.Contains("Sensor details updated successfully"))
            {
                Assert.Fail("Sensor update success messgae not found");
            }
        }

        /// <summary>
        /// Test Case:-23928 RG:-18999 PlantSetUp - Verify the Delete Button Functionality
        /// </summary>
        [TestCategory(TestType.functional, "TC12_VerifyDeleteButtonSensorsFunctionality")]
        [Test]
        public void TC12_VerifyDeleteButtonSensorsFunctionality()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Thread.Sleep(5000);
            Page.SensorTabPage.SensorTab.Click();
            Thread.Sleep(5000);
            DialogHandler.GetMessageAndOKButton();
            Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
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
            Assert.IsTrue(Page.SensorTabPage.SensorAddedSuccess.BaseElement.InnerText.Contains("Sensor Deleted Successfully"), "Success Message not matched");
        }

        /// <summary>
        /// Test case 23685: Verify the page view access permissions
        /// </summary>
        [TestCategory(TestType.functional, "TC13_VerifyPageViewAccessPermissions")]
        [Test]
        public void TC13_VerifyPageViewAccessPermissions()
        {
            //TestFixtureTearDown();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestTMBasic", "test");
            userAccess.Add("AutoTestAdmin", "test");
            userAccess.Add("AutoTestPE", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
                Page.PlantSetupPage.UtilityTab.Click();
                HtmlControl utilityContainersTab = Page.SensorTabPage.UtilityContainerSubMenu;
                string strbreadCrumb = string.Empty;
                List<string> myList = new List<string>();
                ICollection<Element> ctrl = utilityContainersTab.ChildNodes;
                foreach (Element e in ctrl)
                {
                    myList.Add(e.InnerText.Trim());
                }
                if (pair.Key == "AutoTestPE")
                {
                    if (myList.Contains("Sensors"))
                    {
                        Assert.Fail("Sensors tab is visible for user :" + pair.Key);
                    }
                }
                else
                {
                    Page.SensorTabPage.SensorTab.Click();
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        /// <summary>
        /// Test case 23686: Verify the Edit access permissions on Meters Page 
        /// </summary>
        [TestCategory(TestType.functional, "TC14_VerifyEditAccessPermissionOnSensors")]
        [Test]
        public void TC14_VerifyEditAccessPermissionOnSensors()
        {
            //TestFixtureTearDown();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Dictionary<string, string> userAccess = new Dictionary<string, string>();
            userAccess.Add("AutoTestEng", "test");
            userAccess.Add("AutoTestAdmin", "test");
            userAccess.Add("AutoTestTMBasic", "test");
            foreach (KeyValuePair<string, string> pair in userAccess)
            {
                Page.LoginPage.VerifyLogin(pair.Key, pair.Value);
                Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
                Page.PlantSetupPage.UtilityTab.Click();
                Page.SensorTabPage.SensorTab.Click();
                if (pair.Key == "AutoTestTMBasic")
                {
                    Thread.Sleep(2000);
                    Page.SensorTabPage.UsersRolesSensorsTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                    HtmlControl ctrlSensorslabel = Page.SensorTabPage.ViewSensorsLabel;
                    if (ctrlSensorslabel.ChildNodes[0].Content == "View Sensor")
                    {
                        Assert.True(true, "Logged in with level 6 user:" + pair.Key + " but found edit option is visible");
                    }
                }
                else
                {
                    Page.SensorTabPage.SensorTableGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
                    HtmlControl ctrlSensors = Page.SensorTabPage.EditSensorsLabel;
                    if (ctrlSensors.ChildNodes[0].Content == "Edit Sensor")
                    {
                        Assert.True(true, "Logged in with level 8 & 9 user:" + pair.Key + " but found edit option is not visible");
                    }
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }
    }
}
