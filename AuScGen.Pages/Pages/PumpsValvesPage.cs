using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;
using System.Windows.Forms;
using ArtOfTest.WebAii.Core;

namespace Ecolab.Pages.Pages
{
    public class PumpsValvesPage : PageBase
    {
        private string guiMap;

        public PumpsValvesPage(List<object> utilsList)
            : base(utilsList, "Pumps.xml")
        {
            guiMap = string.Concat(GuiMapPath, "Pumps.xml");
        }

        public CommonControls.EcolabDataGrid ControllerSetupGridTable
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "ControllerSetupGridTable");
            }
        }

        public CommonControls.EcolabDataGrid ControllerRoleSetupGridTable
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "ControllerRoleSetupGridTable");
            }
        }

        

        public CommonControls.EcolabDataGrid PumpsListTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "PumpsListTableGrid");
            }
        }

        public HtmlAnchor tabPumpList
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabPumpList");
            }
        }

        public HtmlSelect ddlSort
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlSort");
            }
        }

        public HtmlSpan BacktoControllers
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("BacktoControllers");
            }
        }

        public HtmlSelect ddlProducts
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlProducts");
            }
        }

        public HtmlInputText txtPumpCalibration
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtPumpCalibration");
            }
        }

        public HtmlInputText txtMaxDosingTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaxDosingTime");
            }
        }

        public HtmlButton SavePump
        {
            get
            {
                return GetHtmlControl<HtmlButton>("SavePump");
            }
        }

        public HtmlButton CancelPump
        {
            get
            {
                return GetHtmlControl<HtmlButton>("CancelPump");
            }
        }
                
        public HtmlSpan BacktoPumps
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("BacktoPumps");
            }
        }

        public HtmlSelect InLineddlProducts
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("InLineddlProducts");
            }
        }

        public HtmlInputText InLinetxtPumpCalibration
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("InLinetxtPumpCalibration");
            }
        }

        public HtmlInputText InLinetxtMaxDosingTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("InLinetxtMaxDosingTime");
            }
        }

        public HtmlDiv ValidationMessage
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("ValidationMessage");
            }
        }

        public HtmlSpan dialogMsg
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("dialogMsg");
            }
        }

        public void AddingPumps(string pumpCalibration, string maxDosingTime)
        {
            ddlProducts.SelectByIndex(1);
            txtPumpCalibration.TypeText(pumpCalibration);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.TypeText(maxDosingTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtPumpCalibration.SetText(pumpCalibration);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.MouseClick();
            txtMaxDosingTime.TypeText(maxDosingTime);
            txtPumpCalibration.TypeText(pumpCalibration);
            SavePump.Focus();
            SavePump.Click();
        }

        public void UpdateCancellingPumps(string pumpCalibration, string maxDosingTime)
        {
            ddlProducts.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtPumpCalibration.TypeText("0");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.TypeText("0");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtPumpCalibration.MouseClick();
            txtPumpCalibration.SetText(pumpCalibration);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.MouseClick();
            txtMaxDosingTime.TypeText(maxDosingTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            CancelPump.Focus();
            CancelPump.Click();
        }

        public void UpdatingPumps(string pumpCalibration, string maxDosingTime) 
        {
            ddlProducts.SelectByIndex(1);
            txtPumpCalibration.TypeText(pumpCalibration);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.TypeText(maxDosingTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtPumpCalibration.SetText(pumpCalibration);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            txtMaxDosingTime.MouseClick();
            txtMaxDosingTime.TypeText(maxDosingTime);
            txtPumpCalibration.TypeText(pumpCalibration);
            SavePump.Focus();
            SavePump.Click();
        }

        public void InlineEditingPumps(string pumpCalibration, string maxDosingTime)
        {
            InLineddlProducts.SelectByIndex(1);
            InLinetxtPumpCalibration.TypeText(pumpCalibration);
            InLinetxtMaxDosingTime.TypeText(maxDosingTime);
        }
    }
}
