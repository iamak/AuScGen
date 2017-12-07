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

namespace Ecolab.Pages.Pages.PlantSetupTab
{
    public class RedFlagTabPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public RedFlagTabPage(Ecolab.TelerikPlugin.TelerikFramework telerikPlugin)
            : base(telerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "RedFlagTab.xml");
        }
        public RedFlagTabPage(List<object> utilsList)
            : base(utilsList, "RedFlagTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "RedFlagTab.xml");
        }
        /// <summary>
        /// The table grid
        /// </summary>
        private CommonControls.EcolabDataGrid tableGrid;
        /// <summary>
        ///Gets Redflag Table Grid control
        /// </summary>
        public CommonControls.EcolabDataGrid RedFlagTabGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "RedFlagGridTable");
                return tableGrid;
            }
        }
        /// <summary>
        /// Gets the control of success MSG.
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
        /// Gets the Duplication Message control value
        /// </summary>
        /// <returns></returns>
        public HtmlSpan VerifyDuplicationMsg
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("VerifyDuplication");
            }
        }
        /// <summary>
        /// Gets and Returns the AddRedFlag Button Control
        /// </summary>
        public HtmlAnchor BtnAddRedFlag
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("btnAddRedFlag");
            }
        }
        /// <summary>
        /// Gets RedFlag Item selection control
        /// </summary>
        public HtmlSelect DdlRedFlagItemAdd
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlRedFlagItem");
            }
        }
        /// <summary>
        /// Gets the Location add control
        /// </summary>
        public HtmlSelect DdlLocationAdd
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLocationAdd");
            }
        }
        /// <summary>
        /// Gets the Minimum Range control on Add functionality
        /// </summary>
        public HtmlInputText TxtMinimumRangeAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMinimumRangeAdd");
            }
        }
        /// <summary>
        /// Gets the UOM type control on Add functionality
        /// </summary>
        public HtmlControl MinRangeUOMAdd
        {
            get
            {
                return GetHtmlControl<HtmlControl>("minRangeUOMAdd");
            }
        }
        /// <summary>
        /// Gets the Maximum Range control on Add functionality
        /// </summary>
        public HtmlInputText TxtMaximumRangeAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaximumRangeAdd");
            }
        } 
        /// <summary>
        /// Gets the UOM type control on Add functionality
        /// </summary>
        public HtmlControl MaxRangeUOMAdd
        {
            get
            {
                return GetHtmlControl<HtmlControl>("maxRangeUOMAdd");
            }
        }
        /// <summary>
        /// Gets the save button control on add functionality
        /// </summary>
        public HtmlSpan BtnSaveAdd
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSaveAdd");
            }
        }
        /// <summary>
        /// Gets the Cancel Button control while performing Add Operations
        /// </summary>
        public HtmlSpan BtnCancelAdd
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnCancelAdd");
            }
        }
        /// <summary>
        /// Gets the controls for selecting multiple items on add functionality
        /// </summary>
        public HtmlButton BtnMachineAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnMachine");
            }
        }
        /// <summary>
        /// Gets the Machine MultiSelect Menu items 
        /// </summary>
        public HtmlInputCheckBox ChkMultiSelectMenu
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("chkboxMultiSelectMenu");
            }
        }
        /// <summary>
        /// Gets the control active or inactive status for Save Add functionality
        /// </summary>
        public HtmlButton VerifyAddSaveButtonStatus
        {
            get
            {
                return GetHtmlControl<HtmlButton>("VerifySaveAddButtonStatus");
            }
        }
        /// <summary>
        /// Gets RedFlag Item selection control on update functionality
        /// </summary>
        public HtmlSelect DdlRedFlagUpdate
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlRedFlagItemEdit");
            }
        }
        /// <summary>
        /// Gets the Location add control on update functionality
        /// </summary>
        public HtmlSelect DdlLocationUpdate
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLocationEdit");
            }
        }
        /// <summary>
        /// Gets the Minimum Range control on Update functionality
        /// </summary>
        public HtmlInputText TxtMinimumRangeUpdate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMinimumRangeEdit");
            }
        }
        /// <summary>
        /// Gets the UOM type control on Update functionality
        /// </summary>
        public HtmlControl MinRangeUOMUpdate
        {
            get
            {
                return GetHtmlControl<HtmlControl>("minRangeUOMEdit");
            }
        }
        /// <summary>
        /// Gets the Maximum Range control on Update functionality
        /// </summary>
        public HtmlInputText TxtMaximumRangeUpdate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaximumRangeEdit");
            }
        }
        /// <summary>
        /// Gets the UOM type control on Update functionality
        /// </summary>
        public HtmlControl MaxRangeUOMUpdate
        {
            get
            {
                return GetHtmlControl<HtmlControl>("maxRangeUOMEdit");
            }
        }
        /// <summary>
        /// Gets the save button control on add functionality
        /// </summary>
        public HtmlSpan BtnSaveUpdate
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSaveEdit");
            }
        }
        /// <summary>
        /// Gets the Cancel Button control while performing Update Operations
        /// </summary>
        public HtmlSpan BtnCancelUpdate
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnCancelEdit");
            }
        }
        /// <summary>
        /// Gets the controls for selecting multiple items on add functionality
        /// </summary>
        public HtmlButton BtnMachineUpdate
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnMachineMultiSelectEdit");
            }
        }
        /// <summary>
        /// Gets the MinRange  textbox control on InlineEdit functionality
        /// </summary>
        public HtmlInputText TxtMinRangeInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMinRangeInlineEdit");
            }
        }
        /// <summary>
        /// Gets the Maximum Range textbox control on InlineEdit functionality
        /// </summary>
        public HtmlInputText TxtMaxRangeInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaxRangeInlineEdit");
            }
        }
        /// <summary>
        /// Gets the Sorting items using grid header
        /// </summary>
        public HtmlControl SortRedFlagGrid
        {
            get
            {
                ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "RedFlagGridHeader").ChildNodes;
                foreach (Element e in childNodes)
                {
                    if (e.InnerText.Contains("Red Flag Item"))
                    {
                        return new HtmlControl(e);
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Gets message type on CRUD operations
        /// </summary>
        public HtmlControl VerifyMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("VerifyMessages");
            }
        }
        /// <summary>
        /// Gets language preferred control from general tab
        /// </summary>
        public HtmlSelect LanguagePreferred
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("SelectPreferredLanguage");
            }
        }
        /// <summary>
        /// Gets save button control
        /// </summary>
        public HtmlButton GeneralTabSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpload");
            }
        }
        /// <summary>
        /// Gets User roles grid table control
        /// </summary>
        public CommonControls.EcolabDataGrid UserRolesGridTable
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "UserRolesRedFlagTabGridTable");
            }
        }
        /// <summary>
        /// Gets ErrorMsg label value
        /// </summary>
        public HtmlDiv ErrorMsg
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("ErrorMsg");
            }
        }
        /// <summary>
        /// Gets Label content based on user roles
        /// </summary>
        public HtmlControl EditLabelUserRole
        {
            get
            {
                return GetHtmlControl<HtmlControl>("EditUserRoleLabel");
            }
        }
        /// <summary>
        /// Gets Label control and content based on user roles
        /// </summary>
        public HtmlControl ViewLabelUserRole
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ViewUserRoleLabel");
            }
        }
        /// <summary>
        /// Gets span control to verify the min validation msg
        /// </summary>
        public HtmlSpan VerifyMinValidMsg
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("verifyMinUOM");
            }
        }
        /// <summary>
        /// Gets span control to verify max validation msg
        /// </summary>
        public HtmlSpan VerifyMaxValidMsg
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("verifyMaxUOM");
            }
        }
        /// <summary>
        /// Method for Adding RedFlag Details
        /// </summary>
        /// <param name="RedFlag"></param>
        public void AddRefFlagDetails()
        {
            MouseKeyBoardSimulator objChars = new MouseKeyBoardSimulator();
            System.Threading.Thread.Sleep(1000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlRedFlagItemAdd.Focus();
            DdlRedFlagItemAdd.ExtendedMouseClick();
            DdlRedFlagItemAdd.SelectByText("Chemical Consumption", true);

            System.Threading.Thread.Sleep(2000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlLocationAdd.Focus();
            DdlLocationAdd.ExtendedMouseClick();
            DdlLocationAdd.SelectByIndex(1);

            System.Threading.Thread.Sleep(200);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMinimumRangeAdd.ExtendedMouseClick();
            objChars.SetNumeric("15");

            System.Threading.Thread.Sleep(200);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMaximumRangeAdd.ExtendedMouseClick();
            objChars.SetNumeric("25");
            
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            System.Threading.Thread.Sleep(2000);
            BtnSaveAdd.Focus();
            BtnSaveAdd.DeskTopMouseClick();
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }
        /// <summary>
        /// Method for Verifying Minimum Range Validation on Edit and Popup control
        /// </summary>
        public void VerifyMinValidation()
        {
            Thread.Sleep(1000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMaximumRangeUpdate.Focus();
            TxtMaximumRangeUpdate.ExtendedMouseClick();
            TxtMaximumRangeUpdate.TypeText("40");
            TxtMinimumRangeUpdate.Focus();
            TxtMinimumRangeUpdate.ExtendedMouseClick();
            TxtMinimumRangeUpdate.TypeText("100");
            Thread.Sleep(1000);
            BtnSaveUpdate.Focus();
            BtnSaveUpdate.DeskTopMouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }
        /// <summary>
        /// Verifying max range validation on Edit and PopUp Control
        /// </summary>
        public void VerifyMaxValidation()
        {
            System.Threading.Thread.Sleep(1000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMinimumRangeUpdate.Focus();
            TxtMinimumRangeUpdate.ExtendedMouseClick();
            TxtMinimumRangeUpdate.TypeText("20");
            TxtMaximumRangeUpdate.Focus();
            TxtMaximumRangeUpdate.ExtendedMouseClick();
            TxtMaximumRangeUpdate.TypeText("10");
            System.Threading.Thread.Sleep(1000);
            BtnSaveUpdate.Focus();
            BtnSaveUpdate.DeskTopMouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }
        /// <summary>
        /// Inline Editing and Updating the Records from Red Flag Gridview
        /// </summary>
        /// <param name="InlineEditRedFlag"></param>
        public void InlineEditRedFlag()
        {
            Thread.Sleep(2000);
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            TxtMinRangeInlineEdit.Value = "";
            TxtMinRangeInlineEdit.Focus();
            TxtMinRangeInlineEdit.MouseClick();
            objKeyPress.SetNumeric("15");
            objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            TxtMaxRangeInlineEdit.Value = "";
            TxtMaxRangeInlineEdit.Focus();
            TxtMaxRangeInlineEdit.MouseClick();
            objKeyPress.SetNumeric("30");
        }
        /// <summary>
        /// Deleting RedFlag Details
        /// </summary>
        /// <param name="RedFlagDetails">The Redflag details.</param>
        public void DeleteRedFlagDetails(string text)
        {
            ConfirmDialog confirmDialog =
                          ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            Thread.Sleep(2000);
            RedFlagTabGrid.SelectedRows(text)[0].GetButtonControls()[1].Click();
        }
    }
}
