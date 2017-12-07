using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.Pages
{
    public class ChemicalsTabPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public ChemicalsTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "ChemicalsTab.xml");
        }

        public ChemicalsTabPage(List<object> utilsList)
            : base(utilsList, "ChemicalsTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ChemicalsTab.xml");
        }

        /// <summary>
        /// Verify the PlantSetUp SubMenu from SetUp tab of Main Menu
        /// </summary>
        public HtmlAnchor PlantSetupSubmenu
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("PlantSetUp");
            }
        }
        /// <summary>
        /// Verify Controller SetUP sub menu from setup tab of main menu
        /// </summary>
        public HtmlAnchor ControllerSetupSubmenu
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("ControllerSetUp");
            }
        }
        /// <summary>
        /// Verify the General Tab Active Status after clicking the PlantSetUp SubMenu from SetUp tab
        /// </summary>
        public HtmlControl GeneralTabActiveStatus
        {
            get
            {
                return GetHtmlControl<HtmlControl>("GeneralTabVerify");
            }
        }
        /// <summary>
        /// The table grid
        /// </summary>
        private CommonControls.EcolabDataGrid tableGrid;

        /// <summary>
        /// Contacts Table Grid
        /// </summary>
        public CommonControls.EcolabDataGrid ChemicalsTabGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "ChemicalsGridTable");
                return tableGrid;
            }
        }

        /// <summary>
        /// Verifies the success MSG.
        /// </summary>
        /// <returns></returns>
        public HtmlControl VerifySuccessMsg
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SuccessValidationMessage");
            }
        }
        /// <summary>
        /// Returns control for cost on InlineEdit Operation
        /// </summary>
        public HtmlInputText TxtCostInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtCostInlineEdit");
            }
        }
        /// <summary>
        /// Returns control of IncludeInCI on Inline Edit operation
        /// </summary>
        public HtmlInputCheckBox ChkIncludeCiInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("chkboxIncludeCIinlineEdit");
            }
        }
        /// <summary>
        /// Returns the cost textbox control while editing and updating the cost.
        /// </summary>
        public HtmlInputText TxtCostEditUpdate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtCostEdit");
            }
        }
        /// <summary>
        /// Returns the IncludeinCI check box control while editing and updating from popup 
        /// </summary>
        public HtmlInputCheckBox ChkIncludeCIEditUpdate
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("chkIncludeinCIEdit");
            }
        }
        /// <summary>
        /// Returns the Save Button control while performing Edit and Update operations
        /// </summary>
        public HtmlSpan BtnSaveEditButton
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnEditSave");
            }
        }
        /// <summary>
        /// Returns AddChemical button control
        /// </summary>
        public HtmlSpan BtnAddChemical
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnAddChemical");
            }
        }
        /// <summary>
        /// Returns the Cancel Button control while performing Edit/Update Operations
        /// </summary>
        public HtmlButton BtnCancelEditUpdate
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnEditCancel");
            }
        }
        /// <summary>
        /// Returns NameOrSkuNumber textbox control for adding
        /// </summary>
        public HtmlInputText TxtNameorSkuNumberAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNameSKUNumberAdd");
            }
        }

        /// <summary>
        /// Returns the Cost textbox control for adding
        /// </summary>
        public HtmlInputText TxtCostAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtCostAdd");
            }
        }
        /// <summary>
        /// Returns the IncludeInCI control for Adding
        /// </summary>
        public HtmlInputCheckBox ChkIncludeinCIAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("chkIncludeinCIAdd");
            }
        }

        /// <summary>
        /// Returns the controls for saving the data on Add operation
        /// </summary>
        public HtmlButton BtnSaveAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpdate");
            }
        }

        /// <summary>
        /// Returns the control for Cancel the data on Add operation
        /// </summary>
        public HtmlButton BtnCancelAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        /// <summary>
        /// Deleting Contact Details
        /// </summary>
        /// <param name="contactDetails">The Contact details.</param>
        public void DeleteContactsDetails(string text)
        {
            ConfirmDialog confirmDialog =
                          ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            ChemicalsTabGrid.SelectedRows(text)[0].GetButtonControls()[1].Click();
        }

        /// <summary>
        /// Returns Chemical tab header controls
        /// </summary>
        public HtmlControl ChemicalTabHeader
        {
            get
            {
                ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "ChemicalGridHeader").ChildNodes;
                foreach (Element e in childNodes)
                {
                    if (e.InnerText.Contains("SKU Number"))
                    {
                        return (new HtmlControl(e));
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Verifying the Column fields exists or not comparing with actual and expected columns of chemical grid.
        /// </summary>
        /// <returns></returns>
        public bool VerifyColumnsExist()
        {
            List<string> actualColumnList = new List<string>();
            List<string> expectedColumnList = new List<string>();
            expectedColumnList.Add("SKU Number");
            expectedColumnList.Add("Name");
            expectedColumnList.Add("Category");
            expectedColumnList.Add("Price($)");
            expectedColumnList.Add("Packaging Size");
            expectedColumnList.Add("Supplier");
            expectedColumnList.Add("Include in C&I");
            ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "ChemicalGridHeader").ChildNodes;
            foreach (Element e in childNodes)
            {
                actualColumnList.Add(e.InnerText);
            }
            actualColumnList.RemoveAt(0);
            List<string> diff = actualColumnList.Except(expectedColumnList).ToList();
            if (diff.Count > 0)
            {
                return  false;
            }
            else
            {
                return  true;
            }
        }

        /// <summary>
        /// Inline Editing and Updating the Records from Chemicals Gridview
        /// </summary>
        /// <param name="InlineEditChemicalsGrid"></param>
        public void InlineEditChemicalsGrid(string cost)
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            TxtCostInlineEdit.Value = "";
            TxtCostInlineEdit.Focus();
            TxtCostInlineEdit.MouseClick();
            objKeyPress.SetNumeric(cost);
            Thread.Sleep(1000);
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            ChkIncludeCiInlineEdit.Checked = true;
        }
        /// <summary>
        /// Updates the chemicals data
        /// </summary>
        public void UpdateChemicalsData(string updateCost)
        {
            Thread.Sleep(1000);
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtCostEditUpdate.Value = "";
            TxtCostEditUpdate.Focus();
            TxtCostEditUpdate.MouseClick();
            objKeyPress.SetNumeric(updateCost);
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            ChkIncludeCIEditUpdate.Checked = true;
        }
        /// <summary>
        /// Click on update button after verfication
        /// </summary>
        public void ClickOnUpdateButton()
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            BtnSaveEditButton.Focus();
            BtnSaveEditButton.MouseClick();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Enter);
        }
        /// <summary>
        /// Select the chemical name from dropdownlist
        /// </summary>
        /// <param name="txtChemName"></param>
        public void SelectChemicalName(string txtChemName)
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtNameorSkuNumberAdd.Value = "";
            TxtNameorSkuNumberAdd.Focus();
            TxtNameorSkuNumberAdd.MouseClick();
            objKeyPress.SetText(txtChemName);
            Thread.Sleep(2000);
            objKeyPress.KeyDown(System.Windows.Forms.Keys.Select);
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
        }
    }
}
