using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using Ecolab.Pages.CommonControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ecolab.FunctionalTest
{
    public class PlantSetupChemicalsTests : TestBase
    {
        private static string testData = TestDataPath + "testdata.xlsx";
        
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
        }
        [TestCategory(TestType.bvt, "TC01_VerifyUserNavigateToChemicalsSetUpTab")]
        [Test, Description("Test case 20426: RG:17902 : Plant Setup -> Chemicals Setup Page:Verify if the User could navigate to Chemical Setup tab.")]
        public void TC01_VerifyUserNavigateToChemicalsSetUpTab()
        {
            HtmlControl ctrl = Page.ChemicalsTabPage.GeneralTabActiveStatus;
            if (ctrl.ChildNodes[0].InnerText == "General")
            {
                Assert.True(Page.ChemicalsTabPage.GeneralTabActiveStatus.ChildNodes[0].InnerText == "General", "Verified General tab details selected by default");
            }
            else
            {
                Assert.Fail("Verification Failed, General Tab details not selected by default");
            }
            Page.PlantSetupPage.ChemicalsTab.Click();
        }

        [TestCategory(TestType.regression, "TC02_ValidateChemicalInlineEdit")]
        [TestCategory(TestType.functional, "TC02_ValidateChemicalInlineEdit")]
        [Test, Description("Test case 20728: RG:17902 : Plant Setup -> Chemicals Setup Page:Validate inline edit")]
        public void TC02_ValidateChemicalInlineEdit()
        {
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            if (Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count > 0)
            {
                Assert.True(Page.ChemicalsTabPage.VerifyColumnsExist(), "Chemical grid header field column names not matching with expected header names");
                Telerik.ActiveBrowser.RefreshDomTree();
                string actualCost = Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[0].GetColumnValues()[4];
                Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                Page.ChemicalsTabPage.InlineEditChemicalsGrid("1236.00");
                Thread.Sleep(2000);
                Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.FirstOrDefault().GetButtonControls()[0].Click();
                Assert.True(Page.ChemicalsTabPage.VerifySuccessMsg.IsVisible(), "Chemical details failed to update");
                Telerik.ActiveBrowser.RefreshDomTree();
                Page.PlantSetupPage.ChemicalsTab.Click();
                Thread.Sleep(2000);
                string expectedCost = Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[0].GetColumnValues()[4];
                Assert.True(expectedCost == "1236.00", "Failed to update the cost or Cost value is not matching with edited row");
                string strCommand = "select * from TCD.productDatamapping where Cost='" + expectedCost + "'  and Is_Deleted = 0 ";
                DataSet ds = DBValidation.GetData(strCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Assert.True(true, "Verified the previous value of cost in DB which doesn't exist");
                }
                else
                {
                    Assert.Fail("Cost value not updated in DB expected cost is " + expectedCost);
                }
            }
            else
            {
                Assert.Fail("Chemical details not found in the existing grid table");
            }

        }

        [TestCategory(TestType.regression, "TC03_VerifyAddChemicalFunctionality")]
        [Test, Description("Test case 29880: RG:27284 :Verify Add chemical functionality")]
        public void TC03_VerifyAddChemicalFunctionality()
        {
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            bool verifyAddChemicalButton = Page.ChemicalsTabPage.BtnAddChemical.IsVisible();
            if (verifyAddChemicalButton == false)
            {
                Assert.Fail("AddChemical button not found");
            }
            Page.ChemicalsTabPage.BtnAddChemical.Click();
            Page.ChemicalsTabPage.SelectChemicalName("In");
            string verifyCostValue = Page.ChemicalsTabPage.TxtCostAdd.Value;
            if (verifyCostValue == null)
            {
                Page.ChemicalsTabPage.TxtCostAdd.SetText("0");
            }
            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Page.ChemicalsTabPage.ChkIncludeinCIAdd.Checked = true;
            Page.ChemicalsTabPage.BtnSaveAdd.Focus();
            Page.ChemicalsTabPage.BtnSaveAdd.MouseClick();
            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
            Thread.Sleep(3000);
            Assert.True(Page.ChemicalsTabPage.VerifySuccessMsg.IsVisible(), "Failed to add the chemical");
        }

        [TestCategory(TestType.regression, "TC04_VerifyDeleteFunctionality")]
        [TestCategory(TestType.functional, "TC04_VerifyDeleteFunctionality")]
        [Test, Description("Test case 28080: RG:27284 :Verify the Delete functionality")]
        public void TC04_VerifyDeleteFunctionality()
        {
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            if (Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count > 0)
            {
                ReadOnlyCollection<string> skuValue = Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[0].GetColumnValues();
                Page.ChemicalsTabPage.DeleteContactsDetails(skuValue[1]);
                HtmlControl Ctrl_SuccessMsg = Page.ChemicalsTabPage.VerifySuccessMsg;
                Thread.Sleep(2000);
                Assert.IsTrue(Ctrl_SuccessMsg.BaseElement.InnerText.Contains("Chemical Deleted Successfully"), "Success message not matched");
                Telerik.ActiveBrowser.RefreshDomTree();
                EcolabDataGridItems deletedRow = Page.ChemicalsTabPage.ChemicalsTabGrid.GetRow(skuValue[1]);
                Assert.True(deletedRow == null, "Chemical details not found in chemicals grid");
                Thread.Sleep(2000);
                string strCommand = "select * from TCD.productDatamapping pd inner join TCD.productdatamappinghistory ph on pd.productid = ph.productid where pd.sku='" + skuValue[1] + "' and pd.cost=ph.cost and pd.Is_Deleted =" + '1';
                DataSet ds = DBValidation.GetData(strCommand);
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    Assert.True(true, skuValue[1] + "Chemical details deleted and updated in history successfully in DB");
                }
                else
                {
                    Assert.Fail(skuValue[1] + " Chemical details not updated in DB");
                }
            }
            else
            {
                Assert.Fail("Chemical details not found in the existing grid table");
            }
        }

        [TestCategory(TestType.regression, "TC05_VerifyUpdateandInLineEditButton")]
        [Test, Description("Test case 26329: RG:17902 : Plant Setup -> Chemicals Setup Page:Verify Update and Inline edit button")]
        public void TC05_VerifyUpdateandInLineEditButton()
        {
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            int nCount = Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count;
            if (nCount > 0)
            {
                for (int i = 0; i <= nCount - 1; i++)
                {
                    Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[i].GetButtonControls()[0].Click();
                    Assert.True(Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[i].GetButtonControls()[0].IsVisible(),
                                "Verified Inline Edit and Update button for every row");
                    Page.ChemicalsTabPage.InlineEditChemicalsGrid("73");
                    Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[i].GetButtonControls()[0].Click();
                    Assert.True(Page.ChemicalsTabPage.VerifySuccessMsg.IsVisible(), "Chemical updated Successfully");
                }
            }
            else
            {
                Assert.Fail("Chemical details not found in the existing grid table");
            }
        }

        [TestCategory(TestType.regression, "TC06_VerifySortingFeatureonChemicalsHeader")]
        [Test, Description("Test case 28900: RG:27284 :Verify whether the application is having sorting feature on Chemicals Page or not")]
        public void TC06_VerifySortingFeatureonChemicalsHeader()
        {
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            List<string> actualValues = new List<string>();
            List<string> expectedValues = new List<string>();
            if (Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count > 0)
            {
                for (int i = 0; i <= Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count - 1; i++)
                {
                    actualValues.Add(Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[i].GetColumnValues()[1]);
                }
                actualValues.Sort();
                Page.ChemicalsTabPage.ChemicalTabHeader.Click();
                int chkCount = Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count;
                for (int i = 0; i <= chkCount - 1; i++)
                {
                    expectedValues.Add(Page.ChemicalsTabPage.ChemicalsTabGrid.Rows[i].GetColumnValues()[1]);
                }
                int result = string.Compare(actualValues[0] + actualValues[1], expectedValues[0] + expectedValues[1]);
                if (result == 0)
                {
                    Assert.True(true, "Verified chemicals tab grid header have sorting feature");
                }
                else
                {
                    Assert.Fail("Sorting feature not found in chemicals tab grid header");
                }
            }
            else
            {
                Assert.Fail("Chemical details not found in the existing grid table");
            }

        }

        [TestCategory(TestType.regression, "TC07_VerifySaveButtonOnEditFunctionality")]
        [Test, Description("Test case 20430: RG:27284 :Verify Save button functionality while editing")]
        public void TC07_VerifySaveButtonOnEditFunctionality()
        {
            bool saveButtonActiveStatus;
            Page.PlantSetupPage.ChemicalsTab.Click();
            Thread.Sleep(3000);
            if (Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.Count > 0)
            {
                Page.ChemicalsTabPage.ChemicalsTabGrid.Rows.FirstOrDefault().GetButtonControls()[2].Click();
                saveButtonActiveStatus = Page.ChemicalsTabPage.BtnSaveEditButton.IsActiveElement;
                if (saveButtonActiveStatus == true)
                {
                    Assert.Fail("Verified save button in enabled mode.Expected is disabled mode");
                }
                Page.ChemicalsTabPage.UpdateChemicalsData("434");
                Telerik.ActiveBrowser.RefreshDomTree();
                bool saveButtonEnabled = Page.ChemicalsTabPage.BtnSaveEditButton.IsEnabled;
                if (saveButtonEnabled == true)
                {
                    Page.ChemicalsTabPage.ClickOnUpdateButton();
                }
                else
                {
                    Assert.Fail("Verified save button in disabled mode.Expected is Enabled");
                }
                if (Page.ChemicalsTabPage.ChemicalsTabGrid.GetRow("434.00").GetColumnValues()[4].ToString() != null)
                {
                    Assert.True(true, "Chemical details not found in the existing grid table");
                }
                else
                {
                    Assert.Fail("Chemical details not found in the existing grid table");
                }
            }
            else
            {
                Assert.Fail("Chemical details not found in the existing grid table");
            }
        }

        [TestCategory(TestType.functional, "TC08_VerifyListOfChemicalsRegionWise")]
        [Test, Description("Test case 28666: RG:27284 :Verify the list of chemicals displaying based on region (Doing negative testing with region id : 1;Test case 28081: RG:27284")]
        public void TC08_VerifyListOfChemicalsRegionWise()
        {
            string setRegion = string.Empty;
            Page.PlantSetupPage.ChemicalsTab.Click();
            string strPlantCmd = "select RegionId from TCD.plant where Is_Deleted =" + '0';
            DataSet ds = DBValidation.GetData(strPlantCmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Plant details not found in DB");
            }
            else
            {
                Assert.Fail("Plant details not found in DB");
            }
            string getRegionId = ds.Tables[0].Rows[0]["RegionId"].ToString();
            string strRegionCmd = "select Replace(((rtrim(ltrim(Name)) +'('+ ltrim(rtrim(SKU) +')'))),' ','') as NameORSKURegion from TCD.Productmaster where  regionid=" + ds.Tables[0].Rows[0]["RegionId"].ToString();
            DataSet dsregion = DBValidation.GetData(strRegionCmd);
            if (dsregion.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Data not found in Product Master table using regionid: " + getRegionId);
            }
            else
            {
                Assert.Fail("Data not found in Product Master table using regionid: " + getRegionId);
            }
            Page.ChemicalsTabPage.BtnAddChemical.Click();
            Page.ChemicalsTabPage.SelectChemicalName("In");
            Thread.Sleep(3000);
            string chemicalName = Page.ChemicalsTabPage.TxtNameorSkuNumberAdd.Value.Trim();
            DataRow[] foundRows = dsregion.Tables[0].Select("NameORSKURegion ='" + chemicalName.Replace(" ", "").Trim() + "'");
            int count = foundRows.Length;
            if (count >= 1)
            {
                Assert.True(true, chemicalName + "Chemical name not found in DB");
            }
            else
            {
                Assert.Fail(chemicalName + " Chemical name not found in DB");
            }
            //Negative testing checking with RegionId.based on plant table regionId we will set the regionId for negative testing.
            if(getRegionId == "1")
            {
                 setRegion = "2";
            }
            else
            {
                setRegion = "1";
            }
            string strRegionOneCmd = "select Replace(((rtrim(ltrim(Name)) +'('+ ltrim(rtrim(SKU) +')'))),' ','') as NameORSKURegion from TCD.Productmaster where regionid=" + setRegion;
            DataSet dsregionOne = DBValidation.GetData(strRegionOneCmd);
            if (dsregionOne.Tables[0].Rows.Count > 0)
            {
                Assert.True(true, "Data not found in Product Master table for regionid: 1");
            }
            else
            {
                Assert.Fail("Data not found in Product Master table for regionid: 1");
            }
            Thread.Sleep(3000);
            DataRow[] foundRow = dsregionOne.Tables[0].Select("NameORSKURegion ='" + chemicalName.Replace(" ", "").Trim() + "'");
            int counts = foundRow.Length;
            if (counts >= 1)
            {
                Assert.Fail(chemicalName + "chemical name related to region two found in Region one DB table");
            }
            else
            {
                Assert.True(true, chemicalName + "chemical name related to region two found in Region one DB table");
            }
        }
    }
}
