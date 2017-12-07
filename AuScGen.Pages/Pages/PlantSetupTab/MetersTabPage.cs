using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Ecolab.Pages
{
    public class MetersTabPage : PageBase
    {
        private string guiMap;

        public MetersTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin, "MetersTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "MetersTab.xml");
        }

        public MetersTabPage(List<object> utilsList)
            : base(utilsList, "MetersTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "MetersTab.xml");
        }

        public HtmlControl AddMeterButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddMeter");                
            }
        }

        public HtmlButton AddMeterSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveMeter");                
            }
        }

        public HtmlButton EditMeterSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnEditSave");
            }
        }

        public HtmlButton AddMeterCancelButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");                
            }
        }

        public HtmlButton EditMeterCancelButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnEditCancel");
            }
        }
        public HtmlControl EditMetersLabel
        {
            get
            {
                return GetHtmlControl<HtmlControl>("MetersEditLabel");
            }
        }
        public HtmlControl ViewMetersLabel
        {
            get
            {
                return GetHtmlControl<HtmlControl>("MetersViewLabel");
            }
        }
        public HtmlInputText MeterName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMeterName");                
            }

        }

        public HtmlSelect UtilityType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlUtilityType");                
            }

        }

        public HtmlSelect UtilityLocation
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlUtilityLocation");                
            }

        }

        public HtmlSelect MachineCompartment
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlMachineCompartment");                
            }

        }

        public HtmlSelect Parent
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlParent");                
            }

        }

        public HtmlInputText Calibration
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtCalibration");                
            }

        }

        public HtmlSelect UOM
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlUOM");                
            }

        }

        public HtmlSelect Controller
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlController");                
            }

        }

        public HtmlControl Controllerlabel 
        { 
            get
            {
                return GetHtmlControl<HtmlControl>("ddlControllerLabel");
            }
        }

        public HtmlInputCheckBox ManualEntryEdit
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("ddlAllowManualEntryEdit");                
            }

        }

        public HtmlInputText MaxRollOverPoint
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaxValueLimit");                
            }

        }

        public CommonControls.EcolabDataGrid MetersTabGrid
        {
            get
            {
               return new CommonControls.EcolabDataGrid(Telerik, guiMap, "MetersTabGridTable");
            }
        }
        public CommonControls.EcolabDataGrid UsersRolesMetersTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "UserRolesMetersTabGridTable");
            }
        }

        public HtmlControl MeterAddedSuccess
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblMeterAddedSuccess");                
            }

        }

        public HtmlControl ErrorMessage    
        {
            get
            {
                HtmlControl errorMessage = GetHtmlControl<HtmlControl>("lblErrorMessage");
                //errorMessage.ScrollToVisible();
                return errorMessage;
            }

        }

        public bool AddMeterAndVerify()
        {
            
            AddMeterSaveButton.Focus();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Enter);

            //SaveButton.ExtendedMouseClick();
            if(null == MeterAddedSuccess)
            {
                return false;
            }

            return true;
        }

        public bool IsMachineCompartmentPresent()
        {
            return IsPresent<HtmlSelect>("ddlMachineCompartment");
        }

        public HtmlSpan YesNoButton
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("YesNoSwitch");
            }
        }

        public HtmlInputCheckBox AllowManualEntry
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("ddlAllowManualEntryAdd");
            }
        }

    }
}
