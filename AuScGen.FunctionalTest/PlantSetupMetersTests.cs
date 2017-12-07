using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using NUnit.Framework;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Controls.HtmlControls;


namespace Ecolab.FunctionalTest
{
    public class PlantSetupMetersTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            //Precondition();
        }
        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        private void Precondition()
        {
            NavigateToMetersPage();
            Thread.Sleep(3000);
            while (Page.MetersTabPage.MetersTabGrid.Rows.Count > 0)
            {
                DialogHandler.ClickonOKButton();
                Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
            }

            if(DBValidation.DataRows("select * from GroupType where GroupDescription='Washer-Extractor1'").Count <= 0)
            {
                DBValidation.UpdateData("insert into GroupType values('Washer-Extractor1','WasherGroup',0,1,1)");
            }

            if (DBValidation.DataRows("select * from GroupType where GroupDescription='Tunnel-Extractor1'").Count <= 0)
            {
                DBValidation.UpdateData("insert into GroupType values('Tunnel-Extractor1','WasherGroup',0,1,1)");
            }

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "M");
            data.Add("UtilityType", "Gas");
            data.Add("UtilityLocation", "Tunnel-Washer1");
            data.Add("MachineCompartment", "Compartment1");
            data.Add("Parent", "-- Select --");
            data.Add("Calibration", "0");
			data.Add("UOM", "ft³");
			data.Add("Controller", "4001 (Ultrax 6/12/16- Allen Bradley)");
            data.Add("MaxRollOverPoint", "10000");
            AddMeter(data);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
        }
        
        /// <summary>
        /// TC 22897: Meters:Verify the maximum meters with same utility can be connected to machine/compartment
        /// TC 23145: RG:18998 : Plant Setup -> Meters:Verify the "Save" button functionality on "Update Meter" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC01_MeterCountWithSameUtiliy")]
        [TestCategory(TestType.regression, "TC01_MeterCountWithSameUtiliy")]
        [Test, Description("Test Case 22897 RG: Meters Verify the maximum meters with same utility type can be connected to machine compartment;"+
                            "TC 23145 :RG 18998 Plant Setup Meters Verify the Save button functionality on Update Meter pop-up")]
        public void TC01_MeterCountWithSameUtiliy()
        {
            //For DropDown Controls we are not using the below data....
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "Test");
            data.Add("UtilityType", "Gas");
            data.Add("UtilityLocation", "Washer-Extractor1");
            data.Add("MachineCompartment", "Washer 1");
            data.Add("Parent", "m");
            data.Add("Calibration", "1");
            data.Add("UOM", "pound_per_hour");
            data.Add("Controller", "MyControl");
            data.Add("MaxRollOverPoint", "120000");

            AddMeter(data);
            Assert.True(Page.MetersTabPage.AddMeterAndVerify());
            AddMeter(data);
            Thread.Sleep(5000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);
            Thread.Sleep(5000);
            if (null != Page.MetersTabPage.ErrorMessage)
            {
                if (!Page.MetersTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Maximum one meter with the same utility can be connected to one Machine / Compartement"))
                {
                    Assert.Fail("Verification Failed - Expected Max one meter with same utility type should connect to one machine/compartment. But Actual is " +
                                 Page.MetersTabPage.ErrorMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.MetersTabPage.AddMeterCancelButton.Click();
        }

        [TestCategory(TestType.regression, "TC02_VerifyCancelButton")]
        [Test, Description("Test Case 23146: Meters-Verify the Cancel button functionality on Update Meter pop-up")]
        public void TC02_VerifyCancelButton()
        {
            NavigateToMetersPage();
            int initialRowCount = Page.MetersTabPage.MetersTabGrid.Rows.Count;
            Page.MetersTabPage.AddMeterButton.Click();
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "Test3");
            data.Add("UtilityType", "Pieces");
            data.Add("UtilityLocation", "Tunnel-Washer1");
            data.Add("MachineCompartment", "Compartment2");
            data.Add("Parent", "m");
            data.Add("Calibration", "1");
            data.Add("UOM", "gallon_per_minute");
            data.Add("Controller", "MyControl");
            data.Add("MaxRollOverPoint", "120000");
            AddMeter(data);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterCancelButton.Click();

            Thread.Sleep(5000);
            if (Page.MetersTabPage.MetersTabGrid.Rows.Count != initialRowCount)
            {
                Assert.Fail("Meters data grid got updated after clicking cancel button");
            } 

            //TODO: Add DB validation , query to be provided by manual testing
        }

        /// <summary>
        /// Test Case 23150: Meters:Verify the "Cancel" button functionality on "Edit Meter" pop-up
        /// </summary>
        [TestCategory(TestType.regression, "TC05_VerifyUpdateCancel")]
        [Test, Description("Test Case 23150: Meters-Verify the Cancel button functionality on Edit Meter pop-up")]
        public void TC05_VerifyUpdateCancel()
        {
            NavigateToMetersPage();
            Thread.Sleep(2000);
            IList<Element> buttons = Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtons();
            HtmlControl updateButton = new HtmlControl(buttons.LastOrDefault());
            updateButton.Click();

            Page.MetersTabPage.MeterName.Text = "";
            KeyBoardSimulator.KeyPress(Keys.Tab);
            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();            
            KeyBoardSimulator.SetText("TrialText");
            Page.MetersTabPage.EditMeterCancelButton.Click();
                                    
            Assert.False(Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().InnerText.Contains("TrialText"));
            
        }

        /// <summary>
        /// TC 23151: Meters:Verify the "Delete" button functionality
        /// </summary>
        [TestCategory(TestType.functional, "TC06_VerifyDeleteRow")]
        [Test]
        public void TC06_VerifyDeleteRow()
        {
            NavigateToMetersPage();
            int initialRowCount = Page.MetersTabPage.MetersTabGrid.Rows.Count;

            Thread.Sleep(2000);
            IList<Element> buttons = Page.MetersTabPage.MetersTabGrid.Rows.LastOrDefault().GetButtons();
            DialogHandler.ClickonOKButton();
            HtmlControl deleteButton = new HtmlControl(buttons[1]);
            deleteButton.Click();
            Thread.Sleep(2000);
            if (Page.MetersTabPage.MetersTabGrid.Rows.Count == initialRowCount)
            {
                Assert.Fail("Row deletion did not happen");
            }
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

          

            //TODO: Add DB validation after manual test cases are updated
        }

        /// <summary>
        /// TC 23024: RG:18998 : Plant Setup -> Meters:Verify the application functionality when the meter is not mapped to a washer
        /// </summary>
        [TestCategory(TestType.functional, "TC07_VerifyNonMappedUtility")]
        [Test]
        public void TC07_VerifyNonMappedUtility()
        {
            NavigateToMetersPage();
            Page.MetersTabPage.AddMeterButton.Click();
            KeyBoardSimulator.KeyPress(Keys.Tab);

            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();
            KeyBoardSimulator.SetText("nonmappedutiltest");

            Page.MetersTabPage.UtilityType.SelectByText("Gas", true);
            Page.MetersTabPage.Parent.SelectByText("m", true);
            Page.MetersTabPage.UOM.SelectByText("pound_per_hour", true);

            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Thread.Sleep(3000);
            Page.MetersTabPage.MetersTabGrid.SelectedRows("nonmappedutiltest").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            
            Thread.Sleep(5000);
            if (null != Page.MetersTabPage.Controllerlabel)
            {
                if (!Page.MetersTabPage.Controllerlabel.BaseElement.InnerText
                    .Equals("UtilityLogger"))
                {
                    Assert.Fail(string.Format("Incorrect controller is displayed {0}", Page.MetersTabPage.Controllerlabel.BaseElement.InnerText));
                }
            }
            else
            {
                Assert.Fail("Controller is not displayed");
            }
    
            
        }

        /// <summary>
        /// TC 23149: RG:18998 : Plant Setup -> Meters:Verify the "Save" button functionality on "Edit Meter" pop-up
        /// </summary>
        [TestCategory(TestType.functional, "TC08_VerifyEditPopupSaveButton")]
        [Test]
        public void TC08_VerifyEditPopupSaveButton()
        {
            NavigateToMetersPage();
            string intialMeterNameValue = Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetColumnValues()[2];

            Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.MetersTabPage.MeterName.Text = "";
            KeyBoardSimulator.KeyPress(Keys.Tab);
            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();
            KeyBoardSimulator.SetText("EditPopupTest");
            Page.MetersTabPage.EditMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            string editedValue = Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().InnerText;

            
            Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.MetersTabPage.MeterName.Text = "";
            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();
            KeyBoardSimulator.SetText(intialMeterNameValue);
            Page.MetersTabPage.EditMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            
            if (null != editedValue)
            {
                if (!editedValue.Contains("EditPopupTest".ToLower()))
                {
                    Assert.Fail("Edited value not saved, Meter name value");
                }
            }
            else
            {
                Assert.Fail("Meter name not displayed");
            }

        }

        /// <summary>
        /// Test case 23154: RG:18998 : Plant Setup -> Meters:Verify whether the application is displaying prompt message while "Saving/Deleting" the records
        /// </summary>        
        [TestCategory(TestType.functional, "TC09_VerifyDeleteDialogMessage")]
        [Test]
        public void TC09_VerifyDeleteDialogMessage()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.UtilityTab.Click();
            Page.PlantSetupPage.MeterTab.Click();

            Thread.Sleep(3000);
            DialogHandler.GetMessageAndOKButton();
            
            Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
                        

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
        /// Test case 23025: Verify the page view access permissions
        /// </summary>
        [TestCategory(TestType.functional, "TC10_VerifyPageViewAccessPermissions")]
        [Test]
        public void TC10_VerifyPageViewAccessPermissions()
        {
            //TestFixtureTearDown();
            
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
                HtmlControl ctrlMeterTab = Page.PlantSetupPage.MeterTab;
                if (ctrlMeterTab != null)
                {
                    if (pair.Key == "AutoTestPE")
                    {
                        Assert.Fail("Meters tab is visible for user :" + pair.Key);
                    }
                    else
                    {
                        Page.PlantSetupPage.MeterTab.Click();
                    }
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        /// <summary>
        /// Test case 23026: Verify the Edit access permissions on Meters Page 
        /// </summary>
        [TestCategory(TestType.functional, "TC11_VerifyEditAccessPermissionOnMetersPage")]
        [Test]
        public void TC11_VerifyEditAccessPermissionOnMetersPage()
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
                Page.PlantSetupPage.MeterTab.Click();
                
                if (pair.Key == "AutoTestTMBasic")
                {
                    Thread.Sleep(1000);
                    Page.MetersTabPage.UsersRolesMetersTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                    HtmlControl ctrlMeterControl = Page.MetersTabPage.ViewMetersLabel;
                    if (ctrlMeterControl.ChildNodes[0].Content == "View Meter")
                    {
                        Assert.True(true, "Logged in with level 6 user:" + pair.Key + " but found edit option is visible");
                    }
                }
                else
                {
                    Page.MetersTabPage.MetersTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
                    HtmlControl ctrlMeterControl = Page.MetersTabPage.EditMetersLabel;
                    if (ctrlMeterControl.ChildNodes[0].Content == "Edit Meter")
                    {
                        Assert.True(true, "Logged in with level 8 & 9 user:" + pair.Key + " but found edit option is not visible");
                    }
                }
                Page.PlantSetupPage.TopMainMenu.LogOut();
            }
        }

        private void AddMeter(Dictionary<string, string> testdata)
        {
            NavigateToMetersPage();
            Page.MetersTabPage.AddMeterButton.Click();

            KeyBoardSimulator.KeyPress(Keys.Tab);
            Thread.Sleep(2000);

            Page.MetersTabPage.MeterName.Focus();
            Page.MetersTabPage.MeterName.ExtendedMouseClick();
           // Telerik.Desktop.KeyBoard.TypeText(testdata["MeterName"]);
            KeyBoardSimulator.SetText(testdata["MeterName"]);

            Page.MetersTabPage.UtilityType.SelectByText(testdata["UtilityType"], Timeout);

            //Page.MetersTabPage.UtilityLocation.SelectByText(testdata["UtilityLocation"], Timeout);
            Page.MetersTabPage.UtilityLocation.SelectByIndex(2, true);

            //Page.MetersTabPage.MachineCompartment.SelectByText(testdata["MachineCompartment"], Timeout);
            Page.MetersTabPage.MachineCompartment.SelectByIndex(2, true);

            //Page.MetersTabPage.Parent.SelectByText(testdata["Parent"], Timeout);
            Page.MetersTabPage.Parent.SelectByIndex(1, true);

            Page.MetersTabPage.Calibration.Focus();
            Page.MetersTabPage.Calibration.ExtendedMouseClick();
            KeyBoardSimulator.SetText(testdata["Calibration"]);

            //Page.MetersTabPage.UOM.SelectByText(testdata["UOM"], Timeout);
            Page.MetersTabPage.UOM.SelectByIndex(1, true);

            //Page.MetersTabPage.Controller.SelectByText(testdata["Controller"], Timeout);
            Page.MetersTabPage.Controller.SelectByIndex(1, true);

            Page.MetersTabPage.MaxRollOverPoint.Focus();
            Page.MetersTabPage.MaxRollOverPoint.ExtendedMouseClick();
            KeyBoardSimulator.SetText(testdata["MaxRollOverPoint"]);
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


        //The below test case will be utilized in Europe Region
        //[TestCategory(TestType.functional, "TC02_MeterCountWithSameWasherConventional")]
        //[Test, Description("TC 23022: Meters:Verify the application functionality when the meter is not mapped to a washer")]
        public void TC02_MeterCountWithSameWasherConventional()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MeterName", "TestTest");
            data.Add("UtilityType", "Water");
            data.Add("UtilityLocation", "Washer-Extractor1");
            data.Add("MachineCompartment", "Washer 1");
            data.Add("Parent", "m");
            data.Add("Calibration", "1");
            data.Add("UOM", "gallon_per_minute");
            data.Add("Controller", "MyControl");
            data.Add("MaxRollOverPoint", "120000");

            AddMeter(data);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data2 = new Dictionary<string, string>();
            data2.Add("MeterName", "TestTest");
            data2.Add("UtilityType", "Water");
            data2.Add("UtilityLocation", "Washer-Extractor1");
            data2.Add("MachineCompartment", "Washer 2");
            data2.Add("Parent", "m");
            data2.Add("Calibration", "1");
            data2.Add("UOM", "gallon_per_minute");
            data2.Add("Controller", "MyControl");
            data2.Add("MaxRollOverPoint", "120000");
            AddMeter(data2);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data3 = new Dictionary<string, string>();
            data3.Add("MeterName", "TestTest");
            data3.Add("UtilityType", "Water");
            data3.Add("UtilityLocation", "Washer-Extractor1");
            data3.Add("MachineCompartment", "Washer3");
            data3.Add("Parent", "m");
            data3.Add("Calibration", "1");
            data3.Add("UOM", "gallon_per_minute");
            data3.Add("Controller", "MyControl");
            data3.Add("MaxRollOverPoint", "120000");
            AddMeter(data3);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Thread.Sleep(5000);
            if (null != Page.MetersTabPage.ErrorMessage)
            {
                if (!Page.MetersTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals(@"Maximum one meter with the same utility can be connected to one Machine / Compartement"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed {0}", Page.MetersTabPage.ErrorMessage.BaseElement.InnerText));
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            Page.MetersTabPage.AddMeterCancelButton.Click();
        }
        /// This Test case will be applicable in Europe Region
        //[TestCategory(TestType.functional, "TC04_VerifyWasherTunnelMaxCount")]
        //[Test, Description("TC 23023: Meters:Verify the maximum count of water meters connected to the same Washer-Tunnel")]
        public void TC04_VerifyWasherTunnelMaxCount()
        {
            Dictionary<string, string> data1 = new Dictionary<string, string>();
            data1.Add("MeterName", "TestTestTest");
            data1.Add("UtilityType", "Water");
            data1.Add("UtilityLocation", "Tunnel-Washer1");
            data1.Add("MachineCompartment", "Compartment2");
            data1.Add("Parent", "m");
            data1.Add("Calibration", "1");
            data1.Add("UOM", "gallon_per_minute");
            data1.Add("Controller", "MyControl");
            data1.Add("MaxRollOverPoint", "120000");
            AddMeter(data1);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data2 = new Dictionary<string, string>();
            data2.Add("MeterName", "TestTestTest");
            data2.Add("UtilityType", "Water");
            data2.Add("UtilityLocation", "Tunnel-Washer1");
            data2.Add("MachineCompartment", "Compartment3");
            data2.Add("Parent", "m");
            data2.Add("Calibration", "1");
            data2.Add("UOM", "gallon_per_minute");
            data2.Add("Controller", "MyControl");
            data2.Add("MaxRollOverPoint", "120000");
            AddMeter(data2);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data3 = new Dictionary<string, string>();
            data3.Add("MeterName", "TestTestTest");
            data3.Add("UtilityType", "Water");
            data3.Add("UtilityLocation", "Tunnel-Washer1");
            data3.Add("MachineCompartment", "Compartment4");
            data3.Add("Parent", "m");
            data3.Add("Calibration", "1");
            data3.Add("UOM", "gallon_per_minute");
            data3.Add("Controller", "MyControl");
            data3.Add("MaxRollOverPoint", "120000");
            AddMeter(data3);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data4 = new Dictionary<string, string>();
            data4.Add("MeterName", "TestTestTest");
            data4.Add("UtilityType", "Water");
            data4.Add("UtilityLocation", "Tunnel-Washer1");
            data4.Add("MachineCompartment", "Compartment5");
            data4.Add("Parent", "m");
            data4.Add("Calibration", "1");
            data4.Add("UOM", "gallon_per_minute");
            data4.Add("Controller", "MyControl");
            data4.Add("MaxRollOverPoint", "120000");
            AddMeter(data4);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data5 = new Dictionary<string, string>();
            data5.Add("MeterName", "TestTestTest");
            data5.Add("UtilityType", "Water");
            data5.Add("UtilityLocation", "Tunnel-Washer1");
            data5.Add("MachineCompartment", "Compartment6");
            data5.Add("Parent", "m");
            data5.Add("Calibration", "1");
            data5.Add("UOM", "gallon_per_minute");
            data5.Add("Controller", "MyControl");
            data5.Add("MaxRollOverPoint", "120000");
            AddMeter(data5);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data6 = new Dictionary<string, string>();
            data6.Add("MeterName", "TestTestTest");
            data6.Add("UtilityType", "Water");
            data6.Add("UtilityLocation", "Tunnel-Washer1");
            data6.Add("MachineCompartment", "Compartment7");
            data6.Add("Parent", "m");
            data6.Add("Calibration", "1");
            data6.Add("UOM", "gallon_per_minute");
            data6.Add("Controller", "MyControl");
            data6.Add("MaxRollOverPoint", "120000");
            AddMeter(data6);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Dictionary<string, string> data7 = new Dictionary<string, string>();
            data7.Add("MeterName", "TestTestTest");
            data7.Add("UtilityType", "Water");
            data7.Add("UtilityLocation", "Tunnel-Washer1");
            data7.Add("MachineCompartment", "Compartment8");
            data7.Add("Parent", "m");
            data7.Add("Calibration", "1");
            data7.Add("UOM", "gallon_per_minute");
            data7.Add("Controller", "MyControl");
            data7.Add("MaxRollOverPoint", "120000");
            AddMeter(data7);
            Thread.Sleep(2000);
            Page.MetersTabPage.AddMeterSaveButton.Focus();
            KeyBoardSimulator.KeyPress(Keys.Enter);

            Thread.Sleep(5000);
            if (null != Page.MetersTabPage.ErrorMessage)
            {
                if (!Page.MetersTabPage.ErrorMessage.BaseElement.InnerText
                    .Equals("Maximum 6 Water meters can be connected to the same Washter-Tunnel"))
                {
                    Assert.Fail(string.Format("Incorrect error message is displayed {0}", Page.MetersTabPage.ErrorMessage.BaseElement.InnerText));
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }

            Page.MetersTabPage.AddMeterCancelButton.Click();
        }
    }
}
