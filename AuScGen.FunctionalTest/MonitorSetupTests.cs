using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using NUnit.Framework;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;


namespace Ecolab.FunctionalTest
{
    class MonitorSetupTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {            
            Console.WriteLine("Test Fixture overridden");
            Precondition();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestPM", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
            Page.MonitorSetupPage.MonitorSetupTab.Click();
        }

        /// <summary>
        /// Test case 38683: Verify Login of the application
        /// Test case 39645: RG: Verify Add button
        /// Test case 39667: RG: Verify delete functionality
        /// Test case 39627: RG: Verify whether Monitor names and Dashboard names are unique
        /// Test case 39659: RG: Verify inline Edit of the application
        /// </summary>
        [Test]
        public void TC01_VerifyAddInlineEditDelete()
        {
            Random randomNumber = new Random();
            string monitorName = string.Format("DashboardTest{0}", randomNumber.Next());
            AddAndVerifyMonitor(monitorName, "Conventional");
            VerifyAddMonitor(monitorName);
            AddAndVerifyMonitor(monitorName, "Conventional");

            if(null != Page.MonitorSetupPage.DuplicateMessage)
            {
                string duplicatemessage = Page.MonitorSetupPage.DuplicateMessage.BaseElement.InnerText;
                {
                    if (!duplicatemessage.Equals("Dashboard name already exists"))
                    {
                        Assert.Fail("After addding monitor incorrect message is displayed, Expected: Saved successfully , Actual:{0}", duplicatemessage);
                    }
                }
            }
            else
            {
                Assert.Fail("Duplication message is not displayed on trying to add duplicate monitor");
            }

            Page.MonitorSetupPage.Cancel.Click();
            monitorName = InlineEdit(monitorName);
            Page.MonitorSetupPage.MonitorTabGrid.SelectedRows(monitorName).FirstOrDefault().GetButtonControls()[3].Click();
            DialogHandler.YesButton.Click();
            

            if (null != Page.MonitorSetupPage.Message)
            {
                string message = Page.MonitorSetupPage.Message.BaseElement.InnerText;
                {
                    if (!message.Equals("Deleted successfully"))
                    {
                        Assert.Fail("After addding monitor incorrect message is displayed, Expected: Saved successfully , Actual:{0}", message);
                    }
                }
            }
            else
            {
                Assert.Fail("Delete message is not displayed after monitor deletion");
            }

            if (DBValidation.DataRows("select * from tcd.MonitorSetUpMapping where MonitorId = '1' and IsDeleted='0'").Count > 0)
            {
                Assert.Fail("Database is not updated after monitor deletion");
            }

        }        

        private void Precondition()
        {
            if (DBValidation.DataRows("select * from tcd.Monitor where MONITORID = '1'").Count <= 0)
            {
                DBValidation.UpdateData("insert into tcd.Monitor (MONITORID,EcolabAccountNumber) values ('1','1')");
            }

            DBValidation.UpdateData("update tcd.MonitorSetUpMapping set IsDeleted = '1' where IsDeleted = '0'");

            DBValidation.UpdateData("update tcd.Dashboard set IsDeleted = '1' where IsDeleted = '0'");

            DBValidation.UpdateData("update tcd.DashboardHistory set IsDeleted = '1' where IsDeleted = '0'");
        }

        private void AddAndVerifyMonitor(string monitorName , string dashboradType)
        {
            Page.MonitorSetupPage.AddMonitor.Click();
            Page.MonitorSetupPage.DashboardName.TypeText(monitorName);
            Page.MonitorSetupPage.DashboardType.SelectByText(dashboradType, Timeout);
            Page.MonitorSetupPage.SelectMachineName(Page.MonitorSetupPage.GetMachineNames.FirstOrDefault());
            Page.MonitorSetupPage.SelectMonitorName(Page.MonitorSetupPage.GetMonitorNames.FirstOrDefault());
            Page.MonitorSetupPage.Save.Click();

        }

        private string InlineEdit(string monitorName)
        {
            Random randomNumber = new Random();
            string newMonitorName = string.Format("DashboardTest{0}", randomNumber.Next());
            Pages.CommonControls.EcolabDataGridItems selectedRow = Page.MonitorSetupPage.MonitorTabGrid.SelectedRows(monitorName).FirstOrDefault();
            selectedRow.GetButtonControls()[2].Click();
            Telerik.ActiveBrowser.RefreshDomTree();
            Element inlineDashboradName = selectedRow.GetEditableControls()[1].ChildNodes[1];
            (new HtmlControl(inlineDashboradName)).TypeText(newMonitorName);
            selectedRow.GetButtonControls().FirstOrDefault().Click();
            DataValidation(newMonitorName);
            return newMonitorName;
        }

        private void VerifyAddMonitor(string monitorName)
        {
            if (null != Page.MonitorSetupPage.Message)
            {
                string message = Page.MonitorSetupPage.Message.BaseElement.InnerText;
                {
                    if (!message.Equals("Saved successfully"))
                    {
                        Assert.Fail("After addding monitor incorrect message is displayed, Expected: Saved successfully , Actual:{0}", message);
                    }
                }
            }
            else
            {
                Assert.Fail("Success message is not displayed after monitor addition");
            }

            DataValidation(monitorName);           
        }

        private void DataValidation(string monitorName)
        {
            if (DBValidation.DataRows("select * from tcd.MonitorSetUpMapping where MonitorId = '1'").Count <= 0)
            {
                Assert.Fail("MonitorSetUpMapping table is not updated after monitor addition");
            }

            if (DBValidation.DataRows(string.Format("select * from tcd.Dashboard where DashBoardName = '{0}'and IsDeleted = '0'", monitorName)).Count <= 0)
            {
                Assert.Fail("Dashboard table is not updated after monitor addition");
            }

            if (DBValidation.DataRows(string.Format("select * from tcd.DashboardHistory where DashBoardName = '{0}' and IsDeleted = '0'", monitorName)).Count <= 0)
            {
                Assert.Fail("DashboardHistory table is not updated after monitor addition");
            }
        }
    }
}
