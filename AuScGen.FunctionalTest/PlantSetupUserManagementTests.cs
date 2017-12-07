using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using NUnit.Framework;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Threading;
using System.Data;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupUserManagementTests : TestBase
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

            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTab.Click();

        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        //protected override void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    base.TestFixtureTearDown();
        //}

        [TestCategory(TestType.functional, "TC01_VerifyAddUser")]
        [TestCategory(TestType.regression, "TC01_VerifyAddUser")]
        [Test]
        public void TC01_VerifyAddUser()
        {
            Page.UserManagement.AddUser.Click();
            if(!Page.UserManagement.txtFirstName.IsEnabled)
            {
                Assert.Fail("Add User popup not appeared");
            }
            Page.UserManagement.Cancel.Click();
        }

        [TestCategory(TestType.functional, "TC02_AddUser")]
        [TestCategory(TestType.regression, "TC02_AddUser")]
        [Test]
        public void TC02_AddUser()
        {
            Page.UserManagement.AddingUser("Auto", "Test12", "Auto1", "auto@gmail.com", "9069675467", "12345", "Adminstrator");
            Thread.Sleep(2000);
            if (null != Page.UserManagement.userManagementMsgDiv)
            {
                if (!Page.UserManagement.userManagementMsgDiv.BaseElement.InnerText
                    .Equals(@"User created successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        [TestCategory(TestType.functional, "TC03_VerifyUserInGrid")]
        [TestCategory(TestType.regression, "TC03_VerifyUserInGrid")]
        [Test]
        public void TC03_VerifyUserInGrid()
        {
            Page.UserManagement.AddingUser("Auto", "Test", "Auto2", "test@gmail.com", "9069675467", "1234", "Adminstrator");
            Thread.Sleep(2000);
            if (null != Page.UserManagement.userManagementMsgDiv)
            {
                if (!Page.UserManagement.userManagementMsgDiv.BaseElement.InnerText
                    .Equals(@"User created successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            if (!Page.UserManagement.isRecordExist("Auto2"))
            {
                Assert.Fail("Auto2" + "record not displayed in user management grid");
            }
        }

        [TestCategory(TestType.functional, "TC04_VerifyUpdateButton")]
        [TestCategory(TestType.regression, "TC04_VerifyUpdateButton")]
        [Test]
        public void TC04_VerifyUpdateButton()
        {
            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[4].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[4].DeskTopMouseClick();
            Thread.Sleep(2000);
            if(!Page.UserManagement.txtEditFirstName.IsVisible())
            {
                Assert.Fail("User Management edit popup not displayed on clicking on update in grid");
            }
            Page.UserManagement.Cancel.Click();
            Page.UserManagement.AddUser.ScrollToVisible();
        }

        [TestCategory(TestType.functional, "TC05_VerifyDeleteButton")]
        [TestCategory(TestType.regression, "TC05_VerifyDeleteButton")]
        [Test]
        public void TC05_VerifyDeleteButton()
        {
            string strUserId = Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetColumnValues()[3];
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[3].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.NoButton.Click();
            Thread.Sleep(2000);
            if (Page.UserManagement.isRecordExist(strUserId) != true)
            {
                Assert.Fail("User deleted on clicking on NO button in delete confirmation popup");
            }
            Page.UserManagement.AddUser.ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[3].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto2")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.UserManagement.isRecordExist(strUserId) == true)
            {
                Assert.Fail("User not deleted on clicking on YES button in delete confirmation popup");
            }
            Page.UserManagement.AddUser.ScrollToVisible();
        }

        [TestCategory(TestType.functional, "TC06_VerifyInLineEditing")]
        [TestCategory(TestType.regression, "TC06_VerifyInLineEditing")]
        [Test]
        public void TC06_VerifyInLineEditing()
        {
            string strFirtName = Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetColumnValues()[1];
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[2].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[2].DeskTopMouseClick();
            Page.UserManagement.InlineEditingUser("Auto1", "InLine");
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[0].DeskTopMouseClick();
            if (null != Page.UserManagement.userManagementMsgDiv)
            {
                if (!Page.UserManagement.userManagementMsgDiv.BaseElement.InnerText
                    .Equals(@"User updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            if (Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetColumnValues()[1] != "InLine")
            {
                Assert.Fail("Updated value not displayed in grid after inline editing");
            }
            Page.UserManagement.AddUser.ScrollToVisible();
            
        }

        [TestCategory(TestType.functional, "TC07_VerifyUpdateFunctionality")]
        [TestCategory(TestType.regression, "TC07_VerifyUpdateFunctionality")]
        [Test]
        public void TC07_VerifyUpdateFunctionality()
        {
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[4].MouseClick();
            Page.UserManagement.UpdatingUser("Update", "Auto", "Auto1", "auto@gmail.com", "9069675467", "123456", "Adminstrator");
            if (null != Page.UserManagement.userManagementMsgDiv)
            {
                if (!Page.UserManagement.userManagementMsgDiv.BaseElement.InnerText
                    .Equals(@"User updated successfully"))
                {
                    Assert.Fail("Incorrect error message is displayed Expected - User updated successfully , Actual - " + Page.UserManagement.userErrorMsg.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
        }

        [TestCategory(TestType.functional, "TC08_VerifyDeletedUserLogin")]
        [TestCategory(TestType.regression, "TC08_VerifyDeletedUserLogin")]
        [Test]
        public void TC08_VerifyDeletedUserLogin()
        {
            Page.UserManagement.AddingUser("DeleteUser", "Test123", "Delete1", "delete@gmail.com", "9069675467", "12345", "Adminstrator");
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTable.SelectedRows("DeleteUser")[0].GetButtonControls()[3].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("DeleteUser")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.UserManagement.isRecordExist("Delete1") == true)
            {
                Assert.Fail("User not deleted on clicking on YES button in delete confirmation popup");
            }
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Thread.Sleep(2000);

            Page.LoginPage.ClickLoginLink();
            Page.LoginPage.EnterLoginCredentials("Delete1", "12345");
            Page.LoginPage.ClickLoginButton();
            if (null != Page.UserManagement.Formerror)
            {
                if (!Page.UserManagement.Formerror.BaseElement.InnerText
                    .Equals(@"The username or password is incorrect."))
                {
                    Assert.Fail("Incorrect error message is displayed");
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTab.Click();        
        }

        [TestCategory(TestType.functional, "TC09_VerifyCreatedUserLogin")]
        [TestCategory(TestType.regression, "TC09_VerifyCreatedUserLogin")]
        [Test]
        public void TC09_VerifyCreatedUserLogin()
        {
            Page.UserManagement.AddingUser("CreatedUser", "Test123", "create", "create@gmail.com", "9069675467", "12345", "Adminstrator");
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Thread.Sleep(2000);
            Page.LoginPage.VerifyLogin("create", "12345");
            Thread.Sleep(2000);
            if (Page.PlantSetupPage.TopMainMenu.IsNavigateToPlantSetupPageAvailable())
            {
                Assert.Fail("Login failed with created user - " + "create");
            }
            Page.PlantSetupPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTab.Click();
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTable.SelectedRows("CreatedUser")[0].GetButtonControls()[3].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("CreatedUser")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.UserManagement.isRecordExist("create") == true)
            {
                Assert.Fail("User not deleted on clicking on YES button in delete confirmation popup");
            }
        }

        [TestCategory(TestType.functional, "TC10_AddUser_Duplicate")]
        [TestCategory(TestType.regression, "TC10_AddUser_Duplicate")]
        [Test]
        public void TC10_AddUser_Duplicate()
        {
            Page.UserManagement.AddingUser("Auto", "Test12", "Auto1", "auto@gmail.com", "9069675467", "12345", "Adminstrator");
            Thread.Sleep(2000);
            if (null != Page.UserManagement.userErrorMsg)
            {
                if (!Page.UserManagement.userErrorMsg.BaseElement.InnerText
                    .Equals(@"User already exists.Please use a different User Id"))
                {
                    Assert.Fail("Incorrect error message is displayed Expected - User already exists.Please use a different User Id , Actual - " + Page.UserManagement.userErrorMsg.BaseElement.InnerText);
                }
            }
            else
            {
                Assert.Fail("Error message is not displayed");
            }
            Page.UserManagement.Cancel.Click();
            Thread.Sleep(2000);
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[3].ScrollToVisible();
            Page.UserManagement.UserManagementTable.SelectedRows("Auto1")[0].GetButtonControls()[3].DeskTopMouseClick();
            Thread.Sleep(2000);
            DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
            if (Page.UserManagement.isRecordExist("Auto1") == true)
            {
                Assert.Fail("User not deleted on clicking on YES button in delete confirmation popup");
            }
        }


    }
}
