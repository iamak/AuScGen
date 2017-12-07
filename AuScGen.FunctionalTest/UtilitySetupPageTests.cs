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
	public class UtilitySetupPageTest : TestBase
	{
		[TestFixtureSetUp]
		public void TestFixture()
		{
			Console.WriteLine("Test Fixture overridden");
			Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
			Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
			NavigateToUtilityTab();
		}

		private void CloseBrowser()
		{
			Telerik.Shutdown();
			Telerik.CleanUp();
		}

		[TestCategory(TestType.functional, "TC01_VerifySaveButtonFunctionality")]
		[TestCategory(TestType.regression, "TC01_VerifySaveButtonFunctionality")]
		[Test, Description("Test case 25616: RG: Plant Setup -> Utility Setup Page : Verify the Save button functionality.;" +
		"Test case 41107: RG Verify the Save button functionality. ;" +
		"Test case 25669: RG Plant Setup -> Utility Setup Page : Validate the fields and units for North America users. ;" +
		"Test case 25670: RG Plant Setup -> Utility Setup Page : Validate the fields and units for European users.")]
		public void TC01_VerifySaveButtonFunctionality()
		{
			UpdateRegionIdInDB(2);
			Thread.Sleep(2000);
			Telerik.ActiveBrowser.RefreshDomTree();
			Thread.Sleep(2000);
			Telerik.ActiveBrowser.Refresh();
			VerifyRegionChangeOnUI("°C", "€/gal", "kWh/kg", "€/kWh", "2");
			Thread.Sleep(2000);
			UpdateRegionIdInDB(1);
			Thread.Sleep(2000);
			Telerik.ActiveBrowser.RefreshDomTree();
			Page.UtilitySetupPage.BtnTabUtilities.Click();
			VerifyRegionChangeOnUI("°C", "$/gal", "kWh/kg", "$/kwh", "1");
			Page.UtilitySetupPage.AddUtilityDetails();
		}

		[TestCategory(TestType.functional, "TC02_VerifyAuditTablesForInsert")]
		[TestCategory(TestType.regression, "TC02_VerifyAuditTablesForInsert")]
		[Test, Description("Test case 28029: RG: Plant Setup -> Utility Setup Page : Validate Audit tables for Insert")]
		public void TC02_VerifyAuditTablesForInsert()
		{
			Thread.Sleep(2000);
			Page.UtilitySetupPage.AddUtilityDetails();
			Thread.Sleep(2000);
			if (null != Page.UtilitySetupPage.SuccessMessage)
			{
				if (!Page.UtilitySetupPage.SuccessMessage.BaseElement.InnerText
					.Equals(@"Saved Successfully"))
				{
					Assert.Fail("Incorrect error message is displayed,Expected: Saved Successfully"
									+ " but Actual:" + Page.UtilitySetupPage.SuccessMessage.BaseElement.InnerText);
				}
			}
			else
			{
				Assert.Fail("Error message is not displayed");
			}
		}
		/// <summary>
		/// Post Localization 
		/// </summary>
		[TestCategory(TestType.functional, "TC03_Verifythelocalization")]
		[TestCategory(TestType.regression, "TC03_Verifythelocalization")]
		[Test, Description("Test case 40788 RG: Verify the localization")]
		public void TC03_VerifyUtilitySetUpPageLocalization()
		{
			Thread.Sleep(2000);
			Page.PlantSetupPage.TopMainMenu.LogOut();
			Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
			Thread.Sleep(2000);
			Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
			Page.PlantSetupPage.GeneralTab.Click();
			Page.WasherGroupPage.LanguagePreferred.Focus();
			Page.WasherGroupPage.LanguagePreferred.SelectByText("Deutsch", true);
			Page.WasherGroupPage.LanguagePreferred.ScrollToVisible();
			Page.WasherGroupPage.GeneralTabSaveButton.Click();
			Telerik.ActiveBrowser.RefreshDomTree();
			CloseBrowser();
			TestFixtureSetupBase();
			TestFixture();
			Thread.Sleep(3000);
			if (Page.UtilitySetupPage.BtnTabUtilities.ChildNodes[0].Content == "Washer Groups")
			{
				Assert.Fail("Incorrect label displayed when localization changed to Deutsch language.Expected Utility tab name in - Deutsh lang, but Actual-"
					+ Page.UtilitySetupPage.BtnTabUtilities.ChildNodes[0].Content);
			}
		}

		/// <summary>
		/// Post Localization 
		/// </summary>
		[TestCategory(TestType.functional, "TC04_UtilitySetupPagePostLocalization")]
		[TestCategory(TestType.regression, "TC04_UtilitySetupPagePostLocalization")]
		[Test, Description("Test case 40788 RG: Verify the localization")]
		public void TC04_UtilitySetupPagePostLocalization()
		{
			//Post Condition to revert back the localization
			Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
			Page.PlantSetupPage.GeneralTab.Click();
			Page.ShiftTabPage.LanguagePreferred.Focus();
			Page.ShiftTabPage.LanguagePreferred.SelectByText("English US", true);
			Page.ShiftTabPage.LanguagePreferred.ScrollToVisible();
			Page.ShiftTabPage.GeneralTabSaveButton.Click();
		}

		private void UpdateRegionIdInDB(Int32 regionId)
		{
			string strCommand = string.Format("update TCD.Plant set RegionId='" + regionId + "' where EcolabAccountNumber='1'");
			DBValidation.UpdateData(strCommand);
			Thread.Sleep(2000);
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

		private void NavigateToUtilityTab()
		{
			Page.PlantSetupPage.TopMainMenu.NavigateToPlantSetupPage();
			Thread.Sleep(2000);
			Page.UtilitySetupPage.BtnTabUtilities.Click();
			Thread.Sleep(2000);
		}

		private void VerifyRegionChangeOnUI(string waterTemp, string waterPrice, string oilPrice, string electricity, string regionId)
		{
			Assert.True(Page.UtilitySetupPage.WaterFactorTemp(waterTemp, oilPrice), "Changed region id to- " + regionId + " Expected WaterFactorTemp value to- " + waterTemp);
			Assert.True(Page.UtilitySetupPage.WaterFactorPrice(waterPrice, electricity), "Changed region id to- " + regionId + " Expected WaterFactorPrice value to- " + waterPrice);
		}

		private void DBCheck(bool returnValue, Int32 ranColdTemp)
		{
			if (VerifyDB(ranColdTemp) == returnValue)
			{
				Assert.True(true, string.Format("Expected Office Phone Number - {0} which doesn't exist in DB", ranColdTemp));
			}
			else
			{
				Assert.Fail(string.Format("Expected Office Phone Number- {0} which doesn't exist in DB", ranColdTemp));
			}
		}

		private bool VerifyDB(long ranColdTemp)
		{
			string strCommand = string.Format("select Phone from TCD.UserMaster where LoginName = 'AutoTestAdmin' and Phone='{0}'", ranColdTemp);
			DataRowCollection rows = DBValidation.DataRows(strCommand);
			Thread.Sleep(2000);
			return rows.Count > 0;
		}
	}
}
