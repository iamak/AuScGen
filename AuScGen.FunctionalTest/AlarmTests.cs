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
    public class AlarmTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }

        [TestCategory(TestType.regression, "TC01_Verify_AddAlarm")]
        [TestCategory(TestType.functional, "TC01_Verify_AddAlarm")]
        [Test, Description("Test Case 38700:RG Verify whether Active alarm flashes immediately")]
        public void TC01_Verify_AddAlarm()
        {
            int nAlarmCount = Convert.ToInt32(Page.AlarmPage.AlarmCountContainer.InnerText.ToString());
            int nAferInsertAlarmCount = 0;
            bool bDBInsert = false;
            string strCommand = "Insert into [TCD].[AlarmData] (EcoalabAccountNumber,AlarmCode,ControllerTypeId,StartDate,GroupId,MachineNumber,IsActive,UserId) values(1,143,1,'2014-11-30',3,2,1,1)";
            bDBInsert = DBValidation.InsertData(strCommand);
            if (bDBInsert)
            {
                Assert.True(true, "Alarm record inserted in DB with Code:" + "143");
            }
            else
            {
                Assert.Fail("Alarm record not inserted in DB");
            }
            Thread.Sleep(10000);
            nAferInsertAlarmCount = Convert.ToInt32(Page.AlarmPage.AlarmCountContainer.InnerText);
            if(nAferInsertAlarmCount > nAlarmCount)
            {
                Assert.True(true, "Alarm record inserted in DB with Code:" + "143");
            }
            else
            {
                Assert.Fail("Alarm not reflected in UI after inserting in DB IntialAlarmCount: " + nAlarmCount + "AfterInsertAlarmCount: " + nAferInsertAlarmCount);
            }
        }

        [TestCategory(TestType.functional, "TC02_Verify_AlarmData")]
        [TestCategory(TestType.regression, "TC02_Verify_AlarmData")]
        [Test, Description("Test Case 38713:RG Verify whether Alarm related information i.e displayed in the popup are same as in the database")]
        public void TC02_Verify_AlarmData()
        {
            Page.AlarmPage.AlarmIcon.Click();
            Thread.Sleep(3000);
            string alarmNumber = Page.AlarmPage.AlarmTableGrid.SelectedRows("143")[0].GetColumnValues()[0];
            Thread.Sleep(3000);
            if(alarmNumber == "143")
            {
                Assert.True(true, "Alarm record inserted in DB with Code:" + "143");
            }
            else
            {
                Page.AlarmPage.Close.Click();
                Assert.Fail("AlarmNumber not reflected in UI after inserting in DB IntialAlarmCount: " + "143" + "AfterInsertAlarmCount: " + alarmNumber);
            }
            Thread.Sleep(2000);
            Page.AlarmPage.Close.Click();
        }

        [TestCategory(TestType.functional, "TC03_Verfiy_InActiveAlarm")]
        [TestCategory(TestType.regression, "TC03_Verfiy_InActiveAlarm")]
        [Test, Description("Test Case 38732:RG Verify whether Notification number gets cleared after alarm has viewed")]
        public void TC03_Verfiy_InActiveAlarm()
        {
            int nAlarmCount = Convert.ToInt32(Page.AlarmPage.AlarmCountContainer.InnerText.ToString());
            bool bDBInsert = false;
            bool bDBDelete = false;
            string strCommand = "update [TCD].[AlarmData] set IsActive=0 where AlarmCode = 143";
            bDBInsert = DBValidation.InsertData(strCommand);
            if (bDBInsert)
            {
                Assert.True(true, "Column value set to inactive = 0 in DB for Alarm Code:" + "143");
            }
            else
            {
                Assert.Fail("Column value not set to inactive = 0 in DB for Alarm Code:" + "143");
            }
            Thread.Sleep(10000);
            int nAferInsertAlarmCount = Convert.ToInt32(Page.AlarmPage.AlarmCountContainer.InnerText.ToString());
            if (nAferInsertAlarmCount < nAlarmCount)
            {
                Assert.True(true, "Alarm is inactive in UI with Code:" + "143");
            }
            else
            {
                Assert.Fail("Alarm is not inactive in UI with Code:" + "143");
            }
            string strDelCommand = "delete from [TCD].[AlarmData] where AlarmCode = 143";
            bDBDelete = DBValidation.InsertData(strDelCommand);
            if (bDBInsert)
            {
                Assert.True(true, "Column value set to inactive = 0 in DB for Alarm Code:" + "143");
            }
            else
            {
                Assert.Fail("Column value not set to inactive = 0 in DB for Alarm Code:" + "143");
            }

        }

        [TestCategory(TestType.functional, "TC04_Verify_Localization_Alarm")]
        [TestCategory(TestType.regression, "TC04_Verify_Localization_Alarm")]
        [Test, Description("Test Case 38739:RG Verify Localization")]
        public void TC04_Verify_Localization_Alarm()
        {
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("Deutsch", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.AlarmPage.AlarmIcon.Click();
            if(Page.AlarmPage.AlarmPopupTitle.BaseElement.InnerText != "")
            {
                PostLocalizationAlarm();
                Assert.Fail(string.Format("Incorrect Label is displayed in Alarms - {0} when localization changed to Detsuch, Expected - aktive Alarme", Page.AlarmPage.AlarmPopupTitle.BaseElement.InnerText));
            }
            PostLocalizationAlarm();
        }

        private void PostLocalizationAlarm()
        {
            Page.PlantSetupPage.GeneralTab.Click();
            Page.SensorTabPage.Language.Focus();
            Page.SensorTabPage.Language.SelectByText("English US", true);
            Page.SensorTabPage.Language.ScrollToVisible();
            Page.SensorTabPage.GeneralTabSave.Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Page.AlarmPage.AlarmIcon.Click();
            if (Page.AlarmPage.AlarmPopupTitle.BaseElement.InnerText != "Active Alarms")
            {
                Assert.Fail(string.Format("Incorrect Label is displayed in Alarms - {0} when localization changed to English, Expected - Active Alarms", Page.AlarmPage.AlarmPopupTitle.BaseElement.InnerText));
            }
        }
    }
}
