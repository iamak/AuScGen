using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using System.Windows.Forms;

namespace Ecolab.Pages
{
    public class DialogManager : PageBase
    {
        private string guiMap;
        private static log4net.ILog Logger = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DialogManager(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "CommonControls.xml");
        }

        /// <summary>
        /// Ups the load file.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="filePath">The file path.</param>
        public void UpLoadFile(String filePath)
        {
            try
            {
                HtmlInputFile input = Telerik.ActiveBrowser.Find.ByName<HtmlInputFile>("uploadimage");
                System.Threading.Thread.Sleep(3000);
                input.Upload(filePath, 3000);
                //HtmlButton btnSave = Telerik.ActiveBrowser.Find.ByAttributes<HtmlButton>("class=btn btn-success");
                //btnSave.Click();
                System.Threading.Thread.Sleep(3000);
                Logger.Info("Clicked on Save Button");
            }
            catch (FileNotFoundException fne)
            {
                Logger.Error("File not found", fne);
            }
        }

        /// <summary>
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns></returns>
        public void ClickonCancelButton()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
            (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.CANCEL);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
        }

        /// <summary>
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns></returns>
        public void ClickonOKButton()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
            (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);                       
        }

        private string dialogMessage;
        public string LastDialogMessage
        {
            get
            {
                return dialogMessage;
            }            
        }

        /// <summary>
        /// Clickons the cancel preferences button.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns></returns>
        public void GetMessageAndOKButton()
        {
            ConfirmDialog confirmDialog = ConfirmDialog.CreateConfirmDialog
            (Telerik.ActiveBrowser.Manager.ActiveBrowser, DialogButton.OK);
            Telerik.ActiveBrowser.Manager.DialogMonitor.AddDialog(confirmDialog);
            confirmDialog.HandlerDelegate = MyCustomAlertHandler;
        }

        public void DownloadFile(Action printButtonClick)
        {
            DownloadDialogsHandler downloadFile = new DownloadDialogsHandler(Telerik.ActiveBrowser, DialogButton.SAVE, @"D:\testtest.pdf", Telerik.ActiveBrowser.Manager.Desktop);
            printButtonClick();
            //KeyBoardSimulator.KeyPress(Keys.Back);
            downloadFile.WaitUntilHandled(Config.PageClassSettings.Default.MaxTimeoutValue * 10);
        }

        private void MyCustomAlertHandler(IDialog dialog)
        {
            dialogMessage = dialog.Window.AllChildren[dialog.Window.AllChildren.Count - 1].Caption;
            Telerik.Manager.Desktop.KeyBoard.KeyPress(Keys.Enter);
            dialog.HandleCount++;
        }

        public HtmlButton NoButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveNo");
            }
        }

        public HtmlButton YesButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveYes");
            }
        }

        public HtmlButton CancelButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveCancel");
            }
        }

        public HtmlButton OkButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveOk");
            }
        }
    }
}
