using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.Pages
{
    public class LabourTabPage : PageBase
    {
        private string guiMap;

        public LabourTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "ManualInputLabourTab.xml");
        }

        public LabourTabPage(List<object> utilsList)
            : base(utilsList, "ManualInputLabourTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ManualInputLabourTab.xml");
        }

        /// <summary>
        /// Gets labour tab control
        /// </summary>
        public HtmlAnchor LabourTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabLabour");
            }
        }
        /// <summary>
        /// Gets labour tab grid control
        /// </summary>
        public CommonControls.EcolabDataGrid LabourTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "LabourTabGridTable");
            }
        }
        /// <summary>
        /// Gets Location selection control
        /// </summary>
        public HtmlSelect DdlLocation
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLocation");
            }
        }
        /// <summary>
        /// Gets Labout type selection control
        /// </summary>
        public HtmlSelect DdlLabourTypes
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLaborTypes");
            }
        }
        /// <summary>
        /// Gets labour cost control
        /// </summary>
        public HtmlInputText TxtLabourCost
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLaborCost");
            }
        }
        /// <summary>
        /// Gets startdate text box control
        /// </summary>
        public HtmlInputText TxtStartDate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtStartDate");
            }
        }
        /// <summary>
        /// Gets start date DateTime control
        /// </summary>
        public HtmlControl StartDateControl
        {
            get
            {
                return GetHtmlControl<HtmlControl>("startDateControl");
            }
        }
        /// <summary>
        /// Gets enddate textbox control
        /// </summary>
        public HtmlInputText TxtEndDate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEndDate");
            }
        }
        /// <summary>
        /// Gets EndDate DateTime control
        /// </summary>
        public HtmlControl EndDateControl
        {
            get
            {
                return GetHtmlControl<HtmlControl>("endDateControl");
            }
        }
        /// <summary>
        /// Gets Allocated Man Hours textBox  control
        /// </summary>
        public HtmlInputText TxtAllocatedManHours
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtAllocatedManHours");
            }
        }
        /// <summary>
        /// Gets ErrorMessage label control
        /// </summary>
        public HtmlControl ErrorMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("errorMsg");
            }
        }
        /// <summary>
        /// Gets Success Message control
        /// </summary>
        public HtmlControl SuccessMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("successMsg");
            }
        }
        /// <summary>
        /// Gets Save button control
        /// </summary>
        public HtmlButton BtnSave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }
        /// <summary>
        /// Gets Cancel button control
        /// </summary>
        public HtmlButton BtnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
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
        /// Gets save button control of General tab
        /// </summary>
        public HtmlButton GeneralTabSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpload");
            }
        }
        /// <summary>
        /// Add Labour Details Method
        /// </summary>
        /// <param name="location"></param>
        /// <param name="manualHrType"></param>
        /// <param name="allocatedManHours"></param>
        public void AddLabourDetails(string location, string manualHrType,string allocatedManHours)
        {
            Thread.Sleep(1000);
            DdlLocation.Focus();
            DdlLocation.DeskTopMouseClick();
            DdlLocation.SelectByIndex(1, Config.PageClassSettings.Default.MaxTimeoutValue);
            Thread.Sleep(1000);
            DdlLabourTypes.Focus();
            DdlLabourTypes.DeskTopMouseClick();
            DdlLabourTypes.SelectByIndex(1, Config.PageClassSettings.Default.MaxTimeoutValue);
            Thread.Sleep(1000);
            TxtAllocatedManHours.TypeText(allocatedManHours);
            Thread.Sleep(1000);
            BtnSave.Focus();
            BtnSave.DeskTopMouseClick();
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Verifying User Role
        /// </summary>
        public void VerifyUserRoleCreateOption()
        {
            Thread.Sleep(1000);
            DdlLocation.MouseClick();
            DdlLocation.SelectByIndex(1, Config.PageClassSettings.Default.MaxTimeoutValue);
            Thread.Sleep(1000);
            BtnSave.MouseClick();
        }
    }
}