using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.TestingFramework.Controls.KendoUI;

namespace Ecolab.Pages
{
    public class WasherGroupPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public WasherGroupPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "WasherGroupTab.xml");
        }

        public WasherGroupPage(List<object> utilsList)
            : base(utilsList, "WasherGroupTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "WasherGroupTab.xml");
        }
        /// <summary>
        /// The table grid
        /// </summary>
        private CommonControls.EcolabDataGrid tableGrid;

        /// <summary>
        /// Contacts Table Grid
        /// </summary>
        public CommonControls.EcolabDataGrid WasherGroupTableGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "WasherGroupGridTable");
                return tableGrid;
            }
        }
        /// <summary>
        /// Gets WashSteps table grid data
        /// </summary>
        public CommonControls.EcolabDataGrid WashStepsTableGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "WashStepsTableGrid");
                return tableGrid;
            }
        }
        public CommonControls.EcolabDataGrid WasherGroupFormulaTabelGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "WasherGroupFormulaTabelGrid");
                return tableGrid;
            }
        }
        /// <summary>
        ///Gets Washer Group View access label controls
        /// </summary>
        public HtmlControl LabelViewGroupNameAccess
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblViewAccessGroupName");
            }
        }
        /// <summary>
        /// Gets Formulas tab control
        /// </summary>
        public HtmlControl TabFormula
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabFormula");
            }
        }
        public HtmlControl LblWashStepLocalization
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblWashStepLocalization");
            }
        }
        public HtmlControl LblErrMsgTemperature
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrMsgTemperature");
            }
        }
        public HtmlControl LblErrMsgWaterType
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrMsgWaterType");
            }
        }
        public HtmlControl LblErrMsgRunTime
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrMsgRunTime");
            }
        }
        public HtmlControl LblErrMsgWashOperation
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrMsgWashOperation");
            }
        }
        public HtmlControl LblErrMsgWaterLevel
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrMsgWaterLevel");
            }
        }
        public HtmlButton BtnPopUpCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnPopUpCancel");
            }
        }
        public HtmlButton BtnPopUpOk
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnPopUpOK");
            }
        }
        public HtmlButton BtnAddWashStep
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnAddWashStep");
            }
        }
        public HtmlButton BtnSaveAddWashStep
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveWashStep");
            }
        }
        public HtmlButton BtnSaveAddFormula
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnAddFormulaSave");
            }
        }
        public HtmlInputText txtWaterLevel
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWaterLevel");
            }
        }
        public HtmlInputText TxtWashStepNumber
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWashStepNumber");
            }
        }
        public HtmlInputText TxtTemperature
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtTemperature");
            }
        }
        public HtmlSelect DdlWaterTypes
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWaterTypes");
            }
        }
        public HtmlSelect DdlWashOperation
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWashoperation");
            }
        }
        public HtmlInputText TxtRuntime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtRunTime");
            }
        }
        public HtmlSpan BtnBackWashStep
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnBackWashStep");
            }
        }

        public HtmlInputText Number
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNumber");
            }
        }
        public HtmlSelect FormulaName
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlFormulaName");
            }
        }
        public HtmlInputText NominalLoad
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNominalLoad");
            }
        }
        public HtmlInputText LoadsPerMonth
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLoadsPerMonth");
            }
        }
        public HtmlInputText ExtraTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtExtraTime");
            }
        }

        /// <summary>
        ///Gets WasherGroup controls
        /// </summary>
        public HtmlControl TabWasherGroup
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabWasherGroup");
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
        ///Gets WasherGroup controls
        /// </summary>
        public HtmlSpan BtnAddWasherGroup
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnAddWasherGroup");
            }
        }
        /// <summary>
        /// Gets the control of success MSG.
        /// </summary>
        /// <returns></returns>
        public HtmlControl SuccessMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SuccessValidationMessage");
            }
        }
        /// <summary>
        /// Gets the control of success MSG.
        /// </summary>
        /// <returns></returns>
        public HtmlControl ErrorMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrorMsg");
            }
        }
        /// <summary>
        /// Gets GroupNumber control
        /// </summary>
        /// <returns></returns>
        public HtmlInputText TxtGroupNumber
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtGroupNumber");
            }
        }
        /// <summary>
        /// Gets GroupName control
        /// </summary>
        /// <returns></returns>
        public HtmlInputText TxtGroupName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtGroupName");
            }
        }
        /// <summary>
        /// Gets GroupType control
        /// </summary>
        /// <returns></returns>
        public HtmlSelect DdlGroupType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherGroupType");
            }
        }
        /// <summary>
        /// Gets GroupType control
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnSaveAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }
        /// <summary>
        /// Gets popup ok button control for deleting
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnOkDelete
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnOkDelete");
            }
        }
        /// <summary>
        /// Gets popup Yes button control on Cancel Button for saving the data.
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnYesPopUpOnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnYesPopUpOnCancel");
            }
        }
        /// <summary>
        /// Gets the Cancel Button Control
        /// </summary>
        public HtmlButton BtnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }
        /// <summary>
        /// Gets GroupType control
        /// </summary>
        /// <returns></returns>
        public HtmlAnchor BtnBacktoWasherGroup
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("btnBacktoWasherGroup");
            }
        }
        /// <summary>
        /// Gets the Sorting items using grid header
        /// </summary>
        public HtmlControl SortWasherGroupGridHeader
        {
            get
            {
                ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "WasherGroupGridHeader").ChildNodes;
                foreach (Element e in childNodes)
                {
                    if (e.InnerText.Contains("Group Number"))
                    {
                        return new HtmlControl(e);
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Method to perform add functionality for washer group
        /// </summary>
        /// <param name="washerGroupNumber"></param>
        /// <param name="washerGroupName1"></param>
        /// <param name="washerGroupType"></param>
        public void AddWasherGroupItems(string washerGroupNumber, string washerGroupName1, string washerGroupType)
        {
            Random randomNumber = new Random();
            string washerGroupName = string.Format("GroupName{0}", randomNumber.Next());
            Thread.Sleep(2000);
            TxtGroupNumber.Focus();
            TxtGroupNumber.TypeText(randomNumber.Next(1000).ToString());
            TxtGroupName.Focus();
            TxtGroupName.SetText(washerGroupName);
            DdlGroupType.SelectByIndex(1);
            BtnSaveAdd.Focus();
            BtnSaveAdd.DeskTopMouseClick();
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Inline Edit for Washer Group
        /// </summary>
        /// <param name="washerGroupName"></param>
        public void InlineEditUpdateFunctionalityWasherGroup(string washerGroupName)
        {
            Thread.Sleep(1000);
            TxtGroupName.Value = string.Empty;
            TxtGroupName.Focus();
            TxtGroupName.MouseClick();
            TxtGroupName.TypeText(washerGroupName);
            TxtGroupName.DeskTopMouseClick();
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Updating Washer group name method
        /// </summary>
        /// <param name="washerGroupName"></param>
        public void UpdateFunctionalityWasherGroup(string washerGroupName)
        {
            Thread.Sleep(1000);
            TxtGroupName.Value = string.Empty;
            TxtGroupName.Focus();
            TxtGroupName.MouseClick();
            TxtGroupName.SetText(washerGroupName);
            BtnSaveAdd.Focus();
            BtnSaveAdd.DeskTopMouseClick();
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Deleting Washer Group Details
        /// </summary>
        /// <param name="DeleteWasherGroup">The Washer Group details.</param>
        public void DeleteWasherGroup(string text)
        {
            ConfirmDialog confirmDialog =
                          ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            Thread.Sleep(2000);
            WasherGroupTableGrid.SelectedRows(text)[0].GetButtonControls()[3].DeskTopMouseClick();
        }
        /// <summary>
        /// Gets the Button controls
        /// </summary>
        public HtmlSpan AddFormula
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnAddFormula");
            }
        }
        public HtmlControl divMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divMessage");
            }
        }

        /// <summary>
        /// Adding Formula...
        /// </summary>
        /// <param name="number"></param>
        /// <param name="nominalLoad"></param>
        /// <param name="loadsPerMonth"></param>
        /// <param name="extraTime"></param>
        public void AddingFormula(string nominalLoad, string loadsPerMonth, string extraTime)
        {
            Random randomNumber = new Random();
            Number.TypeText(randomNumber.Next(127).ToString());
            //Number.TypeText(number);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            FormulaName.Focus();
            FormulaName.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NominalLoad.TypeText(nominalLoad);
            LoadsPerMonth.TypeText(loadsPerMonth);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ExtraTime.TypeText(extraTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NominalLoad.MouseClick();
            NominalLoad.SetText("0");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ExtraTime.MouseClick();
            NominalLoad.TypeText(nominalLoad);
            BtnSaveAddFormula.Focus();
            BtnSaveAddFormula.DeskTopMouseClick();
        }

        public void AddWashSteps(string washStep, string temperature, string runTime, string waterLevel)
        {
            TxtWashStepNumber.Focus();
            if (TxtWashStepNumber.Text == string.Empty)
            {
                TxtWashStepNumber.SetText(washStep);
            }
            TxtWashStepNumber.MouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtTemperature.TypeText(temperature);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlWaterTypes.Focus();
            DdlWaterTypes.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtRuntime.TypeText(runTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlWashOperation.Focus();
            DdlWashOperation.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtWaterLevel.Focus();
            txtWaterLevel.TypeText(waterLevel);
            Thread.Sleep(2000);
            BtnSaveAddWashStep.Focus();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
            BtnSaveAddWashStep.MouseClick();
        }

        public void UpdateWashSteps(string washStep, string temperature, string runTime, string waterLevel)
        {
            TxtWashStepNumber.Focus();
            if (TxtWashStepNumber.Text == string.Empty)
            {
                TxtWashStepNumber.SetText(washStep);
            }
            TxtWashStepNumber.MouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtTemperature.TypeText(temperature);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlWaterTypes.Focus();
            DdlWaterTypes.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtRuntime.TypeText(runTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlWashOperation.Focus();
            DdlWashOperation.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtWaterLevel.Focus();
            txtWaterLevel.TypeText(waterLevel);
            Thread.Sleep(2000);
            BtnSaveAddWashStep.Focus();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
            BtnSaveAddWashStep.MouseClick();
        }
    }
}
