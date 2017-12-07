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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.Pages
{
    public class ContactsTabPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public ContactsTabPage(Ecolab.TelerikPlugin.TelerikFramework telerikPlugin)
            : base(telerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "ContactTab.xml");
        }
        public ContactsTabPage(List<object> utilsList)
            : base(utilsList, "ContactTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ContactTab.xml");
        }
        /// <summary>
        /// The table grid
        /// </summary>
        private CommonControls.EcolabDataGrid tableGrid;

        /// <summary>
        /// Gets the action controls.
        /// </summary>
        /// <param name="tableRow">The table row.</param>
        /// <returns></returns>
        public static IList<Element> GetActionControls(HtmlControl tableRow)
        {
            IList<Element> controls = new List<Element>();
            int actionCount = 0;
            ICollection<Element> cellList = tableRow.ChildNodes;
            foreach (Element cell in cellList)
            {
                if (cell.ChildNodes[0].TagName == "a")
                {
                    actionCount = cell.Children.Count;
                    for (int i = 0; i <= actionCount - 1; i++)
                    {
                        controls.Add(cell.ChildNodes[i]);
                    }
                    return controls;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the Contacts Table Grid
        /// </summary>
        public CommonControls.EcolabDataGrid ContactsTabGrid
        {
            get
            {
                tableGrid = new CommonControls.EcolabDataGrid(Telerik, guiMap, "ContactTabGridTable");
                return tableGrid;
            }
        }
        /// <summary>
        /// Gets the Contacts tab grid table.
        /// </summary>
        /// <returns></returns>
        public HtmlControl ContactsTabGridTable
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ContactTabGridTable");
            }
        }
        /// <summary>
        /// Gets Property of AddContact Button
        /// </summary>
        public HtmlControl AddContactButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddContact");
            }
        }
        /// <summary>
        /// Gets Property of Cancel Button
        /// </summary>
        public HtmlControl CancelButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnCancel");
            }
        }
        /// <summary>
        /// Gets btnCancelSave Button Property
        /// </summary>
        public HtmlControl VerifySaveButtonDisabled
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnSaveContacts");
            }
        }

        /// <summary>
        /// Gets Verifies the success MSG.
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
        /// Gets the message label control
        /// </summary>
        public HtmlControl VerifyMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("VerifyMessages");
            }
        }
        /// <summary>
        /// Gets the specific Contacts grid row.
        /// </summary>
        /// <param name="strText">The string text.</param>
        /// <returns></returns>
        public HtmlControl GetSpecificContactsGridRow(string strText)
        {
            HtmlControl ctrl = GetHtmlControl<HtmlControl>("ContactsTableGridRow");
            ICollection<Element> ele = ctrl.ChildNodes;
            foreach (Element e in ele)
            {
                if (e.InnerText.Contains(strText.ToLower(CultureInfo.CurrentCulture)))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }
        /// <summary>
        /// Gets Error Message label Control
        /// </summary>
        public HtmlDiv ErrorMsg
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("ErrorMsg");
            }
        }
        /// <summary>
        /// Gets Input text box control for add
        /// </summary>
        public HtmlInputText TxtTitleAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtTitle");
            }
        }
        /// <summary>
        /// Gets InputText control for FirstName
        /// </summary>
        public HtmlInputText TxtFirstNameAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFirstName");
            }
        }
        /// <summary>
        /// Gets the InputText control for Last Name
        /// </summary>
        public HtmlInputText TxtLastNameAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLastName");
            }
        }
        /// <summary>
        /// Gets Position control for add
        /// </summary>
        public HtmlSelect DdlPositionAdd
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("selectPostion");
            }
        }
        /// <summary>
        /// Gets Email control for add
        /// </summary>
        public HtmlInputText TxtEmailAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEmail");
            }
        }
        /// <summary>
        /// Gets Office Phone control for Add
        /// </summary>
        public HtmlInputText TxtOfficePhoneAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtOfficePhone");
            }
        }
        /// <summary>
        /// Gets Mobile phone control for add
        /// </summary>
        public HtmlInputText TxtMobilePhoneAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMobilePhone");
            }
        }
        /// <summary>
        /// Gets Fax Number control for add
        /// </summary>
        public HtmlInputText TxtFaxNumberAdd
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFaxNumber");
            }
        }
        /// <summary>
        /// Gets Save button control for add
        /// </summary>
        public HtmlButton BtnSaveContactsAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveContacts");
            }
        }
        /// <summary>
        /// Gets Cancel Button control
        /// </summary>
        public HtmlButton BtnCancelAdd
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancelContacts");
            }
        }
        /// <summary>
        /// Gets the Title control for update
        /// </summary>
        public HtmlInputText TxtTitleUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtTitleUpdate");
            }
        }
        /// <summary>
        /// Gets First Name update InputTexBox control
        /// </summary>
        public HtmlInputText TxtFirstNmeUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFirstNameUpdate");
            }
        }
        /// <summary>
        /// Gets last name input text control
        /// </summary>
        public HtmlInputText TxtLastNmeUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLastNameUpdate");
            }
        }
        /// <summary>
        /// Gets Position selection dropdown control for update
        /// </summary>
        public HtmlSelect DdlPosUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("selectPositionUpdate");
            }
        }
        /// <summary>
        /// Gets Office phone input text control for update
        /// </summary>
        public HtmlInputText TxtOffPhoneUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtOfficePhoneUpdate");
            }
        }
        /// <summary>
        /// Gets Mobile phone input text control for update
        /// </summary>
        public HtmlInputText TxtMobPhoneUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMobilePhoneUpdate");
            }
        }
        /// <summary>
        /// Gets Email Address Input Text control for update
        /// </summary>
        public HtmlInputText TxtEmailAdrsUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEmailAddressUpdate");
            }
        }
        /// <summary>
        /// Gets Fax input text control for update
        /// </summary>
        public HtmlInputText TxtFaxUpdateControl
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFaxUpdate");
            }
        }
        /// <summary>
        /// Gets Position Inline Edit control
        /// </summary>
        public HtmlInputText DdlPositionInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("selectPositionEditUpdate");
            }
        }
        /// <summary>
        /// Gets Office Phone Inline Edit Control
        /// </summary>
        public HtmlInputText TxtOffPhoneInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtOfficePhoneEdit");
            }
        }
        /// <summary>
        /// Gets Mobile Phone Inline Edit Control
        /// </summary>
        public HtmlInputText TxtMobPhoneInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMobilePhoneEdit");
            }
        }
        /// <summary>
        /// Gets Email Address Inline Edit Control
        /// </summary>
        public HtmlInputText TxtEmailAdrsInlineEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEmailAddressEdit");
            }
        }
        /// <summary>
        /// Adding Contact Details
        /// </summary>
        /// <param name="contactList"></param>
        public void AddContactsDetails(string[] contactList)
        {
            MouseKeyBoardSimulator objChars = new MouseKeyBoardSimulator();
            System.Threading.Thread.Sleep(1000);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtTitleAdd.Focus();
            TxtTitleAdd.ExtendedMouseClick();
            objChars.SetText(contactList[0]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtFirstNameAdd.ExtendedMouseClick();
            objChars.SetText(contactList[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtLastNameAdd.ExtendedMouseClick();
            objChars.SetText(contactList[2]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            System.Threading.Thread.Sleep(3000);
            DdlPositionAdd.ExtendedMouseClick();
            DdlPositionAdd.SelectByIndex(1);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtOfficePhoneAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[5]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMobilePhoneAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[6]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText txtEmailId = TxtEmailAdd;
            string[] splitEmailId = contactList[4].Split('@');
            txtEmailId.Text = splitEmailId[0] + "@";
            txtEmailId.ExtendedMouseClick();
            objChars.SetText(splitEmailId[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtFaxNumberAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[7]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            BtnSaveContactsAdd.Focus();
            BtnSaveContactsAdd.ExtendedMouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }

        /// <summary>
        /// Cancel Contact Details after entering and verify in gridview for that record existance..
        /// </summary>
        /// <param name="contactList"></param>
        public void CancelAddContactDetails(string[] contactList)
        {
            MouseKeyBoardSimulator objChars = new MouseKeyBoardSimulator();
            System.Threading.Thread.Sleep(1000);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtTitleAdd.Focus();
            TxtTitleAdd.ExtendedMouseClick();
            objChars.SetText(contactList[0]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtFirstNameAdd.ExtendedMouseClick();
            objChars.SetText(contactList[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtLastNameAdd.ExtendedMouseClick();
            objChars.SetText(contactList[2]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            System.Threading.Thread.Sleep(3000);
            DdlPositionAdd.ExtendedMouseClick();
            DdlPositionAdd.SelectByIndex(1);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtOfficePhoneAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[5]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMobilePhoneAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[6]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            HtmlInputText txtEmailId = TxtEmailAdd;
            string[] splitEmailId = contactList[4].Split('@');
            txtEmailId.Text = splitEmailId[0] + "@";
            txtEmailId.ExtendedMouseClick();
            objChars.SetText(splitEmailId[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtFaxNumberAdd.ExtendedMouseClick();
            objChars.SetNumeric(contactList[7]);

            Thread.Sleep(1000);
            BtnCancelAdd.ExtendedMouseClick();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            System.Threading.Thread.Sleep(3000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }

        /// <summary>
        /// Updates the Contact details on Edit popup
        /// </summary>
        /// <returns></returns>
        public void UpdateContactDetails(string[] contactList)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            TxtTitleUpdateControl.Focus();
            TxtTitleUpdateControl.Value = string.Empty;
            TxtTitleUpdateControl.ExtendedMouseClick();
            objKeyPress.SetText(contactList[0]);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            
            TxtFirstNmeUpdateControl.Value = string.Empty;
            TxtFirstNmeUpdateControl.ExtendedMouseClick();
            objKeyPress.SetText(contactList[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtLastNmeUpdateControl.Value = string.Empty;
            TxtLastNmeUpdateControl.ExtendedMouseClick();
            objKeyPress.SetText(contactList[2]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlPosUpdateControl.ExtendedMouseClick();
            objKeyPress.SetText(contactList[3]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtOffPhoneUpdateControl.Value = string.Empty;
            TxtOffPhoneUpdateControl.ExtendedMouseClick();
            objKeyPress.SetNumeric(contactList[5]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtMobPhoneUpdateControl.Value = string.Empty;
            TxtMobPhoneUpdateControl.ExtendedMouseClick();
            objKeyPress.SetNumeric(contactList[6]);

            HtmlInputText txtEmail = TxtEmailAdrsUpdateControl;
            string[] splitEmailId = contactList[4].Split('@');
            txtEmail.Focus();
            txtEmail.Value = string.Empty;
            txtEmail.Text = splitEmailId[0] + "@";
            txtEmail.ExtendedMouseClick();
            objKeyPress.SetText(splitEmailId[1]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TxtFaxUpdateControl.Value = string.Empty;
            TxtFaxUpdateControl.MouseClick();
            objKeyPress.SetNumeric(contactList[7]);

            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Enter);
        }

        /// <summary>
        /// Inline editing contact details
        /// </summary>
        /// <param name="EditUpdateInLineDetails"></param>
        public void EditUpdateInlineContactDetails(string[] editContactList)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            MouseKeyBoardSimulator objKeyPress = new MouseKeyBoardSimulator();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            DdlPositionInlineEdit.Value = string.Empty;
            DdlPositionInlineEdit.ExtendedMouseClick();
            objKeyPress.SetText(editContactList[3]);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Select);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            
            TxtOffPhoneInlineEdit.Value = string.Empty;
            TxtOffPhoneInlineEdit.ExtendedMouseClick();
            objKeyPress.SetNumeric(editContactList[5]);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            
            TxtMobPhoneInlineEdit.Value = string.Empty;
            TxtMobPhoneInlineEdit.ExtendedMouseClick();
            objKeyPress.SetNumeric(editContactList[6]);

            HtmlInputText txtEmail = TxtEmailAdrsInlineEdit;
            string[] splitEmailId = editContactList[4].Split('@');
            txtEmail.Focus();
            txtEmail.Value = string.Empty;
            txtEmail.Text = splitEmailId[0] + "@";
            txtEmail.ExtendedMouseClick();
            objKeyPress.SetText(splitEmailId[1]);
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Clicks the dialog cancel button based on text value from contacts grid
        /// </summary>
        /// <param name="contactDetails">The Contact details.</param>
        public void ClickonCancelPreferencesButton(string text)
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.CANCEL);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            ContactsTabGrid.SelectedRows(text)[0].GetButtonControls()[1].Click();
        }
        /// <summary>
        /// Deletes the row based on text value from contacts grid
        /// </summary>
        /// <param name="contactDetails">The Contact details.</param>
        public void DeleteContactsDetails(string text)
        {
            ConfirmDialog confirmDialog =
                          ConfirmDialog.CreateConfirmDialog(Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            ContactsTabGrid.SelectedRows(text)[0].GetButtonControls()[1].Click();
        }
    }
}
