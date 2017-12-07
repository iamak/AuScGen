using ArtOfTest.WebAii.Controls.HtmlControls;
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
    class ManualInputProductionTests : TestBase
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
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputProductionTabPage.ProductionTab.Click();
            Page.ManualInputProductionTabPage.ProductionDataTab.Click();

        }

        /// <summary>s
        /// Tests the fixture tear down.
        /// </summary>
        //protected void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        /// <summary>
        /// Test case 32687: RG:Verify whether New Recording and Current recording is displayed only when user selects three(washergroup,washer and formula)
        /// Test case 32693: RG:Verify "Save" button in Production data
        /// Test case 32704: RG:Verify whether user able to delete the "Current" record
        /// Test case 32725: RG:Verify whether Previous record to the deleted "Current record" is displayed as "CurrentRecord"
        /// Test case 32799: RG:Verify whether user able to add new recording
        /// Test case 34204: RG:Select Washer group and verify whether Formula field gets loaded
        /// Test case 32774: RG:Verify whether Current recording changes as soon as user modifies washer/formula
        /// </summary>
        [TestCategory(TestType.functional, "TC01_AddAndDeleteRecord")]
        [Test]
        public void TC01_AddAndDeleteRecord()
        {
            Page.ManualInputProductionTabPage.WasherGroup.SelectByText("Tunnel-Washer2", Timeout);
            Page.ManualInputProductionTabPage.Washer.SelectByText("Tunnel 2", Timeout);
            Page.ManualInputProductionTabPage.Formula.SelectByText("ecoform1", Timeout);

            Page.ManualInputProductionTabPage.NewValue.TypeText("12");
            Page.ManualInputProductionTabPage.Save.Click();

            Page.ManualInputProductionTabPage.NewValue.TypeText("12");
            Page.ManualInputProductionTabPage.Save.Click();

            HtmlControl successMsg = Page.ManualInputProductionTabPage.Message;
            if (null == successMsg)
            {
                Assert.Fail("Success message is not displayed after manual input production data is saved");
            }
            else
            {
                if (!Page.ManualInputProductionTabPage.IsMessage("Saved successfully"))
                {
                    Assert.Fail("After saving manual input production data Expected:Saved successfully,Actual:{0}", successMsg.BaseElement.InnerText);
                }

            }

            Page.ManualInputProductionTabPage.NewValue.TypeText("13");
            Page.ManualInputProductionTabPage.Save.Click();
          
            string lastValue = Page.ManualInputProductionTabPage.GetlastValue;
            if (!Page.ManualInputProductionTabPage.IsLastValue("13"))
            {
                Assert.Fail("Last value is incorrectly displayed Expected:13 , Actual:{0}", lastValue);
            }

            DataRowCollection dbValues = DBValidation.DataRows(@"select top 1 * from ManualProduction order by ProductionId desc");

            if (Convert.ToBoolean(dbValues[0].ItemArray[6]))
            {
                Assert.Fail("Database is not updated after production data is added through manual input");
            }

            Page.ManualInputProductionTabPage.DeleteLastValue.DeskTopMouseClick();
            DialogHandler.YesButton.DeskTopMouseClick();

            lastValue = Page.ManualInputProductionTabPage.GetlastValue;
            if (!Page.ManualInputProductionTabPage.IsLastValue("12"))
            {
                Assert.Fail("Last value is incorrectly displayed Expected:12 , Actual:{0}", lastValue);
            }

            dbValues = DBValidation.DataRows(@"select top 1 * from ManualProduction order by ProductionId desc");

            if(!Convert.ToBoolean(dbValues[0].ItemArray[6]))
            {
                Assert.Fail("Database is not updated after production data is deleted through manual input");
            }

        }

        /// <summary>
        /// Test case 34217: RG: Verify recording value in Batchdata
        /// </summary>
        [TestCategory(TestType.ondemand, "TC02_EditBatchData")]
        [Test]
        public void TC02_EditBatchData()
        {
            Page.ManualInputProductionTabPage.BatchDataTab.Click();
            Page.ManualInputProductionTabPage.BatchDataDay.DeskTopMouseClick();
            Page.ManualInputProductionTabPage.BatchDataDatePicker.SelectDay("October 2014", "22");
            Page.ManualInputProductionTabPage.WasherGroup.SelectByIndex(1,Timeout);
            Page.ManualInputProductionTabPage.Washer.SelectByIndex(1,Timeout);
            Page.ManualInputProductionTabPage.Formula.SelectByIndex(1,Timeout);
            Page.ManualInputProductionTabPage.BatchDataDropDown.SelectByIndex(1,Timeout);

            Page.ManualInputProductionTabPage.BatchDataRecordingValue.DeskTopMouseClick();
            Page.ManualInputProductionTabPage.BatchDataRecordingValue.TypeText("155");
            Page.ManualInputProductionTabPage.Save.DeskTopMouseClick();

            HtmlControl successMsg = Page.ManualInputProductionTabPage.Message;
            if (null == successMsg)
            {
                Assert.Fail("Success message is not displayed after manual input production data is saved");
            }
            else
            {
                if (!Page.ManualInputProductionTabPage.IsMessage("Saved successfully"))
                {
                    Assert.Fail("After saving manual input batch data Expected:Saved successfully,Actual:{0}", successMsg.BaseElement.InnerText);
                }
            }
        }

        /// <summary>
        /// Test case 32783: RG: Verify only mentioned users are able to enter the data into ManualInput production data page
        /// Test case 32784
        /// </summary>
        [TestCategory(TestType.bvt, "TC02_UserRoleVerification")]
        [TestCategory(TestType.regression, "TC02_UserRoleVerification")]
        [TestCategory(TestType.functional, "TC02_UserRoleVerification")]
        [Test]
        public void TC03_UserRoleVerification()
        {
            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestCAM", "test");
            if(Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Manual Inputs"))
            {
                Assert.Fail("User with CAM role has access to Manual Input option");
            }

            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestPE", "test");
            if (Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Manual Inputs"))
            {
                Assert.Fail("User with Plant engineer role has access to Manual Input option");
            }

            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestPM", "test");
            if (Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Manual Inputs"))
            {
                Assert.Fail("User with Plant manager role has access to Manual Input option");
            }           

        }

        /// <summary>
        /// Test case 32783: RG: Verify only mentioned users are able to enter the data into ManualInput production data page
        /// Test case 34220: RG:Verify only mentioned users are able to enter the data into ManualInput batch data page
        /// </summary>
        [TestCategory(TestType.functional, "TC03_UserRoleVerificationBDMRole")]
        [Test]
        public void TC04_UserRoleVerificationBDMRole()
        {
            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestBDM", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToManualInput();
            Page.ManualInputProductionTabPage.ProductionTab.Click();
            Page.ManualInputProductionTabPage.ProductionDataTab.Click();
            Page.ManualInputProductionTabPage.WasherGroup.SelectByText("Tunnel-Washer2", Timeout);
            Page.ManualInputProductionTabPage.Washer.SelectByText(Page.ManualInputProductionTabPage.Washer.Options[1].Text, Timeout);
            Page.ManualInputProductionTabPage.Formula.SelectByText(Page.ManualInputProductionTabPage.Formula.Options[1].Text, Timeout);

            if (Page.ManualInputProductionTabPage.IsSaveButtonPresent)
            {
                Assert.Fail("With BDM user role paroduction data page is allowing data to be saved");
            }

            Page.ManualInputProductionTabPage.BatchDataTab.Click();

            if (Page.ManualInputProductionTabPage.IsSaveButtonPresent)
            {
                Assert.Fail("With BDM user role batch data page is allowing data to be saved");
            }
        }
    }
}
