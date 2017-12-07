using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Threading;


namespace Ecolab.Pages
{
    public class ProductionTabPage : PageBase
    {
        private string guiMap;

        public ProductionTabPage(List<object> utilsList)
            : base(utilsList, "ManualInputProductionTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ManualInputProductionTab.xml");
        }

        public HtmlControl ProductionTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabProduction");
            }
        }

        public HtmlControl ProductionDataTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabProductionData");
            }
        }

        public HtmlControl BatchDataTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabBatchData");
            }
        }

        public HtmlSelect WasherGroup

        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherGroup");
            }
        }

        public HtmlSelect Washer
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasher");
            }
        }

        public HtmlSelect Formula
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlFormula");
            }
        }

        public HtmlControl NewValueUOM
        {
            get
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return GetHtmlControl<HtmlControl>("newValueUOM");
            }
        }

        public HtmlControl LastValueUOM
        {
            get
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return GetHtmlControl<HtmlControl>("lastValueUOM");
            }
        }

        public HtmlInputText NewValue
        {
            get
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return GetHtmlControl<HtmlInputText>("txtNewValue");
            }
        }

        public HtmlInputText LastValue
        {
            get
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return GetHtmlControl<HtmlInputText>("txtLastValue");
            }
        }
                
        public HtmlButton Save
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlControl DeleteLastValue
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btndeleteLastValue");
            }
        }

        public HtmlControl Message
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divErrorMsg");
            }
        }

        public HtmlControl BatchDataDay
        {
            get
            {
                return GetHtmlControl<HtmlControl>("batchDay");
            }
        }

        public HtmlSelect BatchDataDropDown
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlBatch");
            }
        }

        public HtmlInputText BatchDataStartDate
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("startDate");
            }
        }

        public HtmlInputText BatchDataStartTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("startTime");
            }
        }

        public HtmlInputText BatchDataRecordingValue
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("recordingValue");
            }
        }

        public HtmlControl BatchDataUOM
        {
            get
            {
                return GetHtmlControl<HtmlControl>("batchUOM");
            }
        }

        public CommonControls.EcolabDateTimePicker BatchDataDatePicker
        {
            get
            {
                return new CommonControls.EcolabDateTimePicker(Telerik, guiMap, "batchDatePicker");
            }
        }

        public string GetlastValue 
        { 
            get
            {
                return LastValue.BaseElement.GetAttribute("Value").Value;
            }        
        }

        public bool IsLastValue(string value)
        {
            return WaitforAction(() =>
            {
                return GetlastValue.Equals(value);
            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }
        
        public bool IsMessage(string value)
        {
            return WaitforAction(() =>
            {
                return Message.BaseElement.InnerText.Equals(value);
            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public bool IsSaveButtonPresent 
        { 
            get
            {
                return IsPresent<HtmlButton>("btnSave");
            }
        }
    }
}
