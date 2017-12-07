using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
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
	public class UserProfilePageTests : TestBase
	{
		[TestFixtureSetUp]
		public void TestFixture()
		{
			Console.WriteLine("Test Fixture overridden");
			Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
			Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
			//Page.PlantSetupPage.TopMainMenu.NavigateToWasherGroupsPage();
			Thread.Sleep(2000);
			Page.PlantSetupPage.TopMainMenu.NavigateToUserProfile();

		}

		private void CloseBrowser()
		{
			Telerik.Shutdown();
			Telerik.CleanUp();
		}

		[TestCategory(TestType.functional, "TC01_VerifyAndValidateMyProfilepage")]
		[TestCategory(TestType.regression, "TC01_VerifyAndValidateMyProfilepage")]
		[Test, Description("Test case 38757: RG: Verify the UI fields are displaying on 'MyProfile' page.;" +
		"Test case 38758: RG Verify the navigation when user clicks on 'Change Password' button ;" +
		"Test case 38788: RG Verify the 'Save' button functionaliy. ;" +
		"Test case 38793: RG Verify the 'Cancel' button Functionality.")]
		public void TC01_VerifyAndValidateMyProfilepage()
		{
			Random rand = new Random();
			long ranMobileNumber = rand.Next(100000000, 999999999) * 10;

			EnterData(ranMobileNumber, "English US");
			VerifyUI();
			Page.UserProfilePage.BtnCancelAdd.Click();
			DialogHandler.NoButton.Click();
			DBCheck(false, ranMobileNumber);

			EnterData(ranMobileNumber, "English US");
			Page.UserProfilePage.BtnCancelAdd.Click();
			DialogHandler.YesButton.Click();
            Thread.Sleep(2000);
			VerifySuccessMessage();
			DBCheck(true, ranMobileNumber);

			EnterData(ranMobileNumber, "English US");
            Thread.Sleep(2000);
			Page.UserProfilePage.BtnSaveAdd.Click();
            Thread.Sleep(2000);
			VerifySuccessMessage();
			DBCheck(true, ranMobileNumber);
		}

		[TestCategory(TestType.functional, "TC02_VerifyChangePasswordOnMyProfilepage")]
		[TestCategory(TestType.regression, "TC02_VerifyChangePasswordOnMyProfilepage")]
		[Test, Description("Test case 38811: RG Verify the 'Change Password' page functionlity.;" +
		"Test case 38817: RG Verify the 'Save' button functionality on 'change password' pop-up. ;")]
		public void TC02_VerifyChangePasswordOnMyProfilepage()
		{
			VerifyUI();
			Page.UserProfilePage.LnkChangePwd.Click();
			ChangePassword();
			Page.UserProfilePage.BtnCancelPassword.Click();

			Page.UserProfilePage.LnkChangePwd.Click();
			ChangePassword();
			Page.UserProfilePage.BtnSavePassword.Click();
			VerifyChangePasswordDB();
			if (VerifyChangePasswordDB() == true)
			{
				Assert.True(true, string.Format("Expected Password successfully changed to 'test'"));
			}
			else
			{
				Assert.Fail(string.Format("Password change was not successfully"));
			}
		}

		/// <summary>
		/// Post Localization 
		/// </summary>
		[TestCategory(TestType.functional, "TC03_Verifythelocalization")]
		[TestCategory(TestType.regression, "TC03_Verifythelocalization")]
		[Test, Description("Test case 40788: RG Verify the localization")]
		public void TC03_Verifythelocalization()
		{
			Random rand = new Random();
			long ranMobileNumber = rand.Next(100000000, 999999999) * 10;
			Thread.Sleep(2000);
			Page.UserProfilePage.LanguagePreferred.SelectByText("Deutsch", Timeout);
			Page.UserProfilePage.BtnSaveAdd.Click();
			Page.LoginPage.TopMainMenu.LogOut();
			CloseBrowser();
			TestFixtureSetupBase();
			Thread.Sleep(2000);
			TestFixture();
			Thread.Sleep(2000);

			if (Page.UserProfilePage.BtnSaveAdd.InnerText != "Opslaan")
			{
				Assert.Fail("Actual Found -" + Page.UserProfilePage.BtnSaveAdd.InnerText + "; Expected Deutch Language change is not reflecting for 'Save' Button 'Opslaan'");
			}
			Page.UserProfilePage.LanguagePreferred.SelectByText("English US", Timeout);
			Page.UserProfilePage.BtnSaveAdd.Click();
			Page.LoginPage.TopMainMenu.LogOut();
			CloseBrowser();
		}

		private bool VerifyDB(long ranMobileNumber)
		{
			string strCommand = string.Format("select Phone from TCD.UserMaster where LoginName = 'AutoTestAdmin' and Phone='{0}'", ranMobileNumber);
			DataRowCollection rows = DBValidation.DataRows(strCommand);
			Thread.Sleep(2000);
			return rows.Count > 0;
		}

		private void VerifyUI()
		{
			if (null != Page.UserProfilePage.UserRole)
			{
				string userRole = Page.UserProfilePage.UserRole.BaseElement.InnerText;
				if (!userRole.Equals("Adminstrator"))
				{
					Assert.Fail("User role is incorrectly displayed in profile page, Expected role:Adminstrator, Actual:{0}", userRole);
				}
			}
			else
			{
				Assert.Fail("User role is not displayed in the profile page Expected role:Adminstrator");
			}

			if (null != Page.UserProfilePage.LnkChangePwd)
			{
				if (!Page.UserProfilePage.LnkChangePwd.IsVisible())
				{
					Assert.Fail("Change Password button is not displayed on the My Profile screen.");
				}
			}
			else
			{
				Assert.Fail("User role is not displayed in the profile page Expected role:Adminstrator");
			}
		}

		private void EnterData(long ranMobileNumber, string language)
		{
			//Page.PlantSetupPage.TopMainMenu.UserProfileLink.DeskTopMouseClick();
			Page.UserProfilePage.DdlTitle.SelectByText("Mr.", Timeout);
			Page.UserProfilePage.TxtFirstName.TypeText("AutoTestAdmin");
			Page.UserProfilePage.TxtLastName.TypeText("AutoTestAdmin");
			Page.UserProfilePage.LanguagePreferred.SelectByText(language, Timeout);
			Page.UserProfilePage.TxtEmail.TypeText("AutoTestAdmin@gmail.com");
			Page.UserProfilePage.TxtOfficePhone.TypeText(ranMobileNumber.ToString());
			Page.UserProfilePage.TxtMobilePhone.TypeText("9848230919");
			Page.UserProfilePage.TxtFaxNo.TypeText("9848230919");
		}

		private void VerifySuccessMessage()
		{
			if (null != Page.UserProfilePage.SuccessMsg)
			{
				string message = Page.UserProfilePage.SuccessMsg.BaseElement.InnerText;
				if (!message.Equals("Saved successfully"))
				{
					Assert.Fail("Incorrect error message is displayed,Expected:Saved successfully but Actual:{0}", message);
				}
			}
			else
			{
				Assert.Fail("Success message is not displayed");
			}
		}

		private void DBCheck(bool returnValue, long ranMobileNumber)
		{
			if (VerifyDB(ranMobileNumber) == returnValue)
			{
				Assert.True(true, string.Format("Expected Office Phone Number - {0} which doesn't exist in DB", ranMobileNumber));
			}
			else
			{
				Assert.Fail(string.Format("Expected Office Phone Number- {0} which doesn't exist in DB", ranMobileNumber));
			}
		}

		private void ChangePassword()
		{
			Page.UserProfilePage.TxtOldPassword.DeskTopMouseClick();
			Thread.Sleep(2000);
			KeyBoardSimulator.SetText("test");
			Page.UserProfilePage.TxtNewPassword.Text = "test";
			Page.UserProfilePage.TxtConfirmPassword.Text = "test";
		}

		private bool VerifyChangePasswordDB()
		{
			string strCommand = string.Format("select Password from TCD.UserMaster where LoginName = 'AutoTestAdmin' and Email= 'AutoTestAdmin@gmail.com'");
			DataRowCollection rows = DBValidation.DataRows(strCommand);
			Thread.Sleep(2000);
			return rows.Count > 0;
		}
	}
}
