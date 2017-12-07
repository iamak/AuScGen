using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.TestingFramework.Controls.KendoUI;

namespace Ecolab.Pages
{
    public class WashersPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public WashersPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "WashersTab.xml");
        }

        public WashersPage(List<object> utilsList)
            : base(utilsList, "WashersTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "WashersTab.xml");
        }

        /// <summary>
        /// Contacts Table Grid
        /// </summary>
        public CommonControls.EcolabDataGrid WashersListGridTable
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "WashersListGridTable");
            }
        }
        /// <summary>
        ///Gets Washer tab controls
        /// </summary>
        public HtmlControl WashersTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabWashers");
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
        /// Gets Back To Washers Link Button
        /// </summary>
        /// <returns></returns>
        public HtmlSpan BtnBacktoWashersLink
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnBacktoWashersLink");
            }
        }
        /// <summary>
        /// Gets DropDown Control of Model
        /// </summary>
        /// <returns></returns>
        public HtmlSelect DdlModel
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlModel");
            }
        }
        /// <summary>
        /// Gets DropDown Control of Model
        /// </summary>
        /// <returns></returns>
        public HtmlInputText TxtName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtName");
            }
        }
        /// <summary>
        /// Gets DropDown Control of Controller
        /// </summary>
        /// <returns></returns>
        public HtmlSelect DdlController
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlController");
            }
        }
        /// <summary>
        /// Gets Drop down Control of Washer Mode
        /// </summary>
        /// <returns></returns>
        public HtmlSelect DdlWasherMode
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherMode");
            }
        }
        /// <summary>
        /// Gets TextBox control
        /// </summary>
        /// <returns></returns>
        public HtmlInputText TxtPlantWasher
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtPlantWasher");
            }
        }
        /// <summary>
        /// Gets TextBox control for Program Number
        /// </summary>
        /// <returns></returns>
        public HtmlInputText TxtProgramNo
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtProgramNo");
            }
        }
        /// <summary>
        /// Gets the save button control
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnSaveTunnel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveTunnel");
            }
        }
        /// <summary>
        /// Gets the Cancel Button Control
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
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
        /// Gets popup Cancel button control for deleting
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnCancelDelete
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancelDelete");
            }
        }
        /// <summary>
        /// Gets popup Yes Prompt button control for Navigation
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnYesPopUp
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnYesPopUp");
            }
        }
        /// <summary>
        /// Gets popup No Prompt button control for Navigation
        /// </summary>
        /// <returns></returns>
        public HtmlButton BtnNoPopUp
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnNoPopUp");
            }
        }
        /// <summary>
        /// Deleting Washer Details
        /// </summary>
        /// <param name="DeleteWasherGroup">The Washer details.</param>
        public void DeleteWasherFromGridList(string text)
        {
            ConfirmDialog confirmDialog =
                          ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            Thread.Sleep(2000);
            WashersListGridTable.SelectedRows(text)[0].GetButtonControls()[0].DeskTopMouseClick();
        }

        public bool UpdateWasherDetails(string[] SetValues)
        {
            MouseKeyBoardSimulator objSimulator = new MouseKeyBoardSimulator();
            Thread.Sleep(3000);
            if (DdlController.ChildNodes.Count > 1)
            {
                DdlModel.Focus();
                DdlModel.SelectByIndex(1);
                DdlModel.MouseClick();
                KeyBoardSimulator.KeyDown(Keys.Select);
                KeyBoardSimulator.KeyPress(Keys.Enter);
            }
            else
            {
                return false;
            }
            if(DdlController.ChildNodes.Count > 1)
            {
                DdlController.Focus();
                DdlController.SelectByIndex(1);
                DdlWasherMode.Focus();
                KeyBoardSimulator.KeyDown(Keys.Select);
                KeyBoardSimulator.KeyPress(Keys.Enter);
            }
            else
            {
                return false;
            }
            TxtName.Focus();
            TxtName.Text = string.Empty;
            objSimulator.SetText(SetValues[0]);
            TxtPlantWasher.Focus();
            TxtPlantWasher.Text = string.Empty;
            TxtPlantWasher.TypeText(SetValues[1]);
            TxtProgramNo.Focus();
            TxtProgramNo.Text = string.Empty;
            objSimulator.SetNumeric(SetValues[2]);
            Thread.Sleep(2000);
            BtnSaveTunnel.Focus();
            BtnSaveTunnel.DeskTopMouseClick();
            return true;
        }

    }
}
