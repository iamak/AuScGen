using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
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
    public class PlantSetupShiftTests: TestBase
    {
        private static string testData = TestDataPath + "testdata.xlsx";

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
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.ShiftTabPage.ShiftTab.Click();
        }

        private void CloseBrowser()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        /// <summary>
        /// Test case 27574: RG:17908 : Verify Shift time and Break time
        /// Test case 27602: RG:17908 : Verify SAVE functionality on ADD Shift
        /// Test case 27606: RG:17908 : Verify Delete functionality
        /// </summary>
        [TestCategory(TestType.regression, "TC01_AddAndDeleteShift")]
        [TestCategory(TestType.functional, "TC01_AddAndDeleteShift")]
        [Test(Description = "Test case 27574;Test case 27602;Test case 27606")]
        public void TC01_AddAndDeleteShift()
        {
            //Precondition: Delete existing shifts for wednesday
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Tuesday");

            Page.ShiftTabPage.AddShift.Click();
            string addShiftUoM = Page.ShiftTabPage.TargetProdAddShiftUoM;          

            if(Page.ShiftTabPage.IsDaySelected(DayOfWeek.Sunday))
            {
                Assert.Fail("By default Sunday should be inactive");
            }

            if (Page.ShiftTabPage.IsDaySelected(DayOfWeek.Saturday))
            {
                Assert.Fail("By default Saturday should be inactive");
            }

            if (!Page.ShiftTabPage.IsDaySelected(DayOfWeek.Monday))
            {
                Assert.Fail("By default Monday should be active");
            }

            if (!Page.ShiftTabPage.IsDaySelected(DayOfWeek.Tuesday))
            {
                Assert.Fail("By default Tuesday should be active");
            }

            if (!Page.ShiftTabPage.IsDaySelected(DayOfWeek.Wednesday))
            {
                Assert.Fail("By default Wednesday should be active");
            }

            if (!Page.ShiftTabPage.IsDaySelected(DayOfWeek.Thursday))
            {
                Assert.Fail("By default Thursday should be active");
            }

            if (!Page.ShiftTabPage.IsDaySelected(DayOfWeek.Friday))
            {
                Assert.Fail("By default Friday should be active");
            }

            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Test Shift");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("01:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            Thread.Sleep(5000);
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

            // Uncomment this validation when UoM defect is resolved
            //if (!Page.ShiftTabPage.ShiftTargetProductionUoM("Tuesday", "Auto Test Shift")
            //       .Equals(addShiftUoM))
            //{
            //    Assert.Fail("UoM mismatch, Add Shift popup:{0} , Shift Page:{1}", addShiftUoM,Page.ShiftTabPage.ShiftTargetProductionUoM("Tuesday", "Auto Test Shift"));
            //}
           
            DataRowCollection dbValues = DBValidation.DataRows(@"select ShiftName from Shift where ShiftId = (select ShiftId from ShiftData where StartTime='10:00 AM' and EndTime='01:00 PM' and Is_Deleted=0)");

            if(dbValues.Count <= 0)
            {
                Assert.Fail("After shift addition data base is not updated");
            }

            if(!dbValues[0].ItemArray[0].ToString().Equals("Auto Test Shift"))
            {
                Assert.Fail("Shift name is incorrectly updated in DB");
            }

            Page.ShiftTabPage.ShiftDeleteButton("Tuesday", "Auto Test Shift").Click();
            DialogHandler.OkButton.DeskTopMouseClick();

            dbValues = DBValidation.DataRows(@"select ShiftName from Shift where ShiftId = (select ShiftId from ShiftData where StartTime='10:00 AM' and EndTime='01:00 PM' and Is_Deleted=0)");

            if (dbValues.Count > 0)
            {
                Assert.Fail("After shift deleation data base is not updated");
            }
        }

        /// <summary>
        /// Test case 27603: RG:17908 : Verify SAVE functionality on Edit shift
        /// Test case 27610: RG:17908 : Verify whether shift creates with already existed shift timings
        /// Test case 27612: RG:17908 : Verify whether user creates a shift break with already existed break
        /// </summary>
        [TestCategory(TestType.regression, "TC02_AddShiftWithBreak")]
        [TestCategory(TestType.functional, "TC02_AddShiftWithBreak")]
        [Test]
        public void TC02_AddShiftWithBreak()
        {
            //Precondition: Delete existing shifts for wednesday
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Wednesday");

            Page.ShiftTabPage.AddShift.Click();         

            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Test Shift With Break");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("06:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            if (null != Page.ShiftTabPage.NoBreakMessage("Wednesday", "Auto Test Shift With Break"))
            {
                if(!Page.ShiftTabPage.NoBreakMessage("Wednesday","Auto Test Shift With Break").BaseElement.InnerText
                    .Equals("No Breaks Found"))
                {
                    Assert.Fail("No breaks found message is incorrect: Actual:{0}", Page.ShiftTabPage.NoBreakMessage("Wednesday", "Auto Test Shift With Break").BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("No breaks found message is not displayed");
            }            

            Page.ShiftTabPage.ShiftEditButton("Wednesday", "Auto Test Shift With Break").Click();            
            Page.ShiftTabPage.AddBreak.Click();           
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("01:00 PM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("02:30 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            if (null != Page.ShiftTabPage.ShiftTexts("Wednesday", "Auto Test Shift With Break"))
            {
                if (!Page.ShiftTabPage.ShiftTexts("Wednesday", "Auto Test Shift With Break").Contains("01:00 PM to 02:30 PM"))
                {
                    Assert.Fail("Shift text for Wednesday is not correct , Actual:{0}", Page.ShiftTabPage.ShiftTexts("Wednesday", "Auto Test Shift With Break").FirstOrDefault());
                }                
            }
            else
            {
                Assert.Fail("Shift message is not displayed in shift page");
            }

            Page.ShiftTabPage.AddShift.Click();
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Test Shift With Break");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("02:00 PM");
            Page.ShiftTabPage.ShiftToTime.TypeText("04:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            if (null != Page.ShiftTabPage.ShiftOverlapMessage)
            {
                if (!Page.ShiftTabPage.ShiftOverlapMessage.BaseElement.InnerText
                    .Equals(@"Shift timings overlapping onWednesday"))
                {
                    Assert.Fail("Incorrect message is displayed while adding overlapping shift Actual:{0}", Page.ShiftTabPage.ShiftOverlapMessage.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Shift overlap message is not displayed");
            }

            Page.ShiftTabPage.CancelShiftButton.TypeEnterKey();

            Page.ShiftTabPage.ShiftEditButton("Wednesday", "Auto Test Shift With Break").Click();            
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime[1].TypeText("02:00 PM");
            Page.ShiftTabPage.BreakToTime[1].TypeText("03:30 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            if (!Page.ShiftTabPage.IsBreakOverlapping.Equals("Breaks overlapping"))
            {
                Assert.Fail("Overlapping breaks are allowed");
            }

            Page.ShiftTabPage.CancelShiftButton.Click();

            //DialogHandler.ClickonOKButton();
            Page.ShiftTabPage.ShiftDeleteButton("Wednesday", "Auto Test Shift With Break").Click();      
            DialogHandler.OkButton.DeskTopMouseClick();
            
        }

        /// <summary>
        /// Test case 27616: RG:17908 : Verify whether Shift break is created with in the shift timings only
        /// Test case 27632: RG:17908 : Verify whether Shift break is deleted
        /// </summary>
        [TestCategory(TestType.regression, "TC03_BreakOutsideShiftTime")]
        [TestCategory(TestType.functional, "TC03_BreakOutsideShiftTime")]
        [Test]
        public void TC03_BreakOutsideShiftTime()
        {
            //Precondition: Delete existing shifts for wednesday
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Thursday");

            Page.ShiftTabPage.AddShift.Click();           

            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Break Outside Shift");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("02:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            Page.ShiftTabPage.ShiftEditButton("Thursday", "Auto Break Outside Shift").Click();
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("01:00 PM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("02:30 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            if (!Page.ShiftTabPage.IsBreakOverlapping.Equals("Breaks overlapping"))
            {
                Assert.Fail("Break timings are not within shift timings, break is still allowed");
            }

            Page.ShiftTabPage.CancelShiftButton.Click();

            Page.ShiftTabPage.ShiftEditButton("Thursday", "Auto Break Outside Shift").Click();
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("01:00 PM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("01:30 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            //DialogHandler.ClickonOKButton();
            Page.ShiftTabPage.DeleteBreakButtonsFromShiftPage("Thursday", "Auto Break Outside Shift").FirstOrDefault().Click();
            DialogHandler.OkButton.DeskTopMouseClick();

            DataRowCollection dbValues = DBValidation.DataRows(@"select top 1 IS_Deleted from ShiftBreakDataHistory order by OperationTimestamp desc");
            if (!Convert.ToBoolean(dbValues[0].ItemArray[0]))
            {
                Assert.Fail("Shift break deletion is not updated in DB");
            }

            //DialogHandler.ClickonOKButton();
            Page.ShiftTabPage.ShiftDeleteButton("Thursday", "Auto Break Outside Shift").Click();
            DialogHandler.OkButton.DeskTopMouseClick();
        }

        /// <summary>
        /// Test case 27838: RG:17908 : Verify Delete functionality of labor
        /// Test case 27797: RG:17908 : Verify whether same timings are updated in database(12hrs/24hrs)
        /// </summary>
        [TestCategory(TestType.regression, "TC04_AddLaborToShift")]
        [TestCategory(TestType.functional, "TC04_AddLaborToShift")]
        [Test]
        public void TC04_AddLaborToShift()
        {
            //Precondition: Delete existing shifts for wednesday
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Wednesday");

            Page.ShiftTabPage.AddShift.Click();
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Test Shift With Break");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("06:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            DataRowCollection shiftDBData = DBValidation.DataRows("select top 1 * from ShiftData order by ID desc");

            if (!shiftDBData[0].ItemArray[2].ToString().Equals("10:00:00"))
            {
                Assert.Fail("Shift start time for created shift is incorrectly updated in DB, Actual:{0}", shiftDBData[0].ItemArray[2].ToString());
            }

            if (!shiftDBData[0].ItemArray[3].ToString().Equals("18:00:00"))
            {
                Assert.Fail("End time for created shift is incorrectly updated in DB, Actual:{0}", shiftDBData[0].ItemArray[3].ToString());
            }


            Page.ShiftTabPage.AddLabour("Wednesday", "Auto Test Shift With Break").Click();

            string labourType = Page.ShiftTabPage.LabourType.Options[1].Text;
            Page.ShiftTabPage.LabourType.SelectByText(labourType, true);

            string labourLocation = Page.ShiftTabPage.LabourLocation.Options[1].Text;
            Page.ShiftTabPage.LabourLocation.SelectByText(labourLocation, true);

            Page.ShiftTabPage.ManHours.TypeText("12");
            Page.ShiftTabPage.AvgPricePerHr.TypeText("13");
            Page.ShiftTabPage.AddLabourSaveButton.TypeEnterKey();

            HtmlControl firstRow = Page.ShiftTabPage.LabourRows("Wednesday", "Auto").FirstOrDefault();
            List<string> labourValues = Page.ShiftTabPage.RowValues(firstRow);

            if(labourValues.Count < 4)
            {
                Assert.Fail("Labour table  did not get populated after adding labour to shift");
            }
            else
            {
                if(!labourValues[0].Equals(labourType))
                {
                   Assert.Fail("Labour type is incorrectly displayed in table after adding labour to shift");
                }

                if (!labourValues[1].Equals(labourLocation))
                {
                    Assert.Fail("Labour location is incorrectly displayed in table after adding labour to shift");
                }

                if (!labourValues[2].Equals("12"))
                {
                    Assert.Fail("Man hours is incorrectly displayed in table after adding labour to shift");
                }

                if (!labourValues[3].Equals("13"))
                {
                    Assert.Fail("Average price is incorrectly displayed in table after adding labour to shift");
                }

                //DialogHandler.ClickonOKButton();
                Page.ShiftTabPage.LabourRowDeleteButton(firstRow).Click();
                DialogHandler.OkButton.DeskTopMouseClick();
              
                if (!Page.ShiftTabPage.IsLaborRowAbsent("Wednesday", "Auto"))
                {
                    Assert.Fail("After labour is deleted, the labour grid is not updated in UI");
                }
            }

        }

        /// <summary>
        /// Test case 28348: RG:17908 : Verify whether shift modified/edited is reflected in other weekday when days are selected
        /// </summary>
        [TestCategory(TestType.regression, "TC05_EditShiftDay")]
        [TestCategory(TestType.functional, "TC05_EditShiftDay")]
        [Test]
        public void TC05_EditShiftDay()
        {
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Monday");
            Page.ShiftTabPage.DeleteAllShifts("Tuesday");

            Page.ShiftTabPage.AddShift.Click();
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Auto Test Shift For Day Edit");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("1234");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("06:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            Page.ShiftTabPage.ShiftEditButton("Monday", "Auto Test Shift For Day Edit").Click();
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.Tuesday.Click();
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();

            var test = Page.ShiftTabPage.ShiftsPresent("Tuesday");
            
            //if (!Page.ShiftTabPage.ShiftsPresent("Tuesday").Contains("Auto Test Shift For Day Edit"))
            //{
            //    Assert.Fail("Shift is not displayed for the new day after editing shift day");
            //}

            var test2 = Page.ShiftTabPage.ShiftsPresent("Monday");
            if (Page.ShiftTabPage.ShiftsPresent("Monday").Contains("Auto Test Shift For Day Edit"))
            {
                Assert.Fail("Shift is still displayed for the same day after editing shift day");
            }

            if (null == Page.ShiftTabPage.ShiftDeleteButton("Tuesday", "Auto Test Shift For Day Edit"))
            {
                Assert.Fail("Shift is not displayed for the new day after editing shift day");
            }
            else
            {
                Page.ShiftTabPage.ShiftDeleteButton("Tuesday", "Auto Test Shift For Day Edit").Click();
            }
        }
        
        /// <summary>
        /// Test case 28762: RG:17908 : Verify whether the created shift is displayed
        /// </summary>
        [TestCategory(TestType.regression, "TC06_VerifyCreatedShiftDisplayed")]
        [TestCategory(TestType.functional, "TC06_VerifyCreatedShiftDisplayed")]
        [Test]
        public void TC06_VerifyCreatedShiftDisplayed()
        {
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Monday");
            Page.ShiftTabPage.DeleteAllShifts("Tuesday");
            Page.ShiftTabPage.DeleteAllShifts("Wednesday");
            Page.ShiftTabPage.DeleteAllShifts("Thursday");
            Page.ShiftTabPage.DeleteAllShifts("Friday");
            Page.ShiftTabPage.AddShift.Click();           
            Thread.Sleep(2000);
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> {DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday});
            Thread.Sleep(3000);
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("New Shift Added");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("7373");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("05:00 PM");
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("10:30 AM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("11:30 AM");
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime[1].TypeText("02:30 PM");
            Page.ShiftTabPage.BreakToTime[1].TypeText("03:30 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();
            Thread.Sleep(3000);
            List<string> getShiftCountList = new List<string>();
            getShiftCountList = Page.ShiftTabPage.GetShiftDetails("Monday", "New Shift Added");
            if (getShiftCountList.Count > 1)
            {
                Assert.Fail("Shift created multiple times");
            }
            else
            {
                Assert.True(true, "Shift created multiple times");
            }
            List<string> getBreakTimeList = new List<string>();
            getBreakTimeList = Page.ShiftTabPage.ShiftTexts("Monday", "New Shift Added");
            if(getBreakTimeList.Count > 2)
            {
                Assert.Fail("Break added more than 2 times");
            }
            else
            {
                Assert.True(true, "Break added more than 2 times");
            }
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Test case 30355: RG:17908 : Verify whether break is created even after shift is created
        /// </summary>
        [TestCategory(TestType.regression, "TC07_CreateBreakAfterCreatingShift")]
        [TestCategory(TestType.functional, "TC07_CreateBreakAfterCreatingShift")]
        [Test]
        public void TC07_CreateBreakAfterCreatingShift()
        {
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Monday");
            
            Page.ShiftTabPage.AddShift.Click();
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Creating Break in Shift");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("7373");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("07:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();
            Page.ShiftTabPage.ShiftEditButton("Monday", "Creating Break in Shift").Click();
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime.FirstOrDefault().TypeText("10:30 AM");
            Page.ShiftTabPage.BreakToTime.FirstOrDefault().TypeText("11:30 AM");
            Page.ShiftTabPage.AddBreak.Click();
            Page.ShiftTabPage.BreakFromTime[1].TypeText("02:30 PM");
            Page.ShiftTabPage.BreakToTime[1].TypeText("03:30 PM");
            Thread.Sleep(3000);
            List<string> getShiftNameList = new List<string>();
            getShiftNameList = Page.ShiftTabPage.GetShiftDetails("Monday", "Creating Break in Shift");
            if (getShiftNameList.Count > 1)
            {
                Assert.Fail("Shift created multiple times");
            }
            else
            {
                Assert.True(true, "Shift created multiple times");
            }
            List<string> getBreakTimeList = new List<string>();
            getBreakTimeList = Page.ShiftTabPage.ShiftTexts("Monday", "Creating Break in Shift");
            if (getBreakTimeList.Count > 2)
            {
                Assert.Fail("Break added more than 2 times");
            }
            else
            {
                Assert.True(true, "Break added more than 2 times");
            }
        }

        /// <summary>
        /// Test case 30356: RG:17908 : Verify whether shift created is reflected in other weekday when days are selected
        /// </summary>
        [TestCategory(TestType.regression, "TC08_VerifyShiftReflectedinOtherWeekDays")]
        [TestCategory(TestType.functional, "TC08_VerifyShiftReflectedinOtherWeekDays")]
        [Test]
        public void TC08_VerifyShiftReflectedinOtherWeekDays()
        {
            HtmlControl temp = Page.ShiftTabPage.AddShift;
            Page.ShiftTabPage.DeleteAllShifts("Monday");
            Page.ShiftTabPage.DeleteAllShifts("Tuesday");
            Page.ShiftTabPage.DeleteAllShifts("Wednesday");
            Page.ShiftTabPage.DeleteAllShifts("Thursday");
            Page.ShiftTabPage.DeleteAllShifts("Friday");

            Page.ShiftTabPage.AddShift.Click();            
            Page.ShiftTabPage.UnSelectDay(new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday });
            Page.ShiftTabPage.ShiftNameTextBox.TypeText("Shift for Multiple WeekDays");
            Page.ShiftTabPage.TargetProductionTextBox.TypeText("7373");
            Page.ShiftTabPage.ShiftFromTime.TypeText("10:00 AM");
            Page.ShiftTabPage.ShiftToTime.TypeText("07:00 PM");
            Page.ShiftTabPage.SaveShiftButton.TypeEnterKey();
            List<string> getShiftList = new List<string>();
            getShiftList = Page.ShiftTabPage.GetShiftDetails("Shift for Multiple WeekDays");
            if (getShiftList.Count == 5)
            {
                Assert.True(true, "Shift added to multiple weekdays"); 
            }
            else
            {
                Assert.Fail("Shifts not added to multiple weekdays");
            }
        }

        /// <summary>
        /// Test case 28456: RG:17908 : Verify localization
        /// </summary>
        [TestCategory(TestType.regression, "TC09_VerifyLocalization")]
        [TestCategory(TestType.functional, "TC09_VerifyLocalization")]
        [Test]
        public void TC09_VerifyLocalization()
        {
            //Pre-Condition of localization Language changing
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            //TestFixtureTearDown();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            //Changing language from English US to Deutsch
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("Deutsch", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            //TestFixtureTearDown();
            CloseBrowser();
            TestFixtureSetupBase();
            TestFixture();
            string text = Page.ShiftTabPage.ShiftTab.ChildNodes[0].Content;
            if (Page.ShiftTabPage.ShiftTab.ChildNodes[0].Content == "Verschuiving")
            {
                Assert.True(true, "Incorrect label name found when localized changed to Deutsch Language");
            }
            List<string> getShiftNameList = new List<string>();
            getShiftNameList = Page.ShiftTabPage.GetShiftDetails("Shift for Multiple WeekDays");
            if (getShiftNameList.Count > 0)
            {
                Assert.Fail("Shift name not changed when localization changed to Deutsch language");
            }
            else
            {
                Assert.True(true, "Shift name not changed when localization changed to Deutsch language");
            }
        }

        [TestCategory(TestType.regression, "TC10_PostLocalization")]
        [TestCategory(TestType.functional, "TC10_PostLocalization")]
        [Test]
        public void TC10_PostLocalization()
        {
            //Post Condition to revert back the localization
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.PlantSetupPage.GeneralTab.Click();
            Page.ShiftTabPage.LanguagePreferred.Focus();
            Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
            Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
            Page.ShiftTabPage.GeneralTabSaveButton.Click();
            //Telerik.ActiveBrowser.RefreshDomTree();
            //TestFixtureTearDown();
        }

        [TestCategory(TestType.regression, "TC11_VerifyShiftForLevel3")]
        [TestCategory(TestType.functional, "TC11_VerifyShiftForLevel3")]
        [Test]
        public void TC11_VerifyShiftForLevel3()
        {
            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestPE", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            if(Page.ShiftTabPage.IsShiftTabPresent)
            {
                Assert.Fail("Shift tab is present for user with level 3");
            }
        }

        [TestCategory(TestType.regression, "TC12_VerifyShiftForLevel4")]
        [TestCategory(TestType.functional, "TC12_VerifyShiftForLevel4")]
        [Test]
        public void TC12_VerifyShiftForLevel4()
        {
            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestPM", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.ShiftTabPage.ShiftTab.Click();
            if (null == Page.ShiftTabPage.AddShift)
            {
                Assert.Fail("Add shift button is not present with level 4 user");
            }
        }
    }
}
