using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Threading;
using System.IO;


namespace Ecolab.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneralTabPage : PageBase
    {
        /// <summary>
        /// The GUI map
        /// </summary>
        private string guiMap;
        string imagePath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralTabPage" /> class.
        /// </summary>
        /// <param name="TelerikPlugin">The telerik plugin.</param>
        public GeneralTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "GeneralTabPage.xml");
        }

        public GeneralTabPage(List<object> utilsList)
            : base(utilsList, "GeneralTabPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "GeneralTabPage.xml");
        }

        public HtmlSelect LanguagePreferred
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("SelectPreferredLanguage");
            }
        }

        public HtmlButton btnSave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpload");
            }
        }

        public HtmlButton SaveYesPopUp
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveYesPopUp");
            }
        }

        public HtmlSpan HomeTab
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("homeTab");
            }
        }

        /// <summary>
        /// Sets the and save user preference details.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns></returns>
        public ArrayList SetAndSaveUserPreferenceDetails(string[] userDetails)
        {
            SetUserPreferences(userDetails);
            HtmlButton btnSave = GetHtmlControl<HtmlButton>(guiMap, "btnUpload");
            btnSave.Click();
            Thread.Sleep(2000);
            //Add all selected option to list
            ArrayList prefList = GetSelectedValues();
            return prefList;
        }

        /// <summary>
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void ClickonCancelPreferencesButton(string[] userDetails)
        {
            //Set the preferences
            SetUserPreferences(userDetails);
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
                (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.CANCEL);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            //Click on HTML cancel button
            GetHtmlControl<HtmlButton>(guiMap, "btnCancel").Click();
        }

        /// <summary>
        /// Clickons the ok preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void ClickonOkPreferencesButton(string[] userDetails)
        {
            //Set the preferences
            SetUserPreferences(userDetails);
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
                (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            //Click on HTML cancel button
            GetHtmlControl<HtmlButton>(guiMap, "btnCancel").Click();
            clickOnSaveYesButton.DeskTopMouseClick();
        }

        public void DialogPopupClickSaveData()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
                (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            clickOnSaveYesPopUpButton.DeskTopMouseClick();
        }

        /// <summary>
        /// Selects the user preferences.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void SetUserPreferences(string[] userDetails)
        {
            //Set the preferred language
            GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredLanguage").SelectByText(userDetails[0], true);
            //Set the preferred currency 
            GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredCurrency").SelectByText(userDetails[1], true);
            //Set the Unit of measure
            GetHtmlControl<HtmlSelect>(guiMap, "SelectUOM").SelectByText(userDetails[2], true);
            //Set the flat fee
            GetHtmlControl<HtmlSelect>(guiMap, "SelectBudgetCustomerFlatFee").SelectByText(userDetails[3], true);
            //Set the live time
            GetHtmlControl<HtmlSelect>(guiMap, "SelectDBLiveTimeYears").SelectByText(userDetails[4], true);
            //Set the Database Path
            GetHtmlControl<HtmlInputText>(guiMap, "txtDatabaseExportPath").TypeText(userDetails[5]);
            //Upload the file
            DialogManager fileDialog = new DialogManager(Telerik);
            imagePath = Path.GetFullPath(userDetails[6]);
            fileDialog.UpLoadFile(imagePath);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectedValues()
        {
            ArrayList prefList = new ArrayList();
            prefList.Add(GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredLanguage").SelectedOption.Text);
            prefList.Add(GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredCurrency").SelectedOption.Text);
            prefList.Add(GetHtmlControl<HtmlSelect>(guiMap, "SelectUOM").SelectedOption.Text);
            prefList.Add(GetHtmlControl<HtmlSelect>(guiMap, "SelectBudgetCustomerFlatFee").SelectedOption.Text);
            prefList.Add(GetHtmlControl<HtmlSelect>(guiMap, "SelectDBLiveTimeYears").SelectedOption.Text);
            prefList.Add(GetHtmlControl<HtmlInputText>(guiMap, "txtDatabaseExportPath").Text);
            return prefList;
        }

        /// <summary>
        /// Determines whether [the preferences are saved].
        /// </summary>
        /// <returns></returns>
        public bool IsPreferencesSaved()
        {
            return GetHtmlControl<HtmlSpan>(guiMap, "lblSuccess").IsVisible();
        }

        /// <summary>
        /// Determines whether [is setup pagelaunched].
        /// </summary>
        /// <returns></returns>
        public HtmlControl IsHomePagelaunched()
        {
            return GetHtmlControl<HtmlControl>(guiMap, "lblHomepage");
        }

        /// <summary>
        /// Determines whether [is setup page found].
        /// </summary>
        /// <returns></returns>
        public bool isSetupPageFound()
        {
            return GetHtmlControl<HtmlAnchor>(guiMap, "lnkSetup").IsVisible();
        }

        /// <summary>
        /// Gets the currencyoptions.
        /// </summary>
        /// <returns></returns>
        public ArrayList GetCurrencyoptions()
        {
            ArrayList list = new ArrayList();
            HtmlSelect currencyDropDown = GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredCurrency");
            ICollection<Element> options = currencyDropDown.ChildNodes;
            foreach (Element option in options)
            {
                list.Add(option.InnerText);
            }
            return list;
        }

        /// <summary>
        /// Gets the language options.
        /// </summary>
        /// <returns></returns>
        public ArrayList GetLanguageOptions()
        {
            ArrayList list = new ArrayList();
            HtmlSelect languageDropDown = GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredLanguage");
            ICollection<Element> options = languageDropDown.ChildNodes;
            foreach (Element option in options)
            {
                list.Add(option.InnerText);
            }
            return list;
        }

        /// <summary>
        /// Resets the user preferences.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        public void ResetUserPreferences(string[] userDetails)
        {
            //Set the preferred language
            GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredLanguage").SelectByText("English US", true);
            //Set the preferred currency 
            GetHtmlControl<HtmlSelect>(guiMap, "SelectPreferredCurrency").SelectByText(userDetails[1], true);
            //Set the Unit of measure
            GetHtmlControl<HtmlSelect>(guiMap, "SelectUOM").SelectByText(userDetails[2], true);
            //Set the flat fee
            GetHtmlControl<HtmlSelect>(guiMap, "SelectBudgetCustomerFlatFee").SelectByText(userDetails[3], true);
            //Set the live time
            GetHtmlControl<HtmlSelect>(guiMap, "SelectDBLiveTimeYears").SelectByText(userDetails[4], true);
            //Set the Database Path
            GetHtmlControl<HtmlInputText>(guiMap, "txtDatabaseExportPath").Text = userDetails[5];
            //Upload the file
            DialogManager fileDialog = new DialogManager(Telerik);
            imagePath = Path.GetFullPath(userDetails[6]);
            fileDialog.UpLoadFile(imagePath);
        }

        /// <summary>
        /// Determines whether [is language set].
        /// </summary>
        /// <returns></returns>
        public bool IsLanguageSet()
        {
            return GetHtmlControl<HtmlSpan>(guiMap, "lblNyorsk").IsVisible();
        }

        /// <summary>
        /// Returns No button control
        /// </summary>
        public HtmlButton clickOnSaveNoButton
        {
            get
            {
               return GetHtmlControl<HtmlButton>(guiMap, "btnSaveNo");
            }
        }

        /// <summary>
        /// returns Yes button control 
        /// </summary>
        public HtmlButton clickOnSaveYesButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveYes");
            }
        }

        /// <summary>
        /// returns Yes popUp button control
        /// </summary>
        public HtmlButton clickOnSaveYesPopUpButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveYesPopUp");
            }
        }
    }
}
