using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using ArtOfTest.Common.UnitTesting;

namespace Ecolab.Pages
{
    public class CustomerTabPage : PageBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static log4net.ILog Logger = log4net.LogManager
           .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The GUI map
        /// </summary>
        private string guiMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTabPage"/> class.
        /// </summary>
        /// <param name="TelerikPlugin">The telerik plugin.</param>
        public CustomerTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "CustomerTab.xml");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTabPage"/> class.
        /// </summary>
        /// <param name="utilsList">The utils list.</param>
        public CustomerTabPage(List<object> utilsList)
            : base(utilsList, "CustomerTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "CustomerTab.xml");
        }

        /// <summary>
        /// Customers the tab.
        /// </summary>
        /// <returns></returns>
        public HtmlControl CustomerTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabCustomer");
            }
        }

        /// <summary>
        /// Gets the customer tab grid.
        /// </summary>
        /// <value>
        /// The customer tab grid.
        /// </value>
        public CommonControls.EcolabDataGrid CustomerTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "CustomerTabGridTable");
            }
        }

        /// <summary>
        /// Customers the tab grid table.
        /// </summary>
        /// <returns></returns>
        public HtmlControl CustomerTabGridTable
        {
            get
            {
                return GetHtmlControl<HtmlControl>("CustomerTabGridTable");
            }
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <returns></returns>
        public HtmlControl AddCustomerButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("AddCustomerButton");
            }
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <returns></returns>
        public HtmlControl lblConduitPortlet
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblConduitPortlet");
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
        /// Customers the tab grid table header.
        /// </summary>
        /// <returns></returns>
        public List<string> CustomerTabGridTableHeader()
        {
            List<string> lstHeaderName = null;
            ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "CustomerGridHeader").ChildNodes;
            foreach (Element e in childNodes)
            {
                lstHeaderName.Add(e.InnerText);
            }
            return lstHeaderName;
        }

        /// <summary>
        /// Ares the columns exist.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        public bool AreColumnsExist(string ID, string Name)
        {
            bool bVerifyID = false;
            bool bStatus = true;
            bool bVerifyName = false;
            ICollection<Element> childNodes = GetHtmlControl<HtmlControl>(guiMap, "CustomerGridHeader").ChildNodes;
            foreach (Element e in childNodes)
            {
                if (e.InnerText.Contains(ID))
                {
                    bVerifyID = true;
                }
                if (e.InnerText.Contains(Name))
                {
                    bVerifyName = true;
                }
            }
            if (!bVerifyID && !bVerifyName)
            {
                bStatus = false;
                Logger.Error("Customer ID Column name not found");
            }
            return bStatus;
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <returns></returns>
        public void AddCustomer(string strID, string strName)
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            //objKeyPress.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText ctrl_ID = GetHtmlControl<HtmlInputText>("CustomerID");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ctrl_ID.Focus();
            ctrl_ID.InvokeEvent(ScriptEventType.OnClick);
            objKeyPress.SetNumeric(strID);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText ctrl_Name = GetHtmlControl<HtmlInputText>("CustomerName");
            ctrl_Name.Focus();
            ctrl_Name.ExtendedMouseClick();
            objKeyPress.SetText(strName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(5000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <returns></returns>
        public void UpdateCustomer(string strID, string strName)
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText ctrl_ID = GetHtmlControl<HtmlInputText>(guiMap, "CustomerID");
            Thread.Sleep(2000);
            ctrl_ID.Focus();
            ctrl_ID.Value = "";
            ctrl_ID.ExtendedMouseClick();
            objKeyPress.SetNumeric(strID);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText ctrl_Name = GetHtmlControl<HtmlInputText>(guiMap, "CustomerName");
            ctrl_Name.Value = "";
            ctrl_Name.ExtendedMouseClick();
            objKeyPress.SetText(strName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(5000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }

        /// <summary>
        /// Gets the specific customer grid row.
        /// </summary>
        /// <param name="strText">The string text.</param>
        /// <returns></returns>
        public HtmlControl GetSpecificCustomerGridRow(string strText)
        {
            HtmlControl ctrl = GetHtmlControl<HtmlControl>(guiMap, "CustomerTabGridTableRows");
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
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void ClickonCancelPreferencesButton(string Text)
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.CANCEL);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            HtmlControl ctrl_Row = GetSpecificCustomerGridRow(Text);
            IList<Element> ctrlActions = GetActionControls(ctrl_Row);
            new HtmlControl(ctrlActions[1]).Click();
        }

        /// <summary>
        /// Clickons the ok preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void ClickonOkPreferencesButton(string Text)
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            HtmlControl ctrl_Row = GetSpecificCustomerGridRow(Text);
            IList<Element> ctrlActions = GetActionControls(ctrl_Row);
            new HtmlControl(ctrlActions[1]).Click();
        }

        /// <summary>
        /// Verifies the cancel button.
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        public void VerifyUpdateCustomerCancelButton(string strName)
        {
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText ctrl_Name = GetHtmlControl<HtmlInputText>(guiMap, "CustomerName");
            ctrl_Name.Focus();
            ctrl_Name.Value = "";
            ctrl_Name.ExtendedMouseClick();
            objKeyPress.SetText(strName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(1000);
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            HtmlControl cancelButton = GetHtmlControl<HtmlControl>(guiMap, "btnCancel");
            cancelButton.Click();

        }
    }
}
