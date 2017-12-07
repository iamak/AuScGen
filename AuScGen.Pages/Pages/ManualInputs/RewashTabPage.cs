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

namespace Ecolab.Pages
{
    public class RewashTabPage :PageBase
    {
        private string guiMap;

        public RewashTabPage(List<object> utilsList)
            : base(utilsList, "RewashTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "RewashTab.xml");
        }

        public HtmlControl RewashTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabRewash");
            }
        }

        public HtmlControl ProductionTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabProduction");
            }
        }

        public HtmlControl UtilityTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabUtility");
            }
        }

        public HtmlControl LabourTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabLabour");
            }
        }

        public HtmlControl ProductionData
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabProductionData");
            }
        }

        public HtmlSelect WasherGroup
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherGroup");
            }
        }

        public HtmlSelect Formula
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlFormula");
            }
        }

        public HtmlSelect RewashReason
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlRewashReason");
            }
        }

        public HtmlSelect Location
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLocation");
            }
        }
        
        public HtmlControl AllowRewash
        {
            get
            {
                return GetHtmlControl<HtmlControl>("allowRewashSwitch");
            }
        }

        public HtmlControl AllowRewashContainer
        {
            get
            {
                return GetHtmlControl<HtmlControl>("allowRewashContainer");
            }
        }

        public HtmlControl RewashSwitch
        {
            get
            {
                return GetHtmlControl<HtmlControl>("allowSwitch");
            }
        }

        public HtmlInputText ExportPath
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDatabaseExportPath");
            }
        }
  
        public HtmlControl yesSwitch
        {
            get
            {
                return GetHtmlControl<HtmlControl>("yesSwitch");
            }
        }

        public HtmlControl noSwitch
        {
            get
            {
                return GetHtmlControl<HtmlControl>("noSwitch");
            }
        }

        public string GetSwitchNameActive()
        {
            if (AllowRewashContainer.GetRectangle().Contains(noSwitch.GetRectangle()) && AllowRewashContainer.GetRectangle().Contains(yesSwitch.GetRectangle()))
            {
                return "No";
            }
            if (AllowRewashContainer.GetRectangle().Contains(yesSwitch.GetRectangle()))
            {
                return "Yes";
            }
            else
                return null;
        }

        public HtmlInputText LastDate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLastDate");
            }
        }

        public HtmlInputText NewDate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNewDate");
            }
        }

        public HtmlInputText LastValue
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLastValue");
            }
        }

        public HtmlInputText NewValue
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNewValue");
            }
        }

        public HtmlSpan Save
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSave");
            }
        }

        public HtmlDiv SuccessMsg
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("divErrorMsg");
            }
        }

        public HtmlSelect DataLiveTime
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("txtDataLiveTime");
            }
        }
        
        public void CheckFields()
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            WasherGroup.Focus();
            //WasherGroup.DeskTopMouseClick();
            //WasherGroup.SelectByText("Tunnel-Washer2", Config.PageClassSettings.Default.MaxTimeoutValue);
            WasherGroup.SelectByIndex(1);
            WasherGroup.SelectByText("Tunnel Washer1", Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Formula.Focus();
            //Formula.MouseClick();
            Formula.SelectByIndex(1);
            //Formula.SelectByText("ecoform1", Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            RewashReason.Focus();
            //RewashReason.MouseClick();
            RewashReason.SelectByText("Washing", Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
        }

        public bool IsLastDateEnabled()
        {
            return FieldActive(LastDate);
        }

        public bool IsNewDateEnabled()
        {
            return FieldActive(NewDate);
        }

        public bool FieldActive(HtmlInputText control)
        {
            if (control.BaseElement.Parent.ChildNodes.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddingRewash(string strWasherGroup, string strRewashReason, string strLastValue, string strNewValue)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            WasherGroup.Focus();
            //WasherGroup.SelectByIndex(1);
            WasherGroup.SelectByText(strWasherGroup, Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Formula.Focus();
            Formula.SelectByIndex(1);
            //Formula.SelectByText(strFormula, Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            RewashReason.Focus();
            RewashReason.SelectByText(strRewashReason, Config.PageClassSettings.Default.MaxTimeoutValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            LastValue.Focus();
            LastValue.TypeText(strLastValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NewValue.Focus();
            NewValue.TypeText(strNewValue);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Save.Focus();
            Save.Click();
        }
        

    }
}
