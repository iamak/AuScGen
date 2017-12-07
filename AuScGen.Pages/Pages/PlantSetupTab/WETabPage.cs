using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.Pages
{
    public class WETabPage : PageBase
    {
        /// <summary>
        /// The GUI map
        /// </summary>
        private string guiMap;

        /// <summary>
        /// The table grid
        /// </summary>
        private CommonControls.EcolabDataGrid tableGrid;

        /// <summary>
        /// Initializes a new instance of the <see cref="WETabPage"/> class.
        /// </summary>
        /// <param name="TelerikPlugin">The telerik plugin.</param>
        public WETabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "WaterEnergyTab.xml");
        }

        public WETabPage(List<object> utilsList)
            : base(utilsList, "WaterEnergyTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "WaterEnergyTab.xml");
        }
        /// <summary>
        /// Gets the water energy tab grid.
        /// </summary>
        /// <value>
        /// The water energy tab grid.
        /// </value>
        public CommonControls.EcolabDataGrid WaterEnergyTabGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "WETabGridTable");
                return tableGrid;
            }
        }

        /// <summary>
        /// Gets the water energy tab grid.
        /// </summary>
        /// <value>
        /// The water energy tab grid.
        /// </value>
        public CommonControls.EcolabDataGrid WaterEnergyTabGridRoleBased
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "WETabGridTableRoleBased");
                return tableGrid;
            }
        }

        /// <summary>
        /// Gets the add water energy button.
        /// </summary>
        /// <value>
        /// The add water energy button.
        /// </value>
        public HtmlControl WaterEnergyTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabWEDevice");
            }
        }

        /// <summary>
        /// Gets the water energy tab grid table.
        /// </summary>
        /// <value>
        /// The water energy tab grid table.
        /// </value>
        public HtmlControl WaterEnergyTabGridTable
        {
            get
            {
                return GetHtmlControl<HtmlControl>("WETabGridTable");
            }
        }

        /// <summary>
        /// Gets the add water energy details button.
        /// </summary>
        /// <value>
        /// The add water energy details button.
        /// </value>
        public HtmlControl AddWaterEnergyDetailsButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddWEDetails");
            }
        }

        /// <summary>
        /// Verifies the success MSG.
        /// </summary>
        /// <returns></returns>
        public HtmlDiv VerifySuccessMsg
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("SuccessValidationMessage");
            }
        }

        /// <summary>
        /// Gets the verify field validation MSG.
        /// </summary>
        /// <value>
        /// The verify field validation MSG.
        /// </value>
        public HtmlControl VerifyLastNameFieldValidationMsg
        {
            get
            {
                return GetHtmlControl<HtmlControl>("WEInvalidMsg");
            }
        }

        /// <summary>
        /// Gets the water energy cancel button.
        /// </summary>
        /// <value>
        /// The water energy cancel button.
        /// </value>
        public HtmlControl WaterEnergyCancelButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnWECancel");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public HtmlSelect DeviceType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("SelectDeviceType");
            }
        }

        /// <summary>
        /// Gets the device name add new.
        /// </summary>
        /// <value>
        /// The device name add new.
        /// </value>
        public HtmlInputText DeviceNameAddNew
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDeviceNameAddNew");
            }
        }

        /// <summary>
        /// Adds the water energy device.
        /// </summary>
        /// <param name="strDevivceType">Type of the string devivce.</param>
        /// <param name="strDeviceModel">The string device model.</param>
        /// <param name="strInstallDate">The string install date.</param>
        /// <param name="strComment">The string comment.</param>
        public void AddWaterEnergyDevice(string strDeviceName, string strDevivceType, string strDeviceModel, string strInstallDate, string strComment)
        {

            GetHtmlControl<HtmlInputText>("txtDeviceNameAddNew").TypeText(strDeviceName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlSelect>("SelectDeviceType").SelectByText(strDevivceType,true);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlSelect>("SelectDeviceModel").SelectByText(strDeviceModel,true);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlInputText>("InstallDate").TypeText(strInstallDate);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlTextArea>("txtComment").Text = strComment;
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(3000);
            GetHtmlControl<HtmlSpan>("btnWEUpdate").DeskTopMouseClick();


        }

        /// <summary>
        /// Adds the water energy device.
        /// </summary>
        /// <param name="strDevivceType">Type of the string devivce.</param>
        /// <param name="strDeviceModel">The string device model.</param>
        /// <param name="strInstallDate">The string install date.</param>
        /// <param name="strComment">The string comment.</param>
        public void VerifyFieldValidationMessage(string strDevivceType, string strInstallDate, string strComment)
        {
            GetHtmlControl<HtmlSelect>("SelectDeviceType").SelectByText(strDevivceType,true);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlInputText>("InstallDate").TypeText(strInstallDate);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlTextArea>("txtComment").Text = strComment;
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            GetHtmlControl<HtmlSpan>("btnWEUpdate").DeskTopMouseClick();
        }

        /// <summary>
        /// Update the water energy device.
        /// </summary>
        /// <param name="strDevivceType">Type of the string devivce.</param>
        /// <param name="strDeviceModel">The string device model.</param>
        /// <param name="strInstallDate">The string install date.</param>
        /// <param name="strComment">The string comment.</param>
        public void UpdateWaterEnergyDevice(string strComment)
        {
            GetHtmlControl<HtmlTextArea>("txtCommentUpdate").TypeText(strComment);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlSpan>("btnWEUpdate").Click();
        }

        /// <summary>
        /// Verify Update the water energy device. update data and click cancel
        /// </summary>
        /// <param name="strDevivceType">Type of the string devivce.</param>
        /// <param name="strDeviceModel">The string device model.</param>
        /// <param name="strInstallDate">The string install date.</param>
        /// <param name="strComment">The string comment.</param>
        public void VerifyUpdateCancelButton(string strComment)
        {
            GetHtmlControl<HtmlTextArea>("txtCommentUpdate").TypeText(strComment);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlButton>("btnWECancel").MultiTabClickButton();
        }

        /// <summary>
        /// Gets the specific customer grid row.
        /// </summary>
        /// <param name="strText">The string text.</param>
        /// <returns></returns>
        public HtmlControl GetSpecificCustomerGridRow(string strText)
        {
            HtmlControl ctrl = GetHtmlControl<HtmlControl>(guiMap, "WETabGridTableRows");
            ICollection<Element> ele = ctrl.ChildNodes;
            foreach (Element e in ele)
            {
                if (e.InnerText.Contains(strText))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the action controls.
        /// </summary>
        /// <param name="tableRow">The table row.</param>
        /// <returns></returns>
        public IList<Element> GetActionControls(HtmlControl tableRow)
        {
            IList<Element> controls = new List<Element>();
            int nCount = 0;
            ICollection<Element> cellList = tableRow.ChildNodes;
            foreach (Element cell in cellList)
            {
                if (cell.ChildNodes[0].TagName == "a")
                {
                    nCount = cell.Children.Count;
                    for (int i = 0; i <= nCount - 1; i++)
                    {
                        controls.Add(cell.ChildNodes[i]);
                    }
                    return controls;
                }
            }
            return null;
        }

        /// <summary>
        /// Clickons the ok preferences button.
        /// </summary>
        /// <param name="Text">The text.</param>
        public void ClickonOkPreferencesButton()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
        }

        /// <summary>
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="Text">The text.</param>
        public void ClickonCancelPreferencesButton()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.CANCEL);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            WaterEnergyTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();
        }

        /// <summary>
        /// Inlines the editing.
        /// </summary>
        /// <param name="Date">The date.</param>
        public void InlineEditing(string strDeviceName , string Date)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlInputText>("DeviceName").Focus();
            GetHtmlControl<HtmlInputText>("DeviceName").TypeText(strDeviceName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GetHtmlControl<HtmlInputText>("WEPosition").TypeText(Date);
        }

    }
}
